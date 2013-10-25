#load "api.csx"

using System.Net;
using CsQuery;
using Newtonsoft.Json;

var spec = ApiGenerator.GetRestSpec();

ApiGenerator.GenerateClientInterface(spec);

ApiGenerator.GenerateQueryStringParameters(spec);

Console.WriteLine("Found {0} api documentation endpoints", spec.Endpoints.Count());
