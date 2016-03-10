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
	public class AddAttributeEntriesVisitor : NoopVisitor
	{
		private readonly FileInfo _destination;

		private static readonly Dictionary<string,string> Ids = new Dictionary<string, string>();

		public AddAttributeEntriesVisitor(FileInfo destination)
		{
			_destination = destination;
		}

		public override void Visit(Document document)
		{
			if (!document.Attributes.Any(a => a.Name == "ref_current"))
			{
				document.Attributes.Add(new AttributeEntry("ref_current", "https://www.elastic.co/guide/en/elasticsearch/reference/current"));
			}

			if (!document.Attributes.Any(a => a.Name == "github"))
			{
				document.Attributes.Add(new AttributeEntry("github", "https://github.com/elastic/elasticsearch-net"));
			}

			if (!document.Attributes.Any(a => a.Name == "imagesdir"))
			{
				var targetDirectory = new DirectoryInfo(Program.OutputDirPath).FullName;
				var currentDirectory = _destination.Directory.FullName;
				var difference = currentDirectory.Replace(targetDirectory, string.Empty);
				var count = difference.Count(c => c == '\\');
				var imagesDir = string.Join(string.Empty, Enumerable.Repeat("../", count));

				document.Attributes.Add(new AttributeEntry("imagesdir", $"{imagesDir}{Program.ImagesDir}"));
			}

			base.Visit(document);
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

			// Generate an anchor for all section titles
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

			// check for duplicate ids
			var key = sectionTitle.Attributes.Anchor.Id;
			string existingFile;
			if (Ids.TryGetValue(key, out existingFile))
			{
				throw new Exception($"duplicate id {key} in {_destination.FullName}. Id already exists in {existingFile}");
			}
			else
			{
				Ids.Add(key, _destination.FullName);
			}

			base.Visit(sectionTitle);
		}
	}
}
#endif
