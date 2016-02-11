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
		/// <param name="classDepth">the depth of the class</param>
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

				var repeatedTabs = 2 + ClassDepth;
				_code = node.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString();

				// find x or more repeated tabs and trim x number of tabs from the start
				_code = Regex.Replace(_code, $"\t{{{repeatedTabs},}}", match => match.Value.Substring(repeatedTabs));

				var nodeLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
				var line = _lineNumberOverride ?? nodeLine;
				var codeBlocks = ParseCodeBlocks(_code, line);

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
					var repeatedTabs = 3 + ClassDepth;
					SyntaxNode formattedStatement = statement;

					_code = formattedStatement.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString();
					_code = Regex.Replace(_code, $"\t{{{repeatedTabs},}}", match => match.Value.Substring(repeatedTabs));

					var nodeLine = formattedStatement.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
					var line = _lineNumberOverride ?? nodeLine;
					var codeBlocks = ParseCodeBlocks(_code, line);

					this.Blocks.AddRange(codeBlocks);
				}

				base.Visit(node);
			}
		}

		public override void VisitTrivia(SyntaxTrivia trivia)
		{
			if (trivia.Kind() != SyntaxKind.MultiLineDocumentationCommentTrivia)
			{
				base.VisitTrivia(trivia);
				return;
			}

			var tokens = trivia.ToFullString().TrimStart('/', '*').TrimEnd('*', '/').Split('\n');
			var builder = new StringBuilder();

			foreach (var token in tokens)
			{
				var decodedToken = System.Net.WebUtility.HtmlDecode(token.TrimStart().TrimStart('*').TrimStart());
				builder.AppendLine(decodedToken);
			}

			var text = builder.ToString();
			var line = _firstVisit
				? trivia.SyntaxTree.GetLineSpan(trivia.Span).StartLinePosition.Line
				: _lineNumberOverride.GetValueOrDefault(0);

			this.Blocks.Add(new TextBlock(text, line));
		}

		private List<CodeBlock> ParseCodeBlocks(string code, int line)
		{
			return Regex.Split(code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
				.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
				.Where(b => !string.IsNullOrEmpty(b) && b != ";")
				.Select(b => new CodeBlock(b, line))
				.ToList();
		}
	}
}
