// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class PercentilesItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("new()");
		var hasProps = false;
		{
			writer.Append(hasProps ? ", " : " { ");
			hasProps = true;
			writer.Append("Key = ");
			writer.Append("\"");
			writer.Append(Key);
			writer.Append("\"");
		}

		{
			writer.Append(hasProps ? ", " : " { ");
			hasProps = true;
			writer.Append("Value = ");
			if (Value.HasValue)
				writer.Append(Value.Value.ToString(CultureInfo.InvariantCulture));
			else
				writer.Append("null");
		}

		if (hasProps)
			writer.Append(" }");
	}
}
