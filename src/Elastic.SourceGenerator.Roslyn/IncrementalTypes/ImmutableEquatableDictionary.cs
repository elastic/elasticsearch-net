using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Elastic.SourceGenerator.Roslyn.Helpers;

namespace Elastic.SourceGenerator.Roslyn.IncrementalTypes;

/// <summary>
/// Defines an immutable dictionary that defines structural equality semantics.
/// </summary>
/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(ImmutableEquatableDictionary<,>.DebugView))]
public sealed class ImmutableEquatableDictionary<TKey, TValue> :
	IEquatable<ImmutableEquatableDictionary<TKey, TValue>>,
	IDictionary<TKey, TValue>,
	IReadOnlyDictionary<TKey, TValue>,
	IDictionary

	where TKey : IEquatable<TKey>
	where TValue : IEquatable<TValue>
{
	/// <summary>
	/// An empty <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance.
	/// </summary>
	public static ImmutableEquatableDictionary<TKey, TValue> Empty { get; } = new([]);

	private readonly Dictionary<TKey, TValue> _values;

	private ImmutableEquatableDictionary(Dictionary<TKey, TValue> values)
	{
		Debug.Assert(values.Comparer == EqualityComparer<TKey>.Default);
		_values = values;
	}

	/// <summary>
	/// Gets the number of key/value pairs in the dictionary.
	/// </summary>
	public int Count => _values.Count;

	/// <summary>
	/// Determines whether the dictionary contains the specified key.
	/// </summary>
	public bool ContainsKey(TKey key) => _values.ContainsKey(key);

	/// <summary>
	/// Try to get the value associated with the specified key.
	/// </summary>
	public bool TryGetValue(TKey key, out TValue value) => _values.TryGetValue(key, out value);

	/// <summary>
	/// Gets the value associated with the specified key.
	/// </summary>
	public TValue this[TKey key] => _values[key];

	/// <inheritdoc/>
	public bool Equals(ImmutableEquatableDictionary<TKey, TValue> other)
	{
		if (ReferenceEquals(this, other))
		{
			return true;
		}

		Dictionary<TKey, TValue> thisDict = _values;
		Dictionary<TKey, TValue> otherDict = other._values;
		if (thisDict.Count != otherDict.Count)
		{
			return false;
		}

		foreach (KeyValuePair<TKey, TValue> entry in thisDict)
		{
			if (!otherDict.TryGetValue(entry.Key, out TValue? otherValue) ||
				!entry.Value.Equals(otherValue))
			{
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
		=> obj is ImmutableEquatableDictionary<TKey, TValue> other && Equals(other);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		int hash = 0;
		foreach (KeyValuePair<TKey, TValue> entry in _values)
		{
			int keyHash = entry.Key.GetHashCode();
			int valueHash = entry.Value?.GetHashCode() ?? 0;
			hash ^= CommonHelpers.CombineHashCodes(keyHash, valueHash);
		}

		return hash;
	}

	/// <summary>Gets an enumerator that iterates through the dictionary.</summary>
	public Dictionary<TKey, TValue>.Enumerator GetEnumerator() => _values.GetEnumerator();

	/// <summary>
	/// Gets a collection containing the keys in the dictionary.
	/// </summary>
	public Dictionary<TKey, TValue>.KeyCollection Keys => _values.Keys;

	/// <summary>
	/// Gets a collection containing the values in the dictionary.
	/// </summary>
	public Dictionary<TKey, TValue>.ValueCollection Values => _values.Values;

	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static ImmutableEquatableDictionary<TKey, TValue> UnsafeCreateFromDictionary(Dictionary<TKey, TValue> values)
	{
		return new ImmutableEquatableDictionary<TKey, TValue>(values);
	}

	#region Explicit interface implementations

	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => _values.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

	IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)_values).GetEnumerator();

	IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _values.Keys;
	IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _values.Values;
	ICollection<TKey> IDictionary<TKey, TValue>.Keys => _values.Keys;
	ICollection<TValue> IDictionary<TKey, TValue>.Values => _values.Values;
	ICollection IDictionary.Keys => _values.Keys;
	ICollection IDictionary.Values => _values.Values;

	bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;
	bool IDictionary.IsReadOnly => true;
	bool IDictionary.IsFixedSize => true;
	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;

	TValue IDictionary<TKey, TValue>.this[TKey key] { get => _values[key]; set => throw new InvalidOperationException(); }
	object IDictionary.this[object key] { get => ((IDictionary)_values)[key]; set => throw new InvalidOperationException(); }

	bool IDictionary.Contains(object key) => ((IDictionary)_values).Contains(key);

	void ICollection.CopyTo(Array array, int index) => ((IDictionary)_values).CopyTo(array, index);

	bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => _values.Contains(item);

	void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		=> ((ICollection<KeyValuePair<TKey, TValue>>)_values).CopyTo(array, arrayIndex);

	void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new InvalidOperationException();

	bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new InvalidOperationException();

	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();

	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();

	void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new InvalidOperationException();

	void IDictionary.Add(object key, object value) => throw new InvalidOperationException();

	void IDictionary.Remove(object key) => throw new InvalidOperationException();

	void IDictionary.Clear() => throw new InvalidOperationException();

	#endregion Explicit interface implementations

	private sealed class DebugView(ImmutableEquatableDictionary<TKey, TValue> collection)
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public DebugViewDictionaryItem[] Entries => collection._values.Select(kvp => new DebugViewDictionaryItem(kvp)).ToArray();
	}

	[DebuggerDisplay("{Value}", Name = "[{Key}]")]
	private readonly struct DebugViewDictionaryItem(KeyValuePair<TKey, TValue> keyValuePair)
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public TKey Key { get; } = keyValuePair.Key;

		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public TValue Value { get; } = keyValuePair.Value;
	}
}

/// <summary>
/// Provides a set of static methods for creating instances of <see cref="ImmutableEquatableDictionary{TKey, TValue}"/>.
/// </summary>
public static class ImmutableEquatableDictionary
{
	/// <summary>
	/// Gets an empty <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance.
	/// </summary>
	/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
	/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
	/// <returns>An empty <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance.</returns>
	public static ImmutableEquatableDictionary<TKey, TValue> Empty<TKey, TValue>()
		where TKey : IEquatable<TKey>
		where TValue : IEquatable<TValue>
	{
		return ImmutableEquatableDictionary<TKey, TValue>.Empty;
	}

	/// <summary>
	/// Creates an <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
	/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
	/// <param name="values">The source values used to populate the dictionary.</param>
	/// <param name="keySelector">The projection function from which to derive a key.</param>
	/// <returns>An <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> containing the specified values.</returns>
	public static ImmutableEquatableDictionary<TKey, TValue> ToImmutableEquatableDictionary<TKey, TValue>(this IEnumerable<TValue> values, Func<TValue, TKey> keySelector)
		where TKey : IEquatable<TKey>
		where TValue : IEquatable<TValue>
	{
		return values is ICollection<KeyValuePair<TKey, TValue>> { Count: 0 }
			? ImmutableEquatableDictionary<TKey, TValue>.Empty
			: ImmutableEquatableDictionary<TKey, TValue>.UnsafeCreateFromDictionary(values.ToDictionary(keySelector));
	}

	/// <summary>
	/// Creates an <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="TSource">The element type of the source enumerable.</typeparam>
	/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
	/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
	/// <param name="source">The source enumerable seeding the dictionary entries.</param>
	/// <param name="keySelector">The projection function from which to derive a key.</param>
	/// <param name="valueSelector">The projection function from which to derive a value.</param>
	/// <returns>An <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> containing the specified values.</returns>
	public static ImmutableEquatableDictionary<TKey, TValue> ToImmutableEquatableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
		where TKey : IEquatable<TKey>
		where TValue : IEquatable<TValue>
	{
		return source is ICollection<TSource> { Count: 0 }
			? ImmutableEquatableDictionary<TKey, TValue>.Empty
			: ImmutableEquatableDictionary<TKey, TValue>.UnsafeCreateFromDictionary(source.ToDictionary(keySelector, valueSelector));
	}

	/// <summary>
	/// Creates an <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="TKey">The key type of the dictionary.</typeparam>
	/// <typeparam name="TValue">The value type of the dictionary.</typeparam>
	/// <param name="values">The source key/value pairs with which to populate the dictionary.</param>
	/// <returns>An <see cref="ImmutableEquatableDictionary{TKey, TValue}"/> containing the specified values.</returns>
	public static ImmutableEquatableDictionary<TKey, TValue> ToImmutableEquatableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> values)
		where TKey : IEquatable<TKey>
		where TValue : IEquatable<TValue>
	{
		return values switch
		{
			ICollection<KeyValuePair<TKey, TValue>> { Count: 0 } => ImmutableEquatableDictionary<TKey, TValue>.Empty,
			IDictionary<TKey, TValue> idict => ImmutableEquatableDictionary<TKey, TValue>.UnsafeCreateFromDictionary(new(idict)),
			_ => ImmutableEquatableDictionary<TKey, TValue>.UnsafeCreateFromDictionary(values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value))
		};
	}
}
