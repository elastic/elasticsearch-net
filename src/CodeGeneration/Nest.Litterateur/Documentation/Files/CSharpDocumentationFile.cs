using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Nest.Litterateur.Documentation.Blocks;
using Nest.Litterateur.Walkers;

namespace Nest.Litterateur.Documentation.Files
{
	public class CSharpDocumentationFile : DocumentationFile
	{
		internal CSharpDocumentationFile(FileInfo fileLocation) : base(fileLocation) { }

		private string RenderBlocksToDocumentation(IEnumerable<IDocumentationBlock> blocks)
		{
			var builder = new StringBuilder();
			var lastBlockWasCodeBlock = false;

			RenderBlocksToDocumentation(blocks, builder, ref lastBlockWasCodeBlock);
			if (lastBlockWasCodeBlock) builder.AppendLine("----");

			RenderFooter(builder);

			return builder.ToString();
		}

		private void RenderFooter(StringBuilder builder)
		{
			builder.AppendLine(":ref:  http://www.elastic.co/guide/elasticsearch/reference/current");
		}

		private void RenderBlocksToDocumentation(IEnumerable<IDocumentationBlock> blocks, StringBuilder builder, ref bool lastBlockWasCodeBlock)
		{
			foreach (var block in blocks)
			{
				if (block is TextBlock)
				{
					if (lastBlockWasCodeBlock)
					{
						lastBlockWasCodeBlock = false;
						builder.AppendLine("----");
					}

					builder.AppendLine(block.Value);
				}
				else if (block is CodeBlock)
				{
					if (!lastBlockWasCodeBlock)
					{
						builder.AppendLine("[source, csharp]");
						builder.AppendLine("----");
					}
					else
					{
						builder.AppendLine();
					}

					builder.AppendLine(block.Value);
					lastBlockWasCodeBlock = true;
				}
				else if (block is CombinedBlock)
				{
					var mergedBlocks = MergeAdjacentCodeBlocks(((CombinedBlock)block).Blocks);
					RenderBlocksToDocumentation(mergedBlocks, builder, ref lastBlockWasCodeBlock);
				}
			}
		}

		private List<IDocumentationBlock> MergeAdjacentCodeBlocks(IEnumerable<IDocumentationBlock> unmergedBlocks)
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

			var mergedBlocks = MergeAdjacentCodeBlocks(blocks);
			var body = this.RenderBlocksToDocumentation(mergedBlocks);

			var docFileName = this.CreateDocumentationLocation();
			File.WriteAllText(docFileName.FullName, body);
		}
	}
}
