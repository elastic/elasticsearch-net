using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class HighLevelClientInterfaceGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST client interface";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Client", "Interface", "IElasticClient.cshtml");
			var target = GeneratorLocations.HighLevel("IElasticClient.Generated.cs");

			await DoRazor(spec, view, target);
		}
	}
}
