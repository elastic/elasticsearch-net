using System.Text;

namespace Elastic.Clients.Elasticsearch;

public abstract partial class DateMath : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(ToString());
	}
}
