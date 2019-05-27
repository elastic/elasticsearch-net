using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Code;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor {
	public class LowLevelClientImplementationGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net client implementation";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Client", "Implementation", "ElasticLowLevelClient.cshtml");
			var target = GeneratorLocations.LowLevel($"ElasticLowLevelClient.{CsharpNames.RootNamespace}.cs");
			DoRazor(spec, view, target);
			
			var namespaced = spec.EndpointsPerNamespace.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			var namespacedView = ViewLocations.LowLevel("Client", "Implementation", "ElasticLowLevelClient.Namespace.cshtml");
			DoRazorDependantFiles(progressBar, namespaced, namespacedView, kv => kv.Key, id => GeneratorLocations.LowLevel($"ElasticLowLevelClient.{id}.cs"));
		}
	}
}