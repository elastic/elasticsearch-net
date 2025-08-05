using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Elastic.SourceGenerator.Roslyn.IncrementalTypes;

/// <summary>
/// Defines an immutable set that defines structural equality semantics.
/// </summary>
/// <typeparam name="T">The element type of the set.</typeparam>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(ImmutableEquatableSet<>.DebugView))]
[CollectionBuilder(typeof(ImmutableEquatableSet), nameof(ImmutableEquatableSet.Create))]
public sealed class ImmutableEquatableSet<T> :
	IEquatable<ImmutableEquatableSet<T>>,
	ISet<T>,
	IReadOnlyCollection<T>,
	ICollection

	where T : IEquatable<T>
{
	/// <summary>
	/// An empty <see cref="ImmutableEquatableSet{T}"/> instance.
	/// </summary>
	public static ImmutableEquatableSet<T> Empty { get; } = new([]);

	private readonly HashSet<T> _values;

	private ImmutableEquatableSet(HashSet<T> values)
	{
		Debug.Assert(values.Comparer == EqualityComparer<T>.Default);
		_values = values;
	}

	/// <summary>
	/// Gets an enumerator that iterates through the set.
	/// </summary>
	public int Count => _values.Count;

	/// <summary>
	/// Checks if the set contains the specified item.
	/// </summary>
	public bool Contains(T item)
	{
		return _values.Contains(item);
	}

	/// <inheritdoc/>
	public bool Equals(ImmutableEquatableSet<T> other)
	{
		if (ReferenceEquals(this, other))
		{
			return true;
		}

		HashSet<T> thisSet = _values;
		HashSet<T> otherSet = other._values;
		if (thisSet.Count != otherSet.Count)
		{
			return false;
		}

		foreach (T value in thisSet)
		{
			if (!otherSet.Contains(value))
			{
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is ImmutableEquatableSet<T> other && Equals(other);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		int hash = 0;
		foreach (T value in _values)
		{
			hash ^= value is null ? 0 : value.GetHashCode();
		}

		return hash;
	}

	/// <summary>
	/// Gets an enumerator that iterates through the set.
	/// </summary>
	public HashSet<T>.Enumerator GetEnumerator() => _values.GetEnumerator();

	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static ImmutableEquatableSet<T> UnsafeCreateFromHashSet(HashSet<T> values)
	{
		return new ImmutableEquatableSet<T>(values);
	}

	#region Explicit interface implementations

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => _values.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

	bool ICollection<T>.IsReadOnly => true;

	void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _values.CopyTo(array, arrayIndex);

	void ICollection.CopyTo(Array array, int index) => throw new InvalidOperationException();

	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;

	bool ISet<T>.IsSubsetOf(IEnumerable<T> other) => _values.IsSubsetOf(other);

	bool ISet<T>.IsSupersetOf(IEnumerable<T> other) => _values.IsSupersetOf(other);

	bool ISet<T>.IsProperSubsetOf(IEnumerable<T> other) => _values.IsProperSubsetOf(other);

	bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other) => _values.IsProperSupersetOf(other);

	bool ISet<T>.Overlaps(IEnumerable<T> other) => _values.Overlaps(other);

	bool ISet<T>.SetEquals(IEnumerable<T> other) => _values.SetEquals(other);

	void ICollection<T>.Add(T item) => throw new InvalidOperationException();

	bool ISet<T>.Add(T item) => throw new InvalidOperationException();

	void ISet<T>.UnionWith(IEnumerable<T> other) => throw new InvalidOperationException();

	void ISet<T>.IntersectWith(IEnumerable<T> other) => throw new InvalidOperationException();

	void ISet<T>.ExceptWith(IEnumerable<T> other) => throw new InvalidOperationException();

	void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) => throw new InvalidOperationException();

	bool ICollection<T>.Remove(T item) => throw new InvalidOperationException();

	void ICollection<T>.Clear() => throw new InvalidOperationException();

	#endregion Explicit interface implementations

	private sealed class DebugView(ImmutableEquatableSet<T> set)
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items => set.ToArray();
	}
}

/// <summary>
/// Defines extension methods for creating <see cref="ImmutableEquatableSet{T}"/> instances.
/// </summary>
public static class ImmutableEquatableSet
{
	/// <summary>
	/// Creates a new <see cref="ImmutableEquatableSet{T}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="T">The element type of the set.</typeparam>
	/// <param name="values">The source enumerable with which to populate the set.</param>
	/// <returns>A new <see cref="ImmutableEquatableSet{T}"/> instance containing the specified values.</returns>
	public static ImmutableEquatableSet<T> ToImmutableEquatableSet<T>(this IEnumerable<T> values) where T : IEquatable<T>
	{
		return values is ICollection<T> { Count: 0 }
			? ImmutableEquatableSet<T>.Empty
			: ImmutableEquatableSet<T>.UnsafeCreateFromHashSet(new(values));
	}

	/// <summary>
	/// Creates a new <see cref="ImmutableEquatableSet{T}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="T">The element type of the set.</typeparam>
	/// <param name="values">The source span with which to populate the set.</param>
	/// <returns>A new <see cref="ImmutableEquatableSet{T}"/> instance containing the specified values.</returns>
	public static ImmutableEquatableSet<T> Create<T>(ReadOnlySpan<T> values) where T : IEquatable<T>
	{
		if (values.IsEmpty)
		{
			return ImmutableEquatableSet<T>.Empty;
		}

		HashSet<T> hashSet = [];
		for (int i = 0; i < values.Length; i++)
		{
			hashSet.Add(values[i]);
		}
		return ImmutableEquatableSet<T>.UnsafeCreateFromHashSet(hashSet);
	}
}
