// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport.Extensions;
using FluentAssertions;
using Nest;
using Tests.Core.Serialization;

namespace Tests.Framework.SerializationTests
{
	/// <summary>
	/// <see cref="IIsADictionary"/> implementations do not use
	/// <see cref="VerbatimDictionaryKeysFormatter{TKey,TValue}"/>
	/// so keys are camel cased on serialization, in line with NEST conventions
	/// </summary>
	public class IsADictionarySerializationTests
	{
		protected object ExpectJson => new { key1 = "value1", key2 = "value2", };

		[U]
		public void CanSerializeIsADictionary()
		{
			var isADictionary = new MyIsADictionary(new Dictionary<object, object>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			});

			SerializationTestHelper.Object(isADictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void SerializesIsADictionaryNullValues()
		{
			var isADictionary = new MyIsADictionary(new Dictionary<object, object>
			{
				{ "Key1", null },
				{ "Key2", "value2" },
			});

			var client = new ElasticClient();
			client.RequestResponseSerializer.SerializeToString(isADictionary).Should().Be("{\"key1\":null,\"key2\":\"value2\"}");
		}

		[U]
		public void SerializesIsADictionaryRespectsDefaultFieldNameInferrer()
		{
			var isADictionary = new MyIsADictionary(new Dictionary<object, object>
			{
				{ "Key1", "value1" }
			});

			var client = new ElasticClient(new ConnectionSettings().DefaultFieldNameInferrer(f => f.ToUpperInvariant()));
			client.RequestResponseSerializer.SerializeToString(isADictionary).Should().Be("{\"KEY1\":\"value1\"}");
		}

		private class MyIsADictionary : IsADictionaryBase<object, object>
		{
			public MyIsADictionary(IDictionary<object, object> backingDictionary) : base(backingDictionary) { }
		}
	}

	public class DictionarySerializationTests
	{
		protected object ExpectJson => new { Key1 = "value1", Key2 = "value2", };

		[U]
		public void CanSerializeCustomGenericDictionary()
		{
			var dictionary = new MyGenericDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(dictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeGenericDictionary()
		{
			var dictionary = new Dictionary<string, string>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(dictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeIgnoresDefaultFieldNameInferrer()
		{
			var dictionary = new Dictionary<string, string>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			var client = new ElasticClient(new ConnectionSettings().DefaultFieldNameInferrer(f => f.ToUpperInvariant()));
			client.RequestResponseSerializer.SerializeToString(dictionary).Should().Be("{\"Key1\":\"value1\",\"Key2\":\"value2\"}");
		}

		[U]
		public void CanSerializeGenericReadOnlyDictionary()
		{
			var dictionary = new ReadOnlyDictionary<string, string>(
				new Dictionary<string, string>
				{
					{ "Key1", "value1" },
					{ "Key2", "value2" },
				});

			SerializationTestHelper.Object(dictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeCustomGenericIDictionary()
		{
			var dictionary = new MyGenericIDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(dictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeCustomGenericIReadOnlyDictionary()
		{
			var dictionary = new MyGenericIReadOnlyDictionary(new Dictionary<object, object>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			});

			SerializationTestHelper.Object(dictionary).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeCustomDictionary()
		{
			var hashTable = new MyDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(hashTable).RoundTrips(ExpectJson);
		}

		[U]
		public void CanSerializeHashTable()
		{
			var hashTable = new Hashtable
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(hashTable).RoundTrips(ExpectJson);
		}

		[U]
		public void DoesNotSerializeDictionaryNullValues()
		{
			var dictionary = new Dictionary<string, string>
			{
				{ "Key1", null },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(dictionary)
				.PreserveNull()
				.RoundTrips(new { Key1 = (object)null, Key2 = "value2" });
		}

		[U]
		public void DoesSerializeIDictionaryNullValues()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{ "Key1", null },
				{ "Key2", "value2" },
			};

			SerializationTestHelper.Object(dictionary)
				.PreserveNull()
				.RoundTrips(new { Key1 = (object)null, Key2 = "value2" });
		}

		[U]
		public void DoesSerializeReadOnlyDictionaryNullValues()
		{
			var dictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
			{
				{ "Key1", null },
				{ "Key2", "value2" },
			});

			SerializationTestHelper.Object(dictionary)
				.PreserveNull()
				.RoundTrips(new { Key1 = (object)null, Key2 = "value2" });
		}

		[U]
		public void DoesSerializeIReadOnlyDictionaryNullValues()
		{
			IReadOnlyDictionary<string, string> dictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
			{
				{ "Key1", null },
				{ "Key2", "value2" },
			});

			SerializationTestHelper.Object(dictionary)
				.PreserveNull()
				.RoundTrips(new { Key1 = (object)null, Key2 = "value2" });
		}

		private class MyDictionary : IDictionary
		{
			private readonly IDictionary _dictionary = new Hashtable();

			public int Count => _dictionary.Count;

			public bool IsFixedSize => _dictionary.IsFixedSize;

			public bool IsReadOnly => _dictionary.IsReadOnly;

			public bool IsSynchronized => _dictionary.IsSynchronized;

			public object this[object key]
			{
				get => _dictionary[key];
				set => _dictionary[key] = value;
			}

			public ICollection Keys => _dictionary.Keys;

			public object SyncRoot => _dictionary.SyncRoot;

			public ICollection Values => _dictionary.Values;

			public void CopyTo(Array array, int index) => _dictionary.CopyTo(array, index);

			public void Add(object key, object value) => _dictionary.Add(key, value);

			public void Clear() => _dictionary.Clear();

			public bool Contains(object key) => _dictionary.Contains(key);

			public IDictionaryEnumerator GetEnumerator() => _dictionary.GetEnumerator();

			public void Remove(object key) => _dictionary.Remove(key);

			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_dictionary).GetEnumerator();
		}

		private class MyGenericDictionary : Dictionary<string, string> { }

		private class MyGenericIReadOnlyDictionary : IReadOnlyDictionary<object, object>
		{
			private readonly IDictionary<object, object> _backingDictionary;

			public MyGenericIReadOnlyDictionary(IDictionary<object, object> dictionary) =>
				_backingDictionary = dictionary ?? new Dictionary<object, object>();

			public int Count => _backingDictionary.Count;

			public object this[object key] => _backingDictionary[key];

			public IEnumerable<object> Keys => _backingDictionary.Keys;

			public IEnumerable<object> Values => _backingDictionary.Values;

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

			public IEnumerator<KeyValuePair<object, object>> GetEnumerator() => _backingDictionary.GetEnumerator();

			public bool ContainsKey(object key) => _backingDictionary.ContainsKey(key);

			public bool TryGetValue(object key, out object value) => _backingDictionary.TryGetValue(key, out value);
		}

		private class MyGenericIDictionary : IDictionary<object, object>
		{
			private readonly IDictionary<object, object> _backingDictionary = new Dictionary<object, object>();

			public int Count => _backingDictionary.Count;

			public bool IsReadOnly => _backingDictionary.IsReadOnly;

			public object this[object key]
			{
				get => _backingDictionary[key];
				set => _backingDictionary[key] = value;
			}

			public ICollection<object> Keys => _backingDictionary.Keys;

			public ICollection<object> Values => _backingDictionary.Values;

			public void Add(KeyValuePair<object, object> item) => _backingDictionary.Add(item);

			public void Clear() => _backingDictionary.Clear();

			public bool Contains(KeyValuePair<object, object> item) => _backingDictionary.Contains(item);

			public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex) => _backingDictionary.CopyTo(array, arrayIndex);

			public bool Remove(KeyValuePair<object, object> item) => _backingDictionary.Remove(item);

			public void Add(object key, object value) => _backingDictionary.Add(key, value);

			public bool ContainsKey(object key) => _backingDictionary.ContainsKey(key);

			public bool Remove(object key) => _backingDictionary.Remove(key);

			public bool TryGetValue(object key, out object value) => _backingDictionary.TryGetValue(key, out value);

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

			public IEnumerator<KeyValuePair<object, object>> GetEnumerator() => _backingDictionary.GetEnumerator();
		}
	}
}
