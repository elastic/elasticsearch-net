using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
#if !DOTNETCORE
using AsciiDocNet;
using DocGenerator.AsciiDoc;
#endif

namespace DocGenerator.Documentation.Files
{
	public class RawDocumentationFile : DocumentationFile
	{
		public RawDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			//we simply do a copy of the markdown file
			var destination = this.CreateDocumentationLocation();

			var document = Document.Load(FileLocation.FullName);

			// make any modifications
			var rawVisitor = new RawAsciidocVisitor(FileLocation, destination);
			document.Accept(rawVisitor);

			// write out asciidoc to file
			using (var visitor = new AsciiDocVisitor(destination.FullName))
			{
				document.Accept(visitor);
			}
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
