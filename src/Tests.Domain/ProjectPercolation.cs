using Nest;

namespace Tests.Framework.MockData
{
	public class ProjectPercolation : Project
	{
		public string Id { get; set; }
		public QueryContainer Query { get; set; }
	}
}
