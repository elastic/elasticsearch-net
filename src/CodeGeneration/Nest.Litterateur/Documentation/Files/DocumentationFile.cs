using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Nest.Litterateur.Documentation.Files
{
	public abstract class DocumentationFile
	{
		protected FileInfo FileLocation { get; }

		protected string Extension => this.FileLocation?.Extension.ToLowerInvariant();

		protected DocumentationFile(FileInfo fileLocation)
		{
			this.FileLocation = fileLocation;
		}

		public abstract void SaveToDocumentationFolder();

		public static DocumentationFile Load(FileInfo fileLocation)
		{
			var extension = fileLocation?.Extension;
			switch (extension)
			{
				case ".cs":
					return new CSharpDocumentationFile(fileLocation);
				case ".gif":
				case ".jpg":
				case ".png":
				case ".asciidoc":
					return new RawDocumentationFile(fileLocation);
			}
			throw new ArgumentOutOfRangeException(nameof(fileLocation),
				$"The extension you specified is currently not supported: {extension}");
		}

		protected virtual FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "") + ".asciidoc";

			var documenationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputFolder, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}
}