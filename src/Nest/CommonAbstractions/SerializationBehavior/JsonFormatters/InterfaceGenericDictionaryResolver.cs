// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class InterfaceGenericDictionaryResolver : IJsonFormatterResolver
	{
		public static readonly InterfaceGenericDictionaryResolver Instance = new InterfaceGenericDictionaryResolver();

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		internal static bool IsGenericIDictionary(Type type) => type.GetInterfaces()
			.Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache() => Formatter = (IJsonFormatter<T>)DictionaryFormatterHelper.GetFormatter(typeof(T));
		}

		internal static class DictionaryFormatterHelper
		{
			internal static object GetFormatter(Type t)
			{
				if (!typeof(IEnumerable).IsAssignableFrom(t) || !IsGenericIDictionary(t))
					return null;

				t.TryGetGenericDictionaryArguments(out var typeArguments);
				var genericTypeArgs = new[]
				{
					typeArguments[0],
					typeArguments[1],
					t
				};

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
				var constructors = from c in implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
					let p = c.GetParameters()
					where p.Length == 0 || p.Length == 1 && genericDictionaryInterface.IsAssignableFrom(p[0].ParameterType)
					orderby p.Length descending
					select c;

				var ctor = constructors.FirstOrDefault();
				if (ctor == null)
					throw new Exception($"Cannot create an instance of {t.FullName} because it does not "
						+ $"have a public constructor accepting "
						+ $"IDictionary<{typeArguments[0].FullName},{typeArguments[1].FullName}> argument "
						+ $"or a public parameterless constructor");

				// construct a delegate for the ctor
				var activatorMethod = TypeExtensions.GetActivatorMethodInfo.MakeGenericMethod(t);
				var activator = activatorMethod.Invoke(null, new object[] { ctor });
				return CreateInstance(genericTypeArgs, activator, ctor.GetParameters().Length == 0);
			}

			private static object CreateInstance(Type[] genericTypeArguments, params object[] args) =>
				typeof(InterfaceDictionaryFormatter<,,>).MakeGenericType(genericTypeArguments).CreateInstance(args);
		}
	}

	internal class InterfaceGenericReadOnlyDictionaryResolver : IJsonFormatterResolver
	{
		public static readonly InterfaceGenericReadOnlyDictionaryResolver Instance = new InterfaceGenericReadOnlyDictionaryResolver();

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.Formatter;

		internal static bool IsGenericIReadOnlyDictionary(Type type) => type.GetInterfaces()
			.Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>));

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> Formatter;

			static FormatterCache() => Formatter = (IJsonFormatter<T>)DictionaryFormatterHelper.GetFormatter(typeof(T));
		}

		internal static class DictionaryFormatterHelper
		{
			internal static object GetFormatter(Type t)
			{
				if (!typeof(IEnumerable).IsAssignableFrom(t) || !IsGenericIReadOnlyDictionary(t))
					return null;

				t.TryGetGenericDictionaryArguments(out var typeArguments);
				var genericTypeArgs = new[]
				{
					typeArguments[0],
					typeArguments[1],
					t
				};

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

				var genericDictionaryInterface = typeof(IDictionary<,>).MakeGenericType(typeArguments);

				// find ctor that takes IDictionary<TKey, TValue>
				var constructors = from c in implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
					let p = c.GetParameters()
					where p.Length == 1 && genericDictionaryInterface.IsAssignableFrom(p[0].ParameterType)
					orderby p.Length descending
					select c;

				var ctor = constructors.FirstOrDefault();
				if (ctor == null)
					throw new Exception($"Cannot create an instance of {t.FullName} because it does not "
						+ $"have a public constructor accepting IDictionary<{typeArguments[0].FullName},{typeArguments[1].FullName}> argument");

				// construct a delegate for the ctor
				var activatorMethod = TypeExtensions.GetActivatorMethodInfo.MakeGenericMethod(t);
				var activator = activatorMethod.Invoke(null, new object[] { ctor });
				return CreateInstance(genericTypeArgs, activator, ctor.GetParameters().Length == 0);
			}

			private static object CreateInstance(Type[] genericTypeArguments, params object[] args) =>
				typeof(InterfaceReadOnlyDictionaryFormatter<,,>).MakeGenericType(genericTypeArguments).CreateInstance(args);
		}
	}

	internal abstract class InterfaceDictionaryFormatterBase<TKey, TValue, TDictionary> : IJsonFormatter<TDictionary>
		where TDictionary : class
	{
		protected readonly TypeExtensions.ObjectActivator<TDictionary> Activator;
		protected readonly bool ParameterlessCtor;

		public InterfaceDictionaryFormatterBase(TypeExtensions.ObjectActivator<TDictionary> activator, bool parameterlessCtor)
		{
			Activator = activator;
			ParameterlessCtor = parameterlessCtor;
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
					Add(ref dict, key, value);
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
					Add(ref dict, key, value);
				}
			}

			return Complete(ref dict);
		}

		public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			// TODO 7.0 mutator is not used, should it?
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
						keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
					else
						goto END;

					while (e.MoveNext())
					{
						writer.WriteValueSeparator();
						var item = e.Current;
						keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
				}
				else
				{
					if (e.MoveNext())
					{
						var item = e.Current;
						writer.WriteString(item.Key.ToString());
						writer.WriteNameSeparator();
						valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
					}
					else
						goto END;

					while (e.MoveNext())
					{
						writer.WriteValueSeparator();
						var item = e.Current;
						writer.WriteString(item.Key.ToString());
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

		private Dictionary<TKey, TValue> Create() =>
			new Dictionary<TKey, TValue>();

		private void Add(ref Dictionary<TKey, TValue> collection, TKey key, TValue value) =>
			collection.Add(key, value);

		protected abstract IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source);

		protected abstract TDictionary Complete(ref Dictionary<TKey, TValue> intermediateCollection);
	}

	internal class InterfaceReadOnlyDictionaryFormatter<TKey, TValue, TDictionary> : InterfaceDictionaryFormatterBase<TKey, TValue, TDictionary>
		where TDictionary : class, IReadOnlyDictionary<TKey, TValue>
	{
		public InterfaceReadOnlyDictionaryFormatter(TypeExtensions.ObjectActivator<TDictionary> activator, bool parameterlessCtor)
			: base(activator, parameterlessCtor) { }

		protected override IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source) =>
			source.GetEnumerator();

		protected override TDictionary Complete(ref Dictionary<TKey, TValue> intermediateCollection) =>
			Activator(intermediateCollection);
	}

	internal class InterfaceDictionaryFormatter<TKey, TValue, TDictionary> : InterfaceDictionaryFormatterBase<TKey, TValue, TDictionary>
		where TDictionary : class, IDictionary<TKey, TValue>
	{
		public InterfaceDictionaryFormatter(TypeExtensions.ObjectActivator<TDictionary> activator, bool parameterlessCtor)
			:base (activator, parameterlessCtor)
		{
		}

		protected override IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source) =>
			source.GetEnumerator();

		protected override TDictionary Complete(ref Dictionary<TKey, TValue> intermediateCollection)
		{
			TDictionary dictionary;
			if (ParameterlessCtor)
			{
				dictionary = Activator();
				foreach (var kv in intermediateCollection)
					dictionary.Add(kv);
			}
			else
				dictionary = Activator(intermediateCollection);

			return dictionary;
		}
	}
}
