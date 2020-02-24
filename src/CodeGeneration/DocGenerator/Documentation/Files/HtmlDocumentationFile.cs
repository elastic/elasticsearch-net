using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocGenerator.Documentation.Files {
	public class HtmlDocumentationFile : DocumentationFile
	{
		public HtmlDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override async Task SaveToDocumentationFolderAsync()
		{
			var docFileName = CreateDocumentationLocation();
			
			var copyRelativeTask = CopyFileAsync(FileLocation.FullName, docFileName.FullName);
			await copyRelativeTask;
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = FileLocation.FullName;

			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumenationFolder = Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + Extension + "$)", "")
				.PascalToHyphen() + Extension;

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumenationFolder));

			var fileInfo = new FileInfo(documentationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
