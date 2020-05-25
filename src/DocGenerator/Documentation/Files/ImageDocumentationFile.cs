// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			var docFileName = CreateDocumentationLocation();

			// copy for asciidoc to work when viewing a single asciidoc in the browser (path is relative to file)
			var copyRelativeTask = CopyFileAsync(FileLocation.FullName, docFileName.FullName);

			// copy to the root as well, for the doc generation process (path is relative to root)
			var copyRootTask = CopyFileAsync(FileLocation.FullName, Path.Combine(Program.TmpOutputDirPath, docFileName.Name));

			await copyRelativeTask;
			await copyRootTask;
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = FileLocation.FullName;

			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumenationFolder = Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + Extension + "$)", "")
				.PascalToHyphen() + Extension;

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.TmpOutputDirPath, testInDocumenationFolder));

			var fileInfo = new FileInfo(documentationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
