using System.Linq;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class RequestsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST requests";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Requests", "PlainRequestBase.cshtml");
			var target = GeneratorLocations.HighLevel("Requests.cs");
			await DoRazor(spec, view, target);

			var dependantView = ViewLocations.HighLevel("Requests", "Requests.cshtml");
			string Target(string id) => GeneratorLocations.HighLevel($"Requests.{id}.cs");
			var namespaced = spec.EndpointsPerNamespaceHighLevel.ToList();
			await DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id));
		}
	}
}
