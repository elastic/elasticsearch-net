using System;
using System.Collections.Generic;
using System.Linq;

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

		private bool IsPartless => this.Url.Parts == null || !this.Url.Parts.Any();

		public IEnumerable<Constructor> RequestConstructors()
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;
			var m = this.RequestType;
			foreach (var url in this.Url.Paths)
			{
				var cp = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{p.Value.ClrTypeName} {p.Key}"));
				var routing = string.Empty;

				//Routes that take {indices}/{types} and both are optional
				//we rather not generate a parameterless constructor and force folks to call Indices.All
				if (!cp.Any() && IndicesAndTypes) continue;

				if (cp.Any())
					routing = "r=>r." + string.Join(".", cp.Select(p => $"{(p.Value.Required ? "Required" : "Optional")}(\"{p.Key}\", {p.Key})"));
				var doc = $@"/// <summary>{url}</summary>";
				if (cp.Any())
				{
					doc += "\r\n" + string.Join("\t\t\r\n", cp.Select(p => $"///<param name=\"{p.Key}\">{(p.Value.Required ? "this parameter is required" : "Optional, accepts null")}</param>"));
				}
				var c = new Constructor { Generated = $"public {m}({par}) : base({routing}){{}}", Description = doc };
				ctors.Add(c);
			}
			if (IsDocumentPath && !string.IsNullOrEmpty(this.RequestTypeGeneric))
			{
				var doc = $@"/// <summary>{this.Url.Path}</summary>";
				doc += "\r\n\t\t\r\n" + $"///<param name=\"document\"> describes an elasticsearch document of type T, allows implicit conversion from numeric and string ids </param>";
				var documentRoute = "r=>r.Required(\"index\", document.Self.Index).Required(\"type\", document.Self.Type).Required(\"id\", document.Self.Id)";
				var documentFromPath = $"partial void DocumentFromPath({this.RequestTypeGeneric.Replace("<", "").Replace(">", "")} document);";
				var c = new Constructor { AdditionalCode = documentFromPath, Generated = $"public {m}(DocumentPath{this.RequestTypeGeneric} document) : base({documentRoute}){{ this.DocumentFromPath(document.Document); }}", Description = doc,  };
				ctors.Add(c);
			}
			return ctors.DistinctBy(c => c.Generated);
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
				var par = string.Join(", ", cp.Select(p => $"{p.Value.ClrTypeName} {p.Key}"));
				var routing = string.Empty;
				//Routes that take {indices}/{types} and both are optional
				//We rather not generate a parameterless constructor but leave it to our 'usercode' to determine
				//the best default constructor
				if (!cp.Any() && IndicesAndTypes) continue;
				if (cp.Any())
					routing = "r=>r." + string.Join(".", cp.Select(p => $"Required(\"{p.Key}\", {p.Key})"));
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
				doc += "\r\n\t\t\r\n" + $"///<param name=\"document\"> describes an elasticsearch document of type T, allows implicit conversion from numeric and string ids </param>";
				var documentRoute = "r=>r.Required(\"index\", document.Self.Index).Required(\"type\", document.Self.Type).Required(\"id\", document.Self.Id)";
				var documentFromPath = $"partial void DocumentFromPath({this.DescriptorTypeGeneric.Replace("<", "").Replace(">", "")} document);";
				var c = new Constructor { AdditionalCode = documentFromPath, Generated = $"public {m}(DocumentPath{this.DescriptorTypeGeneric} document) : base({documentRoute}){{}}", Description = doc };
				ctors.Add(c);
			}

			return ctors.DistinctBy(c => c.Generated);
		}

		public IEnumerable<FluentRouteSetter> GetFluentRouteSetters()
		{
			var setters = new List<FluentRouteSetter>();
			if (IsPartless) return setters;
			var parts = this.Url.Parts
				.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
				.Where(p => !p.Value.Required)
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
				// public ClearScrollDescriptor ScrollId(ScrollIds scrollId) => Assign(a=>a.RouteValues.Required("scroll_id", scrollId));
				var code = $"public {returnType} {p.InterfaceName}({p.ClrTypeName} {paramName}) => Assign(a=>a.RouteValues.Required(\"{p.Name}\", {paramName}));";
				var xmlDoc = $"///<sumary>{p.Description}</summary>";
				setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				if ((paramName == "index" || paramName == "type"))
				{
					code = $"public {returnType} {p.InterfaceName}<TOther>() where TOther : class ";
					code += $"=> Assign(a=>a.RouteValues.Required(\"{p.Name}\", ({p.ClrTypeName})typeof(TOther)));";
                    xmlDoc = $"///<sumary>{p.Description}</summary>";
					setters.Add(new FluentRouteSetter { Code = code, XmlDoc = xmlDoc });
				}
			}
			return setters;
		}

		public bool IndicesAndTypes => AllParts.Count() == 2 && AllParts.All(p => p.Type == "list") && AllParts.All(p => new[] { "index", "type"}.Contains(p.Name));
		public bool IsDocumentPath => AllParts.Count() == 3 && AllParts.All(p => p.Type != "list") && AllParts.All(p => new[] { "index", "type", "id" }.Contains(p.Name));
		public IEnumerable<ApiUrlPart> AllParts => (this.Url?.Parts?.Values ?? Enumerable.Empty<ApiUrlPart>()).Where(p => !string.IsNullOrWhiteSpace(p.Name));
	}
}