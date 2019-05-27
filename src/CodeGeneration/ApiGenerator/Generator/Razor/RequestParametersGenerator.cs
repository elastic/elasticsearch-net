using System.Linq;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor 
{
	public class RequestParametersGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net request parameters";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("RequestParameters", "RequestParameters.cshtml");
			string Target(string id) => GeneratorLocations.LowLevel("Api", "RequestParameters", $"RequestParameters.{id}.cs");
			
			var namespaced = spec.EndpointsPerNamespace.ToList();
			DoRazorDependantFiles(progressBar, namespaced, view, kv => kv.Key, id => Target(id));
		}
	}
}