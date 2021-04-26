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
