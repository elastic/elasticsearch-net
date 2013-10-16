using System.Net;
using CsQuery;
using Newtonsoft.Json;
using Xipton.Razor;
using System.IO;

public class ApiEndpoint {
	public string Documentation { get; set; }
	public IEnumerable<string> Methods { get; set; }
	public ApiUrl Url { get; set; }
	public ApiBody Body { get; set; }
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
					var apiDocumentation = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json);
					return apiDocumentation.First();
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