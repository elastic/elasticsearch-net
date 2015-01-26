using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nest.Litterateur.Documentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest.Litterateur.Walkers
{
	class CodeWithDocumentationWalker : CSharpSyntaxWalker
	{
		public List<IDocumentationBlock> Blocks { get; } = new List<IDocumentationBlock>();
		public List<TextBlock> TextBlocks { get; } = new List<TextBlock>();

		private bool _firstVisit = true;
		private string _code;
		public int ClassDepth { get; }
		
		/// <summary>
		/// We want to support inlining /** */ documentations because its super handy 
		/// to document fluent code, what ensues is total hackery
		/// </summary>
		/// <param name="classDepth"></param>
		public CodeWithDocumentationWalker(int classDepth = 1) : base(SyntaxWalkerDepth.StructuredTrivia) 
		{
			ClassDepth = classDepth;
		}

		public override void Visit(SyntaxNode node)
		{
			if (_firstVisit)
			{
				_firstVisit = false;
				var leadingTabs = new String('\t', 2 + ClassDepth);
				_code = node.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString()
					.Replace(leadingTabs, "");

				var codeBlocks = Regex.Split(_code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
					.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
					.Where(b => !string.IsNullOrEmpty(b) && b != ";")
					.Select(b=>new CodeBlock(b))
					.ToList();

				base.Visit(node);

				var blocks = codeBlocks.Intertwine<IDocumentationBlock>(this.TextBlocks);
				this.Blocks.AddRange(blocks);
				return;
			}

			base.Visit(node);
		}

		public override void VisitXmlText(XmlTextSyntax node)
		{
			var text = node.TextTokens
				.Where(n => n.CSharpKind() == SyntaxKind.XmlTextLiteralToken)
				.Aggregate(new StringBuilder(), (a, t) => a.AppendLine(t.Text.TrimStart()), a => a.ToString());

			this.TextBlocks.Add(new TextBlock(text));

			base.VisitXmlText(node);
		}


	}
}
