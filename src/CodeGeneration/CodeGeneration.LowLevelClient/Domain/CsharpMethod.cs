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
			foreach (var url in this.Url.Paths)
			{
				var m = this.RequestType;
				var cp = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{p.Value.ClrTypeName} {p.Key}"));
				var routing = string.Empty;
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
			return ctors.DistinctBy(c => c.Generated);
		}

		public IEnumerable<Constructor> DescriptorConstructors()
		{
			var ctors = new List<Constructor>();
			if (IsPartless) return ctors;
			foreach (var url in this.Url.Paths)
			{
				var m = this.DescriptorType;
				var cp = this.Url.Parts
					.Where(p => !ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => p.Value.Required)
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{p.Value.ClrTypeName} {p.Key}"));
				var routing = string.Empty;
				if (cp.Any())
					routing = "r=>r." + string.Join(".", cp.Select(p => $"Required(\"{p.Key}\", {p.Key})"));
				var doc = $@"/// <summary>{url}</summary>";
				if (cp.Any())
				{
					doc += "\r\n" + string.Join("\t\t\r\n", cp.Select(p => $"///<param name=\"{p.Key}\"> this parameter is required"));
				}
				var c = new Constructor { Generated = $"public {m}({par}) : base({routing}){{}}", Description = doc };
				ctors.Add(c);
			}

			return ctors.DistinctBy(c => c.Generated);
		}

		
		public IEnumerable<ApiUrlPart> AllParts => (this.Url?.Parts?.Values ?? Enumerable.Empty<ApiUrlPart>()).Where(p => !string.IsNullOrWhiteSpace(p.Name));
	}
}