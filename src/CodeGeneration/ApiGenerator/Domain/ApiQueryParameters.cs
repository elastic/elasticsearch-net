using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public class ApiQueryParameters
	{
		public string QueryStringKey { get; set; }

		public bool RenderPartial { get; set; }

		public string ClsName { get; set; }

		public string Obsolete { get; set; }

		public IEnumerable<string> Options { get; set; }

		public string Type { get; set; }

		private static readonly string[] FieldsParams = {"fields", "_source_include", "_source_exclude"};
		public string TypeHighLevel
		{
			get
			{
				if (this.QueryStringKey == "routing") return "Routing";
				var isFields = FieldsParams.Contains(this.QueryStringKey) || this.QueryStringKey.EndsWith("_fields");

				var csharpType = this.TypeLowLevel;
				switch (csharpType)
				{
					case "TimeSpan": return "Time";
				}

				switch (this.Type)
				{
					case "list" when isFields:
					case "string" when isFields: return "Fields";
					case "string" when this.QueryStringKey.Contains("field"): return "Field";
					default:
						return NullableCsharpType(csharpType);
				}
			}
		}

		public string ClsArgumentName => this.ClsName.ToCamelCase();
		public string DescriptorArgumentType => this.Type == "list" && this.TypeHighLevel.EndsWith("[]") ? "params " + this.TypeHighLevel : TypeHighLevel;
		public string SetterHighLevel
		{
			get
			{
				var setter = "value";
				if (this.TypeHighLevel == "Time") setter += ".ToString()";
				return setter;
			}
		}
		public string SetterLowLevel
		{
			get
			{
				var setter = "value";
				if (this.TypeLowLevel == "TimeSpan") setter += ".ToTimeUnit()";
				return setter;
			}
		}

		public bool IsFieldsParam => this.TypeHighLevel == "Fields";
		public bool IsFieldParam => this.TypeHighLevel == "Field";

		public string TypeLowLevel
		{
			get
			{
				switch (this.Type)
				{
					case "boolean": return "bool";
					case "list": return "string[]";
					case "integer": return "int";
					case "date": return "DateTimeOffset";
					case "enum": return this.ClsName;
					case "number":
						return new[] {"boost", "percen", "score"}.Any(s => this.QueryStringKey.ToLowerInvariant().Contains(s))
							? "double"
							: "long";
					case "duration":
					case "time":
						return "TimeSpan";
					case "text":
					case "":
					case null:
						return "string";
					default:
						return this.Type;
				}
			}
		}

		public string Description { get; set; }
		public IEnumerable<string> DescriptionHighLevel
		{
			get
			{
				switch (this.QueryStringKey)
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
					default:
						yield return this.Description;
						yield break;
				}
			}
		}

		private static string NullableCsharpType(string fieldType)
		{
			switch (fieldType)
			{
				case "bool": return "bool?";
				case "integer": return "int?";
				case "double": return "double?";
				case "long": return "long?";
				default:
					return fieldType;
			}
		}

		public static string[] WrapDocumentation(string documentation)
		{
			const int max = 140;
			var lines = documentation.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			var charCount = 0;
			return lines.GroupBy(Wrap).Select(l => string.Join(" ", l.ToArray())).ToArray();
			int Wrap(string w)
			{
				var increase = (charCount % max + w.Length + 1 >= max ? max - (charCount % max) : 0);
				return (charCount += increase + w.Length + 1) / max;
			}
		}

		public string InitializerGenerator(string type, string name, string key, string setter, params string[] doc)
		{
			var components = new List<string>();
			doc = doc?.SelectMany(WrapDocumentation).ToArray();
			if (doc?.Length > 0)
			{
				if (doc.Length == 1) A($"///<summary>{doc[0]}</summary>");
				else
				{
					A("///<summary>");
					foreach (var d in doc) A($"/// {d}");
					A("///</summary>");
				}
			}
			if (!string.IsNullOrWhiteSpace(this.Obsolete)) A($"[Obsolete(\"Scheduled to be removed in 7.0, {this.Obsolete}\")]");

			A(InitializerPropertyGenerator(type, name, key, setter));
			return string.Join("\r\n\t\t", components);

			void A(string s) => components.Add(s);
		}


		public string InitializerPropertyGenerator(string type, string name, string key, string setter)  =>
			$"public {NullableCsharpType(type)} {name} {{ get => Q<{NullableCsharpType(type)}>(\"{key}\"); set => Q(\"{key}\", {setter}); }}";

		public Func<string, string, string, string, string> FluentGenerator { get; set; }
	}
}
