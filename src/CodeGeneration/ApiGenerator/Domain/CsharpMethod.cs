using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
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
		public string ObsoleteMethodVersion { get; set; }

		public string InterfaceType => "I" + (this.RequestTypeGeneric == "" || this.RequestTypeGeneric == "<T>" ? this.RequestType : this.RequestType + this.RequestTypeGeneric);

		public string RequestTypeGeneric { get; set; }
		public bool RequestTypeUnmapped { get; set; }
		public string HttpMethod { get; set; }
		public string Documentation { get; set; }
		public string Path { get; set; }
		public string Arguments { get; set; }
		public bool Unmapped { get; set; }
		public IEnumerable<ApiUrlPart> Parts { get; set; }
		public ApiUrl Url { get; set; }

		public bool SkipInterface { get; set; }

		public static CsharpMethod Clone(CsharpMethod method)
		{
			return new CsharpMethod
			{
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
				var urlRouteParameters = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));

				var par = string.Join(", ", urlRouteParameters.Select(p => $"{ClrParamType(p.Value.ClrTypeName)} {p.Key}"));
				var routing = string.Empty;

				//Routes that take {indices}/{types} and both are optional
				//we rather not generate a parameterless constructor and force folks to call Indices.All
				if (!urlRouteParameters.Any() && IndicesAndTypes)
				{
					ParameterlessIndicesTypesConstructor(ctors, m);
					continue;
				}

				if (urlRouteParameters.Any())
				{
					routing = "r=>r." + string.Join(".", urlRouteParameters
						.Select(p => new
						{
							route = p.Key,
							call = p.Value.Required ? "Required" : "Optional",
							v = p.Key == "metric" || p.Key == "watcher_stats_metric"
								? $"(Metrics){p.Key}"
								: p.Key == "index_metric"
									? $"(IndexMetrics){p.Key}"
									: p.Key
						})
						.Select(p => $"{p.call}(\"{p.route}\", {p.v})")
						);
				}

				var doc = $@"///<summary>{url}</summary>";
				if (urlRouteParameters.Any())
				{
					doc += "\r\n\t\t" + string.Join("\r\n\t\t", urlRouteParameters.Select(p => $"///<param name=\"{p.Key}\">{(p.Value.Required ? "this parameter is required" : "Optional, accepts null")}</param>"));
				}
				var generated = $"public {m}({par}) : base({routing})";

				// special case SearchRequest<T> to pass the type of T as the type, when only the index is specified.
				if ((m == "SearchRequest") && urlRouteParameters.Count() == 1 && !string.IsNullOrEmpty(this.RequestTypeGeneric))
				{
					var generic = this.RequestTypeGeneric.Replace("<", "").Replace(">", "");
					generated = $"public {m}({par}) : this({urlRouteParameters.First().Key}, typeof({generic}))";
				}

				if (string.IsNullOrEmpty(par) && !string.IsNullOrEmpty(this.RequestTypeGeneric))
				{
					var generic = this.RequestTypeGeneric.Replace("<", "").Replace(">", "");
					doc = AppendToSummary(doc, ". Will infer the index from the generic type");
					generated = $"public {m}({par}) : this(typeof({generic}))";
				}

				var c = new Constructor
				{
					Generated = generated,
					Description = doc,
					Body = this.IsDocumentRequest ? $" => Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty
				};

				ctors.Add(c);
			}
			if (IsDocumentPath && !string.IsNullOrEmpty(this.RequestTypeGeneric))
			{
				var documentPathGeneric = Regex.Replace(this.DescriptorTypeGeneric, @"^<?([^\s,>]+).*$", "$1");
				var doc = $"/// <summary>{this.Url.Path}</summary>";
				doc += "\r\n\t\t" + $"///<param name=\"document\"> describes an elasticsearch document of type <typeparamref name=\"{documentPathGeneric}\"/> from which the index, type and id can be inferred</param>";
				var documentRoute = "r=>r.Required(\"index\", index ?? document.Self.Index).Required(\"type\", type ?? document.Self.Type).Required(\"id\", id ?? document.Self.Id)";
				var documentFromPath = $"partial void DocumentFromPath({documentPathGeneric} document);";

				var constructor = $"DocumentPath<{documentPathGeneric}> document, IndexName index = null, TypeName type = null, Id id = null";

				var autoRoute = this.IsDocumentRequest ? "Q(\"routing\", new Routing(() => AutoRouteDocument() ?? document.Document));" : string.Empty;
				var body = $"{{ this.DocumentFromPath(document.Document); {autoRoute} }}";

				var c = new Constructor
				{
					AdditionalCode = documentFromPath,
					Generated = $"public {m}({constructor}) : base({documentRoute})",
					Body = body,
					Description = doc,
				};
				ctors.Add(c);
			}
			return ctors.DistinctBy(c => c.Generated);
		}

		private void ParameterlessIndicesTypesConstructor(List<Constructor> ctors, string m)
		{
			var generic = this.RequestTypeGeneric?.Replace("<", "").Replace(">", "");
			string doc;
			Constructor c;
			if (string.IsNullOrEmpty(generic))
			{
				doc = $@"/// <summary>{this.Url.Path}</summary>";
				c = new Constructor { Generated = $"public {m}()", Description = doc };
			}
			else
			{
				doc = $"///<summary>{this.Url.Path}<para><typeparamref name=\"{generic}\"/> describes an elasticsearch document type from which the index, type and id can be inferred</para></summary>";
				c = new Constructor { Generated = $"public {m}() : this(typeof({generic}), typeof({generic}))", Description = doc, };
			}

			c.Body = this.IsDocumentRequest ? $"Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty;
			ctors.Add(c);
		}

		public IEnumerable<Constructor> DescriptorConstructors()
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;
			var m = this.DescriptorType;
			foreach (var url in this.Url.Paths)
			{
				var requiredUrlRouteParameters = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => p.Value.Required)
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));

				var par = string.Join(", ", requiredUrlRouteParameters.Select(p => $"{ClrParamType(p.Value.ClrTypeName)} {p.Key}"));
				var routing = string.Empty;
				//Routes that take {indices}/{types} and both are optional
				if (!requiredUrlRouteParameters.Any() && IndicesAndTypes)
				{
					AddParameterlessIndicesTypesConstructor(ctors, m);
					continue;
				}
				if (requiredUrlRouteParameters.Any())
					routing = "r=>r." + string.Join(".", requiredUrlRouteParameters
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
				if (requiredUrlRouteParameters.Any())
				{
					doc += "\r\n\t\t" + string.Join("\r\n\t\t", requiredUrlRouteParameters.Select(p => $"///<param name=\"{p.Key}\"> this parameter is required</param>"));
				}

				var generated = $"public {m}({par}) : base({routing})";
				var body = this.IsDocumentRequest ? $"Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty;

				// Add typeof(T) as the default type when only index specified
				if ((m == "DeleteByQueryDescriptor" || m == "UpdateByQueryDescriptor") && requiredUrlRouteParameters.Count() == 1 && !string.IsNullOrEmpty(this.RequestTypeGeneric))
				{
					var generic = this.RequestTypeGeneric.Replace("<", "").Replace(">", "");
					generated = $"public {m}({par}) : base({routing}.Required(\"type\", (Types)typeof({generic})))";
				}

				if ((m == "SearchShardsDescriptor") && !string.IsNullOrEmpty(this.RequestTypeGeneric))
				{
					var generic = this.RequestTypeGeneric.Replace("<", "").Replace(">", "");
					doc = AppendToSummary(doc, ". Will infer the index from the generic type");
					generated = $"public {m}({par}) : base(r => r.Optional(\"index\", (Indices)typeof({generic})))";
				}

				// Use generic T to set the Indices and Types by default in the ctor
				if (m == "PutDatafeedDescriptor" || m == "UpdateDatafeedDescriptor")
				{
					var generic = "T";
					doc = AppendToSummary(doc, ". Will infer the index and type from the generic type");
					generated = $"public {m}({par}) : base({routing})";
					body = $"{{ Self.Indices = typeof({generic}); Self.Types = typeof({generic}); {body} }}";
				}

				var c = new Constructor
				{
					Generated = generated,
					Description = doc,
					Body = (!body.IsNullOrEmpty() && !body.StartsWith("{")) ? ("=> " + body) : body
				};
				ctors.Add(c);
			}
			if (IsDocumentPath && !string.IsNullOrEmpty(this.DescriptorTypeGeneric))
			{
				var documentPathGeneric = Regex.Replace(this.DescriptorTypeGeneric, @"^<?([^\s,>]+).*$", "$1");
				var doc = $"/// <summary>{this.Url.Path}</summary>";
				doc += "\r\n\t\t" + $"///<param name=\"document\"> describes an elasticsearch document of type <typeparamref name=\"{documentPathGeneric}\"/> from which the index, type and id can be inferred</param>";
				var documentRoute = "r=>r.Required(\"index\", document.Self.Index).Required(\"type\", document.Self.Type).Required(\"id\", document.Self.Id)";
				var documentFromPath = $"partial void DocumentFromPath({documentPathGeneric} document);";
				var autoRoute = this.IsDocumentRequest ? $"Q(\"routing\", new Routing(() => AutoRouteDocument() ?? document.Document));" : string.Empty;
				var c = new Constructor
				{
					AdditionalCode = documentFromPath,
					Generated =
						$"public {m}(DocumentPath<{documentPathGeneric}> document) : base({documentRoute})",
					Description = doc,
					Body = $"{{ this.DocumentFromPath(document.Document); {autoRoute}}}"
				};
				ctors.Add(c);
			}

			return ctors.DistinctBy(c => c.Generated);
		}

		private void AddParameterlessIndicesTypesConstructor(List<Constructor> ctors, string m)
		{
			var generic = this.DescriptorTypeGeneric?.Replace("<", "").Replace(">", "");
			var doc = $@"/// <summary>{this.Url.Path}</summary>";
			var documentRoute = $"r=> r.Required(\"index\", (Indices)typeof({generic})).Required(\"type\", (Types)typeof({generic}))";
			var c = new Constructor { Generated = $"public {m}() : base({documentRoute})", Description = doc };
			if (string.IsNullOrEmpty(generic))
				c = new Constructor { Generated = $"public {m}()", Description = doc };
			c.Body = this.IsDocumentRequest ? $" => Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty;
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
				var routeSetter = p.Required ? "Required" : "Optional";

				if (paramName == "metric" || paramName == "watcherStatsMetric") routeValue = "(Metrics)" + paramName;
				else if (paramName == "indexMetric") routeValue = "(IndexMetrics)indexMetric";

				var code = $"public {returnType} {p.InterfaceName}({ClrParamType(p.ClrTypeName)} {paramName}) => Assign(a=>a.RouteValues.{routeSetter}(\"{p.Name}\", {routeValue}));";
				var xmlDoc = $"///<summary>{p.Description}</summary>";
				setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				if (paramName == "index" || paramName == "type")
				{
					code = $"public {returnType} {p.InterfaceName}<TOther>() where TOther : class ";
					code += $"=> Assign(a=>a.RouteValues.{routeSetter}(\"{p.Name}\", ({p.ClrTypeName})typeof(TOther)));";
					xmlDoc = $"///<summary>a shortcut into calling {p.InterfaceName}(typeof(TOther))</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "index" && p.Type == "list")
				{
					code = $"public {returnType} AllIndices() => this.Index(Indices.All);";
					xmlDoc = $"///<summary>A shortcut into calling Index(Indices.All)</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "type" && p.Type == "list")
				{
					code = $"public {returnType} AllTypes() => this.Type(Types.All);";
					xmlDoc = $"///<summary>a shortcut into calling Type(Types.All)</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
				if (paramName == "fields" && p.Type == "list")
				{
					code = $"public {returnType} Fields<T>(params Expression<Func<T, object>>[] fields) ";
					code += $"=> Assign(a => a.RouteValues.{routeSetter}(\"fields\", (Fields)fields));";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
			}
			return setters;
		}

		private string AppendToSummary(string doc, string toAppend)
		{
			return Regex.Replace(
						doc,
						@"^(\/\/\/ <summary>)(.*?)(<\/summary>)(.*)",
						"$1$2" + toAppend + "$3$4");
		}

		private bool IsPartless => this.Url.Parts == null || !this.Url.Parts.Any();
		private bool IsScroll => this.Url.Parts.All(p => p.Key == "scroll_id");
		public bool IndicesAndTypes => AllParts.Count() == 2 && AllParts.All(p => p.Type == "list") && AllParts.All(p => new[] { "index", "type" }.Contains(p.Name));
		public bool IsDocumentPath => AllParts.Count() == 3 && AllParts.All(p => p.Type != "list") && AllParts.All(p => new[] { "index", "type", "id" }.Contains(p.Name));
		public bool IsDocumentRequest => this.IsDocumentPath && this.Url.Params.ContainsKey("routing");
		public IEnumerable<ApiUrlPart> AllParts => (this.Url?.Parts?.Values ?? Enumerable.Empty<ApiUrlPart>()).Where(p => !string.IsNullOrWhiteSpace(p.Name));
	}
}
