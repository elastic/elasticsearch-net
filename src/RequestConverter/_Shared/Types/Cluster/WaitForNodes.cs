using System.Text;

namespace Elastic.Clients.Elasticsearch.Cluster;

public readonly partial struct WaitForNodes : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Condition switch
		{
			WaitForNodesCondition.EqualTo => Nodes.ToString(),
			WaitForNodesCondition.LessThan => $"<{Nodes}",
			WaitForNodesCondition.LessThanOrEqualTo => $"<={Nodes}",
			WaitForNodesCondition.GreaterThan => $">{Nodes}",
			WaitForNodesCondition.GreaterThanOrEqualTo => $">={Nodes}",
			_ => Nodes.ToString()
		});
		sb.Append("\"");
	}
}
