using System;
using System.Collections.Generic;

namespace ApiGenerator.Domain
{
	public class ApiUrlPart
	{
		public string Name { get; set; }
		public string Type { get; set; }


		private string _description;
		public string Description
		{
			get => _description;
			set => _description = CleanUpDescription(value);
		}
		public bool Required { get; set; }
		public IEnumerable<string> Options { get; set; }


		private string CleanUpDescription(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return value;
			return value.Replace("use `_all` or empty string", "use the special string `_all` or Indices.All");
		}

		public string ClrTypeName
		{
			get
			{
				if (ClrTypeNameOverride != null) return ClrTypeNameOverride;

				switch(this.Name)
				{
					case "index":
					case "new_index":
						return this.Type == "string" ? "IndexName" : "Indices";
					case "target":
						return "IndexName";
					case "type": return this.Type == "string" ? "TypeName" : "Types";
					case "watch_id":
					case "job_id":
					case "datafeed_id":
					case "snapshot_id":
					case "filter_id":
					case "id": return this.Type == "string" ? "Id" : "Ids";
					case "category_id": return "CategoryId";
					case "nodes":
					case "node_id": return this.Type == "string" ? "NodeId" : "NodeIds";
					case "scroll_id": return this.Type == "string" ? "ScrollId" : "ScrollIds";
					case "field":
					case "fields": return this.Type == "string" ? "Field" : "Fields";
					case "index_metric": return "IndexMetrics";
					case "metric":
					case "watcher_stats_metric":
						return "Metrics";
					case "feature": return "Features";
					case "action_id": return "ActionIds";
					case "repository":
					case "snapshot":
					case "lang":
					case "username":
					case "usernames":
					case "realm":
					case "realms":
					case "alias":
					case "context":
					case "name":
					case "thread_pool_patterns":
						return this.Type == "string" ? "Name" : "Names";
					case "parent_task_id":
					case "task_id": return "TaskId";
					case "timestamp": return "Timestamp";
					default: return this.Type + "_";
				}
			}
		}

		public string ClrTypeNameOverride { get; set; }

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

		public string Argument
		{
			get
			{
				switch (this.Type)
				{
					case "int":
					case "string":
						return this.Type + " " + this.Name;
					case "list":
						return "string " + this.Name;
					case "enum":
						return ApiGenerator.PascalCase(this.Name) + " " + this.Name;
					case "number":
						return "string " + this.Name;
					default:
						return this.Type + " " + this.Name;
				}
			}
		}
	}
}
