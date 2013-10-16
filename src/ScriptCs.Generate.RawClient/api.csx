using System.Net;
using CsQuery;
using Newtonsoft.Json;
using Xipton.Razor;
using System.IO;
using System.Globalization;

public class ApiEndpoint {
	public string CsharpMethodName { get; set; }
	public string Documentation { get; set; }
	public IEnumerable<string> Methods { get; set; }
	public ApiUrl Url { get; set; }
	public ApiBody Body { get; set; }
	public string PascalCase(string s) {
		return ApiGenerator.PascalCase(s);
	}
	public IEnumerable<CsharpMethod> CsharpMethods {
		get
		{
			foreach(var method in this.Methods) 
			{
				var methodName = this.CsharpMethodName + this.PascalCase(method);
				foreach (var path in this.Url.Paths) 
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
							case "enum":
								return this.CsharpMethodName + "Options " + p.Name;
							default:
								return "string " + p.Name;

						}
					});
					if (this.Body != null)
					{
						args = args.Concat(new [] { "object body" });
						parts.Add(new ApiUrlPart {
							Name = "body",
							Description = this.Body.Description
						});
					}
					args = args.Concat(new [] { "NameValueCollection queryString = null" });

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
					yield return apiMethod;
					apiMethod.FullName += "Async";
					apiMethod.ReturnType = "Task<ConnectionStatus>";
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
	private readonly static RazorMachine _razorMachine = new RazorMachine();

	static ApiGenerator() {
	}
	public static string PascalCase(string s)
	{
		var textInfo = new CultureInfo("en-US").TextInfo;
		return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
	}
	public static IDictionary<string, ApiEndpoint> GetAllApiEndpoints() 
	{
		Console.WriteLine("Getting a listing of all the api endpoints from the elasticsearch-rest-api-spec repos");
		var dom = CQ.CreateFromUrl(_listingUrl);
		var endpoints = dom[".js-directory-link"]
			.Select(s=>s.InnerText)
			.Where(s=>!string.IsNullOrEmpty(s) && s.EndsWith(".json"))
			.Take(3)
			.Select(s=>{
				using (var client = new WebClient())
				{
					var rawFile = _rawUrlPrefix + s;
					Console.WriteLine("Downloading {0}", rawFile);
					var json = client.DownloadString(rawFile);
					var apiDocumentation = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
					apiDocumentation.Value.CsharpMethodName = PascalCase(apiDocumentation.Key);
					return apiDocumentation;
				}
			})
			.ToDictionary(d=>d.Key, d=>d.Value);

		return endpoints;
	}

	public static void GenerateClientInterface(IDictionary<string, ApiEndpoint> model)
	{
		Console.WriteLine(_razorMachine.Execute(File.ReadAllText(@"Views\IRawElasticClient.cshtml"), model));
	}

}