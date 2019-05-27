using System;
using System.IO;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor 
{
	public class LowLevelClientInterfaceGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net client interface";
		
		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Client", "Interface", "IElasticLowLevelClient.cshtml");
			var target = GeneratorLocations.LowLevel("IElasticLowLevelClient.Generated.cs");
			
			DoRazor(spec, view, target);
		}
	}
}