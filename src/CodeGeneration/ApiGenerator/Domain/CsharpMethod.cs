using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public class CsharpMethod
	{
		public string Arguments { get; set; }
		public string CallTypeGeneric { get; set; }
		public string DescriptorType { get; set; }
		public string DescriptorTypeGeneric { get; set; }
		public string Documentation { get; set; }
		public string FullName { get; set; }
		public string HttpMethod { get; set; }

		public string InterfaceType => $"I{RequestType}";

		public string InterfaceTypeGeneric =>
			string.IsNullOrEmpty(RequestTypeGeneric) ? null : $"I{RequestType}{RequestTypeGeneric}";

		public string ObsoleteMethodVersion { get; set; }
		public IEnumerable<ApiUrlPart> Parts { get; set; }
		public string Path { get; set; }
		public string QueryStringParamName { get; set; }
		public string RequestType { get; set; }

		public bool GenericAndNonGeneric { get; set; }
		public string RequestTypeGeneric { get; set; }
		public bool RequestTypeUnmapped { get; set; }
		public string ReturnDescription { get; set; }
		public string ReturnType { get; set; }
		public string ReturnTypeGeneric { get; set; }

		public bool SkipInterface { get; set; }
		public bool Unmapped { get; set; }
		public ApiUrl Url { get; set; }

		private bool IsPartless => !Url.ExposedApiParts.Any();

		public static CsharpMethod Clone(CsharpMethod method) => new CsharpMethod
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


		public IEnumerable<Constructor> RequestConstructors(bool inheritsFromPlainRequestBase)
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;

			ctors.AddRange(from url in Url.ExposedApiPaths
				let baseArgs = inheritsFromPlainRequestBase ? url.RequestBaseArguments : url.TypedSubClassBaseArguments
				let generated = $"public {RequestType}({url.ConstructorArguments}) : base({baseArgs})"
				select new Constructor
				{
					Generated = generated,
					Description = url.GetXmlDocs("\r\n\t\t"),
					//Body = isDocumentApi ? $" => Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty
					Body = string.Empty
				});

			var generic = RequestTypeGeneric.Replace("<", "").Replace(">", "").Split(",").First().Trim();
			if (!inheritsFromPlainRequestBase && !string.IsNullOrWhiteSpace(generic))
			{
				foreach (var url in Url.ExposedApiPaths.Where(path => path.HasResolvableArguments))
				{
					var baseArgs = url.AutoResolveBaseArguments(generic);
					var constructorArgs = url.AutoResolveConstructorArguments;
					var idOnly = constructorArgs == "Id id";
					var generated = $"public {RequestType}({constructorArgs}) : base({baseArgs})";
					ctors.Add(new Constructor
					{
						Generated = generated,
						Description = url.GetXmlDocs("\r\n\t\t", skipResolvable: true),
						Body = string.Empty
					});
					if (!idOnly) continue;

					ctors.Add(new Constructor
					{
						Generated = $"public {RequestType}({generic} id) : this(Id.From(id))",
						AdditionalCode = $"partial void DocumentFromPath({generic} document);",
						Description = url.GetXmlDocs("\r\n\t\t", skipResolvable: true, documentConstructor: true),
						Body = "=> DocumentFromPath(id);"
					});
//                    if (Url.IsDocumentApi)
//                    ctors.Add(new Constructor
//                    {
//                        Generated = $"public {RequestType}(DocumentPath<{generic}> path) : this(path.Self.Index, path.Self.Id)",
//                        Description = url.GetDocumentPathXmlDocs("\r\n\t\t"),
//                    });
				}
			}
			return ctors.GroupBy(c => c.Generated.Split(new [] {':'}, 2)[0]).Select(g=>g.Last());
		}

		public IEnumerable<Constructor> DescriptorConstructors()
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;

			var isDocumentApi = DescriptorTypeGeneric?.Contains("TDocument") ?? false;
			var m = DescriptorType;
			foreach (var url in Url.ExposedApiPaths)
			{
				var requiredUrlRouteParameters = url.Parts;
				var par = string.Join(", ", requiredUrlRouteParameters.Select(p => $"{p.ClrTypeName} {p.Name}"));
				var routing = string.Empty;
				if (requiredUrlRouteParameters.Any())
					routing = "r=>r." + string.Join(".", requiredUrlRouteParameters
						.Select(p => new
						{
							route = p.Name,
							call = p.Required ? "Required" : "Optional",
							v = p.Name == "metric"
								? $"(Metrics){p.Name}"
								: p.Name == "index_metric"
									? $"(IndexMetrics){p.Name}"
									: p.Name
						})
						.Select(p => $"{p.call}(\"{p.route}\", {p.v})")
					);
				var doc = $@"/// <summary>{url}</summary>";
				if (requiredUrlRouteParameters.Any())
					doc += "\r\n\t\t" + string.Join("\r\n\t\t",
						requiredUrlRouteParameters.Select(p => $"///<param name=\"{p.Name}\"> this parameter is required</param>"));

				var generated = $"public {m}({par}) : base({routing})";
				var body = isDocumentApi ? $"Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty;

				// Add typeof(T) as the default type when only index specified
				if ((m == "DeleteByQueryDescriptor" || m == "UpdateByQueryDescriptor") && requiredUrlRouteParameters.Count() == 1
					&& !string.IsNullOrEmpty(RequestTypeGeneric))
				{
					var generic = RequestTypeGeneric.Replace("<", "").Replace(">", "");
					generated = $"public {m}({par}) : base({routing})";
				}

				if (m == "SearchShardsDescriptor" && !string.IsNullOrEmpty(RequestTypeGeneric))
				{
					var generic = RequestTypeGeneric.Replace("<", "").Replace(">", "");
					doc = AppendToSummary(doc, ". Will infer the index from the generic type");
					generated = $"public {m}({par}) : base(r => r.Optional(\"index\", (Indices)typeof({generic})))";
				}

				// Use generic T to set the Indices and Types by default in the ctor
				if (m == "PutDatafeedDescriptor" || m == "UpdateDatafeedDescriptor")
				{
					var generic = "T";
					doc = AppendToSummary(doc, ". Will infer the index and type from the generic type");
					generated = $"public {m}({par}) : base({routing})";
					body = $"{{ Self.Indices = typeof({generic}); {body} }}";
				}

				var c = new Constructor
				{
					Generated = generated,
					Description = doc,
					Body = !body.IsNullOrEmpty() && !body.StartsWith("{") ? "=> " + body : body
				};
				ctors.Add(c);
			}
			if (isDocumentApi && !string.IsNullOrEmpty(DescriptorTypeGeneric))
			{
				var documentPathGeneric = Regex.Replace(DescriptorTypeGeneric, @"^<?([^\s,>]+).*$", "$1");
				var doc = $"/// <summary>{Url.Path}</summary>";
				doc += "\r\n\t\t"
					+ $"///<param name=\"document\"> describes an elasticsearch document of type <typeparamref name=\"{documentPathGeneric}\"/> from which the index, type and id can be inferred</param>";
				var documentRoute =
					"r=>r.Required(\"index\", document.Self.Index).Required(\"id\", document.Self.Id)";
				var documentFromPath = $"partial void DocumentFromPath({documentPathGeneric} document);";
				var autoRoute = isDocumentApi ? $"Q(\"routing\", new Routing(() => AutoRouteDocument() ?? document.Document));" : string.Empty;
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

		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (IsPartless) return setters;

			var alwaysGenerate = new[] { "index" };
			var parts = Url.ExposedApiParts
				.Where(p => !p.Required || alwaysGenerate.Contains(p.Name))
				.Where(p => !string.IsNullOrEmpty(p.Name))
				.ToList();
			var returnType = DescriptorType + DescriptorTypeGeneric;
			foreach (var part in parts)
			{
				var p = part;
				var paramName = p.Name.ToPascalCase();
				if (paramName.Length > 1)
					paramName = paramName.Substring(0, 1).ToLowerInvariant() + paramName.Substring(1);
				else
					paramName = paramName.ToLowerInvariant();

				var routeValue = paramName;
				var routeSetter = p.Required ? "Required" : "Optional";

				if (paramName == "metric" || paramName == "watcherStatsMetric") routeValue = "(Metrics)" + paramName;
				else if (paramName == "indexMetric") routeValue = "(IndexMetrics)indexMetric";

				var code =
					$"public {returnType} {p.InterfaceName}({p.ClrTypeName} {paramName}) => Assign(a=>a.RouteValues.{routeSetter}(\"{p.Name}\", {routeValue}));";
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

		private string AppendToSummary(string doc, string toAppend) => Regex.Replace(
			doc,
			@"^(\/\/\/ <summary>)(.*?)(<\/summary>)(.*)",
			"$1$2" + toAppend + "$3$4");
	}
}
