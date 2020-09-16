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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal sealed class DynamicGenericResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new DynamicGenericResolver();

		private DynamicGenericResolver()
		{
		}

		public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache() => formatter = (IJsonFormatter<T>)DynamicGenericResolverGetFormatterHelper.GetFormatter(typeof(T));
		}
	}

	internal static class DynamicGenericResolverGetFormatterHelper
	{
		private static readonly Dictionary<Type, Type> FormatterMap = new Dictionary<Type, Type>()
		{
			{typeof(List<>), typeof(ListFormatter<>)},
			{typeof(LinkedList<>), typeof(LinkedListFormatter<>)},
			{typeof(Queue<>), typeof(QueueFormatter<>)},
			{typeof(Stack<>), typeof(StackFormatter<>)},
			{typeof(HashSet<>), typeof(HashSetFormatter<>)},
			{typeof(ReadOnlyCollection<>), typeof(ReadOnlyCollectionFormatter<>)},
			{typeof(IList<>), typeof(InterfaceListFormatter<>)},
			{typeof(ICollection<>), typeof(InterfaceCollectionFormatter<>)},
			{typeof(IEnumerable<>), typeof(InterfaceEnumerableFormatter<>)},
			{typeof(Dictionary<,>), typeof(DictionaryFormatter<,>)},
			{typeof(IDictionary<,>), typeof(InterfaceDictionaryFormatter<,>)},
			{typeof(SortedDictionary<,>), typeof(SortedDictionaryFormatter<,>)},
			{typeof(SortedList<,>), typeof(SortedListFormatter<,>)},
			{typeof(ILookup<,>), typeof(InterfaceLookupFormatter<,>)},
			{typeof(IGrouping<,>), typeof(InterfaceGroupingFormatter<,>)},
			{typeof(ObservableCollection<>), typeof(ObservableCollectionFormatter<>)},
			{typeof(ReadOnlyObservableCollection<>),(typeof(ReadOnlyObservableCollectionFormatter<>))},
			{typeof(IReadOnlyList<>), typeof(InterfaceReadOnlyListFormatter<>)},
			{typeof(IReadOnlyCollection<>), typeof(InterfaceReadOnlyCollectionFormatter<>)},
			{typeof(ISet<>), typeof(InterfaceSetFormatter<>)},
			{typeof(ConcurrentBag<>), typeof(ConcurrentBagFormatter<>)},
			{typeof(ConcurrentQueue<>), typeof(ConcurrentQueueFormatter<>)},
			{typeof(ConcurrentStack<>), typeof(ConcurrentStackFormatter<>)},
			{typeof(ReadOnlyDictionary<,>), typeof(ReadOnlyDictionaryFormatter<,>)},
			{typeof(IReadOnlyDictionary<,>), typeof(InterfaceReadOnlyDictionaryFormatter<,>)},
			{typeof(ConcurrentDictionary<,>), typeof(ConcurrentDictionaryFormatter<,>)},
			{typeof(Lazy<>), typeof(LazyFormatter<>)},
			{typeof(Task<>), typeof(TaskValueFormatter<>)},
		};

		// Reduce IL2CPP code generate size(don't write long code in <T>)
		internal static object GetFormatter(Type t)
		{
			if (t.IsArray)
			{
				var rank = t.GetArrayRank();
				if (rank == 1)
				{
					if (t.GetElementType() == typeof(byte)) // byte[] is also supported in builtin formatter.
						return ByteArrayFormatter.Default;

					return Activator.CreateInstance(typeof(ArrayFormatter<>).MakeGenericType(t.GetElementType()));
				}
				if (rank == 2)
					return Activator.CreateInstance(typeof(TwoDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()));

				if (rank == 3)
					return Activator.CreateInstance(typeof(ThreeDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()));

				if (rank == 4)
					return Activator.CreateInstance(typeof(FourDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()));

				return null; // not supported built-in
			}

			if (t.IsGenericType)
			{
				var genericType = t.GetGenericTypeDefinition();
				var isNullable = genericType.IsNullable();
				var nullableElementType = isNullable ? t.GenericTypeArguments[0] : null;

				if (genericType == typeof(KeyValuePair<,>))
					return CreateInstance(typeof(KeyValuePairFormatter<,>), t.GenericTypeArguments);

				if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
					return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
#if NETSTANDARD2_1
				// ValueTask
				if (genericType == typeof(ValueTask<>))
					return CreateInstance(typeof(ValueTaskFormatter<>), t.GenericTypeArguments);

				if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(ValueTask<>))
					return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
#endif

				// Tuple
				if (t.FullName.StartsWith("System.Tuple"))
				{
					Type tupleFormatterType = null;
					switch (t.GenericTypeArguments.Length)
					{
						case 1:
							tupleFormatterType = typeof(TupleFormatter<>);
							break;
						case 2:
							tupleFormatterType = typeof(TupleFormatter<,>);
							break;
						case 3:
							tupleFormatterType = typeof(TupleFormatter<,,>);
							break;
						case 4:
							tupleFormatterType = typeof(TupleFormatter<,,,>);
							break;
						case 5:
							tupleFormatterType = typeof(TupleFormatter<,,,,>);
							break;
						case 6:
							tupleFormatterType = typeof(TupleFormatter<,,,,,>);
							break;
						case 7:
							tupleFormatterType = typeof(TupleFormatter<,,,,,,>);
							break;
						case 8:
							tupleFormatterType = typeof(TupleFormatter<,,,,,,,>);
							break;
						default:
							break;
					}

					return CreateInstance(tupleFormatterType, t.GenericTypeArguments);
				}

#if NETSTANDARD
				// ValueTuple
				if (t.FullName.StartsWith("System.ValueTuple"))
				{
					Type tupleFormatterType = null;
					switch (t.GenericTypeArguments.Length)
					{
						case 1:
							tupleFormatterType = typeof(ValueTupleFormatter<>);
							break;
						case 2:
							tupleFormatterType = typeof(ValueTupleFormatter<,>);
							break;
						case 3:
							tupleFormatterType = typeof(ValueTupleFormatter<,,>);
							break;
						case 4:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,>);
							break;
						case 5:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,>);
							break;
						case 6:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,>);
							break;
						case 7:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,>);
							break;
						case 8:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,,>);
							break;
						default:
							break;
					}

					return CreateInstance(tupleFormatterType, t.GenericTypeArguments);
				}

				// Nullable ValueTuple
				if (isNullable && nullableElementType.IsConstructedGenericType &&
					nullableElementType.FullName.StartsWith("System.ValueTuple"))
				{
					Type tupleFormatterType = null;
					switch (nullableElementType.GenericTypeArguments.Length)
					{
						case 1:
							tupleFormatterType = typeof(ValueTupleFormatter<>);
							break;
						case 2:
							tupleFormatterType = typeof(ValueTupleFormatter<,>);
							break;
						case 3:
							tupleFormatterType = typeof(ValueTupleFormatter<,,>);
							break;
						case 4:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,>);
							break;
						case 5:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,>);
							break;
						case 6:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,>);
							break;
						case 7:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,>);
							break;
						case 8:
							tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,,>);
							break;
						default:
							break;
					}

					var tupleFormatter = CreateInstance(tupleFormatterType, nullableElementType.GenericTypeArguments);
					return CreateInstance(typeof(StaticNullableFormatter<>), new [] { nullableElementType }, tupleFormatter);
				}
#endif

				// ArraySegment
				if (genericType == typeof(ArraySegment<>))
				{
					if (t.GenericTypeArguments[0] == typeof(byte))
						return ByteArraySegmentFormatter.Default;

					return CreateInstance(typeof(ArraySegmentFormatter<>), t.GenericTypeArguments);
				}

				if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(ArraySegment<>))
				{
					if (nullableElementType == typeof(ArraySegment<byte>))
						return new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Default);

					return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
				}

				// Mapped formatter
				if (FormatterMap.TryGetValue(genericType, out var formatterType))
					return CreateInstance(formatterType, t.GenericTypeArguments);

				// generic collection
				if (t.GenericTypeArguments.Length == 1
					&& t.GetInterfaces().Any(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))
					&& t.GetDeclaredConstructors().Any(x => x.GetParameters().Length == 0))
				{
					var elemType = t.GenericTypeArguments[0];
					return CreateInstance(typeof(GenericCollectionFormatter<,>), new[] { elemType, t });
				}
				// generic dictionary
				if (t.GenericTypeArguments.Length == 2
					&& t.GetInterfaces().Any(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
					&& t.GetDeclaredConstructors().Any(x => x.GetParameters().Length == 0))
				{
					var keyType = t.GenericTypeArguments[0];
					var valueType = t.GenericTypeArguments[1];
					return CreateInstance(typeof(GenericDictionaryFormatter<,,>), new[] { keyType, valueType, t });
				}
			}
			else
			{
				// NonGeneric Collection
				if (t == typeof(IEnumerable))
					return NonGenericInterfaceEnumerableFormatter.Default;

				if (t == typeof(ICollection))
					return NonGenericInterfaceCollectionFormatter.Default;

				if (t == typeof(IList))
					return NonGenericInterfaceListFormatter.Default;

				if (t == typeof(IDictionary))
					return NonGenericInterfaceDictionaryFormatter.Default;

				if (typeof(IList).IsAssignableFrom(t) && t.GetDeclaredConstructors().Any(x => x.GetParameters().Length == 0))
					return Activator.CreateInstance(typeof(NonGenericListFormatter<>).MakeGenericType(t));

				if (typeof(IDictionary).IsAssignableFrom(t) && t.GetDeclaredConstructors().Any(x => x.GetParameters().Length == 0))
					return Activator.CreateInstance(typeof(NonGenericDictionaryFormatter<>).MakeGenericType(t));
			}

			return null;
		}

		private static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments) =>
			Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
	}
}
