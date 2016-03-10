using System.IO;

namespace Nest.Litterateur.Documentation.Files
{
	public class ImageDocumentationFile : RawDocumentationFile
	{
		public ImageDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

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