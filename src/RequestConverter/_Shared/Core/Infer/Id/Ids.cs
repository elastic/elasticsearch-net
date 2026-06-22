using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Ids : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		writer.WriteImplicitArray(_ids, static (w, id) => ((RequestConverter.ICodeFormattable)id).FormatCode(w));
}
