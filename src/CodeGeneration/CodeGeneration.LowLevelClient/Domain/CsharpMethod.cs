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

		public IEnumerable<string> RequestConstructors()
		{
			var consts = new List<string>();
			if (this.Url.Parts == null || !this.Url.Parts.Any()) return consts;
			foreach (var url in this.Url.Paths)
			{
				var m = this.RequestType;
				var cp = this.Url.Parts
					.Where(p=>!ApiUrl.BlackListRouteValues.Contains(p.Key))
					.Where(p => url.Contains($"{{{p.Value.Name}}}"))
					.OrderBy(kv => url.IndexOf($"{{{kv.Value.Name}}}", StringComparison.Ordinal));
				var par = string.Join(", ", cp.Select(p => $"{p.Value.ClrTypeName} {p.Key}"));
				var routing = string.Empty;
				if (cp.Any())
					routing = "r=>r." + string.Join(".", cp.Select(p => $"{(p.Value.Required ? "Required" : "Optional")}(\"{p.Key}\", {p.Key})"));
				consts.Add($"public {m}({par}) : base({routing}){{}}");
			}
			return consts;
		}


		public IEnumerable<ApiUrlPart> AllParts => (this.Url?.Parts?.Values ?? Enumerable.Empty<ApiUrlPart>()).Where(p=>!string.IsNullOrWhiteSpace(p.Name));
	}
}