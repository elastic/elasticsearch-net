using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tests.Framework
{
	public class IsADictionarySerializationTests : SerializationTestBase
	{
		// This doesn't technically support deserialization as the keys
		// get camel cased on serialization and remain cased this way on deserialization,
		// not matching the original key cased form
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
			{
				key1 = "value1",
				key2 = "value2",
			};

		// IIsADictionary implementations do not use the VerbatimDictionaryKeysConverter
		// so keys are camel cased on serialization, in line with NEST conventions
		[U]
		public void CanSerializeIsADictionary()
		{
			var isADictionary = new MyIsADictionary(new Dictionary<object, object>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			});

			this.AssertSerializesAndRoundTrips(isADictionary);
		}

		private class MyIsADictionary : IsADictionaryBase<object, object>
		{
			public MyIsADictionary(IDictionary<object, object> backingDictionary) : base(backingDictionary) { }
		}
	}

	public class DictionarySerializationTests : SerializationTestBase
	{
		protected override object ExpectJson => new
			{
				Key1 = "value1",
				Key2 ="value2",
			};

		[U]
		public void CanSerializeCustomGenericDictionary()
		{
			var dictionary = new MyGenericDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			this.AssertSerializesAndRoundTrips(dictionary);
		}

		[U]
		public void CanSerializeGenericDictionary()
		{
			var dictionary = new Dictionary<string,string>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			this.AssertSerializesAndRoundTrips(dictionary);
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

			this.AssertSerializesAndRoundTrips(dictionary);
		}

		[U]
		public void CanSerializeCustomGenericIDictionary()
		{
			var dictionary = new MyGenericIDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			this.AssertSerializesAndRoundTrips(dictionary);
		}

		[U]
		public void CanSerializeCustomGenericIReadOnlyDictionary()
		{
			var dictionary = new MyGenericIReadOnlyDictionary(new Dictionary<object, object>
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			});

			this.AssertSerializesAndRoundTrips(dictionary);
		}

		[U]
		public void CanSerializeCustomDictionary()
		{
			var hashTable = new MyDictionary
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			this.AssertSerializesAndRoundTrips(hashTable);
		}

		[U]
		public void CanSerializeDictionary()
		{
			var hashTable = new Hashtable
			{
				{ "Key1", "value1" },
				{ "Key2", "value2" },
			};

			this.AssertSerializesAndRoundTrips(hashTable);
		}

		private class MyDictionary : IDictionary
		{
			private readonly IDictionary _dictionary = new Hashtable();

			public bool Contains(object key) => _dictionary.Contains(key);

			public void Add(object key, object value) => _dictionary.Add(key, value);

			public void Clear() => _dictionary.Clear();

			public IDictionaryEnumerator GetEnumerator() => _dictionary.GetEnumerator();

			public void Remove(object key) => _dictionary.Remove(key);

			public object this[object key]
			{
				get { return _dictionary[key]; }
				set { _dictionary[key] = value; }
			}

			public ICollection Keys => _dictionary.Keys;

			public ICollection Values => _dictionary.Values;

			public bool IsReadOnly => _dictionary.IsReadOnly;

			public bool IsFixedSize => _dictionary.IsFixedSize;

			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_dictionary).GetEnumerator();

			public void CopyTo(Array array, int index) => _dictionary.CopyTo(array, index);

			public int Count => _dictionary.Count;

			public object SyncRoot => _dictionary.SyncRoot;

			public bool IsSynchronized => _dictionary.IsSynchronized;
		}

		private class MyGenericDictionary : Dictionary<string, string> {}

		private class MyGenericIReadOnlyDictionary : IReadOnlyDictionary<object, object>
		{
			private readonly IDictionary<object, object> _backingDictionary;

			public MyGenericIReadOnlyDictionary(IDictionary<object, object> dictionary)
			{
				_backingDictionary = dictionary ?? new Dictionary<object, object>();
			}

			public IEnumerator<KeyValuePair<object, object>> GetEnumerator() => _backingDictionary.GetEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_backingDictionary).GetEnumerator();

			public int Count => _backingDictionary.Count;

			public bool ContainsKey(object key) => _backingDictionary.ContainsKey(key);

			public bool TryGetValue(object key, out object value) => _backingDictionary.TryGetValue(key, out value);

			public object this[object key] => _backingDictionary[key];

			public IEnumerable<object> Keys => _backingDictionary.Keys;

			public IEnumerable<object> Values => _backingDictionary.Values;
		}

		private class MyGenericIDictionary : IDictionary<object, object>
		{
			private readonly IDictionary<object, object> _backingDictionary = new Dictionary<object, object>();

			public IEnumerator<KeyValuePair<object, object>> GetEnumerator() => _backingDictionary.GetEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_backingDictionary).GetEnumerator();

			public void Add(KeyValuePair<object, object> item) => _backingDictionary.Add(item);

			public void Clear() => _backingDictionary.Clear();

			public bool Contains(KeyValuePair<object, object> item) => _backingDictionary.Contains(item);

			public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex) => _backingDictionary.CopyTo(array, arrayIndex);

			public bool Remove(KeyValuePair<object, object> item) => _backingDictionary.Remove(item);

			public int Count => _backingDictionary.Count;

			public bool IsReadOnly => _backingDictionary.IsReadOnly;

			public bool ContainsKey(object key) => _backingDictionary.ContainsKey(key);

			public void Add(object key, object value) => _backingDictionary.Add(key, value);

			public bool Remove(object key) => _backingDictionary.Remove(key);

			public bool TryGetValue(object key, out object value) => _backingDictionary.TryGetValue(key, out value);

			public object this[object key]
			{
				get { return _backingDictionary[key]; }
				set { _backingDictionary[key] = value; }
			}

			public ICollection<object> Keys => _backingDictionary.Keys;

			public ICollection<object> Values => _backingDictionary.Values;
		}
	}
}
