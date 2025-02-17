// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

// TODO: Hand-craft this type for custom parsing.

public partial class ByteSize :
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<ByteSize>
#endif
{
	public string GetString(ITransportConfiguration settings) =>
		Match(
			v => v.ToString(CultureInfo.InvariantCulture),
			v => v ?? string.Empty
		);

	#region IParsable

	public static ByteSize Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out ByteSize? result)
	{
		if (string.IsNullOrEmpty(s))
		{
			result = null;
			return false;
		}

		if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
		{
			result = new ByteSize(value);
			return true;
		}

		result = new ByteSize(s);
		return true;
	}

	#endregion IParsable
}
