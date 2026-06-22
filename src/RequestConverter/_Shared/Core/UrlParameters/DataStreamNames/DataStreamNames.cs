using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DataStreamNames : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(this, static (w, name) => ((RequestConverter.ICodeFormattable)name).FormatCode(w));
}
