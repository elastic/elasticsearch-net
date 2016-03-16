using System.IO;

namespace Nest.Litterateur.Documentation.Files
{
	public class ImageDocumentationFile : DocumentationFile
	{
		public ImageDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			var docFileName = this.CreateDocumentationLocation();
			this.FileLocation.CopyTo(docFileName.FullName, true);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = Path.GetFileName(testFullPath).PascalToHyphen();

			var documenationTargetPath = Path.GetFullPath(Path.Combine(Program.ImagesDirPath, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}