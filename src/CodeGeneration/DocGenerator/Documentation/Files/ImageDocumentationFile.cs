using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocGenerator.Documentation.Files
{
	public class ImageDocumentationFile : DocumentationFile
	{
		public ImageDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override async Task SaveToDocumentationFolderAsync()
		{
			var docFileName = this.CreateDocumentationLocation();

            // copy for asciidoc to work when viewing a single asciidoc in the browser (path is relative to file)
            var copyRelativeTask = CopyFileAsync(this.FileLocation.FullName, docFileName.FullName);

            // copy to the root as well, for the doc generation process (path is relative to root)
            var copyRootTask = CopyFileAsync(this.FileLocation.FullName, Path.Combine(Program.OutputDirPath, docFileName.Name));

		    await copyRelativeTask;
		    await copyRootTask;
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;

			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumenationFolder = Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + this.Extension + "$)", "")
				.PascalToHyphen() + this.Extension;

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumenationFolder));

			var fileInfo = new FileInfo(documentationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
