using System.Text;

namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;

public sealed partial class SearchTemplateRequestItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append(ToString());
	}
}
