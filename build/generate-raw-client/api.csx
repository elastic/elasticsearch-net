using System.Net;
using CsQuery;
using Newtonsoft.Json;
using Xipton.Razor;
using System.IO;
using System.Globalization;


public class RestApiSpec {
	public string Commit { get; set; }
	public IDictionary<string, ApiEndpoint> Endpoints { get; set; }

	public IList<ApiQueryParameters> ApiQueryParameters { get; set; }
}

public class ApiEndpoint {
	public string CsharpMethodName { get; set; }
	public string Documentation { get; set; }
	public IEnumerable<string> Methods { get; set; }
	public ApiUrl Url { get; set; }
	public ApiBody Body { get; set; }
	public string PascalCase(string s) {
		return ApiGenerator.PascalCase(s);
	}

	public IEnumerable<CsharpMethod> GetCsharpMethods() 
	{
		//we distinct by here to catch aliased endpoints like:
		//  /_cluster/nodes/hotthreads and /_nodes/hotthreads
		return Extensions.DistinctBy(this.CsharpMethods.ToList(), m=> m.ReturnType + "--" + m.FullName + "--" + m.Arguments);
	}

	public IDictionary<string>

	public IEnumerable<CsharpMethod> CsharpMethods {
		get
		{
			foreach(var method in this.Methods) 
			{
				var methodName = this.CsharpMethodName + this.PascalCase(method);
				//the distinctby here catches aliases routes i.e
				//  /_cluster/nodes/{node_id}/hotthreads vs  /_cluster/nodes/{node_id}/hot_threads
				foreach (var path in Extensions.DistinctBy(this.Url.Paths, p=>p.Replace("_", "")))
				{

					var parts = this.Url.Parts
						.Where(p=>path.Contains("{" + p.Key + "}"))
						.Select(p=> {
							p.Value.Name = p.Key;
							return p.Value;
						})
						.ToList();
					var args = parts.Select(p=>
					{
						switch(p.Type)
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
						parts.Add(new ApiUrlPart {
							Name = "body",
							Type = "object",
							Description = this.Body.Description
						});
					}
					var queryStringParamName = "FluentQueryString";
					if (this.Url.Params != null && this.Url.Params.Any())
						queryStringParamName = methodName + "QueryString";

					args = args.Concat(new [] 
					{ 
						"Func<"+queryStringParamName+", NameValueCollection> queryString = null" 
					});

					var apiMethod = new CsharpMethod 
					{
						ReturnType = "ConnectionStatus",
						FullName = methodName,
						HttpMethod = method,
						Documentation = this.Documentation,
						Arguments = string.Join(", ", args),
						Path = path,
						Parts = parts
					};
					ApiGenerator.PatchMethod(apiMethod);
					yield return apiMethod;
					apiMethod = new CsharpMethod 
					{
						ReturnType = "Task<ConnectionStatus>",
						FullName = methodName + "Async",
						HttpMethod = method,
						Documentation = this.Documentation,
						Arguments = string.Join(", ", args),
						Path = path,
						Parts = parts
					};
					ApiGenerator.PatchMethod(apiMethod);
					yield return apiMethod;
				}
			}
		}
	}
}
public class CsharpMethod {
	public string ReturnType { get; set; }
	public string FullName { get; set; }
	public string HttpMethod { get; set; }
	public string Documentation { get; set; }
	public string Path { get; set; }
	public string Arguments { get; set; }
	public IEnumerable<ApiUrlPart> Parts { get; set; }
	public ApiUrl Url { get; set; }
}
public class ApiBody {
	public string Description { get; set; }
}
public class ApiUrl { 
	public string Path { get; set; }
	public IEnumerable<string> Paths { get; set; }
	public IDictionary<string, ApiUrlPart> Parts { get; set; }
	public IDictionary<string, ApiQueryParameters> Params { get; set; }
}

public class ApiUrlPart {
	public string Name { get; set; }
	public string Type { get; set; }
	public string Description { get; set; }
	public IEnumerable<string> Options { get; set; }
}
public class ApiQueryParameters {
	public string Type { get; set; }
	public string Desciption { get; set; }
	public IEnumerable<string> Options { get; set; }
}

public static class ApiGenerator 
{
	private readonly static string _listingUrl = "https://github.com/elasticsearch/elasticsearch-rest-api-spec/tree/master/api";
	private readonly static string _rawUrlPrefix = "https://raw.github.com/elasticsearch/elasticsearch-rest-api-spec/master/api/";
	private readonly static string _nestFolder = @"..\..\src\Nest\";
	private readonly static RazorMachine _razorMachine = new RazorMachine();	


	static ApiGenerator() {
	}
	public static string PascalCase(string s)
	{
		var textInfo = new CultureInfo("en-US").TextInfo;
		return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
	}
	public static RestApiSpec GetRestSpec() 
	{
		Console.WriteLine("Getting a listing of all the api endpoints from the elasticsearch-rest-api-spec repos");
		var dom = CQ.CreateFromUrl(_listingUrl);
		var endpoints = dom[".js-directory-link"]
			.Select(s=>s.InnerText)
			.Where(s=>!string.IsNullOrEmpty(s) && s.EndsWith(".json"))
			.Select(s=>{
				using (var client = new WebClient())
				{
					var rawFile = _rawUrlPrefix + s;
					Console.WriteLine("Downloading {0}", rawFile);
					var json = client.DownloadString(rawFile);
					var apiDocumentation = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
					apiDocumentation.Value.CsharpMethodName = CreateMethodName(
						apiDocumentation.Key, 
						apiDocumentation.Value
					);
					return apiDocumentation;
				}
			})
			.ToDictionary(d=>d.Key, d=>d.Value);

		var restSpec = new RestApiSpec {
			Endpoints = endpoints,
			Commit = dom[".sha:first"].Text()
		};


		return restSpec;
	}

	//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
	//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
	public static void PatchMethod(CsharpMethod method) 
	{
		if (method.FullName.StartsWith("IndicesStats") && method.Path.Contains("{index}"))
			method.FullName = method.FullName.Replace("IndicesStats", "IndexStats");
		//IndicesPutAliasPutAsync
		if (method.FullName.StartsWith("IndicesPutAlias") && method.Path.Contains("{index}"))
			method.FullName = method.FullName.Replace("IndicesPutAlias", "IndexPutAlias");

		if (method.FullName.StartsWith("IndicesStats") || method.FullName.StartsWith("IndexStats"))
		{
			if (method.Path.Contains("/indexing/"))
				method.FullName = method.FullName.Replace("Stats", "IndexingStats");
			if (method.Path.Contains("/search/"))
				method.FullName = method.FullName.Replace("Stats", "SearchStats");
			if (method.Path.Contains("/fielddata/"))
				method.FullName = method.FullName.Replace("Stats", "FieldDataStats");
		}
	}

	public static string CreateMethodName(string apiEnpointKey, ApiEndpoint endpoint) 
	{
		return PascalCase(apiEnpointKey);
	}

	public static void GenerateClientInterface(RestApiSpec model)
	{
		var targetFile =_nestFolder + @"IRawElasticClient.cs";
		var source = _razorMachine.Execute(File.ReadAllText(@"Views\IRawElasticClient.cshtml"), model).ToString();
		File.WriteAllText(targetFile, source);
	}

	public static void GenerateQueryStringParameters(RestApiSpec model)
	{
		var targetFile = _nestFolder + @"QueryStringParameters\GeneratedQueryStringParameters.cs";
		var source = _razorMachine.Execute(File.ReadAllText(@"Views\GeneratedQueryStringParameters.cshtml"), model).ToString();
		File.WriteAllText(targetFile, source);
	}
}


//extensions methods dont work because scriptcs wraps everything
//in its own class
public static class Extensions {
	public static IEnumerable<T> DistinctBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> property)
	{
    	return items.GroupBy(property).Select(x => x.First());
	}
}