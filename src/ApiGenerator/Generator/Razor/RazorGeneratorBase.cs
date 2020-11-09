// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RazorLight;
using RazorLight.Generation;
using RazorLight.Razor;
using ShellProgressBar;

namespace ApiGenerator.Generator.Razor
{
	public abstract class RazorGeneratorBase
	{
		private static readonly RazorLightEngine Engine = new RazorLightEngineBuilder()
			.UseProject(new FileSystemRazorProject(Path.GetFullPath(ViewLocations.Root)))
			.UseMemoryCachingProvider()
			.Build();

		protected async Task DoRazor<TModel>(TModel model, string viewLocation, string targetLocation, string cacheNameSuffix, CancellationToken token)
		{
			try
			{
				var name = GetType().Name + cacheNameSuffix;
				var sourceFileContents = await File.ReadAllTextAsync(viewLocation, token);
				token.ThrowIfCancellationRequested();
				var generated = await Engine.CompileRenderStringAsync(name, sourceFileContents,  model);
				WriteFormattedCsharpFile(targetLocation, generated);
			}
			catch (TemplateGenerationException e)
			{
				foreach (var d in e.Diagnostics) Console.WriteLine(d.GetMessage());
				throw;
			}
		}

		protected async Task DoRazorDependantFiles<TModel>(
			ProgressBar pbar, IReadOnlyCollection<TModel> items, string viewLocation,
			Func<TModel, string> identifier, Func<string, string> target,
			CancellationToken token
			)
		{
			using (var c = pbar.Spawn(items.Count, "Generating namespaces", new ProgressBarOptions
			{
				ProgressCharacter = '─',
				ForegroundColor = ConsoleColor.Yellow
			}))
			{
				foreach (var item in items)
				{
					var id = identifier(item);
					var targetLocation = target(id);
					await DoRazor(item, viewLocation, targetLocation, id, token);
					c.Tick($"{Title}: {id}");
				}
			}
		}

		protected static void WriteFormattedCsharpFile(string path, string contents)
		{
			var tree = CSharpSyntaxTree.ParseText(contents);
			var root = tree.GetRoot().NormalizeWhitespace(indentation:"\t", "\n");
			contents = root.ToFullString();
			File.WriteAllText(path, contents);
		}

		public abstract string Title { get; }
		public abstract Task Generate(RestApiSpec spec, ProgressBar progressBar, CancellationToken token);
	}
}
