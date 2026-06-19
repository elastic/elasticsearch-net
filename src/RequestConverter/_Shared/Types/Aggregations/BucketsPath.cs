using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class BucketsPath : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		switch (_kind)
		{
			case Kind.Single:
				writer.WriteString((string)_value);
				break;
			case Kind.Array:
				writer.WriteInlineList((string[])_value, static (w, item) => w.WriteString(item));
				break;
			case Kind.Dictionary:
				writer.Write("new() ");
				writer.WriteInlineList(
					(Dictionary<string, string>)_value,
					static (w, kvp) => w.Write("[").WriteString(kvp.Key).Write("] = ").WriteString(kvp.Value),
					open: "{ ",
					close: " }");
				break;
		}
	}
}
