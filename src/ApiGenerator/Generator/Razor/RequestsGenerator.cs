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
	public class RequestsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST requests";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token)
		{
			// Delete existing files
			foreach (var file in Directory.GetFiles(GeneratorLocations.NestFolder, "Requests.*.cs"))
				File.Delete(file);

			var view = ViewLocations.HighLevel("Requests", "PlainRequestBase.cshtml");
			var target = GeneratorLocations.HighLevel("Requests.cs");
			await DoRazor(spec, view, target, null, token);

			var dependantView = ViewLocations.HighLevel("Requests", "Requests.cshtml");
			string Target(string id) => GeneratorLocations.HighLevel($"Requests.{id}.cs");
			var namespaced = spec.EndpointsPerNamespaceHighLevel.ToList();
			await DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id), token);
		}
	}
}
