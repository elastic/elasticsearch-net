#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Formatters
{
	public static class EnumFormatterHelper
	{
		public static object GetSerializeDelegate(Type type, out bool isBoxed)
		{
			var underlyingType = Enum.GetUnderlyingType(type);

			isBoxed = false;
			var dynamicMethod = new DynamicMethod("EnumSerializeByUnderlyingValue", null, new[] { typeof(JsonWriter).MakeByRefType(), type, typeof(IJsonFormatterResolver) }, type.Module, true);
			var il = dynamicMethod.GetILGenerator();
			il.Emit(OpCodes.Ldarg_0); // writer
			il.Emit(OpCodes.Ldarg_1); // value
			il.Emit(OpCodes.Call, typeof(JsonWriter).GetRuntimeMethod("Write" + underlyingType.Name, new[] { underlyingType }));
			il.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(typeof(JsonSerializeAction<>).MakeGenericType(type));
		}

		public static object GetDeserializeDelegate(Type type, out bool isBoxed)
		{
			var underlyingType = Enum.GetUnderlyingType(type);

			isBoxed = false;
			var dynamicMethod = new DynamicMethod("EnumDeserializeByUnderlyingValue", type, new[] { typeof(JsonReader).MakeByRefType(), typeof(IJsonFormatterResolver) }, type.Module, true);
			var il = dynamicMethod.GetILGenerator();
			il.Emit(OpCodes.Ldarg_0); // reader
			il.Emit(OpCodes.Call, typeof(JsonReader).GetRuntimeMethod("Read" + underlyingType.Name, Type.EmptyTypes));
			il.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(typeof(JsonDeserializeFunc<>).MakeGenericType(type));
		}
	}

	// can inherit for set optimize manual serialize/deserialize func.
	internal class EnumFormatter<T> : IJsonFormatter<T>, IObjectPropertyNameFormatter<T>
	{
		private static readonly ByteArrayStringHashTable<T> NameValueMapping;
		private static readonly Dictionary<T, string> ValueNameMapping;

		private static readonly JsonSerializeAction<T> DefaultSerializeByUnderlyingValue;
		private static readonly JsonDeserializeFunc<T> DefaultDeserializeByUnderlyingValue;

		static EnumFormatter()
		{
			var names = new List<string>();
			var values = new List<object>();

			var type = typeof(T);
			foreach (var item in type.GetFields().Where(fi => fi.FieldType == type))
			{
				var value = item.GetValue(null);
				var name = item.Name;
				var dataMember = item.GetCustomAttributes(typeof(DataMemberAttribute), true)
					.OfType<DataMemberAttribute>()
					.FirstOrDefault();
				var enumMember = item.GetCustomAttributes(typeof(EnumMemberAttribute), true)
					.OfType<EnumMemberAttribute>()
					.FirstOrDefault();

				values.Add(value);
				names.Add(
					(enumMember != null && enumMember.Value != null) ? enumMember.Value
					: (dataMember != null && dataMember.Name != null) ? dataMember.Name
					: name);
			}

			NameValueMapping = new ByteArrayStringHashTable<T>(names.Count);
			ValueNameMapping = new Dictionary<T, string>(names.Count);

			for (var i = 0; i < names.Count; i++)
			{
				NameValueMapping.Add(JsonWriter.GetEncodedPropertyNameWithoutQuotation(names[i]), (T)values[i]);
				ValueNameMapping[(T)values[i]] = names[i];
			}

			// boxed... or generate...
			{
				var serialize = EnumFormatterHelper.GetSerializeDelegate(typeof(T), out var isBoxed);
				if (isBoxed)
				{
					var boxSerialize = (JsonSerializeAction<object>)serialize;
					DefaultSerializeByUnderlyingValue = (ref JsonWriter writer, T value, IJsonFormatterResolver _) => boxSerialize.Invoke(ref writer, value, _);
				}
				else
					DefaultSerializeByUnderlyingValue = (JsonSerializeAction<T>)serialize;
			}

			{
				var deserialize = EnumFormatterHelper.GetDeserializeDelegate(typeof(T), out var isBoxed);
				if (isBoxed)
				{
					var boxDeserialize = (JsonDeserializeFunc<object>)deserialize;
					DefaultDeserializeByUnderlyingValue = (ref JsonReader reader, IJsonFormatterResolver _) => (T)boxDeserialize.Invoke(ref reader, _);
				}
				else
					DefaultDeserializeByUnderlyingValue = (JsonDeserializeFunc<T>)deserialize;
			}
		}

		private readonly bool _serializeByName;
		private readonly JsonSerializeAction<T> _serializeByUnderlyingValue;
		private readonly JsonDeserializeFunc<T> _deserializeByUnderlyingValue;

		public EnumFormatter(bool serializeByName)
		{
			_serializeByName = serializeByName;
			_serializeByUnderlyingValue = DefaultSerializeByUnderlyingValue;
			_deserializeByUnderlyingValue = DefaultDeserializeByUnderlyingValue;
		}

		/// <summary>
		/// If can not use dynamic code-generation environment and want to avoid boxing, you can set func manually.
		/// </summary>
		public EnumFormatter(JsonSerializeAction<T> valueSerializeAction, JsonDeserializeFunc<T> valueDeserializeAction)
		{
			_serializeByName = false;
			_serializeByUnderlyingValue = valueSerializeAction;
			_deserializeByUnderlyingValue = valueDeserializeAction;
		}

		public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			if (_serializeByName)
			{
				if (!ValueNameMapping.TryGetValue(value, out var name))
					name = value.ToString(); // fallback for flags etc. But Enum.ToString is slow...
				writer.WriteString(name);
			}
			else
				_serializeByUnderlyingValue(ref writer, value, formatterResolver);
		}

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				// avoid string decoding if possible.
				var key = reader.ReadStringSegmentUnsafe();

				if (!NameValueMapping.TryGetValue(key, out var value))
				{
					var str = StringEncoding.UTF8.GetString(key.Array, key.Offset, key.Count);
					value = (T)Enum.Parse(typeof(T), str, true); // Enum.Parse is slow
				}
				return value;
			}

			if (token == JsonToken.Number)
				return _deserializeByUnderlyingValue(ref reader, formatterResolver);

			throw new InvalidOperationException("Can't parse JSON to Enum format.");
		}

		public void SerializeToPropertyName(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			if (_serializeByName)
				Serialize(ref writer, value, formatterResolver);
			else
			{
				writer.WriteQuotation();
				Serialize(ref writer, value, formatterResolver);
				writer.WriteQuotation();
			}
		}

		public T DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (_serializeByName)
				return Deserialize(ref reader, formatterResolver);

			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.String) throw new InvalidOperationException("Can't parse JSON to Enum format.");
			reader.AdvanceOffset(1); // skip \""
			var t = Deserialize(ref reader, formatterResolver); // token is Number
			reader.SkipWhiteSpace();
			reader.AdvanceOffset(1); // skip \""
			return t;
		}
	}
}
