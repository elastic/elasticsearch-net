using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

using Elastic.SourceGenerator.Roslyn.Helpers;

namespace Elastic.SourceGenerator.Roslyn.IncrementalTypes;

/// <summary>
/// Defines an immutable array that defines structural equality semantics.
/// </summary>
/// <typeparam name="T">The type of the array elements.</typeparam>
[DebuggerDisplay("Length = {Length}")]
[DebuggerTypeProxy(typeof(ImmutableEquatableArray<>.DebugView))]
[CollectionBuilder(typeof(ImmutableEquatableArray), nameof(ImmutableEquatableArray.Create))]
public sealed class ImmutableEquatableArray<T> :
	IEquatable<ImmutableEquatableArray<T>>,
	IReadOnlyList<T>,
	IList<T>,
	IList

	where T : IEquatable<T>
{
	/// <summary>
	/// An empty <see cref="ImmutableEquatableArray{T}"/> instance.
	/// </summary>
	public static ImmutableEquatableArray<T> Empty { get; } = new([]);

	private readonly T[] _values;

	/// <summary>
	/// Gets the element at the specified index.
	/// </summary>
	public ref readonly T this[int index] => ref _values[index];

	/// <summary>
	/// Gets the number of elements in the array.
	/// </summary>
	public int Length => _values.Length;

	private ImmutableEquatableArray(T[] values)
	{
		_values = values;
	}

	/// <inheritdoc/>
	public bool Equals(ImmutableEquatableArray<T> other)
	{
		return ReferenceEquals(this, other) || ((ReadOnlySpan<T>)_values).SequenceEqual(other._values);
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is ImmutableEquatableArray<T> other && Equals(other);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		int hash = 0;
		foreach (T value in _values)
		{
			hash = CommonHelpers.CombineHashCodes(hash, value is null ? 0 : value.GetHashCode());
		}

		return hash;
	}

	/// <inheritdoc/>
	public Enumerator GetEnumerator() => new(_values);

	/// <summary>
	/// Defines an enumerator for the <see cref="ImmutableEquatableArray{T}"/> type.
	/// </summary>
	public struct Enumerator : IEnumerator<T>
	{
		private readonly T[] _values;
		private int _index;

		internal Enumerator(T[] values)
		{
			_values = values;
			_index = -1;
		}

		/// <summary>
		/// Advances the enumerator to the next element of the array.
		/// </summary>
		public bool MoveNext() => ++_index < _values.Length;

		/// <summary>
		/// Gets the element at the current position of the enumerator.
		/// </summary>
		public readonly ref T Current => ref _values[_index];

		readonly T IEnumerator<T>.Current => _values[_index];
		readonly object IEnumerator.Current => _values[_index];

		void IEnumerator.Reset() => throw new NotImplementedException();

		readonly void IDisposable.Dispose()
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static ImmutableEquatableArray<T> UnsafeCreateFromArray(T[] values)
	{
		return new ImmutableEquatableArray<T>(values);
	}

	#region Explicit interface implementations

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();

	bool ICollection<T>.IsReadOnly => true;
	bool IList.IsFixedSize => true;
	bool IList.IsReadOnly => true;
	T IReadOnlyList<T>.this[int index] => _values[index];
	T IList<T>.this[int index] { get => _values[index]; set => throw new InvalidOperationException(); }
	object IList.this[int index] { get => _values[index]; set => throw new InvalidOperationException(); }

	void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _values.CopyTo(array, arrayIndex);

	void ICollection.CopyTo(Array array, int index) => _values.CopyTo(array, index);

	int IList<T>.IndexOf(T item) => _values.AsSpan().IndexOf(item);

	int IList.IndexOf(object value) => ((IList)_values).IndexOf(value);

	bool ICollection<T>.Contains(T item) => _values.AsSpan().IndexOf(item) >= 0;

	bool IList.Contains(object? value) => ((IList)_values).Contains(value);

	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;

	int IReadOnlyCollection<T>.Count => Length;
	int ICollection<T>.Count => Length;
	int ICollection.Count => Length;

	void ICollection<T>.Add(T item) => throw new InvalidOperationException();

	bool ICollection<T>.Remove(T item) => throw new InvalidOperationException();

	void ICollection<T>.Clear() => throw new InvalidOperationException();

	void IList<T>.Insert(int index, T item) => throw new InvalidOperationException();

	void IList<T>.RemoveAt(int index) => throw new InvalidOperationException();

	int IList.Add(object? value) => throw new InvalidOperationException();

	void IList.Clear() => throw new InvalidOperationException();

	void IList.Insert(int index, object value) => throw new InvalidOperationException();

	void IList.Remove(object value) => throw new InvalidOperationException();

	void IList.RemoveAt(int index) => throw new InvalidOperationException();

	#endregion Explicit interface implementations

	private sealed class DebugView(ImmutableEquatableArray<T> array)
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items => array.ToArray();
	}
}

/// <summary>
/// Provides extension methods for the <see cref="ImmutableEquatableArray{T}"/> type.
/// </summary>
public static class ImmutableEquatableArray
{
	/// <summary>
	/// Creates an <see cref="ImmutableEquatableArray{T}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="T">The element type for the array.</typeparam>
	/// <param name="values">The source enumerable from which to populate the array.</param>
	/// <returns>A new <see cref="ImmutableEquatableArray{T}"/> instance.</returns>
	public static ImmutableEquatableArray<T> ToImmutableEquatableArray<T>(this IEnumerable<T> values) where T : IEquatable<T>
	{
		return values is ICollection<T> { Count: 0 }
			? ImmutableEquatableArray<T>.Empty
			: ImmutableEquatableArray<T>.UnsafeCreateFromArray(values.ToArray());
	}

	/// <summary>
	/// Creates an <see cref="ImmutableEquatableArray{T}"/> instance from the specified values.
	/// </summary>
	/// <typeparam name="T">The element type for the array.</typeparam>
	/// <param name="values">The source span from which to populate the array.</param>
	/// <returns>A new <see cref="ImmutableEquatableArray{T}"/> instance.</returns>
	public static ImmutableEquatableArray<T> Create<T>(ReadOnlySpan<T> values) where T : IEquatable<T>
	{
		return values.IsEmpty
			? ImmutableEquatableArray<T>.Empty
			: ImmutableEquatableArray<T>.UnsafeCreateFromArray(values.ToArray());
	}
}
