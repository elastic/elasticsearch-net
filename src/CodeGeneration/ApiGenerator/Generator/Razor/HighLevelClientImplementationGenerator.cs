using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Code;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor 
{
	public class HighLevelClientImplementationGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST client implementation";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.cshtml");
			var target = GeneratorLocations.HighLevel($"ElasticClient.{CsharpNames.RootNamespace}.cs");
			DoRazor(spec, view, target);
			
			string Target(string id) => GeneratorLocations.HighLevel($"ElasticClient.{id}.cs");
			
			var namespaced = spec.EndpointsPerNamespace.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			var dependantView = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.Namespace.cshtml");
			DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id));
			
		}
	}
}