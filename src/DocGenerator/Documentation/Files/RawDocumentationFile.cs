// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			var docFileName = CreateDocumentationLocation();
			var document = Document.Load(FileLocation.FullName);

			// make any modifications
			var rawVisitor = new RawAsciidocVisitor(FileLocation, docFileName);
			document.Accept(rawVisitor);

			// write out asciidoc to file
			using (var visitor = new AsciiDocVisitor(docFileName.FullName)) document.Accept(visitor);

			return Task.FromResult(0);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = FileLocation.FullName;
			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumentationFolder = Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + Extension + "$)", "").PascalToHyphen() + Extension;

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.TmpOutputDirPath, testInDocumentationFolder));
			var fileInfo = new FileInfo(documentationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}
