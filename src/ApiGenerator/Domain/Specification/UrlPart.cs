// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Domain.Specification
{

	//TODO once https://github.com/elastic/elasticsearch/pull/42346 lands
	// Rename this type to Deprecation and remove Path duplication
	public class DeprecatedPath
	{
		public string Version { get; set; }
		public string Path { get; set; }
		public string Description { get; set; }
	}


	public class UrlPart
	{
		private string _description;

		public string Argument => $"{LowLevelTypeName} {NameAsArgument}";

		public string LowLevelTypeName
		{
			get
			{
				//TODO treat list with fixed options as Flags Enum
				switch (Type)
				{
					case "int": //does not occur on part
					case "number": //does not occur on part
					case "string":
						return Type;
					case "list":
						return "string";
					case "enum":
						return Name.ToPascalCase();
					default:
						return Type;
				}
			}
		}

		public string HighLevelTypeName
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
					case "ids" when Type == "list":
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
					case "transform_id":
					case "model_id":
					case "id":
						return "Id";

					case "policy_id":
						return Type == "string" ? "Id" : "Ids";

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

					case "block":
						return "IndexBlock";

					case "index_uuid":
						return "IndexUuid";

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
		public bool Deprecated { get; set; }
		public string Type { get; set; }

		private string CleanUpDescription(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return value;

			return value.Replace("use `_all` or empty string", "use the special string `_all` or Indices.All");
		}
	}
}
