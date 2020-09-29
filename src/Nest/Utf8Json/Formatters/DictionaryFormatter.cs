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

namespace Elasticsearch.Net.Utf8Json.Formatters
{
    // unfortunately, can't use IDictionary<KVP> because supports IReadOnlyDictionary.
	internal abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TEnumerator, TDictionary> : IJsonFormatter<TDictionary>
        where TDictionary : class, IEnumerable<KeyValuePair<TKey, TValue>>
        where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
		protected bool SkipValue(TValue value) => false;

        public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>() as IObjectPropertyNameFormatter<TKey>;
                var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

                writer.WriteBeginObject();

                var e = GetSourceEnumerator(value);
                try
                {
					var written = 0;
                    if (keyFormatter != null)
                    {
                        if (e.MoveNext())
                        {
                            var item = e.Current;

							if (!SkipValue(item.Value))
							{
	                            keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
	                            writer.WriteNameSeparator();
	                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
								written++;
							}
                        }
                        else
							goto END;

						while (e.MoveNext())
                        {

                            var item = e.Current;
							if (!SkipValue(item.Value))
							{
								if (written > 0)
									writer.WriteValueSeparator();
	                            keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
	                            writer.WriteNameSeparator();
	                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
								written++;
							}
                        }
                    }
                    else
                    {
                        if (e.MoveNext())
                        {
                            var item = e.Current;
							if (!SkipValue(item.Value))
							{
								written++;
	                            writer.WriteString(item.Key.ToString());
	                            writer.WriteNameSeparator();
	                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
							}
                        }
                        else
							goto END;

						while (e.MoveNext())
                        {
							var item = e.Current;
							if (!SkipValue(item.Value))
							{
								if (written > 0)
									writer.WriteValueSeparator();

								writer.WriteString(item.Key.ToString());
	                            writer.WriteNameSeparator();
	                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
								written++;
							}
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
        }

        public TDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>() as IObjectPropertyNameFormatter<TKey>;
			if (keyFormatter == null) throw new InvalidOperationException(typeof(TKey) + " does not support dictionary key deserialize.");
			var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

			reader.ReadIsBeginObjectWithVerify();

			var dict = Create();
			var i = 0;
			while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
			{
				var key = keyFormatter.DeserializeFromPropertyName(ref reader, formatterResolver);
				reader.ReadIsNameSeparatorWithVerify();
				var value = valueFormatter.Deserialize(ref reader, formatterResolver);
				Add(ref dict, i - 1, key, value);
			}

			return Complete(ref dict);
		}

        // abstraction for serialize

        // Some collections can use struct iterator, this is optimization path
        protected abstract TEnumerator GetSourceEnumerator(TDictionary source);

        // abstraction for deserialize
        protected abstract TIntermediate Create();
        protected abstract void Add(ref TIntermediate collection, int index, TKey key, TValue value);
        protected abstract TDictionary Complete(ref TIntermediate intermediateCollection);
    }

	internal abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TDictionary> : DictionaryFormatterBase<TKey, TValue, TIntermediate, IEnumerator<KeyValuePair<TKey, TValue>>, TDictionary>
        where TDictionary : class, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        protected override IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source) => source.GetEnumerator();
	}

	internal abstract class DictionaryFormatterBase<TKey, TValue, TDictionary> : DictionaryFormatterBase<TKey, TValue, TDictionary, TDictionary>
        where TDictionary : class, IDictionary<TKey, TValue>
    {
        protected override TDictionary Complete(ref TDictionary intermediateCollection) => intermediateCollection;
	}

	internal sealed class DictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, Dictionary<TKey, TValue>.Enumerator, Dictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override Dictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection) => intermediateCollection;

		protected override Dictionary<TKey, TValue> Create() => new Dictionary<TKey, TValue>();

		protected override Dictionary<TKey, TValue>.Enumerator GetSourceEnumerator(Dictionary<TKey, TValue> source) => source.GetEnumerator();
	}

	internal sealed class GenericDictionaryFormatter<TKey, TValue, TDictionary> : DictionaryFormatterBase<TKey, TValue, TDictionary>
        where TDictionary : class, IDictionary<TKey, TValue>, new()
    {
        protected override void Add(ref TDictionary collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override TDictionary Create() => new TDictionary();
	}

	internal sealed class InterfaceDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, IDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override Dictionary<TKey, TValue> Create() => new Dictionary<TKey, TValue>();

		protected override IDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection) => intermediateCollection;
	}

	internal sealed class SortedListFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, SortedList<TKey, TValue>>
    {
        protected override void Add(ref SortedList<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override SortedList<TKey, TValue> Create() => new SortedList<TKey, TValue>();
	}

	internal sealed class SortedDictionaryFormatter<TKey, TValue>
		: DictionaryFormatterBase<TKey, TValue, SortedDictionary<TKey, TValue>, SortedDictionary<TKey, TValue>.Enumerator, SortedDictionary<TKey, TValue>>
    {
        protected override void Add(ref SortedDictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override SortedDictionary<TKey, TValue> Complete(ref SortedDictionary<TKey, TValue> intermediateCollection) => intermediateCollection;

		protected override SortedDictionary<TKey, TValue> Create() => new SortedDictionary<TKey, TValue>();

		protected override SortedDictionary<TKey, TValue>.Enumerator GetSourceEnumerator(SortedDictionary<TKey, TValue> source) => source.GetEnumerator();
	}

	internal sealed class ReadOnlyDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, ReadOnlyDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override ReadOnlyDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection) => new ReadOnlyDictionary<TKey, TValue>(intermediateCollection);

		protected override Dictionary<TKey, TValue> Create() => new Dictionary<TKey, TValue>();
	}

	internal sealed class InterfaceReadOnlyDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.Add(key, value);

		protected override IReadOnlyDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection) => intermediateCollection;

		protected override Dictionary<TKey, TValue> Create() => new Dictionary<TKey, TValue>();
	}

	internal sealed class ConcurrentDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, ConcurrentDictionary<TKey, TValue>>
    {
        protected override void Add(ref ConcurrentDictionary<TKey, TValue> collection, int index, TKey key, TValue value) => collection.TryAdd(key, value);

		protected override ConcurrentDictionary<TKey, TValue> Create() =>
			// concurrent dictionary can't access defaultConcurrecyLevel so does not use count overload.
			new ConcurrentDictionary<TKey, TValue>();
	}

	internal sealed class NonGenericDictionaryFormatter<T> : IJsonFormatter<T>
        where T : class, IDictionary, new()
    {
        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                writer.WriteBeginObject();

                var e = value.GetEnumerator();
                try
                {
                    if (e.MoveNext())
                    {
                        var item = (DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                    else
                    {
                        goto END;
                    }

                    while (e.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        var item = (DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                }
                finally
                {
					if (e is IDisposable disp)
						disp.Dispose();
				}

                END:
                writer.WriteEndObject();
            }
        }

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return default;

			var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

			reader.ReadIsBeginObjectWithVerify();

			var dict = new T();
			var i = 0;
			while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
			{
				var key = reader.ReadPropertyName();
				var value = valueFormatter.Deserialize(ref reader, formatterResolver);
				dict.Add(key, value);
			}

			return dict;
		}
    }

	internal sealed class NonGenericInterfaceDictionaryFormatter : IJsonFormatter<IDictionary>
    {
        public static readonly IJsonFormatter<IDictionary> Default = new NonGenericInterfaceDictionaryFormatter();

        public void Serialize(ref JsonWriter writer, IDictionary value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                writer.WriteBeginObject();

                var e = value.GetEnumerator();
                try
                {
                    if (e.MoveNext())
                    {
                        var item = (DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                    else
						goto END;

					while (e.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        var item = (DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                }
                finally
                {
					if (e is IDisposable disp)
						disp.Dispose();
				}

                END:
                writer.WriteEndObject();
            }
        }

        public IDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

			reader.ReadIsBeginObjectWithVerify();

			var dict = new Dictionary<object, object>();
			var i = 0;
			while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
			{
				var key = reader.ReadPropertyName();
				var value = valueFormatter.Deserialize(ref reader, formatterResolver);
				dict.Add(key, value);
			}

			return dict;
		}
    }
}
