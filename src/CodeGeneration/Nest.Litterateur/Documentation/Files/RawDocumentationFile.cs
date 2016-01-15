using System.IO;
using System.Text.RegularExpressions;

namespace Nest.Litterateur.Documentation.Files
{
	public class RawDocumentationFile : DocumentationFile
	{
		public RawDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			//we simply do a copy of the markdown file
			var docFileName = this.CreateDocumentationLocation();
			this.FileLocation.CopyTo(docFileName.FullName, true);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "") + this.Extension;

			var documenationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputFolder, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}