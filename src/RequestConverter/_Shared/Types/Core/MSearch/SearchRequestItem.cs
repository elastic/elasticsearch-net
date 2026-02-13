using System.Text;

namespace Elastic.Clients.Elasticsearch.Core.MSearch;

public sealed partial class SearchRequestItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append(ToString());
	}
}
