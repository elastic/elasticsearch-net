#load "api.csx"

using System.Net;
using CsQuery;
using Newtonsoft.Json;

var url = "https://github.com/elasticsearch/elasticsearch-rest-api-spec/tree/master/api";
Console.WriteLine("Getting a listing of all the api endpoints from the elasticsearch-rest-api-spec repos");

var dom = CQ.CreateFromUrl(url);

var files = dom[".js-directory-link"].Select(s=>s.InnerText).Where(s=>!string.IsNullOrEmpty(s) && s.EndsWith(".json"));
Console.WriteLine("Found {0} api documentation files", files.Count());



var rawUrlPrefix = "https://raw.github.com/elasticsearch/elasticsearch-rest-api-spec/master/api/";
foreach (var file in files) {
	var rawFile = rawUrlPrefix + file;
	Console.WriteLine("Downloading {0}", rawFile);
	using (var client = new WebClient())
	{
    	var content = client.DownloadString(rawFile);
	}
}