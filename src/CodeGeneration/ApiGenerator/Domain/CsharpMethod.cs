using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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



		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (Url.IsPartless) return setters;

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

	}
}
