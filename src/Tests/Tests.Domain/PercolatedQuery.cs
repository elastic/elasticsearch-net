using Nest;

namespace Tests.Domain
{
	public class PercolatedQuery
	{
		public string Id { get; set; }

		public QueryContainer Query { get; set; }
	}
}
