/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
					return new RawDocumentationFile(fileLocation);
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

			var documentationTargetPath = Path.GetFullPath(Path.Combine(Program.TmpOutputDirPath, testInDocumentationFolder));
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
				await sourceStream.CopyToAsync(destinationStream).ConfigureAwait(false);
		}
	}
}
