using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocGenerator.Documentation.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DocGenerator.Walkers
{
    public class DocConverter
    {
        public async Task<IList<IDocumentationBlock>> ConvertAsync(Document document)
        {
            var node = await document.GetSyntaxRootAsync().ConfigureAwait(false);
	        var fileName = document.Name;
	        IList<IDocumentationBlock> blocks = new List<IDocumentationBlock>();

	        // use different walking rules for different source files
	        if (fileName.EndsWith("UsageTests.cs", StringComparison.OrdinalIgnoreCase) ||
	            fileName.Equals("WritingAggregations.doc.cs", StringComparison.OrdinalIgnoreCase))
	        {
		        var walker = new UsageTestsWalker(blocks);
		        walker.Visit(node);

		        // apply the usage conventions to writing aggregations,
		        // but don't rearrange any blocks
		        if (!fileName.Equals("WritingAggregations.doc.cs", StringComparison.OrdinalIgnoreCase))
		        {
			        blocks = RearrangeCodeBlocks(blocks);
		        }
	        }
	        else
	        {
		        var walker = new CSharpDocumentationFileWalker(blocks);
		        walker.Visit(node);
		        blocks = CondenseCodeBlocks(blocks);
	        }

	        return blocks;
        }

	    private IList<IDocumentationBlock> CondenseCodeBlocks(IList<IDocumentationBlock> blocks)
        {
            var newBlocks = new List<IDocumentationBlock>(blocks.Count);
            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                var codeBlock = block as CodeBlock;
                if (codeBlock == null)
                {
                    newBlocks.Add(block);
                    continue;
                }

	            if (newBlocks.LastOrDefault() is CodeBlock previousBlock && previousBlock.Language == codeBlock.Language)
                {
                    previousBlock.AddLine(Environment.NewLine);
                    previousBlock.AddLine(codeBlock.Value);
                }
                else
                {
                    newBlocks.Add(codeBlock);
                }
            }

            return newBlocks;
        }

        /// <summary>
        /// Rearranges the code blocks in usage tests so that they follow the order:
        /// 1. Fluent Example
        /// 2. Object Initializer Example
        /// 3. Example json
        /// </summary>
        /// <param name="blocks">The blocks.</param>
        /// <returns></returns>
        private IList<IDocumentationBlock> RearrangeCodeBlocks(IList<IDocumentationBlock> blocks)
        {
            var newBlocks = new List<IDocumentationBlock>(blocks.Count);
            var seenFluentExample = false;
            var seenInitializerExample = false;
            var index = -1;

            CodeBlock javascriptBlock = null;
            CodeBlock initializerExample = null;

            for (var i = 0; i < blocks.Count; i++)
            {
                if (seenFluentExample && seenInitializerExample && javascriptBlock != null)
                {
                    newBlocks.Insert(index + 1, javascriptBlock);
                    javascriptBlock = null;
                    seenFluentExample = false;
                    seenInitializerExample = false;
                    index = -1;
                }

                var block = blocks[i];

                var codeBlock = block as CodeBlock;
                if (codeBlock == null)
                {
                    newBlocks.Add(block);
                    continue;
                }

                if (codeBlock.Language == "javascript")
                {
                    ((JavaScriptBlock) codeBlock).Title = "Example json output";

                    if (seenFluentExample && seenInitializerExample)
                    {
                        newBlocks.Insert(index + 1, codeBlock);
                        seenFluentExample = false;
                        seenInitializerExample = false;
                        index = -1;
                    }
                    else
                    {
                        javascriptBlock = codeBlock;
                    }
                }
                else
                {
                    if (codeBlock.MemberName != null && codeBlock.MemberName.Contains("fluent"))
                    {
                        seenFluentExample = true;
                        newBlocks.Add(codeBlock);
                        index = newBlocks.IndexOf(codeBlock);

                        if (initializerExample != null)
                        {
                            newBlocks.Add(initializerExample);
                            index = newBlocks.IndexOf(initializerExample);
                            initializerExample = null;
                        }
                    }
                    else if (codeBlock.MemberName != null && codeBlock.MemberName.Contains("initializer"))
                    {
                        seenInitializerExample = true;

                        if (seenFluentExample)
                        {
                            newBlocks.Add(codeBlock);
                            index = newBlocks.IndexOf(codeBlock);
                        }
                        else
                        {
                            initializerExample = codeBlock;
                        }
                    }
                    else
                    {
                        newBlocks.Add(codeBlock);
                    }
                }

                if (i == blocks.Count - 1 && javascriptBlock != null)
                {
                    if (seenFluentExample && seenInitializerExample)
                    {
                        newBlocks.Insert(index + 1, javascriptBlock);
                    }
                    else
                    {
                        newBlocks.Add(javascriptBlock);
                    }
                }
            }

            return newBlocks;
        }
    }
}
