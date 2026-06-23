using System.Text;

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct WaitForActiveShards : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(Value);
	}
}
