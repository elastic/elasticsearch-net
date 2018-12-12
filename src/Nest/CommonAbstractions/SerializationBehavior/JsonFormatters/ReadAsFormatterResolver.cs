using System;
using System.Reflection;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	public sealed class ReadAsFormatterResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new ReadAsFormatterResolver();

		private ReadAsFormatterResolver() { }

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache()
			{
				var readAsAttribute = typeof(T).GetTypeInfo().GetCustomAttribute<ReadAsAttribute>();
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

	public sealed class WriteAsFormatterResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new WriteAsFormatterResolver();

		private WriteAsFormatterResolver() { }

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache()
			{
				var writeAsAttribute = typeof(T).GetTypeInfo().GetCustomAttribute<WriteAsAttribute>();
				if (writeAsAttribute == null)
					return;

				try
				{
					Type formatterType;
					if (writeAsAttribute.Type.IsGenericType && !writeAsAttribute.Type.IsConstructedGenericType)
					{
						var genericType = writeAsAttribute.Type.MakeGenericType(typeof(T).GenericTypeArguments);
						formatterType = typeof(WriteAsFormatter<,>).MakeGenericType(typeof(T), genericType);
					}
					else
						formatterType = typeof(WriteAsFormatter<,>).MakeGenericType(typeof(T), writeAsAttribute.Type);

					Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException($"Can not create formatter from {nameof(ReadAsAttribute)} for {writeAsAttribute.Type.Name}", ex);
				}
			}
		}
	}

	internal class WriteAsFormatter<T, TWrite> : IJsonFormatter<T>
		where T : TWrite
	{
		public virtual T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<T>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public virtual void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<TWrite>();
			formatter.Serialize(ref writer, value, formatterResolver);
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

		public virtual void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<T>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
