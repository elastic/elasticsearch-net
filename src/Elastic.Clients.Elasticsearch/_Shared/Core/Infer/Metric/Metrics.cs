// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Represents a collection of unique metric names to be included in URL paths to limit the request.
/// </summary>
public sealed class Metrics :
	IEquatable<Metrics>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Metrics>
#endif
{
	private static readonly HashSet<string> EmptyMetrics = new();

	/// <summary>
	/// An instance of <see cref="Metrics"/> representing all statistics.
	/// </summary>
	public static Metrics All { get; } = new("_all");

	/// <summary>
	///	Initializes a new instance of the <see cref="Metrics"/> class containing a single metric name.
	/// </summary>
	public Metrics(string metric)
	{
		if (string.IsNullOrEmpty(metric))
			Values = EmptyMetrics;

		Values = new HashSet<string>()
		{
			metric
		};
	}

	/// <summary>
	///	Initializes a new instance of the <see cref="Metrics"/> class containing a collection of metric names.
	/// </summary>
	public Metrics(IEnumerable<string> metrics)
	{
		if (metrics is null)
			Values = EmptyMetrics;

		Values = new HashSet<string>(metrics);
	}

	private HashSet<string> Values { get; }

	/// <inheritdoc />
	public bool Equals(Metrics other)
	{
		if (other is null)
			return false;

		// Equality is true when both instances have the same metric names.
		return Values.SetEquals(other.Values);
	}

	string IUrlParameter.GetString(ITransportConfiguration settings) => GetString();

	/// <inheritdoc />
	public override string ToString() => GetString();

	private string GetString()
	{
		if (Values == EmptyMetrics)
			return string.Empty;

		return string.Join(",", Values);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		// Lifting the minimal target framework to .NET Standard 2.1
		// would be the best solution ever due to the HashCode type.
		var hashCode = 0;
		foreach (var metric in Values)
			hashCode = (hashCode * 397) ^ metric.GetHashCode();

		return hashCode;
	}

	public static bool operator ==(Metrics left, Metrics right) => Equals(left, right);

	public static bool operator !=(Metrics left, Metrics right) => !Equals(left, right);

	public static implicit operator Metrics(string metric) => new(metric);

	public static implicit operator Metrics(string[] metrics) => new(metrics);

	/// <inheritdoc />
	public override bool Equals(object obj)
	{
		if (obj is not Metrics metrics)
			return false;

		return Equals(metrics);
	}

	#region IParsable

	public static Metrics Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Metrics? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new Metrics(string.Empty);
			return true;
		}

		result = new Metrics(list);
		return true;
	}

	#endregion IParsable
}
