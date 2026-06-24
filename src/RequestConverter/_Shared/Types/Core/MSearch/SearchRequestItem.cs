using System.Text;

namespace Elastic.Clients.Elasticsearch.Core.MSearch;

public sealed partial class SearchRequestItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// SearchRequestItem has no parameterless constructor (Header/Body are init-only and set via the ctor),
		// so emit a constructor call rather than an object initializer.
		writer.Write("new SearchRequestItem(");
		if (Header is not null)
		{
			Header.FormatCode(writer);
			writer.Write(", ");
		}

		Body.FormatCode(writer);
		writer.Write(")");
	}
}
