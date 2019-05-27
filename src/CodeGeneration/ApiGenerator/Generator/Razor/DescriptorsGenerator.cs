using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor 
{
	public class DescriptorsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST descriptors";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Descriptors", "Descriptors.cshtml");
			var target = GeneratorLocations.HighLevel("_Generated", "Descriptors.generated.cs");
			
			DoRazor(spec, view, target);
		}
	}
}