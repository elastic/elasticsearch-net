using System.IO;
using System.Text.RegularExpressions;

namespace Nest.Litterateur.Documentation.Files
{
	public class ImageDocumentationFile : DocumentationFile
	{
		public ImageDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			var docFileName = this.CreateDocumentationLocation();

			// copy for asciidoc to work (path is relative to file)
			this.FileLocation.CopyTo(docFileName.FullName, true);

			// copy to the root as well, for the doc generation process (path is relative to root)
			this.FileLocation.CopyTo(Path.Combine(Program.OutputDirPath, docFileName.Name), true);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;

			var testInDocumenationFolder = Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "")
				.PascalToHyphen() + this.Extension;

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumenationFolder));

			var fileInfo = new FileInfo(documentationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
