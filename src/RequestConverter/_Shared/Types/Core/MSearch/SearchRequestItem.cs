using System.Text;

namespace Elastic.Clients.Elasticsearch.Core.MSearch;

public sealed partial class SearchRequestItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append(ToString());
	}
}
