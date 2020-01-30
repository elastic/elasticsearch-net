//This is nancyfx's dynamicdictionary
//it is slightly modified to add the ability to chain dynamic property access of arbitrary depth
//without binding on null ref errors in between.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Utf8Json;

// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable RemoveRedundantBraces
// ReSharper disable ArrangeAccessorOwnerBody

namespace Elasticsearch.Net
{
	/// <summary>
	/// A dictionary that supports dynamic access.
	/// </summary>
	[JsonFormatter(typeof(DynamicDictionaryFormatter))]
	public class DynamicDictionary
		: DynamicObject,
			IEquatable<DynamicDictionary>,
			IEnumerable<KeyValuePair<string, DynamicValue>>,
			IDictionary<string, DynamicValue>
	{
		private readonly IDictionary<string, DynamicValue> _backingDictionary = new Dictionary<string, DynamicValue>(StringComparer.OrdinalIgnoreCase);

		/// <summary>
		/// Gets the number of elements contained in the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns>The number of elements contained in the <see cref="DynamicDictionary" />.</returns>
		public int Count
		{
			get { return _backingDictionary.Count; }
		}

		/// <summary>
		/// Creates a new instance of Dictionary{String,Object} using the keys and underlying object values of this DynamicDictionary instance's key values.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> ToDictionary() => _backingDictionary.ToDictionary(kv => kv.Key, kv => kv.Value.Value);

		/// <summary>
		/// Returns an empty dynamic dictionary.
		/// </summary>
		/// <value>A <see cref="DynamicDictionary" /> instance.</value>
		public static DynamicDictionary Empty => new DynamicDictionary();

		/// <summary>
		/// Gets a value indicating whether the <see cref="DynamicDictionary" /> is read-only.
		/// </summary>
		/// <returns>Always returns <see langword="false" />.</returns>
		public bool IsReadOnly
		{
			get { return false; }
		}

		private static Regex SplitRegex = new Regex(@"(?<!\\)\.");

		/// <summary>
		/// Traverses data using path notation.
		/// <para><c>e.g some.deep.nested.json.path</c></para>
		/// <para></para>
		/// <para> A special lookup is available for ANY key <c>_arbitrary_key_<c> <c>e.g some.deep._arbitrary_key_.json.path</c> which will traverse into the first key</para>
		/// <para> If <c>_arbitrary_key_</c> is the last value it will return the key name</para>
		/// <para></para>
		/// </summary>
		/// <param name="path">path into the stored object, keys are separated with a dot and the last key is returned as T</param>
		/// <typeparam name="T"></typeparam>
		/// <returns>T or default</returns>
		public T Get<T>(string path)
		{
			if (path == null) return default;

			var split = SplitRegex.Split(path);
			var queue = new Queue<string>(split);
			if (queue.Count == 0) return default;

			var d = new DynamicValue(_backingDictionary);
			while (queue.Count > 0)
			{
				var key = queue.Dequeue().Replace(@"\.", ".");
				if (key == "_arbitrary_key_")
				{
					if (queue.Count > 0) d = d[0];
					else
					{
						var v = d?.ToDictionary()?.Keys?.FirstOrDefault();
						d = v != null ? new DynamicValue(v) : DynamicValue.NullValue;
					}
				}
				else if (int.TryParse(key, out var i)) d = d[i];
				else d = d[key];
			}

			return d.TryParse<T>();
		}

		/// <summary>
		/// Gets or sets the <see cref="DynamicValue" /> with the specified name.
		/// </summary>
		/// <value>A <see cref="DynamicValue" /> instance containing a value.</value>
		public DynamicValue this[string name]
		{
			get
			{
				name = GetNeutralKey(name);

				if (!_backingDictionary.TryGetValue(name, out var member))
				{
					member = new DynamicValue(null);
				}

				return member;
			}
			set
			{
				name = GetNeutralKey(name);

				_backingDictionary[name] = value is DynamicValue ? value : new DynamicValue(value);
			}
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="DynamicDictionary" />.</returns>
		public ICollection<string> Keys => _backingDictionary.Keys;

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="DynamicDictionary" />.</returns>
		public ICollection<DynamicValue> Values
		{
			get { return _backingDictionary.Values; }
		}

		/// <summary>
		/// Adds an item to the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="DynamicDictionary" />.</param>
		public void Add(KeyValuePair<string, DynamicValue> item)
		{
			this[item.Key] = item.Value;
		}

		/// <summary>
		/// Removes all items from the <see cref="DynamicDictionary" />.
		/// </summary>
		public void Clear() => _backingDictionary.Clear();

		/// <summary>
		/// Determines whether the <see cref="DynamicDictionary" /> contains a specific value.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if <paramref name="item" /> is found in the <see cref="DynamicDictionary" />; otherwise, <see langword="false" />.
		/// </returns>
		/// <param name="item">The object to locate in the <see cref="DynamicDictionary" />.</param>
		public bool Contains(KeyValuePair<string, DynamicValue> item)
		{
			return _backingDictionary.Contains(item);
		}

		/// <summary>
		/// Copies the elements of the <see cref="DynamicDictionary" /> to an <see cref="T:System.Array" />, starting at a particular
		/// <see cref="T:System.Array" /> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the
		/// <see cref="DynamicDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(KeyValuePair<string, DynamicValue>[] array, int arrayIndex)
		{
			_backingDictionary.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="DynamicDictionary" />; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="item">The object to remove from the <see cref="DynamicDictionary" />.</param>
		public bool Remove(KeyValuePair<string, DynamicValue> item)
		{
			return _backingDictionary.Remove(item);
		}

		/// <summary>
		/// Adds an element with the provided key and value to the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		public void Add(string key, DynamicValue value)
		{
			this[key] = value;
		}

		/// <summary>
		/// Determines whether the <see cref="DynamicDictionary" /> contains an element with the specified key.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the <see cref="DynamicDictionary" /> contains an element with the key; otherwise, <see langword="false" />.
		/// </returns>
		/// <param name="key">The key to locate in the <see cref="DynamicDictionary" />.</param>
		public bool ContainsKey(string key)
		{
			return _backingDictionary.ContainsKey(key);
		}

		/// <summary>
		/// Removes the element with the specified key from the <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		public bool Remove(string key)
		{
			key = GetNeutralKey(key);
			return _backingDictionary.Remove(key);
		}

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the <see cref="DynamicDictionary" /> contains an element with the specified key; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">
		/// When this method returns, the value associated with the specified key, if the key is found; otherwise, the default
		/// value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
		/// </param>
		public bool TryGetValue(string key, out DynamicValue value)
		{
			if (_backingDictionary.TryGetValue(key, out value)) return true;

			return false;
		}

		/// <summary>
		/// Returns the enumeration of all dynamic member names.
		/// </summary>
		/// <returns>A <see cref="IEnumerator" /> that contains dynamic member names.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _backingDictionary.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		IEnumerator<KeyValuePair<string, DynamicValue>> IEnumerable<KeyValuePair<string, DynamicValue>>.GetEnumerator()
		{
			return _backingDictionary.GetEnumerator();
		}

		/// <summary>
		/// Indicates whether the current <see cref="DynamicDictionary" /> is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the current instance is equal to the <paramref name="other" /> parameter; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="other">An <see cref="DynamicDictionary" /> instance to compare with this instance.</param>
		public bool Equals(DynamicDictionary other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			return ReferenceEquals(this, other) || Equals(other._backingDictionary, _backingDictionary);
		}

		/// <summary>
		/// Creates a dynamic dictionary from an <see cref="IDictionary{TKey,TValue}" /> instance.
		/// </summary>
		/// <param name="values">An <see cref="IDictionary{TKey,TValue}" /> instance, that the dynamic dictionary should be created from.</param>
		/// <returns>An <see cref="DynamicDictionary" /> instance.</returns>
		public static DynamicDictionary Create(IDictionary<string, object> values)
		{
			var instance = new DynamicDictionary();

			foreach (var key in values.Keys)
			{
				var v = values[key];
				instance[key] = v is DynamicValue av ? av : new DynamicValue(v);
			}

			return instance;
		}

		/// <summary>
		/// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" />
		/// class can override this method to specify dynamic behavior for operations such as setting a value for a property.
		/// </summary>
		/// <returns>
		/// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language
		/// determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the object that called the dynamic operation. The binder.Name property provides the name of
		/// the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is
		/// an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The
		/// binder.IgnoreCase property specifies whether the member name is case-sensitive.
		/// </param>
		/// <param name="value">
		/// The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an
		/// instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".
		/// </param>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			this[binder.Name] = new DynamicValue(value);
			return true;
		}

		/// <summary>
		/// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" />
		/// class can override this method to specify dynamic behavior for operations such as getting a value for a property.
		/// </summary>
		/// <returns>
		/// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language
		/// determines the behavior. (In most cases, a run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the object that called the dynamic operation. The binder.Name property provides the name of
		/// the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement,
		/// where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns
		/// "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.
		/// </param>
		/// <param name="result">
		/// The result of the get operation. For example, if the method is called for a property, you can assign the property
		/// value to <paramref name="result" />.
		/// </param>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (!_backingDictionary.TryGetValue(binder.Name, out var v))
			{
				result = new DynamicValue(null);
			}
			else result = v;

			return true;
		}

		/// <summary>
		/// Returns the enumeration of all dynamic member names.
		/// </summary>
		/// <returns>A <see cref="IEnumerable{T}" /> that contains dynamic member names.</returns>
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return _backingDictionary.Keys;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise,
		/// <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj.GetType() == typeof(DynamicDictionary) && Equals((DynamicDictionary)obj);
		}

		/// <summary>
		/// Returns a hash code for this <see cref="DynamicDictionary" />.
		/// </summary>
		/// <returns> A hash code for this <see cref="DynamicDictionary" />, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode() => _backingDictionary?.GetHashCode() ?? 0;

		private static KeyValuePair<string, dynamic> GetDynamicKeyValuePair(KeyValuePair<string, DynamicValue> item)
		{
			var dynamicValueKeyValuePair =
				new KeyValuePair<string, dynamic>(item.Key, item.Value);
			return dynamicValueKeyValuePair;
		}

		private static string GetNeutralKey(string key)
		{
			return key;
		}
	}
}
