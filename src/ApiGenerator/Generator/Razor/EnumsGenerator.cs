// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public class EnumsGenerator : RazorGeneratorBase
	{
		public override string Title => "Elasticsearch.Net enums";

		public override async Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token)
		{
			var view = ViewLocations.LowLevel("Enums.Generated.cshtml");
			var target = GeneratorLocations.LowLevel("Api", "Enums.Generated.cs");

			await DoRazor(spec, view, target, null, token);
		}
	}
}
