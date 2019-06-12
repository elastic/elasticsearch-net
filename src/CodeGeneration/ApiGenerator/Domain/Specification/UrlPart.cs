using System.Collections.Generic;
using System.Transactions;

namespace ApiGenerator.Domain.Specification
{
	public class UrlPart
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
						return Type + " " + NameAsArgument;
					case "list":
						return "string " + NameAsArgument;
					case "enum":
						return Name.ToPascalCase() + " " + NameAsArgument;
					case "number":
						return "string " + NameAsArgument;
					default:
						return Type + " " + NameAsArgument;
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
					case "category_id": return "LongId";
					case "timestamp": return "Timestamp";
					case "index_metric": return "IndexMetrics";
					case "metric": return "Metrics";
					
					case "node_id" when Type == "list": 
						return "NodeIds";
					
					case "fields" when Type == "list": 
						return "Fields";
					
					case "parent_task_id":
					case "task_id": 
						return "TaskId";
					
					case "forecast_id":
					case "action_id":
						return "Ids";
					
					case "index":
					case "new_index":
					case "target": 
						return Type == "string" ? "IndexName" : "Indices";
					
					case "watch_id":
					case "job_id":
					case "calendar_id":
					case "event_id":
					case "datafeed_id":
					case "snapshot_id":
					case "filter_id":
					case "policy_id":
					case "id": 
						return "Id";
					
					case "application":
					case "repository":
					case "snapshot":
					case "user":
					case "username":
					case "realms":
					case "alias":
					case "context":
					case "name":
					case "thread_pool_patterns":
					case "type":
						return Type == "string" ? "Name" : "Names";
					
					
					//This forces a compilation error post code generation as intended
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
		public string NameAsArgument => Name.ToCamelCase();
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
