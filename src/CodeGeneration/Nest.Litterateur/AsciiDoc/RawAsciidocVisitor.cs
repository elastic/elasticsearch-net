#if !DOTNETCORE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AsciiDocNet;

namespace Nest.Litterateur.AsciiDoc
{
	/// <summary>
	/// Visits raw asciidoc files (i.e. not generated) to make modifications
	/// </summary>
	public class RawAsciidocVisitor : NoopVisitor
	{
		private readonly FileInfo _destination;

		private static readonly Dictionary<string, string> IncludeDirectories = new Dictionary<string, string>
		{
			{ "aggregations.asciidoc", "aggregations-usage.asciidoc" },
			{ "query-dsl.asciidoc", "query-dsl-usage.asciidoc" }
		};

		public RawAsciidocVisitor(FileInfo destination)
		{
			_destination = destination;
		}

		public override void Visit(Document document)
		{
			var directoryAttribute = document.Attributes.FirstOrDefault(a => a.Name == "docdir");
			if (directoryAttribute != null)
			{
				document.Attributes.Remove(directoryAttribute);
			}

			// check if this document has generated includes to other files
			var includeAttribute = document.Attributes.FirstOrDefault(a => a.Name == "includes-from-dirs");

			if (includeAttribute != null)
			{
				var thisFileUri = new Uri(_destination.FullName);
				var directories = includeAttribute.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var directory in directories)
				{
					foreach (var file in Directory.EnumerateFiles(Path.Combine(Program.OutputDirPath, directory), "*.asciidoc", SearchOption.AllDirectories))
					{
						var fileInfo = new FileInfo(file);
						var referencedFileUri = new Uri(fileInfo.FullName);
						var relativePath = thisFileUri.MakeRelativeUri(referencedFileUri);
						var include = new Include(relativePath.OriginalString);

						document.Add(include);
					}
				}
			}

			base.Visit(document);
		}

		public override void Visit(Open open)
		{
			// include links to all the query dsl usage and aggregation usage pages on the landing query dsl and aggregations pages, respectively.
			string usageFilePath;
			if (IncludeDirectories.TryGetValue(_destination.Name, out usageFilePath))
			{
				var usageDoc = Document.Load(Path.Combine(Program.OutputDirPath, usageFilePath));

				var includeAttribute = usageDoc.Attributes.FirstOrDefault(a => a.Name == "includes-from-dirs");

				if (includeAttribute != null)
				{
					var directories = includeAttribute.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

					var list = new UnorderedList();

					foreach (var directory in directories)
					{
						foreach (var file in Directory.EnumerateFiles(Path.Combine(Program.OutputDirPath, directory), "*usage.asciidoc", SearchOption.AllDirectories))
						{
							var fileInfo = new FileInfo(file);
							var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);

							list.Items.Add(new UnorderedListItem
							{
								new Paragraph(new InternalAnchor(fileNameWithoutExtension, fileNameWithoutExtension.LowercaseHyphenToPascal()))
							});
						}
					}

					open.Add(list);
				}
			}

			base.Visit(open);
		}
	}
}
#endif
