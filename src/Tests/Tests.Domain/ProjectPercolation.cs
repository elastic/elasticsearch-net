using Nest;

namespace Tests.Domain
{
	public class ProjectPercolation : Project
	{
		public string Id { get; set; }
		public QueryContainer Query { get; set; }
	}
}
