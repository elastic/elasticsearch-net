using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class ApiUrlPart
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Options { get; set; }
	}
}