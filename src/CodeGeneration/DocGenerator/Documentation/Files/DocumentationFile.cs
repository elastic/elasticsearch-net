using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocGenerator.Documentation.Files
{
	public abstract class DocumentationFile
	{
		protected DocumentationFile(FileInfo fileLocation) => FileLocation = fileLocation;

		protected string Extension => FileLocation?.Extension.ToLowerInvariant();
		protected FileInfo FileLocation { get; }

		public abstract Task SaveToDocumentationFolderAsync();

		public static DocumentationFile Load(FileInfo fileLocation)
		{
			if (fileLocation == null) throw new ArgumentNullException(nameof(fileLocation));

			var extension = fileLocation.Extension;
			switch (extension)
			{
				case ".gif":
				case ".jpg":
				case ".jpeg":
				case ".png":
					return new ImageDocumentationFile(fileLocation);
				case ".asciidoc":
					return new AsciiDocDocumentationFile(fileLocation);
				case ".html":
					return new HtmlDocumentationFile(fileLocation);
				default:
					throw new ArgumentOutOfRangeException(nameof(fileLocation),
						$"The extension you specified is currently not supported: {extension}");
			}
		}

		protected virtual FileInfo CreateDocumentationLocation()
		{
			var testFullPath = FileLocation.FullName;
			var p = "\\" + Path.DirectorySeparatorChar.ToString();
			var testInDocumentationFolder =
				Regex.Replace(testFullPath, $@"(^.+{p}Tests{p}|\" + Extension + "$)", "")
					.TrimEnd(".doc")
					.TrimEnd("Tests")
					.PascalToHyphen() + ".asciidoc";

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.OutputDirPath, testInDocumentationFolder));
			var fileInfo = new FileInfo(documentationTargetPath);

			if (fileInfo.Directory != null)
				Directory.CreateDirectory(fileInfo.Directory.FullName);

			return fileInfo;
		}

		protected async Task CopyFileAsync(string sourceFile, string destinationFile)
		{
			using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
				FileOptions.Asynchronous | FileOptions.SequentialScan))
			using (var destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None, 4096,
				FileOptions.Asynchronous | FileOptions.SequentialScan))
				await sourceStream.CopyToAsync(destinationStream);
		}
	}
}
