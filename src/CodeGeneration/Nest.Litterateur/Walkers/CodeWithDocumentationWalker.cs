using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nest.Litterateur.Documentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.Formatting;
#if !DOTNETCORE
using Microsoft.CodeAnalysis.MSBuild;
#endif
using Microsoft.CodeAnalysis.Text;
using Nest.Litterateur.Documentation.Blocks;

namespace Nest.Litterateur.Walkers
{
	class CodeWithDocumentationWalker : CSharpSyntaxWalker
	{
		public List<IDocumentationBlock> Blocks { get; } = new List<IDocumentationBlock>();
		public List<TextBlock> TextBlocks { get; } = new List<TextBlock>();

		private bool _firstVisit = true;
		private string _code;
		public int ClassDepth { get; }
		private readonly int? _lineNumberOverride;

		/// <summary>
		/// We want to support inlining /** */ documentations because its super handy 
		/// to document fluent code, what ensues is total hackery
		/// </summary>
		/// <param name="classDepth"></param>
		/// <param name="lineNumber">line number used for sorting</param>
		public CodeWithDocumentationWalker(int classDepth = 1, int? lineNumber = null) : base(SyntaxWalkerDepth.StructuredTrivia) 
		{
			ClassDepth = classDepth;
			_lineNumberOverride = lineNumber;
		}

		public override void Visit(SyntaxNode node)
		{
			if (_firstVisit)
			{
				_firstVisit = false;
				var leadingTabs = new String('\t', 2 + ClassDepth);
				_code = node.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString()
					.Replace(leadingTabs, string.Empty);

				var nodeLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;

				var line = _lineNumberOverride ?? nodeLine;

				var codeBlocks = Regex.Split(_code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
					.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
					.Where(b => !string.IsNullOrEmpty(b) && b != ";")
					.Select(b=>new CodeBlock(b, line))
					.ToList();

				base.Visit(node);

				var nodeHasLeadingTriva = node.HasLeadingTrivia && node.GetLeadingTrivia()
					.Any(c=>c.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia);
				var blocks = codeBlocks.Intertwine<IDocumentationBlock>(this.TextBlocks, swap: nodeHasLeadingTriva);
				this.Blocks.Add(new CombinedBlock(blocks, line));
				return;
			}

			base.Visit(node);
		}

		public override void VisitBlock(BlockSyntax node)
		{
			if (_firstVisit)
			{
				_firstVisit = false;
				foreach (var statement in node.Statements)
				{
					var leadingTabs = new string('\t', 3 + ClassDepth);
					SyntaxNode formattedStatement = statement;

					_code = formattedStatement.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString().Replace(leadingTabs, string.Empty);

					var nodeLine = formattedStatement.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;

					var line = _lineNumberOverride ?? nodeLine;

					var codeBlocks = Regex.Split(_code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
						.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
						.Where(b => !string.IsNullOrEmpty(b) && b != ";")
						.Select(b => new CodeBlock(b, line))
						.ToList();

					this.Blocks.AddRange(codeBlocks);
				}

				base.Visit(node);
			}
		}

		public override void VisitXmlText(XmlTextSyntax node)
		{
			var nodeLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
			var line = _lineNumberOverride ?? nodeLine;
			var text = node.TextTokens
				.Where(n => n.Kind() == SyntaxKind.XmlTextLiteralToken)
				.Aggregate(new StringBuilder(), (a, t) => a.AppendLine(t.Text.TrimStart()), a => a.ToString());

			this.TextBlocks.Add(new TextBlock(text, line));

			base.VisitXmlText(node);
		}


	}
}
