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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AsciiDocNet;

namespace DocGenerator.AsciiDoc
{
	/// <summary>
	/// Visits raw asciidoc files (i.e. not generated) to make modifications
	/// </summary>
	public class RawAsciidocVisitor : NoopVisitor
	{
		private readonly FileInfo _destination;
		private readonly FileInfo _source;
		private Document _document;

		public RawAsciidocVisitor(FileInfo source, FileInfo destination)
		{
			_source = source;
			_destination = destination;
		}

		public override void VisitDocument(Document document)
		{
			_document = document;

			var directoryAttribute = document.Attributes.FirstOrDefault(a => a.Name == "docdir");
			if (directoryAttribute != null) document.Attributes.Remove(directoryAttribute);

			var github = "https://github.com/elastic/elasticsearch-net";
			var originalFile = Regex.Replace(_source.FullName.Replace("\\", "/"),
				@"^(.*Tests/)", $"{github}/tree/{Program.BranchName}/src/Tests/Tests/");
			var eol = Environment.NewLine;
			document.Insert(0, new Comment
			{
				Style = CommentStyle.MultiLine,
				Text = $"IMPORTANT NOTE{eol}=============={eol}This file has been generated from {originalFile}. {eol}" +
					$"If you wish to submit a PR for any spelling mistakes, typos or grammatical errors for this file,{eol}" +
					"please modify the original csharp file found at the link and submit the PR with that change. Thanks!"
			});

			base.VisitDocument(document);
		}

		public override void VisitAttributeEntry(AttributeEntry attributeEntry)
		{
			if (attributeEntry.Name == "includes-from-dirs")
			{
				var thisFileUri = new Uri(_destination.FullName);
				var directories = attributeEntry.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				var counter = 1;

				foreach (var file in DirectoryFiles(directories))
				{
					var fileInfo = new FileInfo(file);
					var referencedFileUri = new Uri(fileInfo.FullName);
					var relativePath = thisFileUri.MakeRelativeUri(referencedFileUri);
					var include = new Include(relativePath.OriginalString);

					if (attributeEntry.Parent != null)
					{
						attributeEntry.Parent.Insert(attributeEntry.Parent.IndexOf(attributeEntry) + counter, include);
						++counter;
					}
					else
						_document.Add(include);
				}
			}
			else if (attributeEntry.Name == "anchor-list")
			{
				var directories = attributeEntry.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				var list = new UnorderedList();

				foreach (var file in DirectoryFiles(directories))
				{
					var fileInfo = new FileInfo(file);
					var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);

					list.Items.Add(new UnorderedListItem
					{
						new Paragraph(new InternalAnchor(fileNameWithoutExtension, fileNameWithoutExtension.LowercaseHyphenToPascal()))
					});
				}

				if (attributeEntry.Parent != null)
					attributeEntry.Parent.Insert(attributeEntry.Parent.IndexOf(attributeEntry) + 1, list);
				else
					_document.Add(list);
			}

			base.VisitAttributeEntry(attributeEntry);
		}

		private static IEnumerable<string> DirectoryFiles(string[] directories) =>
			directories
				.SelectMany(directory =>
					Directory.EnumerateFiles(Path.Combine(Program.TmpOutputDirPath, directory), "*.asciidoc", SearchOption.AllDirectories))
				.OrderBy(f => f, StringComparer.Ordinal);
	}
}
