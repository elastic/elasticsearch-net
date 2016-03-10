using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Nest.Litterateur.Documentation.Blocks;
using Nest.Litterateur.Walkers;

#if !DOTNETCORE
using AsciiDoc;
using Nest.Litterateur.AsciiDoc;
#endif

namespace Nest.Litterateur.Documentation.Files
{
	public class CSharpDocumentationFile : DocumentationFile
	{
		private readonly Dictionary<string, decimal> _sections;

		internal CSharpDocumentationFile(FileInfo fileLocation, Dictionary<string, decimal> sections) : base(fileLocation)
		{
			_sections = sections;
		}

		private string RenderBlocksToDocumentation(IEnumerable<IDocumentationBlock> blocks)
		{
			var builder = new StringBuilder();
			var lastBlockWasCodeBlock = false;
			RenderBlocksToDocumentation(blocks, builder, ref lastBlockWasCodeBlock);
			if (lastBlockWasCodeBlock) builder.AppendLine("----");
			return builder.ToString();
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
					var codeBlock = (CodeBlock)block;

					if (!lastBlockWasCodeBlock)
					{
						builder.AppendLine($"[source, {codeBlock.Language}]");
						if (codeBlock.Language == "javascript")
						{
							builder.AppendLine(".Example json output");
						}

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
			string language = null;

			foreach (var b in unmergedBlocks)
			{
				//if current block is not a code block and we've been collapsing code blocks
				//at this point close that buffer and add a new codeblock 
				if (!(b is CodeBlock) && collapseCodeBlocks != null)
				{
					blocks.Add(new CodeBlock(string.Join(Environment.NewLine, collapseCodeBlocks), lineNumber, language));
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
				var codeBlock = (CodeBlock)b;

				if (language != null && codeBlock.Language != language)
				{
					blocks.Add(codeBlock);
					continue;
				}

				language = codeBlock.Language;
				collapseCodeBlocks.Add(codeBlock.Value);
				lineNumber = codeBlock.LineNumber;
			}
			//make sure we flush our code buffer
			if (collapseCodeBlocks != null)
				blocks.Add(new CodeBlock(string.Join(Environment.NewLine, collapseCodeBlocks), lineNumber, language));
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

#if !DOTNETCORE
			// tidy up
			var document = Document.Parse(body);

			// get the section numbers
			var sectionAttributeEntry = document.Attributes.SingleOrDefault(a => a.Name == "section-number");
			decimal sectionValue;
			if (sectionAttributeEntry != null && decimal.TryParse(sectionAttributeEntry.Value, out sectionValue))
			{
				_sections.Add(docFileName.Name, sectionValue);
			}

			// add attributes and write to destination
			using (var file = new StreamWriter(docFileName.FullName))
			{
				document.Accept(new AddAttributeEntriesVisitor(docFileName));
				document.Accept(new AsciiDocVisitor(file));
			}
#else
			File.WriteAllText(docFileName.FullName, body);
#endif
		}
	}
}
