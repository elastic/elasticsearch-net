using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Indices : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(this, static (w, index) => ((RequestConverter.ICodeFormattable)index).FormatCode(w));
}
