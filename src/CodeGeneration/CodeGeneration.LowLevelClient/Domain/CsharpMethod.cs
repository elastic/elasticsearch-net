using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class CsharpMethod
	{
		public string ReturnType { get; set; }
		public string ReturnTypeGeneric { get; set; }
		public string CallTypeGeneric { get; set; }
		public string ReturnDescription { get; set; }
		public string FullName { get; set; }
		public string QueryStringParamName { get; set; }
		public string DescriptorType { get; set; }
		public string DescriptorTypeGeneric { get; set; }
		public string RequestType { get; set; }

		public string InterfaceType => "I" + (this.RequestTypeGeneric == "" || this.RequestTypeGeneric == "<T>" ? this.RequestType : this.RequestType + this.RequestTypeGeneric);

		public string RequestTypeGeneric { get; set; }
		public bool RequestTypeUnmapped { get; set; }
		public string HttpMethod { get; set; }
		public string Documentation { get; set; }
		public string Path { get; set; }
		public string Arguments { get; set; }
		public bool Allow404 { get; set; }
		public bool Unmapped { get; set; }
		public IEnumerable<ApiUrlPart> Parts { get; set; }
		public ApiUrl Url { get; set; }

		public bool SkipInterface { get; set; }

		public static CsharpMethod Clone(CsharpMethod method)
		{
			return new CsharpMethod
			{
				Allow404 = method.Allow404,
				Path = method.Path,
				RequestType = method.RequestType,
				ReturnDescription = method.ReturnDescription,
				Arguments = method.Arguments,
				CallTypeGeneric = method.CallTypeGeneric,
				DescriptorType = method.DescriptorType,
				DescriptorTypeGeneric = method.DescriptorTypeGeneric,
				Documentation = method.Documentation,
				FullName = method.FullName,
				HttpMethod = method.HttpMethod,
				Parts = method.Parts,
				QueryStringParamName = method.QueryStringParamName,
				RequestTypeGeneric = method.RequestTypeGeneric,
				RequestTypeUnmapped = method.RequestTypeUnmapped,
				ReturnType = method.ReturnType,
				ReturnTypeGeneric = method.ReturnTypeGeneric,
				Unmapped = method.Unmapped,
				Url = method.Url,
				SkipInterface = method.SkipInterface
			};
		}

		private string MetricPrefix => this.RequestType.Replace("Request", "");
		private string ClrParamType(string clrType) => clrType.EndsWith("Metrics", StringComparison.OrdinalIgnoreCase)
			? this.MetricPrefix + clrType.Replace("Metrics", "Metric") : clrType;

		public IEnumerable<Constructor> RequestConstructors()
		{
			var ctors = new List<Constructor>();

			if (IsPartless) return ctors;

			// Do not generate ctors for scroll apis
			// Scroll ids should always be passed as part of the request body and enforced via manual ctors
			if (IsScroll) return ctors;

			var m = this.RequestType;
			foreach (var url in this.Url.Paths)
			{
				var cp = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{ClrParamType(p.Value.ClrTypeName)} {p.Key}"));
				var routing = string.Empty;

				//Routes that take {indices}/{types} and both are optional
				//we rather not generate a parameterless constructor and force folks to call Indices.All
				if (!cp.Any() && IndicesAndTypes)
				{
					ParameterlessIndicesTypesConstructor(ctors, m);
					continue;
				}

				if (cp.Any())
				{
					routing = "r=>r." + string.Join(".", cp
						.Select(p => new
						{
							route = p.Key,
							call = p.Value.Required ? "Required" : "Optional",
							v = p.Key == "metric"
								? $"(Metrics){p.Key}"
								: p.Key == "index_metric"
									? $"(IndexMetrics){p.Key}"
									: p.Key
						})
						.Select(p => $"{p.call}(\"{p.route}\", {p.v})")
						);
				}

				var doc = $@"/// <summary>{url}</summary>";
				if (cp.Any())
				{
					doc += "\r\n" + string.Join("\t\t\r\n", cp.Select(p => $"///<param name=\"{p.Key}\">{(p.Value.Required ? "this parameter is required" : "Optional, accepts null")}</param>"));
				}
				var generated = $"public {m}({par}) : base({routing}){{}}";

				// special case SearchRequest<T> to pass the type of T as the type, when only the index is specified.
				if (m == "SearchRequest" && cp.Count() == 1 && !string.IsNullOrEmpty(this.RequestTypeGeneric))
				{
					var generic = this.RequestTypeGeneric.Replace("<", "").Replace(">", "");
					generated = $"public {m}({par}) : this({cp.First().Key}, typeof({generic})){{}}";
                }

				var c = new Constructor { Generated = generated, Description = doc };
				ctors.Add(c);
			}
			if (IsDocumentPath && !string.IsNullOrEmpty(this.RequestTypeGeneric))
			{
				var doc = $@"/// <summary>{this.Url.Path}</summary>";
				doc += "\r\n\t\t\r\n" + "///<param name=\"document\"> describes an elasticsearch document of type T, allows implicit conversion from numeric and string ids </param>";
				var documentRoute = "r=>r.Required(\"index\", index ?? document.Self.Index).Required(\"type\", type ?? document.Self.Type).Required(\"id\", id ?? document.Self.Id)";
				var documentPathGeneric = Regex.Replace(this.DescriptorTypeGeneric, @"^<?([^\s,>]+).*$", "$1");
				var documentFromPath = $"partial void DocumentFromPath({documentPathGeneric} document);";

				var constructor = $"DocumentPath<{documentPathGeneric}> document, IndexName index = null, TypeName type = null, Id id = null";

				var c = new Constructor { AdditionalCode = documentFromPath, Generated = $"public {m}({constructor}) : base({documentRoute}){{ this.DocumentFromPath(document.Document); }}", Description = doc, };
				ctors.Add(c);
			}
			return ctors.DistinctBy(c => c.Generated);
		}

		private void ParameterlessIndicesTypesConstructor(List<Constructor> ctors, string m)
		{
			var generic = this.RequestTypeGeneric?.Replace("<", "").Replace(">", "");
			var doc = $@"/// <summary>{this.Url.Path}</summary>";
			doc += "\r\n\t\t\r\n" + "///<param name=\"document\"> describes an elasticsearch document of type T, allows implicit conversion from numeric and string ids </param>";
			var c = new Constructor { Generated = $"public {m}() {{}}", Description = doc, };
			if (!string.IsNullOrEmpty(generic))
				c = new Constructor { Generated = $"public {m}() : this(typeof({generic}), typeof({generic})) {{}}", Description = doc, };
			ctors.Add(c);
		}

		public IEnumerable<Constructor> DescriptorConstructors()
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;
			var m = this.DescriptorType;
			foreach (var url in this.Url.Paths)
			{
				var cp = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => p.Value.Required)
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{ClrParamType(p.Value.ClrTypeName)} {p.Key}"));
				var routing = string.Empty;
				//Routes that take {indices}/{types} and both are optional
				if (!cp.Any() && IndicesAndTypes)
				{
					AddParameterlessIndicesTypesConstructor(ctors, m);
					continue;
				}
				if (cp.Any())
					routing = "r=>r." + string.Join(".", cp
						.Select(p => new
						{
							route = p.Key,
							call = p.Value.Required ? "Required" : "Optional",
							v = p.Key == "metric"
								? $"(Metrics){p.Key}"
								: p.Key == "index_metric"
									? $"(IndexMetrics){p.Key}"
									: p.Key
						})
						.Select(p => $"{p.call}(\"{p.route}\", {p.v})")
					);
				var doc = $@"/// <summary>{url}</summary>";
				if (cp.Any())
				{
					doc += "\r\n" + string.Join("\t\t\r\n", cp.Select(p => $"///<param name=\"{p.Key}\"> this parameter is required</param>"));
				}
				var c = new Constructor { Generated = $"public {m}({par}) : base({routing}){{}}", Description = doc };
				ctors.Add(c);
			}
			if (IsDocumentPath && !string.IsNullOrEmpty(this.DescriptorTypeGeneric))
			{
				var doc = $@"/// <summary>{this.Url.Path}</summary>";
				doc += "\r\n\t\t\r\n" + "///<param name=\"document\"> describes an elasticsearch document of type T, allows implicit conversion from numeric and string ids </param>";
				var documentRoute = "r=>r.Required(\"index\", document.Self.Index).Required(\"type\", document.Self.Type).Required(\"id\", document.Self.Id)";
				var documentPathGeneric = Regex.Replace(this.DescriptorTypeGeneric, @"^<?([^\s,>]+).*$", "$1");
				var documentFromPath = $"partial void DocumentFromPath({documentPathGeneric} document);";
				var c = new Constructor { AdditionalCode = documentFromPath, Generated = $"public {m}(DocumentPath<{documentPathGeneric}> document) : base({documentRoute}){{ this.DocumentFromPath(document.Document); }}", Description = doc };
				ctors.Add(c);
			}

			return ctors.DistinctBy(c => c.Generated);
		}

		private void AddParameterlessIndicesTypesConstructor(List<Constructor> ctors, string m)
		{
			var generic = this.DescriptorTypeGeneric?.Replace("<", "").Replace(">", "");
			var doc = $@"/// <summary>{this.Url.Path}</summary>";
			var documentRoute = $"r=> r.Required(\"index\", (Indices)typeof({generic})).Required(\"type\", (Types)typeof({generic}))";
			var c = new Constructor { Generated = $"public {m}() : base({documentRoute}){{}}", Description = doc };
			if (string.IsNullOrEmpty(generic))
				c = new Constructor { Generated = $"public {m}() {{}}", Description = doc };
			ctors.Add(c);
		}

		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (IsPartless) return setters;
			var alwaysGenerate = new[] { "index", "type" };
			var parts = this.Url.Parts
				.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
				.Where(p => !p.Value.Required || alwaysGenerate.Contains(p.Key))
				.Where(p => !string.IsNullOrEmpty(p.Value.Name))
				.ToList();
			var returnType = this.DescriptorType + this.DescriptorTypeGeneric;
			foreach (var part in parts)
			{
				var p = part.Value;
				var paramName = p.Name.ToPascalCase();
				if (paramName.Length > 1)
					paramName = paramName.Substring(0, 1).ToLowerInvariant() + paramName.Substring(1);
				else
					paramName = paramName.ToLowerInvariant();

				var routeValue = paramName;
				if (paramName == "metric") routeValue = "(Metrics)metric";
				else if (paramName == "indexMetric") routeValue = "(IndexMetrics)indexMetric";

				var code = $"public {returnType} {p.InterfaceName}({ClrParamType(p.ClrTypeName)} {paramName}) => Assign(a=>a.RouteValues.Optional(\"{p.Name}\", {routeValue}));";
				var xmlDoc = $"///<summary>{p.Description}</summary>";
				setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				if (paramName == "index" || paramName == "type")
				{
					code = $"public {returnType} {p.InterfaceName}<TOther>() where TOther : class ";
					code += $"=> Assign(a=>a.RouteValues.Optional(\"{p.Name}\", ({p.ClrTypeName})typeof(TOther)));";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "index" && p.Type == "list")
				{
					code = $"public {returnType} AllIndices() => this.Index(Indices.All);";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "type" && p.Type == "list")
				{
					code = $"public {returnType} AllTypes() => this.Type(Types.All);";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "fields" && p.Type == "list")
				{
					code = $"public {returnType} Fields<T>(params Expression<Func<T, object>>[] fields) ";
					code += "=> Assign(a => a.RouteValues.Optional(\"fields\", (Fields)fields));";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
			}
			return setters;
		}


		private bool IsPartless => this.Url.Parts == null || !this.Url.Parts.Any();
		private bool IsScroll => this.Url.Parts.All(p => p.Key == "scroll_id");
		public bool IndicesAndTypes => AllParts.Count() == 2 && AllParts.All(p => p.Type == "list") && AllParts.All(p => new[] { "index", "type" }.Contains(p.Name));
		public bool IsDocumentPath => AllParts.Count() == 3 && AllParts.All(p => p.Type != "list") && AllParts.All(p => new[] { "index", "type", "id" }.Contains(p.Name));
		public IEnumerable<ApiUrlPart> AllParts => (this.Url?.Parts?.Values ?? Enumerable.Empty<ApiUrlPart>()).Where(p => !string.IsNullOrWhiteSpace(p.Name));
	}
}
