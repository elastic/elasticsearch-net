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
						formatterType = typeof(ConcreteInterfaceFormatter<,>).MakeGenericType(genericType, typeof(T));
					}
					else
						formatterType = typeof(ConcreteInterfaceFormatter<,>).MakeGenericType(readAsAttribute.Type, typeof(T));

					Formatter = (IJsonFormatter<T>)Activator.CreateInstance(formatterType);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException($"Can not create formatter from {nameof(ReadAsAttribute)} for {readAsAttribute.Type.Name}", ex);
				}
			}
		}
	}

	internal class ConcreteInterfaceFormatter<TConcrete, TInterface> : IJsonFormatter<TInterface>
		where TConcrete : TInterface
	{
		public virtual TInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<TConcrete>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public virtual void Serialize(ref JsonWriter writer, TInterface value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<TInterface>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
