// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch.Cluster;

public readonly partial struct WaitForNodes : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Condition switch
		{
			WaitForNodesCondition.EqualTo => Nodes.ToString(),
			WaitForNodesCondition.LessThan => $"<{Nodes}",
			WaitForNodesCondition.LessThanOrEqualTo => $"<={Nodes}",
			WaitForNodesCondition.GreaterThan => $">{Nodes}",
			WaitForNodesCondition.GreaterThanOrEqualTo => $">={Nodes}",
			_ => Nodes.ToString()
		});
		writer.Append("\"");
	}
}
