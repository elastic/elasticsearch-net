using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class EnumsGenerator : RazorGeneratorBase
	{
		public override string Title => "Elasticsearch.Net enums";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Enums.Generated.cshtml");
			var target = GeneratorLocations.LowLevel("Api", "Enums.Generated.cs");

			await DoRazor(spec, view, target);
		}
	}
}
