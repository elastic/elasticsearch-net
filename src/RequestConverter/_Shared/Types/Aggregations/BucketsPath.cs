// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
				writer.WriteImplicitArray((string[])_value, static (w, item) => w.WriteString(item));
				break;
			case Kind.Dictionary:
				writer.Write("new ").WriteTypeRef("System.Collections.Generic.Dictionary<string, string>").Write("()");
				writer.WriteBlockList(
					(Dictionary<string, string>)_value,
					static (w, kvp) => w.Write("[").WriteString(kvp.Key).Write("] = ").WriteString(kvp.Value));
				break;
		}
	}
}
