// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class IsADictionaryFormatterResolver : IJsonFormatterResolver
	{
		public static readonly IsADictionaryFormatterResolver Instance = new IsADictionaryFormatterResolver();

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		internal static bool IsIsADictionary(Type type) => type.GetInterfaces()
			.Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IIsADictionary<,>));

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache() => Formatter = (IJsonFormatter<T>)IsADictionaryFormatter.GetFormatter(typeof(T));
		}

		internal static class IsADictionaryFormatter
		{
			internal static object GetFormatter(Type t)
			{
				if (!typeof(IIsADictionary).IsAssignableFrom(t) || !IsIsADictionary(t))
					return null;

				t.TryGetGenericDictionaryArguments(out var typeArguments);
				var genericTypeArgs = new List<Type>();
				genericTypeArgs.AddRange(typeArguments);
				genericTypeArgs.Add(t);

				var genericDictionaryInterface = typeof(IDictionary<,>).MakeGenericType(typeArguments);

				Type implementationType;
				if (t.IsInterface)
				{
					// need an implementation to deserialize interface to
					var readAsAttribute = t.GetCustomAttribute<ReadAsAttribute>();
					if (readAsAttribute == null)
						throw new Exception($"Unable to deserialize interface {t.FullName}");

					implementationType = readAsAttribute.Type.IsGenericType
						? readAsAttribute.Type.MakeGenericType(typeArguments)
						: readAsAttribute.Type;
				}
				else
					implementationType = t;

				// find either a parameterless ctor or ctor that takes IDictionary<TKey, TValue>
				var constructors = from c in implementationType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
					let p = c.GetParameters()
					where p.Length == 0 || p.Length == 1 && genericDictionaryInterface.IsAssignableFrom(p[0].ParameterType)
					orderby p.Length descending
					select c;

				var ctor = constructors.FirstOrDefault();
				if (ctor == null)
					throw new Exception($"Cannot create an instance of {t.FullName} because it does not "
						+ $"have a constructor accepting "
						+ $"{genericDictionaryInterface.FullName} argument "
						+ $"or a parameterless constructor");

				// construct a delegate for the ctor
				var activatorMethod = TypeExtensions.GetActivatorMethodInfo.MakeGenericMethod(t);
				var activator = activatorMethod.Invoke(null, new object[] { ctor });
				return CreateInstance(genericTypeArgs.ToArray(), activator, ctor.GetParameters().Length == 0);
			}

			private static object CreateInstance(Type[] genericTypeArguments, params object[] args) =>
				typeof(IsADictionaryBaseFormatter<,,>).MakeGenericType(genericTypeArguments).CreateInstance(args);
		}
	}

	internal class IsADictionaryBaseFormatter<TKey, TValue, TDictionary>
		: IJsonFormatter<TDictionary>
		where TDictionary : class, IIsADictionary<TKey, TValue>
	{
		private readonly TypeExtensions.ObjectActivator<TDictionary> _activator;
		private readonly bool _parameterlessCtor;

		public IsADictionaryBaseFormatter(TypeExtensions.ObjectActivator<TDictionary> activator, bool parameterlessCtor)
		{
			_activator = activator;
			_parameterlessCtor = parameterlessCtor;
		}

		protected Dictionary<TKey, TValue> Create() => new Dictionary<TKey, TValue>();

		protected void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value) =>
			collection.Add(key, value);

		protected TDictionary Complete(ref Dictionary<TKey, TValue> intermediateCollection)
		{
			TDictionary dictionary;
			if (_parameterlessCtor)
			{
				dictionary = _activator();
				foreach (var kv in intermediateCollection)
					dictionary.Add(kv);
			}
			else
				dictionary = _activator(intermediateCollection);

			return dictionary;
		}


		protected IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source) =>
			source.GetEnumerator();

		public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver)
		{
		    if (value == null)
            {
                writer.WriteNull();
                return;
            }

			var mutator = formatterResolver.GetConnectionSettings().DefaultFieldNameInferrer;
			var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>() as IObjectPropertyNameFormatter<TKey>;
			var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

			writer.WriteBeginObject();

			var e = GetSourceEnumerator(value);
			try
			{
				if (keyFormatter != null)
				{
					if (e.MoveNext())
					{
						var item = e.Current;
						var innerWriter = new JsonWriter();
						keyFormatter.SerializeToPropertyName(ref innerWriter, item.Key, formatterResolver);
						writer.WriteString(mutator(innerWriter.ToString()));
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
					else
						goto END;

					while (e.MoveNext())
					{
						writer.WriteValueSeparator();
						var item = e.Current;
						var innerWriter = new JsonWriter();
						keyFormatter.SerializeToPropertyName(ref innerWriter, item.Key, formatterResolver);
						writer.WriteString(mutator(innerWriter.ToString()));
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
				}
				else
				{
					if (e.MoveNext())
					{
						var item = e.Current;
						writer.WriteString(mutator(item.Key.ToString()));
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
					else
						goto END;

					while (e.MoveNext())
					{
						writer.WriteValueSeparator();
						var item = e.Current;
						writer.WriteString(mutator(item.Key.ToString()));
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
				}
			}
			finally
			{
				e.Dispose();
			}

			END:
			writer.WriteEndObject();
		}

        public TDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

			var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>();
			var objectKeyFormatter = keyFormatter as IObjectPropertyNameFormatter<TKey>;
			var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();
			reader.ReadIsBeginObjectWithVerify();

			var dict = Create();
			var i = 0;
			if (objectKeyFormatter != null)
			{
				while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
				{
					var key = objectKeyFormatter.DeserializeFromPropertyName(ref reader, formatterResolver);
					reader.ReadIsNameSeparatorWithVerify();
					var value = valueFormatter.Deserialize(ref reader, formatterResolver);
					Add(ref dict, i - 1, key, value);
				}
			}
			else
			{
				while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
				{
					var keyString = reader.ReadString();
					var key = (TKey)Convert.ChangeType(keyString, typeof(TKey));
					reader.ReadIsNameSeparatorWithVerify();
					var value = valueFormatter.Deserialize(ref reader, formatterResolver);
					Add(ref dict, i - 1, key, value);
				}
			}

			return Complete(ref dict);
		}
	}
}
