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
			var folders = GetTestFolders(useCache);

			var yamlFiles = new ConcurrentDictionary<string, IList<YamlDefinition>>();
			using (var pbar = new ProgressBar(folders.Count, "Finding all the yaml files"))
			Parallel.ForEach(folders, (folder) =>
			{
				var definitions = GetFolderFiles(folder, useCache);
				yamlFiles.TryAdd(folder, definitions);
				pbar.Tick(string.Format("Found {0} yaml test files in {1}", definitions.Count(), folder));
			});
			return new YamlSpecification
			{
				Definitions = yamlFiles.ToDictionary(k => k.Key, v => v.Value)
			};
		}

		private static IList<YamlDefinition> GetFolderFiles(string folder, bool useCache = false)
		{
			var folderHtml = new WebClient().DownloadString(_listingUrl + "/" + folder);
			var files = (from a in CQ.Create(folderHtml)[".js-directory-link"]
				let fileName = a.InnerText
				where fileName.EndsWith(".yaml")
				select fileName).ToList();

			var definitions = new ConcurrentBag<YamlDefinition>();
			foreach (var file in files)
			{
				var yaml = GetYamlFile(folder, useCache, file);
				definitions.Add(new YamlDefinition
				{
					FileName = file,
					Contents = yaml
				});
			}
			;
			return definitions.ToList();
		}

		private static string GetYamlFile(string folder, bool useCache, string file)
		{
			var folderFile = folder + "/" + file;
			var url = useCache ? LocalUri(folderFile) : _rawUrlPrefix + folderFile;
			var yaml = new WebClient().DownloadString(url);
			if (useCache) 
				return yaml;
			if (!Directory.Exists(_cacheFolder + folder))
				Directory.CreateDirectory(_cacheFolder + folder);

			File.WriteAllText(_cacheFolder + folderFile, yaml);
			return yaml;
		}

		private static List<string> GetTestFolders(bool useCache)
		{
			var url = useCache ? LocalUri("root.html") : _listingUrl;
			var folderListingHtml = new WebClient().DownloadString(url);
			if (!useCache)
				File.WriteAllText(_cacheFolder + "root.html", folderListingHtml);
			
			var folders = (from a in CQ.Create(folderListingHtml)[".js-directory-link"]
				let folderName = a.InnerText
				where !folderName.EndsWith(".asciidoc")
				select folderName).ToList();
			return folders;
		}

		public static void GenerateProject(YamlSpecification specification)
		{
			var project = CsProjFile.LoadFrom(_testProjectFolder + @"Nest.Tests.Integration.Yaml.csproj");
			var existingYamlTests = project.All<CodeFile>().Where(c=>c.Link != null && c.Link.EndsWith(".yaml.cs"));
			foreach (var c in existingYamlTests)
				project.Remove(c);

			var definitions = specification.Definitions;

			using (var pbar = new ProgressBar(definitions.Count, "Generating Code and project for yaml tests", ConsoleColor.Blue))
			foreach (var kv in specification.Definitions)
			{
				var folder = kv.Key;
				foreach (var d in kv.Value)
				{
					var path = folder + @"\" + d.FileName + ".cs";
					if (!Directory.Exists(_testProjectFolder + folder))
						Directory.CreateDirectory(_testProjectFolder + folder);
					File.WriteAllText(_testProjectFolder + path, "");
					project.Add<CodeFile>(path);
				}
				pbar.Tick();
			}
			project.Save();
		}
		private static string LocalUri(string file)
		{
			var basePath = Path.Combine(Assembly.GetEntryAssembly().Location, @"..\" + _cacheFolder + file);
			var assemblyPath = Path.GetFullPath((new Uri(basePath)).LocalPath);
			var fileUri = new Uri(assemblyPath).AbsoluteUri;
			return fileUri;
		}
	}
}
