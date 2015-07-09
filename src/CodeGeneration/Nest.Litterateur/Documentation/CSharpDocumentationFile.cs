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
				case ".asciidoc":
					return new AsciiDocumentationFile(fileLocation);
			}
			throw new ArgumentOutOfRangeException(nameof(fileLocation),
				$"We currently only support cs and md files, you provided: {extension}");
		}

		protected FileInfo CreateDocumentationLocation()
		{
			var testFullPath = this.FileLocation.FullName;
			var testInDocumenationFolder = 
				Regex.Replace(testFullPath, @"(^.+\\Tests\\|\" + this.Extension + "$)", "") + ".asciidoc";

			var documenationTargetPath = Path.GetFullPath(Path.Combine(DocFolder, testInDocumenationFolder));
			var fileInfo = new FileInfo(documenationTargetPath);
			Directory.CreateDirectory(fileInfo.Directory.FullName);
			return fileInfo;
		}
	}

	class AsciiDocumentationFile : DocumentationFile
	{
		public AsciiDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		public override void SaveToDocumentationFolder()
		{
			//we simply do a copy of the markdown file
			var docFileName = this.CreateDocumentationLocation();
			this.FileLocation.CopyTo(docFileName.FullName, true);
		}
	}


	class CSharpDocumentationFile : DocumentationFile
	{
		internal CSharpDocumentationFile(FileInfo fileLocation)
			: base(fileLocation)
		{

		}

		private string RenderBlocksToDocumentation(IEnumerable<IDocumentationBlock> blocks)
		{
			var sb = new StringBuilder();
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
			}
			return sb.ToString();
		}

		private List<IDocumentationBlock> MergeAdjecentCodeBlocks(DocumentationFileWalker walker)
		{
			var blocks = new List<IDocumentationBlock>();
			List<string> collapseCodeBlocks = null;
			foreach (var b in walker.Blocks)
			{
				if (b is TextBlock && collapseCodeBlocks != null)
				{
					blocks.Add(new CodeBlock(string.Join("\r\n", collapseCodeBlocks)));
					collapseCodeBlocks = null;
				}
				if (b is TextBlock)
					blocks.Add(b);
				else if (b is CodeBlock)
				{
					if (collapseCodeBlocks == null) collapseCodeBlocks = new List<string>();
					collapseCodeBlocks.Add(b.Value);
				}
			}
			if (collapseCodeBlocks != null)
				blocks.Add(new CodeBlock(string.Join("\r\n", collapseCodeBlocks)));
			return blocks;
		}

		public override void SaveToDocumentationFolder()
		{
			var code = File.ReadAllText(this.FileLocation.FullName);

			var ast = CSharpSyntaxTree.ParseText(code);
			var walker = new DocumentationFileWalker();
			walker.Visit(ast.GetRoot());

			var blocks = this.MergeAdjecentCodeBlocks(walker);
			var body = this.RenderBlocksToDocumentation(blocks);

			var docFileName = this.CreateDocumentationLocation();
			File.WriteAllText(docFileName.FullName, body);
		}
	}
}
