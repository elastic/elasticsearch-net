// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public readonly struct WaitForActiveShards :
	IStringable
#if NET7_0_OR_GREATER
	, IParsable<WaitForActiveShards>
#endif
{
	public static WaitForActiveShards All = new("all");

	public WaitForActiveShards(string value) => Value = value;

	public string Value { get; }

	public static implicit operator WaitForActiveShards(int v) => new(v.ToString());

	public static implicit operator WaitForActiveShards(string v) => new(v);

	public string GetString() => Value ?? string.Empty;

	#region IParsable

	public static WaitForActiveShards Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		out WaitForActiveShards result)
	{
		if (s is null)
		{
			result = default;
			return false;
		}

		result = new WaitForActiveShards(s);
		return true;
	}

	#endregion IParsable
}
