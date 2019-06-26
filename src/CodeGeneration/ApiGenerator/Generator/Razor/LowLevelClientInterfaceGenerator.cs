using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class LowLevelClientInterfaceGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net client interface";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Client", "Interface", "IElasticLowLevelClient.cshtml");
			var target = GeneratorLocations.LowLevel("IElasticLowLevelClient.Generated.cs");

			await DoRazor(spec, view, target);
		}
	}
}
