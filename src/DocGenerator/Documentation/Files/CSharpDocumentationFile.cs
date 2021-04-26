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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsciiDocNet;
using DocGenerator.AsciiDoc;
using DocGenerator.Walkers;
using Microsoft.CodeAnalysis;
using Document = Microsoft.CodeAnalysis.Document;

namespace DocGenerator.Documentation.Files
{
	public class CSharpDocumentationFile : DocumentationFile
	{
		private readonly Document _document;
		private readonly Dictionary<string, Project> _projects;

		public CSharpDocumentationFile(Document document, Dictionary<string, Project> projects)
			: base(new FileInfo(document.FilePath))
		{
			_document = document;
			_projects = projects;
		}

		public override async Task SaveToDocumentationFolderAsync()
		{
			var converter = new DocConverter();
			var blocks = await converter.ConvertAsync(_document).ConfigureAwait(false);

			if (!blocks.Any()) return;

			var builder = new StringBuilder();
			using (var writer = new StringWriter(builder))
				foreach (var block in blocks)
					await writer.WriteLineAsync(block.ToAsciiDoc()).ConfigureAwait(false);

			var destination = CreateDocumentationLocation();

			// Now add Asciidoc headers, rearrange sections, etc.
			var document = AsciiDocNet.Document.Parse(builder.ToString());
			var visitor = new GeneratedAsciidocVisitor(FileLocation, destination, _projects);
			document = visitor.Convert(document);

			// Write out document to file
			using (var writer = new StreamWriter(destination.FullName)) document.Accept(new AsciiDocVisitor(writer));
		}
	}
}
