#if !DOTNETCORE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsciiDoc;

namespace Nest.Litterateur.AsciiDoc
{
	/// <summary>
	/// Visits the "raw" asciidoc generated using Roslyn and adds attribute entries, section titles
	/// </summary>
	public class AddAttributeEntriesVisitor : NoopVisitor
	{
		private static readonly Dictionary<string,string> Ids = new Dictionary<string, string>();

		private readonly FileInfo _destination;
		private Document _newDocument;
		private bool _topLevel = true;

		public AddAttributeEntriesVisitor(FileInfo destination)
		{
			_destination = destination;
		}

		public Document Convert(Document document)
		{
			document.Accept(this);
			return _newDocument;
		}

		public override void Visit(Document document)
		{
			_newDocument = new Document
			{
				Title = document.Title,
				DocType = document.DocType
			};

			foreach (var authorInfo in document.Authors)
			{
				_newDocument.Authors.Add(authorInfo);
			}

			foreach (var attributeEntry in document.Attributes)
			{
				_newDocument.Attributes.Add(attributeEntry);
			}

			if (!document.Attributes.Any(a => a.Name == "ref_current"))
			{
				_newDocument.Attributes.Add(new AttributeEntry("ref_current", "https://www.elastic.co/guide/en/elasticsearch/reference/current"));
			}

			if (!document.Attributes.Any(a => a.Name == "github"))
			{
				_newDocument.Attributes.Add(new AttributeEntry("github", "https://github.com/elastic/elasticsearch-net"));
			}

			if (!document.Attributes.Any(a => a.Name == "imagesdir"))
			{
				var targetDirectory = new DirectoryInfo(Program.OutputDirPath).FullName;
				var currentDirectory = _destination.Directory.FullName;
				var difference = currentDirectory.Replace(targetDirectory, string.Empty);
				var count = difference.Count(c => c == '\\');
				var imagesDir = string.Join(string.Empty, Enumerable.Repeat("../", count));

				_newDocument.Attributes.Add(new AttributeEntry("imagesdir", $"{imagesDir}{Program.ImagesDir}"));
			}

			base.Visit(document);
		}

		public override void Visit(IList<IElement> elements)
		{
			if (_topLevel)
			{
				_topLevel = false;
				for (int index = 0; index < elements.Count; index++)
				{
					var element = elements[index];
					var source = element as Source;

					if (source != null)
					{
						// remove empty source blocks
						if (string.IsNullOrWhiteSpace(source.Text))
						{
							continue;
						}

						// if there is a section title since the last source block, don't add one
						var lastSourceBlock = _newDocument.Elements.LastOrDefault(e => e is Source);
						var lastSectionTitle = _newDocument.Elements.OfType<SectionTitle>().LastOrDefault(e => e.Level == 3);
						if (lastSourceBlock != null && lastSectionTitle != null)
						{
							var lastSectionTitleIndex = _newDocument.Elements.IndexOf(lastSectionTitle);
							var lastSourceBlockIndex = _newDocument.Elements.IndexOf(lastSourceBlock);
							if (lastSectionTitleIndex > lastSourceBlockIndex)
							{
								_newDocument.Elements.Add(element);
								continue;
							}
						}

						var method = source.Attributes.OfType<NamedAttribute>().FirstOrDefault(a => a.Name == "method");
						if (method == null)
						{
							_newDocument.Elements.Add(element);
							continue;
						}

						switch (method.Value)
						{
							case "fluent":
							case "queryfluent":
								_newDocument.Elements.Add(new SectionTitle("Fluent DSL Example", 3));
								break;
							case "initializer":
							case "queryinitializer":
								_newDocument.Elements.Add(new SectionTitle("Object Initializer Syntax Example", 3));
								break;
							case "expectresponse":
								_newDocument.Elements.Add(new SectionTitle("Handling Responses", 3));
								break;
						}

					}

					_newDocument.Elements.Add(element);
				}
			}

			base.Visit(elements);
		}

		public override void Visit(Source source)
		{
			if (source.Attributes.Count > 1 && 
				source.Attributes[1].Name == "javascript" &&
				!source.Attributes.HasTitle)
			{
				source.Attributes.Add(new Title("Example json output"));
			}

			base.Visit(source);
		}

		public override void Visit(SectionTitle sectionTitle)
		{
			if (sectionTitle.Level != 2)
			{
				return;
			}

			// Generate an anchor for all Level 2 section titles
			if (!sectionTitle.Attributes.HasAnchor)
			{
				var builder = new StringBuilder();
				using (var writer = new AsciiDocVisitor(new StringWriter(builder)))
				{
					writer.Visit(sectionTitle.Elements);
				}

				var title = builder.ToString().PascalToHyphen();
				sectionTitle.Attributes.Add(new Anchor(title));
			}

			// Check for duplicate ids across documents
			var key = sectionTitle.Attributes.Anchor.Id;
			string existingFile;
			if (Ids.TryGetValue(key, out existingFile))
			{
				throw new Exception($"duplicate id {key} in {_destination.FullName}. Id already exists in {existingFile}");
			}

			Ids.Add(key, _destination.FullName);
			base.Visit(sectionTitle);
		}
	}
}
#endif
