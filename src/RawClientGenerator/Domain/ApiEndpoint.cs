using System.Collections.Generic;
using System.Linq;

namespace RawClientGenerator
{
	public class ApiEndpoint
	{
		public string CsharpMethodName { get; set; }
		public string Documentation { get; set; }
		public IEnumerable<string> Methods { get; set; }
		public ApiUrl Url { get; set; }
		public ApiBody Body { get; set; }
		public string PascalCase(string s)
		{
			return ApiGenerator.PascalCase(s);
		}

		public IEnumerable<CsharpMethod> GetCsharpMethods()
		{
			//we distinct by here to catch aliased endpoints like:
			//  /_cluster/nodes/hotthreads and /_nodes/hotthreads
			return Extensions.DistinctBy(this.CsharpMethods.ToList(), m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments);
		}



		public IEnumerable<CsharpMethod> CsharpMethods
		{
			get
			{
				foreach (var method in this.Methods)
				{
					var methodName = this.CsharpMethodName + this.PascalCase(method);
					//the distinctby here catches aliases routes i.e
					//  /_cluster/nodes/{node_id}/hotthreads vs  /_cluster/nodes/{node_id}/hot_threads
					foreach (var path in Extensions.DistinctBy(this.Url.Paths, p => p.Replace("_", "")))
					{

						var parts = this.Url.Parts
							.Where(p => path.Contains("{" + p.Key + "}"))
							.Select(p =>
							{
								p.Value.Name = p.Key;
								return p.Value;
							})
							.ToList();
						var args = parts.Select(p =>
						{
							switch (p.Type)
							{
								case "int":
								case "string":
									return p.Type + " " + p.Name;
								case "list":
									return "string " + p.Name;
								case "enum":
									return this.PascalCase(p.Name) + "Options " + p.Name;
								default:
									return p.Type + " " + p.Name;
									//return "string " + p.Name;

							}
						});
						if (this.Body != null)
						{
							//args = args.Concat(new [] { "object body" });
							parts.Add(new ApiUrlPart
							{
								Name = "body",
								Type = "object",
								Description = this.Body.Description
							});
						}
						var queryStringParamName = "FluentQueryString";
						if (this.Url.Params != null && this.Url.Params.Any())
							queryStringParamName = methodName + "QueryString";

						args = args.Concat(new[] 
						{ 
							"Func<"+queryStringParamName+", FluentQueryString> queryString = null" 
						});

						var apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "ConnectionStatus",
							FullName = methodName,
							HttpMethod = method,
							Documentation = this.Documentation,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = this.Url
						};
						ApiGenerator.PatchMethod(apiMethod);
						yield return apiMethod;
						apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "Task<ConnectionStatus>",
							FullName = methodName + "Async",
							HttpMethod = method,
							Documentation = this.Documentation,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = this.Url
						};
						ApiGenerator.PatchMethod(apiMethod);
						yield return apiMethod;
					}
				}
			}
		}
	}
}