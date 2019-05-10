using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using ApiGenerator.Overrides.Descriptors;
using Newtonsoft.Json;

namespace ApiGenerator.Domain
{
	public class RequestInterface
	{
		public IReadOnlyCollection<UrlPart> UrlParts { get; set; }

		/// <summary>
		/// Partial parameters are query string parameters we prefer to send over the body of a request.
		/// We declare these on the generated interfaces so that we don't forget to implement them in our request
		/// implementations
		/// </summary>
		public IReadOnlyCollection<QueryParameters> PartialParameters { get; set; }

		public string OfficialDocumentationLink { get; set; }
		public CsharpNames CsharpNames { get; set; }
	}

	public class RequestPartialImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<UrlPart> Parts { get; set; }
		public IReadOnlyCollection<UrlPath> Paths { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public IReadOnlyCollection<Constructor> Constructors { get; set; }
		public IReadOnlyCollection<Constructor> GenericConstructors { get; set; }
		public bool HasBody { get; set; }
	}
	
	public class RequestParameterImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public string HttpMethod { get; set; }
	}

	public class DescriptorPartialImplementation
	{
		public CsharpNames CsharpNames { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public IReadOnlyCollection<Constructor> Constructors { get; set; }
		public IReadOnlyCollection<UrlPart> Parts { get; set; }
		public IReadOnlyCollection<UrlPath> Paths { get; set; }
		public IReadOnlyCollection<QueryParameters> Params { get; set; }
		public bool HasBody { get; set; }
		
		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (!Parts.Any()) return setters;

			var alwaysGenerate = new[] { "index" };
			var parts = Parts
				.Where(p => !p.Required || alwaysGenerate.Contains(p.Name))
				.Where(p => !string.IsNullOrEmpty(p.Name))
				.ToList();
			var returnType = CsharpNames.GenericOrNonGenericDescriptorName;
			foreach (var part in parts)
			{
				var p = part;
				var paramName = p.Name.ToPascalCase();
				if (paramName.Length > 1)
					paramName = paramName.Substring(0, 1).ToLowerInvariant() + paramName.Substring(1);
				else
					paramName = paramName.ToLowerInvariant();

				var routeValue = "v";
				var routeSetter = p.Required ? "Required" : "Optional";

				if (paramName == "metric" || paramName == "watcherStatsMetric") routeValue = "(Metrics)v";
				else if (paramName == "indexMetric") routeValue = "(IndexMetrics)v";

				var code =
					$"public {returnType} {p.InterfaceName}({p.ClrTypeName} {paramName}) => Assign({paramName}, (a,v)=>a.RouteValues.{routeSetter}(\"{p.Name}\", {routeValue}));";
				var xmlDoc = $"///<summary>{p.Description}</summary>";
				setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				if (paramName == "index")
				{
					code = $"public {returnType} {p.InterfaceName}<TOther>() where TOther : class ";
					code += $"=> Assign(typeof(TOther), (a,v)=>a.RouteValues.{routeSetter}(\"{p.Name}\", ({p.ClrTypeName})v));";
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
					code += $"=> Assign(fields, (a,v)=>a.RouteValues.{routeSetter}(\"fields\", (Fields)v));";
					xmlDoc = $"///<summary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
			}
			return setters;
		}
	}

	public class ApiEndpoint
	{
		/// <summary> The filename of the spec describing the api endpoint </summary>
		public string FileName { get; set; }
		
		/// <summary> The original name as declared in the spec </summary>
		public string Name { get; set; }
		
		/// <summary> The original namespace as declared in the spec </summary>
		public string Namespace { get; set; }
		
		/// <summary> The original method name as declared in the spec </summary>
		public string MethodName { get; set; }
		
		/// <summary> Computed Csharp identifier names </summary>
		public CsharpNames CsharpNames { get; set; } 
		
		[JsonProperty("documentation")]
		public string OfficialDocumentationLink { get; set; }
		public UrlInformation Url { get; set; }
		public Body Body { get; set; }

		[JsonProperty("methods")]
		public IReadOnlyCollection<string> HttpMethods { get; set; }
		public IEndpointOverrides Overrides { get; internal set; }

		public RequestInterface RequestInterface => new RequestInterface
		{
			CsharpNames = CsharpNames,
			UrlParts = Url.Parts,
			PartialParameters = Body == null ? Enumerable.Empty<QueryParameters>().ToList() : Url.Params.Values.Where(p=>p.RenderPartial && !p.Skip).ToList(),
			OfficialDocumentationLink = OfficialDocumentationLink
		};
		
		public RequestPartialImplementation RequestPartialImplementation => new RequestPartialImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink,
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p=>!p.Skip).ToList(),
			Constructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: true).ToList(),
			GenericConstructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: false).ToList(),
			HasBody = Body != null,
		};
		
		public DescriptorPartialImplementation DescriptorPartialImplementation => new DescriptorPartialImplementation
		{
			CsharpNames = CsharpNames, 
			OfficialDocumentationLink = OfficialDocumentationLink,
			Constructors = Constructor.DescriptorConstructors(CsharpNames, Url).ToList(),
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p=>!p.Skip).ToList(),
			HasBody = Body != null,
		};
		
		public RequestParameterImplementation RequestParameterImplementation => new RequestParameterImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink,
			Params = Url.Params.Values.ToList(),
			HttpMethod = HttpMethods.First()
		};
		

		private List<CsharpMethod> _csharpMethods;
		public IReadOnlyCollection<CsharpMethod> CsharpMethods
		{
			get
			{
				if (_csharpMethods != null && _csharpMethods.Count > 0) return _csharpMethods;

				// enumerate once and cache
				_csharpMethods = new List<CsharpMethod>();

				var httpMethod = HttpMethods.First();
				var methodName = CsharpNames.MethodName;
				foreach (var path in Url.Paths)
				{
					var parts = new List<UrlPart>(path.Parts);

					if (Body != null)
						parts.Add(new UrlPart { Name = "body", Type = "PostData", Description = Body.Description });

					if (Url.Params == null || !Url.Params.Any()) Url.Params = new SortedDictionary<string, QueryParameters>();
					var apiMethod = new CsharpMethod
					{
						QueryStringParamName = CsharpNames.ParametersName,
						ReturnType = "TResponse",
						ReturnTypeGeneric = "<TResponse>",
						CallTypeGeneric = "TResponse",
						ReturnDescription = "",
						FullName = methodName,
						HttpMethod = httpMethod,
						DocumentationUrl = OfficialDocumentationLink,
						ObsoleteMethodVersion = null, //TODO
						Path = path.Path,
						Parts = parts,
						Url = Url,
						HasBody = Body != null
					};
					PatchMethod(apiMethod, CsharpNames.Namespace);

					var args = parts
						.Select(p => p.Argument)
						.Concat(new[] { apiMethod.QueryStringParamName + " requestParameters = null" })
						.ToList();
					apiMethod.Arguments = string.Join(", ", args);
					_csharpMethods.Add(apiMethod);

					args = args.Concat(new[] { "CancellationToken ctx = default" }).ToList();
					apiMethod = new CsharpMethod
					{
						QueryStringParamName = CsharpNames.ParametersName,
						ReturnType = "Task<TResponse>",
						ReturnTypeGeneric = "<TResponse>",
						CallTypeGeneric = "TResponse",
						ReturnDescription = "",
						FullName = methodName + "Async",
						HttpMethod = httpMethod,
						DocumentationUrl = OfficialDocumentationLink,
						ObsoleteMethodVersion = null, //TODO
						Arguments = string.Join(", ", args),
						Path = path.Path,
						Parts = parts,
						Url = Url,
						HasBody = Body != null
					};
					PatchMethod(apiMethod, CsharpNames.Namespace);
					_csharpMethods.Add(apiMethod);
				}
				return _csharpMethods;
			}
		}

		public IEnumerable<CsharpMethod> GetCsharpMethods() => CsharpMethods.ToList()
			.DistinctBy(m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
		);


		//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
		//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
		public void PatchMethod(CsharpMethod method, string @namespace)
		{
			if (method == null) return;

			Func<string, bool> ms = s => @namespace != null && @namespace.StartsWith(s);
			Func<string, bool> pc = s => method.Path.Contains(s);

			if (ms("Indices") && !pc("{index}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			if (ms("Nodes") && !pc("{node_id}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");
			
			//temporary to maintain old behavior before we introduce namespaces
			if (!string.IsNullOrWhiteSpace(@namespace) && !method.FullName.StartsWith(@namespace))
				method.FullName = @namespace + method.FullName;

			method = Overrides?.PatchMethod(method) ?? method;

			if (CodeConfiguration.ApiNameMapping.TryGetValue(Name, out var mapsApiMethodName))
				method.QueryStringParamName = mapsApiMethodName + "RequestParameters";

			method.DescriptorType = method.QueryStringParamName.Replace("RequestParameters", "Descriptor");
			method.RequestType = method.QueryStringParamName.Replace("RequestParameters", "Request");
			if (CodeConfiguration.RequestInterfaceGenericsLookup.TryGetValue("I" + method.RequestType, out var requestGeneric))
			{
				method.RequestTypeGeneric = requestGeneric;
			}
			else method.RequestTypeUnmapped = true;

			if (CodeConfiguration.DescriptorGenericsLookup.TryGetValue(method.DescriptorType, out var generic))
				method.DescriptorTypeGeneric = generic;
			else method.Unmapped = true;
		}
	}
}
