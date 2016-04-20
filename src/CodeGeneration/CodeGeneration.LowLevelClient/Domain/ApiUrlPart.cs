using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class ApiUrlPart
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public bool Required { get; set; }
		public IEnumerable<string> Options { get; set; }

		public string ClrTypeName
		{
			get
			{
				switch(this.Name)
				{
					case "index": return this.Type == "string" ? "IndexName" : "Indices";
					case "type": return this.Type == "string" ? "TypeName" : "Types";
					case "id": return this.Type == "string" ? "Id" : "Ids";
					case "node_id": return this.Type == "string" ? "NodeId" : "NodeIds";
					case "scroll_id": return this.Type == "string" ? "ScrollId" : "ScrollIds";
					case "field":
					case "fields": return this.Type == "string" ? "Field" : "Fields";
					case "index_metric": return "IndexMetrics";
					case "metric": return "Metrics";
					case "feature": return "Features";
					case "repository":
					case "snapshot":
					case "lang":
					case "username":
					case "usernames":
					case "realm":
					case "realms":
					case "name":
						return this.Type == "string" ? "Name" : "Names";
					case "task_id": return "TaskId";
					default: return this.Type + "_";
				}
			}
		}

		public string InterfaceName
		{
			get
			{
				switch(this.Name)
				{
					case "repository": return "RepositoryName";
					default: return this.Name.ToPascalCase();
				}
			}
		}
	}
}
