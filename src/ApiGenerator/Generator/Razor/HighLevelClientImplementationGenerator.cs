// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Code;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class HighLevelClientImplementationGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST client implementation";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token)
		{
			// Delete existing files
			foreach (var file in Directory.GetFiles(GeneratorLocations.NestFolder, "ElasticClient.*.cs"))
				File.Delete(file);

			var view = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.cshtml");
			var target = GeneratorLocations.HighLevel($"ElasticClient.{CsharpNames.RootNamespace}.cs");
			await DoRazor(spec, view, target, null, token);

			string Target(string id) => GeneratorLocations.HighLevel($"ElasticClient.{id}.cs");

			var namespaced = spec.EndpointsPerNamespaceHighLevel.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			var dependantView = ViewLocations.HighLevel("Client", "Implementation", "ElasticClient.Namespace.cshtml");
			await DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id), token);

		}
	}
}
