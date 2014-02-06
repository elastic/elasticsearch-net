using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace RawClientGenerator
{
	public class RawDispatchInfo
	{
		public CsharpMethod CsharpMethod { get; set; }

		public IEnumerable<string> MethodArguments
		{
			get
			{
				var methodArgs = CsharpMethod.Parts
					.Select(p => (p.Name != "body") ? "pathInfo." + p.Name.ToPascalCase() : "body")
					.Concat(new[] {"u => pathInfo.QueryString"});
				return methodArgs;
			}
		} 

		public IEnumerable<string> IfChecks
		{
			get
			{
				return this.CsharpMethod.Parts.Select(p =>
				{
					var name = (p.Name == "body") ? "body" : "pathInfo." + p.Name.ToPascalCase();
					switch (p.Type)
					{
						case "string":
						case "list":
							return "!" + name + ".IsNullOrEmpty()";
						default:
							return name + " != null";
					}
				}).ToList();
			}
		}
	}

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
			return Extensions.DistinctBy(
				this.CsharpMethods.ToList(), 
				m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
			);
		}

		public IDictionary<string, IEnumerable<RawDispatchInfo>> RawDispatches
		{
			get
			{
				return this.GetCsharpMethods()
					.GroupBy(p => p.HttpMethod)
					.ToDictionary(kv => kv.Key, kv => kv
						.DistinctBy(m => m.Path)
						.OrderByDescending(m => m.Parts.Count())
						.Select(m =>
							new RawDispatchInfo
							{
								CsharpMethod = m,
							}
						)
					);
			}
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
							.OrderBy(p => path.IndexOf("{" + p.Key, System.StringComparison.Ordinal))
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
						//.NET does not allow get requests to have a body payload.
						if (method != "GET" && this.Body != null)
						{
							parts.Add(new ApiUrlPart
							{
								Name = "body",
								Type = "object",
								Description = this.Body.Description
							});
						}
						var queryStringParamName = "FluentQueryString";
						if (this.Url.Params == null || !this.Url.Params.Any())
						{
							this.Url.Params = new Dictionary<string, ApiQueryParameters>();
						}
						queryStringParamName = this.CsharpMethodName + "QueryString";

						

						var apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "ConnectionStatus",
							FullName = methodName,
							HttpMethod = method,
							Documentation = this.Documentation,
							Path = path,
							Parts = parts,
							Url = this.Url
						};
						ApiGenerator.PatchMethod(apiMethod);

						args = args.Concat(new[] 
						{ 
							"Func<"+apiMethod.QueryStringParamName+", " + apiMethod.QueryStringParamName + "> queryString = null" 
						});
						apiMethod.Arguments = string.Join(", ", args);

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