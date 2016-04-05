using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nest.Litterateur.Documentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Nest.Litterateur.Documentation.Blocks;

namespace Nest.Litterateur.Walkers
{
	class CodeWithDocumentationWalker : CSharpSyntaxWalker
	{
		private bool _firstVisit = true;
		private string _code;
		private readonly string _propertyOrMethodName;

		public int ClassDepth { get; }

		public List<IDocumentationBlock> Blocks { get; } = new List<IDocumentationBlock>();

		public List<TextBlock> TextBlocks { get; } = new List<TextBlock>();

		private readonly int? _lineNumberOverride;

		/// <summary>
		/// We want to support inlining /** */ documentations because its super handy
		/// to document fluent code, what ensues is total hackery
		/// </summary>
		/// <param name="classDepth">the depth of the class</param>
		/// <param name="lineNumber">line number used for sorting</param>
		/// <param name="propertyOrMethodName">the name of the property that we are walking</param>
		public CodeWithDocumentationWalker(int classDepth = 1, int? lineNumber = null, string propertyOrMethodName = null) : base(SyntaxWalkerDepth.StructuredTrivia)
		{
			ClassDepth = classDepth;
			_lineNumberOverride = lineNumber;
			_propertyOrMethodName = propertyOrMethodName;
		}

		public override void Visit(SyntaxNode node)
		{
			if (_firstVisit)
			{
				_firstVisit = false;

				var repeatedTabs = 2 + ClassDepth;
				var language = Language.CSharp;



				_code = node.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString();
				_code = _code.RemoveNumberOfLeadingTabsAfterNewline(repeatedTabs);

#if !DOTNETCORE
				if (_propertyOrMethodName == "ExpectJson" || _propertyOrMethodName == "QueryJson")
				{
					// try to get the json for the anonymous type.
					// Only supports system types and Json.Net LINQ objects e.g. JObject
					string json;
					if (_code.TryGetJsonForAnonymousType(out json))
					{
						language = Language.JavaScript;
						_code = json;
					}
				}
#endif
				// TODO: Can do this once we get the generic arguments from the Property declaration
				//if (_propertyName == "Fluent")
				//{
				//  // need to know what type we're operating on
				//	_code += $"client.Search({_code});";
				//}

				var nodeLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
				var line = _lineNumberOverride ?? nodeLine;
				var codeBlocks = ParseCodeBlocks(_code, line, language, _propertyOrMethodName);

				base.Visit(node);

				var nodeHasLeadingTriva = node.HasLeadingTrivia &&
					node.GetLeadingTrivia().Any(c => c.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia);
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
					_code = _code.RemoveNumberOfLeadingTabsAfterNewline(repeatedTabs);

					var nodeLine = formattedStatement.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
					var line = _lineNumberOverride ?? nodeLine;
					var codeBlocks = ParseCodeBlocks(_code, line, Language.CSharp, _propertyOrMethodName);

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

			var tokens = trivia.ToFullString()
				.RemoveLeadingAndTrailingMultiLineComments()
				.SplitOnNewLines(StringSplitOptions.None);
			var builder = new StringBuilder();

			foreach (var token in tokens)
			{
				var currentToken = token.RemoveLeadingSpacesAndAsterisk();
				var decodedToken = System.Net.WebUtility.HtmlDecode(currentToken);
				builder.AppendLine(decodedToken);
			}

			var text = builder.ToString();
			var line = _firstVisit
				? trivia.SyntaxTree.GetLineSpan(trivia.Span).StartLinePosition.Line
				: _lineNumberOverride.GetValueOrDefault(0);

			this.Blocks.Add(new TextBlock(text, line));
		}

		private List<CodeBlock> ParseCodeBlocks(string code, int line, Language language, string propertyName)
		{
			return Regex.Split(code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
				.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
				.Where(b => !string.IsNullOrEmpty(b) && b != ";")
				.Select(b => new CodeBlock(b, line, language, propertyName))
				.ToList();
		}
	}
}
