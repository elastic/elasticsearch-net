// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Field : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		var name = Name ?? Expression?.ToString() ?? Property?.Name;

		// A per-field boost ("title^2") is parsed into Name + Boost, so re-append it or it's lost.
		if (Boost.HasValue)
			name += "^" + Boost.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

		writer.WriteString(name);
	}
}
