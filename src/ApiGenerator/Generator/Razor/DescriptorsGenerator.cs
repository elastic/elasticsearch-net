using System.Linq;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class DescriptorsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST descriptors";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Descriptors", "RequestDescriptorBase.cshtml");
			var target = GeneratorLocations.HighLevel("Descriptors.cs");
			await DoRazor(spec, view, target);

			var dependantView = ViewLocations.HighLevel("Descriptors", "Descriptors.cshtml");
			string Target(string id) => GeneratorLocations.HighLevel($"Descriptors.{id}.cs");
			var namespaced = spec.EndpointsPerNamespaceHighLevel.ToList();
			await DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id));
		}
	}
}
