// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using AsciiDocNet;
using DocGenerator.XmlDocs;
using Elastic.Transport;
using Elasticsearch.Net;
using Microsoft.CodeAnalysis;
using Nest;
using NuDoq;
using Container = AsciiDocNet.Container;
using Document = AsciiDocNet.Document;
using Exception = System.Exception;

namespace DocGenerator.AsciiDoc
{
	/// <summary>
	/// Visits the "raw" asciidoc generated using Roslyn and adds attribute entries,
	/// section titles, rearranges sections, etc.
	/// </summary>
	public class GeneratedAsciidocVisitor : NoopVisitor
	{
		private static readonly Dictionary<string, string> Ids = new Dictionary<string, string>();
		private readonly FileInfo _destination;
		private readonly Dictionary<string, Project> _projects;

		private readonly FileInfo _source;
		private Document _document;
		private Document _newDocument;
		private bool _topLevel = true;
		private int _topSectionTitleLevel;

		public GeneratedAsciidocVisitor(FileInfo source, FileInfo destination, Dictionary<string, Project> projects)
		{
			_source = source;
			_destination = destination;
			_projects = projects;
		}

		public Document Convert(Document document)
		{
			_document = document;
			document.Accept(this);
			return _newDocument;
		}

		public override void VisitDocument(Document document)
		{
			_newDocument = new Document
			{
				Title = document.Title,
				DocType = document.DocType
			};

			foreach (var authorInfo in document.Authors) _newDocument.Authors.Add(authorInfo);

			RemoveDocDirectoryAttribute(_newDocument);
			RemoveDocDirectoryAttribute(document);

			foreach (var attributeEntry in document.Attributes) _newDocument.Attributes.Add(attributeEntry);

			if (document.Attributes.All(a => a.Name != "ref_current"))
				_newDocument.Attributes.Add(new AttributeEntry("ref_current",
					$"https://www.elastic.co/guide/en/elasticsearch/reference/{Program.DocVersion}"));

			var github = "https://github.com/elastic/elasticsearch-net";
			if (document.Attributes.All(a => a.Name != "github")) _newDocument.Attributes.Add(new AttributeEntry("github", github));

			if (document.Attributes.All(a => a.Name != "nuget"))
				_newDocument.Attributes.Add(new AttributeEntry("nuget", "https://www.nuget.org/packages"));

			var originalFile = Regex.Replace(_source.FullName.Replace("\\", "/"), @"^(.*Tests/)",
				$"{github}/tree/{Program.BranchName}/src/Tests/Tests/");

			var eol = Environment.NewLine;
			_newDocument.Insert(0, new Comment
			{
				Style = CommentStyle.MultiLine,
				Text = $"IMPORTANT NOTE{eol}=============={eol}This file has been generated from {originalFile}. {eol}" +
					$"If you wish to submit a PR for any spelling mistakes, typos or grammatical errors for this file,{eol}" +
					"please modify the original csharp file found at the link and submit the PR with that change. Thanks!"
			});

			_topSectionTitleLevel = _source.Directory.Name.Equals("request", StringComparison.OrdinalIgnoreCase) &&
				_source.Directory.Parent != null &&
				_source.Directory.Parent.Name.Equals("search", StringComparison.OrdinalIgnoreCase)
					? 2
					: 3;

			// see if the document has some kind of top level title and add one with an anchor if not.
			// Used to add titles to *Usage test files
			if (document.Title == null && document.Count > 0)
			{
				var sectionTitle = document[0] as SectionTitle;

				// capture existing top level
				if (sectionTitle != null && sectionTitle.Level <= 3)
					_topSectionTitleLevel = sectionTitle.Level;

				if (sectionTitle == null || sectionTitle.Level > 3)
				{
					var id = Path.GetFileNameWithoutExtension(_destination.Name);
					var title = id.LowercaseHyphenToPascal();
					sectionTitle = new SectionTitle(title, _topSectionTitleLevel);
					sectionTitle.Attributes.Add(new Anchor(id));
					_newDocument.Add(sectionTitle);
				}
			}

			base.VisitDocument(document);
		}

		public override void VisitContainer(Container elements)
		{
			if (_topLevel)
			{
				_topLevel = false;
				for (var index = 0; index < elements.Count; index++)
				{
					var element = elements[index];
					var source = element as Source;

					if (source != null)
					{
						// remove empty source blocks
						if (string.IsNullOrWhiteSpace(source.Text)) continue;

						var method = source.Attributes.OfType<NamedAttribute>().FirstOrDefault(a => a.Name == "method");
						if (method == null)
						{
							_newDocument.Add(source);
							continue;
						}

						// if there is a section title since the last source block, don't add one
						var lastSourceBlock = _newDocument.LastOrDefault(e => e is Source);
						var lastSectionTitle = _newDocument.OfType<SectionTitle>().LastOrDefault(e => e.Level == _topSectionTitleLevel + 1);
						if (lastSourceBlock != null && lastSectionTitle != null)
						{
							var lastSectionTitleIndex = _newDocument.IndexOf(lastSectionTitle);
							var lastSourceBlockIndex = _newDocument.IndexOf(lastSourceBlock);
							if (lastSectionTitleIndex > lastSourceBlockIndex)
							{
								_newDocument.Add(source);
								continue;
							}
						}

						switch (method.Value)
						{
							case "fluent":
							case "queryfluent":
							case "fluentaggs":
								if (!LastSectionTitleMatches(text => text.StartsWith("Fluent DSL", StringComparison.OrdinalIgnoreCase)))
									_newDocument.Add(CreateSubsectionTitle("Fluent DSL example"));

								_newDocument.Add(source);
								break;
							case "initializer":
							case "queryinitializer":
							case "initializeraggs":
								_newDocument.Add(CreateSubsectionTitle("Object Initializer syntax example"));
								_newDocument.Add(source);
								break;
							case "expectresponse":
								// Don't add the Handlng Response section title if it was the last title (it might be defined in the doc already)
								if (!LastSectionTitleMatches(text => text.Equals("Handling responses", StringComparison.OrdinalIgnoreCase)))
									_newDocument.Add(CreateSubsectionTitle("Handling Responses"));
								_newDocument.Add(source);
								break;
							default:
								_newDocument.Add(source);
								break;
						}
					}
					else
						_newDocument.Add(element);
				}
			}

			base.VisitContainer(elements);
		}

		public override void VisitSource(Source source)
		{
			// remove method attributes as the elastic doc generation doesn't like them; it
			// expects a linenumbering in the index 2 position of a source block
			var methodAttribute = source.Attributes.FirstOrDefault(a => a.Name == "method");
			if (methodAttribute != null) source.Attributes.Remove(methodAttribute);

			// Replace tabs with spaces and remove C# comment escaping from callouts
			// (elastic docs generation does not like this callout format)
			source.Text = Regex.Replace(source.Text.Replace("\t", "    "), @"//[ \t]*\<(\d+)\>[^\r\n]*", "<$1>");

			base.VisitSource(source);
		}

		public override void VisitSectionTitle(SectionTitle sectionTitle)
		{
			// Generate an anchor for all top level section titles
			if (_document.IndexOf(sectionTitle) == 0 && !sectionTitle.Attributes.HasAnchor)
			{
				var builder = new StringBuilder();
				using (var writer = new AsciiDocVisitor(new StringWriter(builder))) writer.VisitInlineContainer(sectionTitle);

				var title = builder.ToString().PascalToHyphen();
				sectionTitle.Attributes.Add(new Anchor(title));
			}

			if (sectionTitle.Attributes.HasAnchor)
			{
				// Check for duplicate ids across documents
				var key = sectionTitle.Attributes.Anchor.Id;
				if (Ids.TryGetValue(key, out var existingFile))
					throw new Exception($"duplicate id {key} in {_destination.FullName}. Id already exists in {existingFile}");

				Ids.Add(key, _destination.FullName);
			}

			base.VisitSectionTitle(sectionTitle);
		}

		public override void VisitAttributeEntry(AttributeEntry attributeEntry)
		{
			if (attributeEntry.Name != "xml-docs") return;

			var configuration = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent?.Name;

			string XmlFile(string project)
			{
				if (configuration == null)
					return Path.Combine(Program.InputDirPath, project, "netstandard2.0", $"{project}.xml");

				return Path.Combine(Program.InputDirPath, project, "bin", configuration, "netstandard2.0",
					$"{project}.xml");
			}

			var value = attributeEntry.Value;

			if (string.IsNullOrEmpty(value))
			{
				base.VisitAttributeEntry(attributeEntry);
				return;
			}

			var parts = value.Split(':');
			var assemblyName = parts[0];
			var typeName = parts[1];

			string xmlDocsFile;
			Assembly assembly;
			string assemblyNamespace;

			//TODO: tidy this up
			switch (assemblyName.ToLowerInvariant())
			{
				case "elasticsearch.net":
					xmlDocsFile = Path.GetFullPath(XmlFile("Elasticsearch.Net"));
					assembly = typeof(ElasticLowLevelClient).Assembly;
					assemblyNamespace = typeof(ElasticLowLevelClient).Namespace;
					break;
				case "elastic.transport":
					xmlDocsFile = Path.GetFullPath(XmlFile("Elastic.Transport"));
					assembly = typeof(CloudConnectionPool).Assembly;
					assemblyNamespace = typeof(CloudConnectionPool).Namespace;
					break;
				default:
					xmlDocsFile = Path.GetFullPath(XmlFile("Nest"));
					assembly = typeof(ElasticClient).Assembly;
					assemblyNamespace = typeof(ElasticClient).Namespace;
					break;
			}

			// build xml documentation file on the fly if it doesn't exist
			if (!File.Exists(xmlDocsFile))
			{
				Console.WriteLine($"Can not find {xmlDocsFile} attempting to build");
				var project = _projects[assemblyName];

				var compilation = project.GetCompilationAsync().Result;

				using (var peStream = new MemoryStream())
				using (var commentStream = File.Create(xmlDocsFile))
				{
					var emitResult = compilation.Emit(peStream, null, commentStream);

					if (!emitResult.Success)
					{
						var failures = emitResult.Diagnostics.Where(diagnostic =>
							diagnostic.IsWarningAsError ||
							diagnostic.Severity == DiagnosticSeverity.Error);

						var builder = new StringBuilder($"Unable to emit compilation for: {assemblyName}");
						foreach (var diagnostic in failures.Take(50)) builder.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");

						builder.AppendLine(new string('-', 30));

						throw new Exception(builder.ToString());
					}
				}
			}

			var assemblyMembers = DocReader.Read(assembly, xmlDocsFile);
			var type = assembly.GetType(assemblyNamespace + "." + typeName);
			var visitor = new XmlDocsVisitor(type);

			visitor.VisitAssembly(assemblyMembers);
			if (visitor.LabeledListItems.Any())
			{
				var labeledList = new LabeledList();
				foreach (var item in visitor.LabeledListItems
					.OrderBy(l => l.Label)
					.GroupBy(l => l.Label)
					.Select(x => x.First()))
					labeledList.Items.Add(item);

				_newDocument.Insert(_newDocument.IndexOf(attributeEntry), labeledList);
			}
		}

		private void RemoveDocDirectoryAttribute(Document document)
		{
			var directoryAttribute = document.Attributes.FirstOrDefault(a => a.Name == "docdir");
			if (directoryAttribute != null) document.Attributes.Remove(directoryAttribute);
		}

		private bool LastSectionTitleMatches(Func<string, bool> predicate)
		{
			var lastSectionTitle = _newDocument.OfType<SectionTitle>().LastOrDefault(e => e.Level == _topSectionTitleLevel + 1);
			if (lastSectionTitle != null && lastSectionTitle.Level == _topSectionTitleLevel + 1)
			{
				var builder = new StringBuilder();
				using (var visitor = new AsciiDocVisitor(new StringWriter(builder))) visitor.VisitInlineContainer(lastSectionTitle);

				return predicate(builder.ToString());
			}

			return false;
		}

		private SectionTitle CreateSubsectionTitle(string title)
		{
			var level = _topSectionTitleLevel + 1;
			var sectionTitle = new SectionTitle(title, level);

			// levels 1-3 need to be floating so the Elasticsearch docs generation does not
			// split into separate file
			if (level < 4)
				sectionTitle.IsFloating = true;

			return sectionTitle;
		}
	}
}
