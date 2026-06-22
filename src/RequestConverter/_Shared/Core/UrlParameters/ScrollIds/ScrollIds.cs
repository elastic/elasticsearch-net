using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollIds : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(this, static (w, id) => ((RequestConverter.ICodeFormattable)id).FormatCode(w));
}
