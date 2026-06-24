// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class PropertyName : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Name ?? Expression?.ToString() ?? Property?.Name);
		writer.Append("\"");
	}
}
