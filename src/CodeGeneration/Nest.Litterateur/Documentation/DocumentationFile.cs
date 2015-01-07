using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Nest.Litterateur.Walkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Litterateur.Documentation
{
	class DocumentationFile
	{
		public string FileName { get; }
		public string Title { get; }
		public string Body { get; }
		public IList<IDocumentationBlock> Blocks { get; }

		internal DocumentationFile(SyntaxTree ast, string fileName)
		{
			this.FileName = fileName;

			var walker = new DocumentationFileWalker();
			walker.Visit(ast.GetRoot());

			this.Blocks = this.MergeAdjecentCodeBlocks(walker);
			this.Body = this.RenderBlocksToDocumentation(this.Blocks);
		}

		private string RenderBlocksToDocumentation(IList<IDocumentationBlock> blocks)
		{
			/*
---
template: layout.jade
title: Breaking Changes
menusection: concepts
menuitem: breaking-changes
---*/

			var sb = new StringBuilder();
			sb.AppendLine("---");
			sb.AppendLine("template: layout.jade");
			sb.AppendLine("title: x");
			sb.AppendLine("menusection: concepts");
			sb.AppendLine("menuitem: breaking-changes");
			sb.AppendLine("---");
			foreach (var block in blocks)
			{
				if (block is TextBlock)
				{
					sb.AppendLine(block.Value);
				}
				else if (block is CodeBlock)
				{
					sb.AppendLine("```");
					sb.AppendLine(block.Value);
					sb.AppendLine("```");
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

		public static DocumentationFile Load(string fileLocation)
		{
			var code = File.ReadAllText(fileLocation);
			var ast = CSharpSyntaxTree.ParseText(code);
			return new DocumentationFile(ast, fileLocation);

		}
	}
}
