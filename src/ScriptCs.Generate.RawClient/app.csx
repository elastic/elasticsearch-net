#load "api.csx"

using System.Net;
using CsQuery;
using Newtonsoft.Json;

var apiEndpoints = ApiGenerator.GetAllApiEndpoints();

ApiGenerator.GenerateClientInterface(apiEndpoints);

Console.WriteLine("Found {0} api documentation endpoints", apiEndpoints.Count());
