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
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Formatters
{
	// MEMO:should write/read base64 directly like corefxlab/System.Binary.Base64
	// https://github.com/dotnet/corefxlab/tree/master/src/System.Binary.Base64/System/Binary
	internal sealed class ByteArrayFormatter : IJsonFormatter<byte[]>
	{
		public static readonly IJsonFormatter<byte[]> Default = new ByteArrayFormatter();

		public void Serialize(ref JsonWriter writer, byte[] value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			writer.WriteString(Convert.ToBase64String(value, Base64FormattingOptions.None));
		}

		public byte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			var str = reader.ReadString();
			return Convert.FromBase64String(str);
		}
	}

	internal sealed class ByteArraySegmentFormatter : IJsonFormatter<ArraySegment<byte>>
	{
		public static readonly IJsonFormatter<ArraySegment<byte>> Default = new ByteArraySegmentFormatter();

		public void Serialize(ref JsonWriter writer, ArraySegment<byte> value, IJsonFormatterResolver formatterResolver)
		{
			if (value.Array == null) { writer.WriteNull(); return; }

			writer.WriteString(Convert.ToBase64String(value.Array, value.Offset, value.Count, Base64FormattingOptions.None));
		}

		public ArraySegment<byte> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return default;

			var str = reader.ReadString();
			var bytes = Convert.FromBase64String(str);
			return new ArraySegment<byte>(bytes, 0, bytes.Length);
		}
	}

	internal sealed class NullableStringFormatter : IJsonFormatter<string>, IObjectPropertyNameFormatter<string>
	{
		public static readonly IJsonFormatter<string> Default = new NullableStringFormatter();

		public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver) => writer.WriteString(value);

		public string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadString();

		public void SerializeToPropertyName(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver) => writer.WriteString(value);

		public string DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadString();
	}

	internal sealed class NullableStringArrayFormatter : IJsonFormatter<string[]>
	{
		public static readonly NullableStringArrayFormatter Default = new NullableStringArrayFormatter();

		public void Serialize(ref JsonWriter writer, string[] value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				writer.WriteBeginArray();

				if (value.Length != 0)
					writer.WriteString(value[0]);
				for (var i = 1; i < value.Length; i++)
				{
					writer.WriteValueSeparator();
					writer.WriteString(value[i]);
				}

				writer.WriteEndArray();
			}
		}

		public string[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			reader.ReadIsBeginArrayWithVerify();
			var array = new string[4];
			var count = 0;
			while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
			{
				if (array.Length < count)
					Array.Resize(ref array, count * 2);
				array[count - 1] = reader.ReadString();
			}

			Array.Resize(ref array, count);
			return array;
		}
	}

	internal sealed class CharFormatter : IJsonFormatter<char>
	{
		public static readonly CharFormatter Default = new CharFormatter();

		// MEMO:can be improvement write directly
		public void Serialize(ref JsonWriter writer, char value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value.ToString(CultureInfo.InvariantCulture));

		public char Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadString()[0];
	}

	internal sealed class NullableCharFormatter : IJsonFormatter<char?>
	{
		public static readonly NullableCharFormatter Default = new NullableCharFormatter();

		public void Serialize(ref JsonWriter writer, char? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				CharFormatter.Default.Serialize(ref writer, value.Value, formatterResolver);
		}

		public char? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			return CharFormatter.Default.Deserialize(ref reader, formatterResolver);
		}
	}

	internal sealed class CharArrayFormatter : IJsonFormatter<char[]>
	{
		public static readonly CharArrayFormatter Default = new CharArrayFormatter();

		public void Serialize(ref JsonWriter writer, char[] value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				writer.WriteBeginArray();

				if (value.Length != 0)
					CharFormatter.Default.Serialize(ref writer, value[0], formatterResolver);
				for (var i = 1; i < value.Length; i++)
				{
					writer.WriteValueSeparator();
					CharFormatter.Default.Serialize(ref writer, value[i], formatterResolver);
				}

				writer.WriteEndArray();
			}
		}

		public char[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			reader.ReadIsBeginArrayWithVerify();
			var array = new char[4];
			var count = 0;
			while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
			{
				if (array.Length < count)
					Array.Resize(ref array, count * 2);
				array[count - 1] = CharFormatter.Default.Deserialize(ref reader, formatterResolver);
			}

			Array.Resize(ref array, count);
			return array;
		}
	}

	internal sealed class GuidFormatter : IJsonFormatter<Guid>, IObjectPropertyNameFormatter<Guid>
	{
		public static readonly IJsonFormatter<Guid> Default = new GuidFormatter();

		public void Serialize(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver)
		{
			writer.EnsureCapacity(38); // unsafe, control underlying buffer manually

			writer.WriteRawUnsafe((byte)'\"');

			var rawData = writer.GetBuffer();
			new GuidBits(ref value).Write(rawData.Array, writer.CurrentOffset); // len = 36
			writer.AdvanceOffset(36);

			writer.WriteRawUnsafe((byte)'\"');
		}

		public Guid Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var segment = reader.ReadStringSegmentUnsafe();
			return new GuidBits(ref segment).Value;
		}

		public void SerializeToPropertyName(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver);

		public Guid DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);
	}

	internal sealed class DecimalFormatter : IJsonFormatter<decimal>
	{
		public static readonly IJsonFormatter<decimal> Default = new DecimalFormatter();

		private readonly bool _serializeAsString;

		public DecimalFormatter()
			: this(false)
		{
		}

		public DecimalFormatter(bool serializeAsString) => _serializeAsString = serializeAsString;

		public void Serialize(ref JsonWriter writer, decimal value, IJsonFormatterResolver formatterResolver)
		{
			// always include decimal point and at least one decimal place
			var s = value.ToString("0.0###########################", CultureInfo.InvariantCulture);
			if (_serializeAsString)
				writer.WriteString(s);
			else
				// write as number format.
				writer.WriteRaw(StringEncoding.UTF8.GetBytes(s));
		}

		public decimal Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Number)
			{
				var number = reader.ReadNumberSegment();
				// ReSharper disable once AssignNullToNotNullAttribute
				return decimal.Parse(StringEncoding.UTF8.GetString(number.Array, number.Offset, number.Count), NumberStyles.Float, CultureInfo.InvariantCulture);
			}

			if (token == JsonToken.String)
				return decimal.Parse(reader.ReadString(), NumberStyles.Float, CultureInfo.InvariantCulture);

			throw new InvalidOperationException("Invalid Json Token for DecimalFormatter:" + token);
		}
	}

	internal sealed class UriFormatter : IJsonFormatter<Uri>
	{
		public static readonly IJsonFormatter<Uri> Default = new UriFormatter();

		public void Serialize(ref JsonWriter writer, Uri value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteString(value.ToString());
		}

		public Uri Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			return new Uri(reader.ReadString(), UriKind.RelativeOrAbsolute);
		}
	}

	internal sealed class VersionFormatter : IJsonFormatter<Version>
	{
		public static readonly IJsonFormatter<Version> Default = new VersionFormatter();

		public void Serialize(ref JsonWriter writer, Version value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteString(value.ToString());
		}

		public Version Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			return new Version(reader.ReadString());
		}
	}

	internal sealed class KeyValuePairFormatter<TKey, TValue> : IJsonFormatter<KeyValuePair<TKey, TValue>>
	{
		public void Serialize(ref JsonWriter writer, KeyValuePair<TKey, TValue> value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteRaw(StandardClassLibraryFormatterHelper.keyValuePairName[0]);
			formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref writer, value.Key, formatterResolver);
			writer.WriteRaw(StandardClassLibraryFormatterHelper.keyValuePairName[1]);
			formatterResolver.GetFormatterWithVerify<TValue>().Serialize(ref writer, value.Value, formatterResolver);

			writer.WriteEndObject();
		}

		public KeyValuePair<TKey, TValue> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, KeyValuePair can not be null.");

			TKey resultKey = default;
			TValue resultValue = default;

			reader.ReadIsBeginObjectWithVerify();

			var count = 0;
			while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
			{
				var keyString = reader.ReadPropertyNameSegmentRaw();
				int key;
				StandardClassLibraryFormatterHelper.keyValuePairAutomata.TryGetValue(keyString, out key);

				switch (key)
				{
					case 0:
						resultKey = formatterResolver.GetFormatterWithVerify<TKey>().Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						resultValue = formatterResolver.GetFormatterWithVerify<TValue>().Deserialize(ref reader, formatterResolver);
						break;
					default:
						reader.ReadNextBlock();
						break;
				}
			}

			return new KeyValuePair<TKey, TValue>(resultKey, resultValue);
		}
	}

	internal sealed class StringBuilderFormatter : IJsonFormatter<StringBuilder>
	{
		public static readonly IJsonFormatter<StringBuilder> Default = new StringBuilderFormatter();

		public void Serialize(ref JsonWriter writer, StringBuilder value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }
			writer.WriteString(value.ToString());
		}

		public StringBuilder Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			reader.ReadIsNull()
				? null
				: new StringBuilder(reader.ReadString());
	}

	// BitArray can be represents other format...
	internal sealed class BitArrayFormatter : IJsonFormatter<BitArray>
	{
		public static readonly IJsonFormatter<BitArray> Default = new BitArrayFormatter();

		public void Serialize(ref JsonWriter writer, BitArray value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			writer.WriteBeginArray();
			for (var i = 0; i < value.Length; i++)
			{
				if (i != 0) writer.WriteValueSeparator();
				writer.WriteBoolean(value[i]);
			}
			writer.WriteEndArray();
		}

		public BitArray Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;
			reader.ReadIsBeginArrayWithVerify();
			var c = 0;
			var buffer = new ArrayBuffer<bool>(4);
			while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref c))
				buffer.Add(reader.ReadBoolean());

			return new BitArray(buffer.ToArray());
		}
	}

	internal sealed class TypeFormatter : IJsonFormatter<Type>
	{
		public static readonly TypeFormatter Default = new TypeFormatter();

		private static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);

		private readonly bool _serializeAssemblyQualifiedName;
		private readonly bool _deserializeSubtractAssemblyQualifiedName;
		private readonly bool _throwOnError;

		public TypeFormatter()
			: this(true, true, true)
		{
		}

		public TypeFormatter(bool serializeAssemblyQualifiedName, bool deserializeSubtractAssemblyQualifiedName, bool throwOnError)
		{
			_serializeAssemblyQualifiedName = serializeAssemblyQualifiedName;
			_deserializeSubtractAssemblyQualifiedName = deserializeSubtractAssemblyQualifiedName;
			_throwOnError = throwOnError;
		}

		public void Serialize(ref JsonWriter writer, Type value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }
			writer.WriteString(_serializeAssemblyQualifiedName ? value.AssemblyQualifiedName : value.FullName);
		}

		public Type Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			var s = reader.ReadString();
			if (_deserializeSubtractAssemblyQualifiedName)
				s = SubtractFullNameRegex.Replace(s, "");

			return Type.GetType(s, _throwOnError);
		}
	}

	internal sealed class BigIntegerFormatter : IJsonFormatter<BigInteger>
	{
		public static readonly IJsonFormatter<BigInteger> Default = new BigIntegerFormatter();

		public void Serialize(ref JsonWriter writer, BigInteger value, IJsonFormatterResolver formatterResolver) =>
			// JSON.NET writes Integer format, not compatible.
			writer.WriteString(value.ToString(CultureInfo.InvariantCulture));

		public BigInteger Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var s = reader.ReadString();
			return BigInteger.Parse(s, CultureInfo.InvariantCulture);
		}
	}

	// Convert to [Real, Imaginary]
	internal sealed class ComplexFormatter : IJsonFormatter<Complex>
	{
		public static readonly IJsonFormatter<Complex> Default = new ComplexFormatter();

		public void Serialize(ref JsonWriter writer, Complex value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginArray();
			writer.WriteDouble(value.Real);
			writer.WriteValueSeparator();
			writer.WriteDouble(value.Imaginary);
			writer.WriteEndArray();
		}

		public Complex Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			reader.ReadIsBeginArrayWithVerify();
			var real = reader.ReadDouble();
			reader.ReadIsValueSeparatorWithVerify();
			var imaginary = reader.ReadDouble();
			reader.ReadIsEndArrayWithVerify();

			return new Complex(real, imaginary);
		}
	}

	internal sealed class ExpandoObjectFormatter : IJsonFormatter<ExpandoObject>
	{
		public static readonly IJsonFormatter<ExpandoObject> Default = new ExpandoObjectFormatter();

		public void Serialize(ref JsonWriter writer, ExpandoObject value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatterWithVerify<IDictionary<string, object>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		public ExpandoObject Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var result = new ExpandoObject() as IDictionary<string, object>;

			var objectFormatter = formatterResolver.GetFormatterWithVerify<object>();
			var c = 0;
			while (reader.ReadIsInObject(ref c))
			{
				var propName = reader.ReadPropertyName();
				var value = objectFormatter.Deserialize(ref reader, formatterResolver);
				result.Add(propName, value);
			}

			return (ExpandoObject)result;
		}
	}

	internal sealed class LazyFormatter<T> : IJsonFormatter<Lazy<T>>
	{
		public void Serialize(ref JsonWriter writer, Lazy<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }
			formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Value, formatterResolver);
		}

		public Lazy<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			// deserialize immediately(no delay, because capture byte[] causes memory leak)
			var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
			return new Lazy<T>(v.AsFunc());
		}
	}

	internal sealed class TaskUnitFormatter : IJsonFormatter<Task>
	{
		public static readonly IJsonFormatter<Task> Default = new TaskUnitFormatter();

		public void Serialize(ref JsonWriter writer, Task value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			value.Wait(); // wait!
			writer.WriteNull();
		}

		public Task Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (!reader.ReadIsNull()) throw new InvalidOperationException("Invalid input");

			return Task.CompletedTask;
		}
	}

	internal sealed class TaskValueFormatter<T> : IJsonFormatter<Task<T>>
	{
		public void Serialize(ref JsonWriter writer, Task<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			// value.Result -> wait...!
			formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Result, formatterResolver);
		}

		public Task<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
			return Task.FromResult(v);
		}
	}

	#if NETSTANDARD2_1
	internal sealed class ValueTaskFormatter<T> : IJsonFormatter<ValueTask<T>>
	{
		public void Serialize(ref JsonWriter writer, ValueTask<T> value, IJsonFormatterResolver formatterResolver) =>
			// value.Result -> wait...!
			formatterResolver.GetFormatterWithVerify<T>().Serialize(ref writer, value.Result, formatterResolver);

		public ValueTask<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var v = formatterResolver.GetFormatterWithVerify<T>().Deserialize(ref reader, formatterResolver);
			return new ValueTask<T>(v);
		}
	}
	#endif

	internal static class StandardClassLibraryFormatterHelper
	{
		internal static readonly byte[][] keyValuePairName;
		internal static readonly AutomataDictionary keyValuePairAutomata;

		static StandardClassLibraryFormatterHelper()
		{
			keyValuePairName = new[]
			{
				JsonWriter.GetEncodedPropertyNameWithBeginObject("Key"),
				JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Value"),
			};
			keyValuePairAutomata = new AutomataDictionary
			{
				{JsonWriter.GetEncodedPropertyNameWithoutQuotation("Key"), 0 },
				{JsonWriter.GetEncodedPropertyNameWithoutQuotation("Value"), 1 },
			};
		}
	}
}
