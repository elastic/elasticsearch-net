using System;
using System.IO;
using System.Linq;
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
	public class LowLevelClientImplementationGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net client implementation";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Client", "Implementation", "ElasticLowLevelClient.cshtml");
			var target = GeneratorLocations.LowLevel($"ElasticLowLevelClient.{CsharpNames.RootNamespace}.cs");
			DoRazor(spec, view, target);
			
			var namespaced = spec.EndpointsPerNamespace.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			var namespacedView = ViewLocations.LowLevel("Client, Implementation", "ElasticLowLevelClient.Namespace.cshtml");
			DoRazorDependantFiles(progressBar, namespaced, namespacedView, kv => kv.Key, id => GeneratorLocations.LowLevel($"ElasticLowLevelClient.{id}.cs"));
		}
	}
	
	public class RequestParametersGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net request parameters";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("RequestParameters", "RequestParameters.cshtml");
			string Target(string id) => GeneratorLocations.LowLevel("Api", "RequestParameters", $"ElasticLowLevelClient.{id}.cs");
			
			var namespaced = spec.EndpointsPerNamespace.ToList();
			DoRazorDependantFiles(progressBar, namespaced, view, kv => kv.Key, id => Target(id));
		}
	}
	public class EnumsGenerator : RazorGeneratorBase
	{
		public override string Title => "Elasticsearch.Net enums";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.LowLevel("Enums.Generated.cshtml");
			var target = GeneratorLocations.LowLevel("Api", "Enums.Generated.cs");
			
			DoRazor(spec, view, target);
		}
	}
	public class HighLevelClientInterfaceGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST client interface";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Client", "Interface", "IElasticClient.cshtml");
			var target = GeneratorLocations.HighLevel("IElasticClient.Generated.cs");
			
			DoRazor(spec, view, target);
			
		}
	}
	public class HighLevelClientImplementationGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST client implementation";

		public override void Generate(RestApiSpec spec, ProgressBar progressBar)
		{
			var view = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.cshtml");
			var target = GeneratorLocations.HighLevel($"ElasticClient.{CsharpNames.RootNamespace}.cs");
			DoRazor(spec, view, target);
			
			string Target(string id) => GeneratorLocations.HighLevel($"ElasticClient.{id}.cs");
			
			var namespaced = spec.EndpointsPerNamespace.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			var dependantView = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.Namespace.cshtml");
			DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id));
			
		}
	}
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