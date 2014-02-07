using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeGeneration.YamlTestsRunner.Domain;
using CsQuery;
using FubuCsProjFile;
using ShellProgressBar;
using Xipton.Razor;

namespace CodeGeneration.YamlTestsRunner
{
	public static class YamlTestsGenerator
	{
		private readonly static string _listingUrl = "https://github.com/elasticsearch/elasticsearch/tree/0.90/rest-api-spec/test";
		private readonly static string _rawUrlPrefix = "https://raw.github.com/elasticsearch/elasticsearch/0.90/rest-api-spec/test/";
		private readonly static string _testProjectFolder = @"..\..\..\..\src\Nest.Tests.Integration.Yaml\";
		private readonly static string _viewFolder = @"..\..\Views\";
		private readonly static string _cacheFolder = @"..\..\YamlCache\";
		
		private static readonly RazorMachine _razorMachine;
		private static readonly Assembly _assembly;

		static YamlTestsGenerator()
		{
			_razorMachine = new RazorMachine();
			_assembly = typeof (YamlTestsGenerator).Assembly;
		}

		public static YamlSpecification GetYamlTestSpecification(bool useCache = false)
		{
			var folders = GetTestFolders();

			var yamlFiles = new ConcurrentDictionary<string, IList<YamlDefinition>>();
			using (var pbar = new ProgressBar(folders.Count, "Finding all the yaml files"))
			Parallel.ForEach(folders, (folder) =>
			{
				var definitions = GetFolderFiles(folder);
				yamlFiles.TryAdd(folder, definitions);
				pbar.Tick(string.Format("Found {0} yaml test files in {1}", definitions, folder));
			});
			return new YamlSpecification
			{
				Definitions = yamlFiles.ToDictionary(k => k.Key, v => v.Value)
			};
		}

		private static IList<YamlDefinition> GetFolderFiles(string folder)
		{
			var folderHtml = new WebClient().DownloadString(_listingUrl + "/" + folder);
			var files = (from a in CQ.Create(folderHtml)[".js-directory-link"]
				let fileName = a.InnerText
				where fileName.EndsWith(".yaml")
				select fileName).ToList();

			var definitions = new ConcurrentBag<YamlDefinition>();
			foreach (var file in files)
			{
				var yaml = new WebClient().DownloadString(_rawUrlPrefix + folder + "/" + file);
				definitions.Add(new YamlDefinition
				{
					FileName = file,
					Contents = yaml
				});
			}
			;
			return definitions.ToList();
		}

		private static List<string> GetTestFolders()
		{
			var folderListingHtml = new WebClient().DownloadString(_listingUrl);
			var folders = (from a in CQ.Create(folderListingHtml)[".js-directory-link"]
				let folderName = a.InnerText
				where !folderName.EndsWith(".asciidoc")
				select folderName).ToList();
			return folders;
		}

		public static void GenerateProject(YamlSpecification specification)
		{
			var project = CsProjFile.LoadFrom(_testProjectFolder + @"Nest.Tests.Integration.Yaml.csproj");
			var definitions = specification.Definitions;

			using (var pbar = new ProgressBar(definitions.Count, "Generating Code and project for yaml tests", ConsoleColor.Blue))
			foreach (var kv in specification.Definitions)
			{
				var folder = kv.Key;
				foreach (var d in kv.Value)
				{
					var path = folder + @"\" + d.FileName + ".cs";
					File.WriteAllText(path, "");
					project.Add<CodeFile>(path);
				}
				pbar.Tick();
			}
			project.Save();
		}
	}
}
