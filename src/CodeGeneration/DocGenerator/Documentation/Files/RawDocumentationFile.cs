using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsciiDocNet;
using DocGenerator.AsciiDoc;

namespace DocGenerator.Documentation.Files
{
	public class RawDocumentationFile : DocumentationFile
	{
		public RawDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override Task SaveToDocumentationFolderAsync()
		{
			//load the asciidoc file for processing
			var docFileName = this.CreateDocumentationLocation();
			var document = Document.Load(FileLocation.FullName);

			// make any modifications
			var rawVisitor = new RawAsciidocVisitor(FileLocation, docFileName);
			document.Accept(rawVisitor);

			// write out asciidoc to file
			using (var visitor = new AsciiDocVisitor(docFileName.FullName))
			{
				document.Accept(visitor);
			}

		    return Task.FromResult(0);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumenationFolder = Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + this.Extension + "$)", "").PascalToHyphen() + this.Extension;

			var documenationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
