using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor 
{
	public class RequestsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST requests";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Requests", "Requests.cshtml");
			var target = GeneratorLocations.HighLevel("_Generated", "Requests.generated.cs");
			
			DoRazor(spec, view, target);
		}
	}
}