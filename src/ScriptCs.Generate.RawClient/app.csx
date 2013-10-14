#load "api.csx"

using System.Net;
using CsQuery;
using Newtonsoft.Json;

var apiEndpoints = ApiGenerator.GetAllApiEndpoints();

Console.WriteLine("Found {0} api documentation endpoints", apiEndpoints.Count());
