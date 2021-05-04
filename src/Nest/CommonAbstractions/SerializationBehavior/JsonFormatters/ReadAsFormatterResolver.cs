// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal sealed class ReadAsFormatterResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new ReadAsFormatterResolver();

		private ReadAsFormatterResolver() { }

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache()
			{
				var readAsAttribute = typeof(T).GetCustomAttribute<ReadAsAttribute>();
				if (readAsAttribute == null)
					return;

				try
				{
					Type formatterType;
					if (readAsAttribute.Type.IsGenericType && !readAsAttribute.Type.IsConstructedGenericType)
					{
						var genericType = readAsAttribute.Type.MakeGenericType(typeof(T).GenericTypeArguments);
						formatterType = typeof(ReadAsFormatter<,>).MakeGenericType(genericType, typeof(T));
					}
					else
						formatterType = typeof(ReadAsFormatter<,>).MakeGenericType(readAsAttribute.Type, typeof(T));

					Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException($"Can not create formatter from {nameof(ReadAsAttribute)} for {readAsAttribute.Type.Name}", ex);
				}
			}
		}
	}

	internal class ReadAsFormatter<TRead, T> : IJsonFormatter<T>
		where TRead : T
	{
		public virtual T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<TRead>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public virtual void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver) =>
			SerializeInternal(ref writer, value, formatterResolver);

		public virtual void SerializeInternal(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<T>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
