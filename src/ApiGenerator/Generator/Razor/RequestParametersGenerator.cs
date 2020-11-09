// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class RequestParametersGenerator : RazorGeneratorBase
	{
		public override string Title { get; } = "Elasticsearch.Net request parameters";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token)
		{
			// Delete existing files
			foreach (var file in Directory.GetFiles(GeneratorLocations.EsNetFolder, "RequestParameters.*.cs"))
				File.Delete(file);

			var view = ViewLocations.LowLevel("RequestParameters", "RequestParameters.cshtml");
			string Target(string id) => GeneratorLocations.LowLevel("Api", "RequestParameters", $"RequestParameters.{id}.cs");

			var namespaced = spec.EndpointsPerNamespaceLowLevel.ToList();
			await DoRazorDependantFiles(progressBar, namespaced, view, kv => kv.Key, id => Target(id), token);
		}
	}
}
