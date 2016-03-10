#if !DOTNETCORE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AsciiDoc;

namespace Nest.Litterateur.AsciiDoc
{
	public class AddAttributeEntriesVisitor : NoopVisitor
	{
		private readonly FileInfo _destination;

		public AddAttributeEntriesVisitor(FileInfo destination)
		{
			_destination = destination;
		}

		public override void Visit(Document document)
		{
			if (!document.Attributes.Any(a => a.Name == "ref_current"))
			{
				document.Attributes.Add(new AttributeEntry("ref_current", "https://www.elastic.co/guide/en/elasticsearch/reference/current/"));
			}

			if (!document.Attributes.Any(a => a.Name == "github"))
			{
				document.Attributes.Add(new AttributeEntry("github", "https://github.com/elastic/elasticsearch-net"));
			}

			if (!document.Attributes.Any(a => a.Name == "imagesdir"))
			{
				var targetDirectory = new DirectoryInfo(Program.OutputDirPath).FullName;
				var currentDirectory = _destination.Directory.FullName;
				var count = 0;

				while (currentDirectory != targetDirectory)
				{
					currentDirectory = new DirectoryInfo(currentDirectory).Parent?.FullName;
					++count;
				}

				document.Attributes.Add(new AttributeEntry(
					"imagesdir", 
					$"{string.Join(string.Empty, Enumerable.Repeat("../", count))}{Program.ImagesDir}/"));
			}

			base.Visit(document);
		}
	}
}
#endif
