// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

public enum UnionTag
{
	None,
	T1,
	T2
}

/// <summary>
/// Represents the union of two types, <typeparamref name="T1"/> and <typeparamref name="T2"/>.
/// </summary>
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
public class Union<T1, T2>
{
	/// <summary>
	/// A tag that signals which value type is encapsulated in the union.
	/// </summary>
	/// <remarks>
	/// This tag should be used as the discriminator in <see langword="switch"/> expressions before accessing the actual encapsulated
	/// value in either <see cref="Value1"/> or <see cref="Value2"/>.
	/// <para>
	///     As an alternative, the <see cref="Match"/> or <see cref="Match{T}"/> method might be used to access the encapsulated value
	///     in a safe way.
	/// </para>
	/// </remarks>
	public UnionTag Tag { get; }

	/// <summary>
	/// The <typeparamref name="T1"/> value. This property is initialized only when <see cref="Tag"/> contains <see cref="UnionTag.T1"/>.
	/// </summary>
	public T1? Value1 { get; }

	/// <summary>
	/// The <typeparamref name="T2"/> value. This property is initialized only when <see cref="Tag"/> contains <see cref="UnionTag.T2"/>.
	/// </summary>
	public T2? Value2 { get; }

	/// <summary>
	/// Creates a new instance of <see cref="Union{T1,T2}"/> that encapsulates a <paramref name="value"/> of
	/// type <typeparamref name="T1"/>.
	/// </summary>
	/// <param name="value">The value to encapsulate.</param>
	public Union(T1? value)
	{
		Value1 = value;
		Tag = UnionTag.T1;
	}

	/// <summary>
	/// Creates a new instance of <see cref="Union{T1,T2}" /> that encapsulates a <paramref name="value"/> of
	/// type <typeparamref name="T2"/>.
	/// </summary>
	/// <param name="value">The value to encapsulate.</param>
	public Union(T2? value)
	{
		Value2 = value;
		Tag = UnionTag.T2;
	}

	/// <summary>
	/// Runs an <see cref="Action{T}"/> delegate against the encapsulated value.
	/// </summary>
	/// <param name="first">The delegate to run when this instance encapsulates a value of type <typeparamref name="T1"/>.</param>
	/// <param name="second">The delegate to run when this instance encapsulates a value of type <typeparamref name="T2"/>.</param>
	public void Match(Action<T1?>? first, Action<T2?>? second)
	{
		switch (Tag)
		{
			case UnionTag.T1:
				first?.Invoke(Value1);
				break;

			case UnionTag.T2:
				second?.Invoke(Value2);
				break;

			default:
				throw new InvalidOperationException($"Unrecognized tag value: {Tag}");
		}
	}

	/// <summary>
	/// Runs a <see cref="Func{T,TResult}"/> delegate against the encapsulated value.
	/// </summary>
	/// <param name="first">The delegate to run when this instance encapsulates a value of type <typeparamref name="T1"/>.</param>
	/// <param name="second">The delegate to run when this instance encapsulates a value of type <typeparamref name="T2"/>.</param>
	public T Match<T>(Func<T1?, T> first, Func<T2?, T> second)
	{
		if (first is null)
		{
			throw new ArgumentNullException(nameof(first));
		}

		if (second is null)
		{
			throw new ArgumentNullException(nameof(second));
		}

		return Tag switch
		{
			UnionTag.T1 => first(Value1),
			UnionTag.T2 => second(Value2),
			_ => throw new InvalidOperationException($"Unrecognized tag value: {Tag}")
		};
	}

#pragma warning disable CA2225

	public static implicit operator Union<T1, T2>(T1? first) => new(first);

	public static implicit operator Union<T1, T2>(T2? second) => new(second);

#pragma warning restore CA2225
}
