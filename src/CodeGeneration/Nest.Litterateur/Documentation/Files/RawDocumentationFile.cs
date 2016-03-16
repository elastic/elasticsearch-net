using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
#if !DOTNETCORE
using AsciiDoc;
#endif

namespace Nest.Litterateur.Documentation.Files
{
	public class RawDocumentationFile : DocumentationFile
	{
		public RawDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			//we simply do a copy of the markdown file
			var docFileName = this.CreateDocumentationLocation();

#if !DOTNETCORE
			var document = Document.Load(FileLocation.FullName);

			// check if this document has generated includes to other files
			var includeAttribute = document.Attributes.FirstOrDefault(a => a.Name == "includes-from-dirs");

			if (includeAttribute == null)
			{
				this.FileLocation.CopyTo(docFileName.FullName, true);
				return;
			}

			var thisFileUri = new Uri(docFileName.FullName);
			var directories = includeAttribute.Value.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var directory in directories)
			{
				foreach (var file in Directory.EnumerateFiles(Path.Combine(Program.OutputDirPath, directory), "*.asciidoc", SearchOption.AllDirectories))
				{
					var fileInfo = new FileInfo(file);
					var referencedFileUri = new Uri(fileInfo.FullName);
					var relativePath = thisFileUri.MakeRelativeUri(referencedFileUri);
					var include = new Include(relativePath.OriginalString);

					document.Elements.Add(include);
				}
			}

			using (var visitor = new AsciiDocVisitor(docFileName.FullName))
			{
				document.Accept(visitor);
			}
#else
			this.FileLocation.CopyTo(docFileName.FullName, true);
#endif
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "").PascalToHyphen() + this.Extension;

			var documenationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}