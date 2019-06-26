using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class ApiUrlsLookupsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST static url lookups";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Requests", "ApiUrlsLookup.cshtml");
			var target = GeneratorLocations.HighLevel("_Generated", "ApiUrlsLookup.generated.cs");

			await DoRazor(spec, view, target);
		}
	}
}
