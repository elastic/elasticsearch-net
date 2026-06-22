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
