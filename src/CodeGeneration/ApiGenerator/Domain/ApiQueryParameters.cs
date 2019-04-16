using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Domain
{
	public class ApiQueryParameters
	{
		private static readonly string[] FieldsParams =
		{
			"fields", "_source_include", "_source_exclude", "_source_includes", "_source_excludes",
		};

		public string ClsArgumentName => ClsName.ToCamelCase();

		public string ClsName { get; set; }

		public string Description { get; set; }

		public IEnumerable<string> DescriptionHighLevel
		{
			get
			{
				switch (QueryStringKey)
				{
					case "routing":
						yield return "A document is routed to a particular shard in an index using the following formula";
						yield return "<para> shard_num = hash(_routing) % num_primary_shards</para>";
						yield return "<para>Elasticsearch will use the document id if not provided. </para>";
						yield return "<para>For requests that are constructed from/for a document NEST will automatically infer the routing key";
						yield return
							"if that document has a <see cref=\"Nest.JoinField\" /> or a routing mapping on for its type exists on <see cref=\"Nest.ConnectionSettings\" /></para> ";

						yield break;
					case "_source":
						yield return "Whether the _source should be included in the response.";

						yield break;
					case "filter_path":
						yield return Description;
						yield return "<para>Use of response filtering can result in a response from Elasticsearch ";
						yield return "that cannot be correctly deserialized to the respective response type for the request. ";
						yield return "In such situations, use the low level client to issue the request and handle response deserialization</para>";

						yield break;
					default:
						yield return Description ?? "TODO";

						yield break;
				}
			}
		}

		public string DescriptorArgumentType =>
			Type == "list" && TypeHighLevel.EndsWith("[]") ? "params " + TypeHighLevel : TypeHighLevel;

		public Func<string, string, string, string, string> FluentGenerator { get; set; }
		public bool IsFieldParam => TypeHighLevel == "Field";

		public bool IsFieldsParam => TypeHighLevel == "Fields";

		public string Obsolete { get; set; }

		public IEnumerable<string> Options { get; set; }
		public string QueryStringKey { get; set; }

		public bool RenderPartial { get; set; }
		public string SetterHighLevel => "value";

		public string SetterLowLevel => "value";

		public string Type { get; set; }

		public string TypeHighLevel
		{
			get
			{
				if (QueryStringKey == "routing") return "Routing";

				var isFields = FieldsParams.Contains(QueryStringKey) || QueryStringKey.EndsWith("_fields");

				var csharpType = TypeLowLevel;
				switch (csharpType)
				{
					case "TimeSpan": return "Time";
				}

				switch (Type)
				{
					case "list" when isFields:
					case "string" when isFields: return "Fields";
					case "string" when QueryStringKey.Contains("field"): return "Field";
					default:
						return csharpType;
				}
			}
		}

		public string TypeLowLevel
		{
			get
			{
				switch (Type)
				{
					case "boolean": return "bool?";
					case "list": return "string[]";
					case "integer": return "int?";
					case "date": return "DateTimeOffset?";
					case "enum": return $"{ClsName}?";
					case "number":
						return new[] { "boost", "percen", "score" }.Any(s => QueryStringKey.ToLowerInvariant().Contains(s))
							? "double?"
							: "long?";
					case "duration":
					case "time":
						return "TimeSpan";
					case "text":
					case "":
					case null:
						return "string";
					default:
						return Type;
				}
			}
		}

		public string InitializerGenerator(string type, string name, string key, string setter, params string[] doc) =>
			CodeGenerator.Property(type, name, key, setter, Obsolete, doc);
	}
}
