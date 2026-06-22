using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Names : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(Values, static (w, name) => ((RequestConverter.ICodeFormattable)name).FormatCode(w));
}
