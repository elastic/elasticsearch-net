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
	public class DescriptorsGenerator : RazorGeneratorBase
	{
		public override string Title => "NEST descriptors";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token)
		{
			// Delete existing files
			foreach (var file in Directory.GetFiles(GeneratorLocations.NestFolder, "Descriptors.*.cs"))
				File.Delete(file);

			var view = ViewLocations.HighLevel("Descriptors", "RequestDescriptorBase.cshtml");
			var target = GeneratorLocations.HighLevel("Descriptors.cs");
			await DoRazor(spec, view, target, null, token);

			var dependantView = ViewLocations.HighLevel("Descriptors", "Descriptors.cshtml");
			string Target(string id) => GeneratorLocations.HighLevel($"Descriptors.{id}.cs");
			var namespaced = spec.EndpointsPerNamespaceHighLevel.ToList();
			await DoRazorDependantFiles(progressBar, namespaced, dependantView, kv => kv.Key, id => Target(id), token);
		}
	}
}
