#if !DOTNETCORE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsciiDocNet;

namespace Nest.Litterateur.AsciiDoc
{
	/// <summary>
	/// Visits the "raw" asciidoc generated using Roslyn and adds attribute entries,
	/// section titles, rearranges sections, etc.
	/// </summary>
	public class GeneratedAsciidocVisitor : NoopVisitor
	{
		private static readonly Dictionary<string,string> Ids = new Dictionary<string, string>();

		private readonly FileInfo _destination;
		private Document _newDocument;
		private bool _topLevel = true;

		public GeneratedAsciidocVisitor(FileInfo destination)
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

			RemoveDocDirectoryAttribute(_newDocument);
			RemoveDocDirectoryAttribute(document);

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

			if (!document.Attributes.Any(a => a.Name == "nuget"))
			{
				_newDocument.Attributes.Add(new AttributeEntry("nuget", "https://www.nuget.org/packages"));
			}

			// see if the document has some kind of top level title and add one with an anchor if not.
			if (document.Title == null && document.Count > 0)
			{
				var sectionTitle = document[0] as SectionTitle;

				if (sectionTitle == null || sectionTitle.Level != 2)
				{
					var id = Path.GetFileNameWithoutExtension(_destination.Name);
					var title = id.LowercaseHyphenToPascal();
					sectionTitle = new SectionTitle(title, 2);
					sectionTitle.Attributes.Add(new Anchor(id));

					_newDocument.Add(sectionTitle);
				}
			}

			base.Visit(document);
		}

		public override void Visit(Container elements)
		{
			if (_topLevel)
			{
				_topLevel = false;
				Source exampleJson = null;
				Source objectInitializerExample = null;

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

						var method = source.Attributes.OfType<NamedAttribute>().FirstOrDefault(a => a.Name == "method");
						if (method == null)
						{
							_newDocument.Add(element);
							continue;
						}

						if ((method.Value == "expectjson" || method.Value == "queryjson") &&
							source.Attributes.Count > 1 &&
							source.Attributes[1].Name == "javascript")
						{
							exampleJson = source;
							continue;
						}

						// if there is a section title since the last source block, don't add one
						var lastSourceBlock = _newDocument.LastOrDefault(e => e is Source);
						var lastSectionTitle = _newDocument.OfType<SectionTitle>().LastOrDefault(e => e.Level == 3);
						if (lastSourceBlock != null && lastSectionTitle != null)
						{
							var lastSectionTitleIndex = _newDocument.IndexOf(lastSectionTitle);
							var lastSourceBlockIndex = _newDocument.IndexOf(lastSourceBlock);
							if (lastSectionTitleIndex > lastSourceBlockIndex)
							{
								_newDocument.Add(element);
								continue;
							}
						}

						switch (method.Value)
						{
							case "fluent":
							case "queryfluent":
								if (!LastSectionTitleMatches(text => text.StartsWith("Fluent DSL", StringComparison.OrdinalIgnoreCase)))
								{
									_newDocument.Add(new SectionTitle("Fluent DSL Example", 3));
								}

								_newDocument.Add(element);

								if (objectInitializerExample != null)
								{
									_newDocument.Add(new SectionTitle("Object Initializer Syntax Example", 3));
									_newDocument.Add(objectInitializerExample);
									objectInitializerExample = null;

									if (exampleJson != null)
									{
										_newDocument.Add(exampleJson);
										exampleJson = null;
									}
								}
								break;
							case "initializer":
								_newDocument.Add(new SectionTitle("Object Initializer Syntax Example", 3));
								_newDocument.Add(element);
								// Move the example json to after the initializer example
								if (exampleJson != null)
								{
									_newDocument.Add(exampleJson);
									exampleJson = null;
								}
								break;
							case "queryinitializer":
								if (objectInitializerExample != null)
								{
									_newDocument.Add(new SectionTitle("Object Initializer Syntax Example", 3));
									_newDocument.Add(objectInitializerExample);

									// Move the example json to after the initializer example
									if (exampleJson != null)
									{
										_newDocument.Add(exampleJson);
										exampleJson = null;
									}
								}
								else
								{
									objectInitializerExample = source;
								}
								break;
							case "expectresponse":
								// Don't add the Handlng Response section title if it was the last title (it might be defined in the doc already)
								if (!LastSectionTitleMatches(text => text.Equals("Handling Responses", StringComparison.OrdinalIgnoreCase)))
								{
									_newDocument.Add(new SectionTitle("Handling Responses", 3));
								}

								_newDocument.Add(element);
								break;
							default:
								_newDocument.Add(element);
								break;
						}
					}
					else
					{
						_newDocument.Add(element);
					}
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

			// remove method attributes as the elastic doc generation doesn't like them; it
			// expects a linenumbering in the index 2 position of a source block
			var methodAttribute = source.Attributes.FirstOrDefault(a => a.Name == "method");
			if (methodAttribute != null)
			{
				source.Attributes.Remove(methodAttribute);
			}

			// Replace tabs with spaces and remove C# comment escaping from callouts
			// (elastic docs generation does not like this callout format)
			source.Text = Regex.Replace(source.Text.Replace("\t", "    "), @"//[ \t]*\<(\d+)\>.*", "<$1>");

			base.Visit(source);
		}

		public override void Visit(SectionTitle sectionTitle)
		{
			if (sectionTitle.Level != 2)
			{
				base.Visit(sectionTitle);
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

		private void RemoveDocDirectoryAttribute(Document document)
		{
			var directoryAttribute = document.Attributes.FirstOrDefault(a => a.Name == "docdir");
			if (directoryAttribute != null)
			{
				document.Attributes.Remove(directoryAttribute);
			}
		}

		private bool LastSectionTitleMatches(Func<string, bool> predicate)
		{
			var lastSectionTitle = _newDocument.OfType<SectionTitle>().LastOrDefault(e => e.Level == 3);
			if (lastSectionTitle != null && lastSectionTitle.Level == 3)
			{
				var builder = new StringBuilder();
				using (var visitor = new AsciiDocVisitor(new StringWriter(builder)))
				{
					visitor.Visit(lastSectionTitle.Elements);
				}

				return predicate(builder.ToString());
			}

			return false;
		}
	}
}
#endif
