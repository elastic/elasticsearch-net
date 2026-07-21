// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public partial class SourceConfigParam :
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<SourceConfigParam>
#endif
{
	public string GetString(ITransportConfiguration settings) =>
		Match(
			v => UrlFormatter.CreateString(v, settings),
			v => UrlFormatter.CreateString(v, settings)
		);

	#region IParsable

	public static SourceConfigParam Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out SourceConfigParam? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (bool.TryParse(s, out var b))
		{
			result = new SourceConfigParam(b);
			return true;
		}

		if (Fields.TryParse(s, provider, out var fields))
		{
			result = new SourceConfigParam(fields);
			return true;
		}

		result = null;
		return false;
	}

	#endregion IParsable
}
