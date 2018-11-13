using System;
using System.Collections.Generic;
using System.Reflection;
using Utf8Json;

namespace Nest
{
	internal class NestGenericSourceTypeFormatterResolver : IJsonFormatterResolver
	{
		public static NestGenericSourceTypeFormatterResolver Instance = new NestGenericSourceTypeFormatterResolver();

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache() => Formatter = (IJsonFormatter<T>)NestGenericFormatterHelper.GetFormatter(typeof(T));
		}

		internal static class NestGenericFormatterHelper
		{
			private static readonly Dictionary<Type, Type> FormatterMap = new Dictionary<Type, Type>()
			{
				{ typeof(Hit<>), typeof(HitFormatter<>) },
				{ typeof(SuggestOption<>), typeof(SuggestOptionFormatter<>) }
			};

			internal static object GetFormatter(Type t)
			{
				var ti = t.GetTypeInfo();

				if (!ti.IsGenericType)
					return null;

				var genericType = ti.GetGenericTypeDefinition();

				return FormatterMap.TryGetValue(genericType, out var formatterType)
					? CreateInstance(formatterType, ti.GenericTypeArguments)
					: null;
			}

			private static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments) =>
				Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
		}
	}
}
