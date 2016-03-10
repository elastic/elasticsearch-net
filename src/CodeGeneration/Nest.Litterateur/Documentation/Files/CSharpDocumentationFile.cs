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
			var callouts = new List<string>();
			Language? language = null;
			RenderBlocksToDocumentation(blocks, builder, ref lastBlockWasCodeBlock, ref callouts, ref language);
			if (lastBlockWasCodeBlock)
			{
				builder.AppendLine("----");
				foreach (var callout in callouts)
				{
					builder.AppendLine(callout);
				}
			}
			return builder.ToString();
		}

		private void RenderBlocksToDocumentation(
			IEnumerable<IDocumentationBlock> blocks, 
			StringBuilder builder, 
			ref bool lastBlockWasCodeBlock, 
			ref List<string> callouts, 
			ref Language? language)
		{
			foreach (var block in blocks)
			{
				if (block is TextBlock)
				{
					if (lastBlockWasCodeBlock)
					{
						lastBlockWasCodeBlock = false;
						builder.AppendLine("----");
						if (callouts.Any())
						{
							foreach (var callout in callouts)
							{
								builder.AppendLine(callout);
							}
							builder.AppendLine();
							callouts = new List<string>();
						}
					}

					builder.AppendLine(block.Value);
				}
				else if (block is CodeBlock)
				{
					var codeBlock = (CodeBlock)block;

					// don't write different language code blocks in the same delimited source block
					if (lastBlockWasCodeBlock && codeBlock.Language != language)
					{
						lastBlockWasCodeBlock = false;
						builder.AppendLine("----");
						if (callouts.Any())
						{
							foreach (var callout in callouts)
							{
								builder.AppendLine(callout);
							}
							builder.AppendLine();
							callouts = new List<string>();
						}
					}

					if (!lastBlockWasCodeBlock)
					{
						builder.AppendLine($"[source, {codeBlock.Language.ToString().ToLowerInvariant()}]");
						builder.AppendLine("----");
					}
					else
					{
						builder.AppendLine();
					}

					builder.AppendLine(codeBlock.Value);

					// add call outs here to write out when closing the block
					callouts.AddRange(codeBlock.CallOuts);
					lastBlockWasCodeBlock = true;
					language = codeBlock.Language;
				}
				else if (block is CombinedBlock)
				{
					var mergedBlocks = MergeAdjacentCodeBlocks(((CombinedBlock)block).Blocks);
					RenderBlocksToDocumentation(mergedBlocks, builder, ref lastBlockWasCodeBlock, ref callouts, ref language);
				}
			}
		}

		private List<IDocumentationBlock> MergeAdjacentCodeBlocks(IEnumerable<IDocumentationBlock> unmergedBlocks)
		{
			var blocks = new List<IDocumentationBlock>();
			List<string> collapseCodeBlocks = null;
			List<string> collapseCallouts = null;
			int lineNumber = 0;
			Language? language = null;

			foreach (var b in unmergedBlocks)
			{
				//if current block is not a code block and we've been collapsing code blocks
				//at this point close that buffer and add a new codeblock 
				if (!(b is CodeBlock) && collapseCodeBlocks != null)
				{
					var block = new CodeBlock(string.Join(Environment.NewLine, collapseCodeBlocks), lineNumber, language.Value);
					block.CallOuts.AddRange(collapseCallouts);
					blocks.Add(block);
					collapseCodeBlocks = null;
					collapseCallouts = null;
				}

				//if not a codeblock simply add it to the final list
				if (!(b is CodeBlock))
				{
					blocks.Add(b);
					continue;
				}

				//wait with adding codeblocks
				if (collapseCodeBlocks == null) collapseCodeBlocks = new List<string>();
				if (collapseCallouts == null) collapseCallouts = new List<string>();

				var codeBlock = (CodeBlock)b;
				if (language != null && codeBlock.Language != language)
				{
					blocks.Add(codeBlock);
					continue;
				}

				language = codeBlock.Language;
				collapseCodeBlocks.Add(codeBlock.Value);
				collapseCallouts.AddRange(codeBlock.CallOuts);

				lineNumber = codeBlock.LineNumber;
			}
			//make sure we flush our code buffer
			if (collapseCodeBlocks != null)
			{
				var joinedCodeBlock = new CodeBlock(string.Join(Environment.NewLine, collapseCodeBlocks), lineNumber, language.Value);
				joinedCodeBlock.CallOuts.AddRange(collapseCallouts);
				blocks.Add(joinedCodeBlock);
			}
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
			var docFile = this.CreateDocumentationLocation();

#if !DOTNETCORE
			CleanDocumentAndWriteToFile(body, docFile);
#else
			File.WriteAllText(docFile.FullName, body);
#endif
		}

#if !DOTNETCORE
		private void CleanDocumentAndWriteToFile(string body, FileInfo docFile)
		{
			// tidy up the asciidoc
			var document = Document.Parse(body);

			// extract section number from doc
			var sectionAttributeEntry = document.Attributes.SingleOrDefault(a => a.Name == "section-number");
			decimal sectionValue;

			var sectionPath = GetPathRelativeToOutput(docFile);
			if (sectionAttributeEntry != null && decimal.TryParse(sectionAttributeEntry.Value, out sectionValue))
			{
				_sections.Add(sectionPath, sectionValue);
			}
			else
			{
				_sections.Add(sectionPath, 1);
			}

			// add attributes and write to destination
			using (var file = new StreamWriter(docFile.FullName))
			{
				document.Accept(new AddAttributeEntriesVisitor(docFile));
				document.Accept(new AsciiDocVisitor(file));
			}
		}
#endif

		private string GetPathRelativeToOutput(FileInfo docFileName)
		{
			var targetDirectory = new DirectoryInfo(Program.OutputDirPath).FullName;
			var currentDirectory = docFileName.FullName;
			return currentDirectory.Replace(targetDirectory, string.Empty).Replace("\\", "/").TrimStart('/');
		}
	}
}
