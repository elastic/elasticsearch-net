//This is nancyfx's dynamicdictionary
//it is slightly modified to add the ability to chain dynamic property access of arbitrary depth
//without binding on null ref errors in between.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A dictionary that supports dynamic access.
	/// </summary>
	public class DynamicBody
		: DynamicObject,
			IEquatable<DynamicBody>,
			IEnumerable<string>,
			IDictionary<string, object>
	{
		private readonly IDictionary<string, dynamic> BackingDictionary = new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);

		/// <summary>
		/// Gets the number of elements contained in the <see cref="DynamicBody" />.
		/// </summary>
		/// <returns>The number of elements contained in the <see cref="DynamicBody" />.</returns>
		public int Count => BackingDictionary.Count;

		/// <summary>
		/// Returns an empty dynamic dictionary.
		/// </summary>
		/// <value>A <see cref="DynamicBody" /> instance.</value>
		public static DynamicBody Empty => new DynamicBody();

		/// <summary>
		/// Gets a value indicating whether the <see cref="DynamicBody" /> is read-only.
		/// </summary>
		/// <returns>Always returns <see langword="false" />.</returns>
		public bool IsReadOnly => false;

		/// <summary>
		/// Gets or sets the <see cref="ElasticsearchDynamicValue" /> with the specified name.
		/// </summary>
		/// <value>A <see cref="ElasticsearchDynamicValue" /> instance containing a value.</value>
		public dynamic this[string name]
		{
			get
			{
				name = GetNeutralKey(name);

				dynamic member;
				if (!BackingDictionary.TryGetValue(name, out member)) member = new ElasticsearchDynamicValue(null);

				return member;
			}
			set
			{
				name = GetNeutralKey(name);

				BackingDictionary[name] = value is ElasticsearchDynamicValue ? value : new ElasticsearchDynamicValue(value);
			}
		}

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="DynamicBody" />.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="DynamicBody" />.</returns>
		public ICollection<string> Keys => BackingDictionary.Keys;

		/// <summary>
		/// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="DynamicBody" />.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="DynamicBody" />.</returns>
		public ICollection<dynamic> Values => BackingDictionary.Values;

		/// <summary>
		/// Adds an item to the <see cref="DynamicBody" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="DynamicBody" />.</param>
		public void Add(KeyValuePair<string, dynamic> item) => this[item.Key] = item.Value;

		/// <summary>
		/// Removes all items from the <see cref="DynamicBody" />.
		/// </summary>
		public void Clear() => BackingDictionary.Clear();

		/// <summary>
		/// Determines whether the <see cref="DynamicBody" /> contains a specific value.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if <paramref name="item" /> is found in the <see cref="DynamicBody" />; otherwise, <see langword="false" />.
		/// </returns>
		/// <param name="item">The object to locate in the <see cref="DynamicBody" />.</param>
		public bool Contains(KeyValuePair<string, dynamic> item)
		{
			var dynamicValueKeyValuePair =
				GetDynamicKeyValuePair(item);

			return BackingDictionary.Contains(dynamicValueKeyValuePair);
		}

		/// <summary>
		/// Copies the elements of the <see cref="DynamicBody" /> to an <see cref="T:System.Array" />, starting at a particular
		/// <see cref="T:System.Array" /> index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the
		/// <see cref="DynamicBody" />. The <see cref="T:System.Array" /> must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(KeyValuePair<string, dynamic>[] array, int arrayIndex) => BackingDictionary.CopyTo(array, arrayIndex);

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="DynamicBody" />.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="DynamicBody" />; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="item">The object to remove from the <see cref="DynamicBody" />.</param>
		public bool Remove(KeyValuePair<string, dynamic> item)
		{
			var dynamicValueKeyValuePair =
				GetDynamicKeyValuePair(item);

			return BackingDictionary.Remove(dynamicValueKeyValuePair);
		}

		/// <summary>
		/// Adds an element with the provided key and value to the <see cref="DynamicBody" />.
		/// </summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		public void Add(string key, dynamic value) => this[key] = value;

		/// <summary>
		/// Determines whether the <see cref="DynamicBody" /> contains an element with the specified key.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the <see cref="DynamicBody" /> contains an element with the key; otherwise, <see langword="false" />.
		/// </returns>
		/// <param name="key">The key to locate in the <see cref="DynamicBody" />.</param>
		public bool ContainsKey(string key) => BackingDictionary.ContainsKey(key);

		/// <summary>
		/// Removes the element with the specified key from the <see cref="DynamicBody" />.
		/// </summary>
		/// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		public bool Remove(string key)
		{
			key = GetNeutralKey(key);
			return BackingDictionary.Remove(key);
		}

		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the <see cref="DynamicBody" /> contains an element with the specified key; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">
		/// When this method returns, the value associated with the specified key, if the key is found; otherwise, the default
		/// value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
		/// </param>
		public bool TryGetValue(string key, out dynamic value)
		{
			if (BackingDictionary.TryGetValue(key, out value)) return true;

			return false;
		}

		/// <summary>
		/// Returns the enumeration of all dynamic member names.
		/// </summary>
		/// <returns>A <see cref="IEnumerator" /> that contains dynamic member names.</returns>
		IEnumerator IEnumerable.GetEnumerator() => BackingDictionary.Keys.GetEnumerator();

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		IEnumerator<KeyValuePair<string, dynamic>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => BackingDictionary.GetEnumerator();

		/// <summary>
		/// Returns the enumeration of all dynamic member names.
		/// </summary>
		/// <returns>A <see cref="IEnumerable{T}" /> that contains dynamic member names.</returns>
		public IEnumerator<string> GetEnumerator() => BackingDictionary.Keys.GetEnumerator();

		/// <summary>
		/// Indicates whether the current <see cref="DynamicBody" /> is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if the current instance is equal to the <paramref name="other" /> parameter; otherwise,
		/// <see langword="false" />.
		/// </returns>
		/// <param name="other">An <see cref="DynamicBody" /> instance to compare with this instance.</param>
		public bool Equals(DynamicBody other)
		{
			if (ReferenceEquals(null, other)) return false;

			return ReferenceEquals(this, other) || Equals(other.BackingDictionary, BackingDictionary);
		}

		/// <summary>
		/// Creates a dynamic dictionary from an <see cref="IDictionary{TKey,TValue}" /> instance.
		/// </summary>
		/// <param name="values">An <see cref="IDictionary{TKey,TValue}" /> instance, that the dynamic dictionary should be created from.</param>
		/// <returns>An <see cref="DynamicBody" /> instance.</returns>
		public static DynamicBody Create(IDictionary<string, object> values)
		{
			var instance = new DynamicBody();

			foreach (var key in values.Keys) instance[key] = values[key];

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
			this[binder.Name] = value;
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
			if (!BackingDictionary.TryGetValue(binder.Name, out result)) result = new ElasticsearchDynamicValue(null);

			return true;
		}

		/// <summary>
		/// Returns the enumeration of all dynamic member names.
		/// </summary>
		/// <returns>A <see cref="IEnumerable{T}" /> that contains dynamic member names.</returns>
		public override IEnumerable<string> GetDynamicMemberNames() => BackingDictionary.Keys;

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
			if (ReferenceEquals(null, obj)) return false;

			if (ReferenceEquals(this, obj)) return true;

			return obj.GetType() == typeof(DynamicBody) && Equals((DynamicBody)obj);
		}

		/// <summary>
		/// Returns a hash code for this <see cref="DynamicBody" />.
		/// </summary>
		/// <returns> A hash code for this <see cref="DynamicBody" />, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode() => BackingDictionary?.GetHashCode() ?? 0;

		private static KeyValuePair<string, dynamic> GetDynamicKeyValuePair(KeyValuePair<string, dynamic> item)
		{
			var dynamicValueKeyValuePair =
				new KeyValuePair<string, dynamic>(item.Key, new ElasticsearchDynamicValue(item.Value));
			return dynamicValueKeyValuePair;
		}

		private static string GetNeutralKey(string key) => key;
	}
}
