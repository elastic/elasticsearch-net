// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public readonly struct Refresh :
	IStringable
#if NET7_0_OR_GREATER
	, IParsable<Refresh>
#endif
{
	public static Refresh WaitFor = new("wait_for");
	public static Refresh True = new("true");
	public static Refresh False = new("false");

	public Refresh(string value) => Value = value;

	public string Value { get; }

	public string GetString() => Value ?? string.Empty;

	#region IParsable

	public static Refresh Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		out Refresh result)
	{
		if (s is null)
		{
			result = default;
			return false;
		}

		result = new Refresh(s);
		return true;
	}

	#endregion IParsable
}
