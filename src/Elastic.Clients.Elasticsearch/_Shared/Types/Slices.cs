// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class Slices :
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Slices>
#endif
{
	public string GetString(ITransportConfiguration settings) =>
		Match(
			v => v.ToString(CultureInfo.InvariantCulture),
			v => UrlFormatter.CreateString(v, settings)
		);

	#region IParsable

	public static Slices Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Slices? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
		{
			result = new Slices(value);
			return true;
		}

		if (EnumValueParser<SlicesCalculation>.TryParse(s, out var computed))
		{
			result = new Slices(computed);
			return true;
		}

		result = null;
		return false;
	}

	#endregion IParsable
}
