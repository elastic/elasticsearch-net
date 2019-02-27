using System.Collections.Generic;

namespace ApiGenerator.Domain
{
	public class ApiUrlPart
	{
		private string _description;

		public string Argument
		{
			get
			{
				//TODO treat list with fixed options as Flags Enum
				switch (Type)
				{
					case "int":
					case "string":
						return Type + " " + Name;
					case "list":
						return "string " + Name;
					case "enum":
						return ApiGenerator.PascalCase(Name) + " " + Name;
					case "number":
						return "string " + Name;
					default:
						return Type + " " + Name;
				}
			}
		}

		public string ClrTypeName
		{
			get
			{
				if (ClrTypeNameOverride != null) return ClrTypeNameOverride;

				switch (Name)
				{
					case "index":
					case "new_index":
						return Type == "string" ? "IndexName" : "Indices";
					case "target":
						return "IndexName";
					case "type": return Type == "string" ? "Name" : "Names";
					case "watch_id":
					case "job_id":
					case "datafeed_id":
					case "snapshot_id":
					case "filter_id":
					case "id": return Type == "string" ? "Id" : "Ids";
					case "category_id": return "CategoryId";
					case "nodes":
					case "node_id": return Type == "string" ? "NodeId" : "NodeIds";
					case "field":
					case "fields": return Type == "string" ? "Field" : "Fields";
					case "index_metric": return "IndexMetrics";
					case "metric":
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
						return Type == "string" ? "Name" : "Names";
					case "parent_task_id":
					case "task_id": return "TaskId";
					case "timestamp": return "Timestamp";
					default: return Type + "_";
				}
			}
		}

		public string ClrTypeNameOverride { get; set; }

		public string Description
		{
			get => _description;
			set => _description = CleanUpDescription(value);
		}

		public string InterfaceName
		{
			get
			{
				switch (Name)
				{
					case "repository": return "RepositoryName";
					default: return Name.ToPascalCase();
				}
			}
		}

		public string Name { get; set; }
		public IEnumerable<string> Options { get; set; }
		public bool Required { get; set; }
		public string Type { get; set; }


		private string CleanUpDescription(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return value;

			return value.Replace("use `_all` or empty string", "use the special string `_all` or Indices.All");
		}
	}
}
