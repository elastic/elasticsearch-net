using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Nest.Litterateur.Walkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest.Litterateur.Documentation
{
	abstract class DocumentationFile
	{
		protected readonly static string DocFolder = @"..\..\..\..\..\docs\contents\new";

		public FileInfo FileLocation { get; }

		public string Extension => this.FileLocation?.Extension?.ToLowerInvariant();

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

			var documenationTargetPath = Path.GetFullPath(Path.Combine(DocFolder, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}

	class RawDocumentationFile : DocumentationFile
	{
		public RawDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			//we simply do a copy of the markdown file
			var docFileName = this.CreateDocumentationLocation();
			this.FileLocation.CopyTo(docFileName.FullName, true);
		}

		protected override FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "") + this.Extension;

			var documenationTargetPath = Path.GetFullPath(Path.Combine(DocFolder, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}


	class CSharpDocumentationFile : DocumentationFile
	{
		internal CSharpDocumentationFile(FileInfo fileLocation)
			: base(fileLocation)
		{

		}

		private string RenderBlocksToDocumentation(IEnumerable<IDocumentationBlock> blocks, StringBuilder builder = null)
		{
			var sb = builder ?? new StringBuilder();
			foreach (var block in blocks)
			{
				if (block is TextBlock)
				{
					sb.AppendLine(block.Value);
				}
				else if (block is CodeBlock)
				{
					sb.AppendLine("[source, csharp]");
					sb.AppendLine("----");
					sb.AppendLine(block.Value);
					sb.AppendLine("----");
				}
				else if (block is CombinedBlock)
				{
					RenderBlocksToDocumentation(MergeAdjecentCodeBlocks(((CombinedBlock)block).Blocks), sb);
				}
			}
			return sb.ToString();
		}

		private List<IDocumentationBlock> MergeAdjecentCodeBlocks(IEnumerable<IDocumentationBlock> unmergedBlocks)
		{
			var blocks = new List<IDocumentationBlock>();
			List<string> collapseCodeBlocks = null;
			int lineNumber = 0;
			foreach (var b in unmergedBlocks)
			{
				//if current block is not a code block and we;ve been collapsing code blocks
				//at this point close that buffre and add a new codeblock 
				if (!(b is CodeBlock) && collapseCodeBlocks != null)
				{
					blocks.Add(new CodeBlock(string.Join("\r\n", collapseCodeBlocks), lineNumber));
					collapseCodeBlocks = null;
				}

				//if not a codeblock simply add it to the final list
				if (!(b is CodeBlock))
				{
					blocks.Add(b);
					continue;
				}
				
				//wait with adding codeblocks
				if (collapseCodeBlocks == null) collapseCodeBlocks = new List<string>();
				collapseCodeBlocks.Add(b.Value);
				lineNumber = b.LineNumber;
			}
			//make sure we flush our code buffer
			if (collapseCodeBlocks != null)
				blocks.Add(new CodeBlock(string.Join("\r\n", collapseCodeBlocks), lineNumber));
			return blocks;
		}

		public override void SaveToDocumentationFolder()
		{
			var code = File.ReadAllText(this.FileLocation.FullName);

			var ast = CSharpSyntaxTree.ParseText(code);
			var walker = new DocumentationFileWalker();
			walker.Visit(ast.GetRoot());
			var blocks = walker.Blocks.OrderBy(b => b.LineNumber).ToList();
			if (blocks.Count <= 0) return;

			var mergedBlocks = MergeAdjecentCodeBlocks(blocks);
			var body = this.RenderBlocksToDocumentation(mergedBlocks);

			var docFileName = this.CreateDocumentationLocation();
			File.WriteAllText(docFileName.FullName, body);
		}
	}
}
