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
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal sealed class DynamicGenericResolver : IJsonFormatterResolver
	{
		public static readonly IJsonFormatterResolver Instance = new DynamicGenericResolver();

		DynamicGenericResolver()
		{

		}

		public IJsonFormatter<T> GetFormatter<T>()
		{
			return FormatterCache<T>.formatter;
		}

		static class FormatterCache<T>
		{
			public static readonly IJsonFormatter<T> formatter;

			static FormatterCache()
			{
				formatter = (IJsonFormatter<T>)DynamicGenericResolverGetFormatterHelper.GetFormatter(typeof(T));
			}
		}
	}

	internal static class DynamicGenericResolverGetFormatterHelper
	{
		static readonly Dictionary<Type, Type> formatterMap = new Dictionary<Type, Type>()
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
			{typeof(System.Collections.Concurrent.ConcurrentBag<>), typeof(ConcurrentBagFormatter<>)},
			{typeof(System.Collections.Concurrent.ConcurrentQueue<>), typeof(ConcurrentQueueFormatter<>)},
			{typeof(System.Collections.Concurrent.ConcurrentStack<>), typeof(ConcurrentStackFormatter<>)},
			{typeof(ReadOnlyDictionary<,>), typeof(ReadOnlyDictionaryFormatter<,>)},
			{typeof(IReadOnlyDictionary<,>), typeof(InterfaceReadOnlyDictionaryFormatter<,>)},
			{typeof(System.Collections.Concurrent.ConcurrentDictionary<,>), typeof(ConcurrentDictionaryFormatter<,>)},
			{typeof(Lazy<>), typeof(LazyFormatter<>)},
			{typeof(Task<>), typeof(TaskValueFormatter<>)},
		};

		// Reduce IL2CPP code generate size(don't write long code in <T>)
		internal static object GetFormatter(Type t)
		{
			var ti = t.GetTypeInfo();

			if (t.IsArray)
			{
				var rank = t.GetArrayRank();
				if (rank == 1)
				{
					if (t.GetElementType() == typeof(byte)) // byte[] is also supported in builtin formatter.
					{
						return ByteArrayFormatter.Default;
					}

					return Activator.CreateInstance(typeof(ArrayFormatter<>).MakeGenericType(t.GetElementType()));
				}
				else if (rank == 2)
				{
					return Activator.CreateInstance(typeof(TwoDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
				}
				else if (rank == 3)
				{
					return Activator.CreateInstance(typeof(ThreeDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
				}
				else if (rank == 4)
				{
					return Activator.CreateInstance(typeof(FourDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
				}
				else
				{
					return null; // not supported built-in
				}
			}
			else if (ti.IsGenericType)
			{
				var genericType = ti.GetGenericTypeDefinition();
				var genericTypeInfo = genericType.GetTypeInfo();
				var isNullable = genericTypeInfo.IsNullable();
				var nullableElementType = isNullable ? ti.GenericTypeArguments[0] : null;

				if (genericType == typeof(KeyValuePair<,>))
				{
					return CreateInstance(typeof(KeyValuePairFormatter<,>), ti.GenericTypeArguments);
				}
				else if (isNullable && nullableElementType.GetTypeInfo().IsConstructedGenericType() && nullableElementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
				{
					return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
				}

#if NETSTANDARD2_1
				// ValueTask
				else if (genericType == typeof(ValueTask<>))
				{
					return CreateInstance(typeof(ValueTaskFormatter<>), ti.GenericTypeArguments);
				}
				else if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(ValueTask<>))
				{
					return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
				}
#endif

				// Tuple
				else if (ti.FullName.StartsWith("System.Tuple"))
				{
					Type tupleFormatterType = null;
					switch (ti.GenericTypeArguments.Length)
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

					return CreateInstance(tupleFormatterType, ti.GenericTypeArguments);
				}

#if NETSTANDARD
				// ValueTuple
				else if (ti.FullName.StartsWith("System.ValueTuple"))
				{
					Type tupleFormatterType = null;
					switch (ti.GenericTypeArguments.Length)
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

					return CreateInstance(tupleFormatterType, ti.GenericTypeArguments);
				}

				// Nullable ValueTuple
				else if (isNullable && nullableElementType.GetTypeInfo().IsConstructedGenericType() &&
					nullableElementType.GetTypeInfo().FullName.StartsWith("System.ValueTuple"))
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

				// ArraySegement
				else if (genericType == typeof(ArraySegment<>))
				{
					if (ti.GenericTypeArguments[0] == typeof(byte))
					{
						return ByteArraySegmentFormatter.Default;
					}
					else
					{
						return CreateInstance(typeof(ArraySegmentFormatter<>), ti.GenericTypeArguments);
					}
				}
				else if (isNullable && nullableElementType.GetTypeInfo().IsConstructedGenericType() && nullableElementType.GetGenericTypeDefinition() == typeof(ArraySegment<>))
				{
					if (nullableElementType == typeof(ArraySegment<byte>))
					{
						return new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Default);
					}
					else
					{
						return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
					}
				}

				// Mapped formatter
				else
				{
					Type formatterType;
					if (formatterMap.TryGetValue(genericType, out formatterType))
					{
						return CreateInstance(formatterType, ti.GenericTypeArguments);
					}

					// generic collection
					else if (ti.GenericTypeArguments.Length == 1
						&& ti.ImplementedInterfaces.Any(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(ICollection<>))
						&& ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
					{
						var elemType = ti.GenericTypeArguments[0];
						return CreateInstance(typeof(GenericCollectionFormatter<,>), new[] { elemType, t });
					}
					// generic dictionary
					else if (ti.GenericTypeArguments.Length == 2
						&& ti.ImplementedInterfaces.Any(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
						&& ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
					{
						var keyType = ti.GenericTypeArguments[0];
						var valueType = ti.GenericTypeArguments[1];
						return CreateInstance(typeof(GenericDictionaryFormatter<,,>), new[] { keyType, valueType, t });
					}
				}
			}
			else
			{
				// NonGeneric Collection
				if (t == typeof(IEnumerable))
				{
					return NonGenericInterfaceEnumerableFormatter.Default;
				}
				else if (t == typeof(ICollection))
				{
					return NonGenericInterfaceCollectionFormatter.Default;
				}
				else if (t == typeof(IList))
				{
					return NonGenericInterfaceListFormatter.Default;
				}
				else if (t == typeof(IDictionary))
				{
					return NonGenericInterfaceDictionaryFormatter.Default;
				}
				if (typeof(IList).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
				{
					return Activator.CreateInstance(typeof(NonGenericListFormatter<>).MakeGenericType(t));
				}
				else if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
				{
					return Activator.CreateInstance(typeof(NonGenericDictionaryFormatter<>).MakeGenericType(t));
				}
			}

			return null;
		}

		static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
		{
			return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
		}
	}
}
