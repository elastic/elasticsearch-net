using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nest.Litterateur.Documentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Litterateur.Walkers
{
	class DocumentationFileWalker : CSharpSyntaxWalker
	{
		public DocumentationFileWalker() : base(SyntaxWalkerDepth.StructuredTrivia) { }

		private int ClassDepth { get; set; }
		private bool InsideMultiLineDocumentation { get; set; }
		private bool InsideAutoIncludeMethodBlock { get; set; }
		private bool InsideFluentOrInitializerExample { get; set; }
		public List<IDocumentationBlock> Blocks { get; } = new List<IDocumentationBlock>();

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			++ClassDepth;
			base.VisitClassDeclaration(node);
			--ClassDepth;
		}

		public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			var propertyName = node.Identifier.Text;
			if (propertyName == "Fluent")
			{
				this.InsideFluentOrInitializerExample = true;
				base.VisitPropertyDeclaration(node);
				this.InsideFluentOrInitializerExample = false;
			}
			else if (propertyName == "Initializer")
			{
				this.InsideFluentOrInitializerExample = true;
				base.VisitPropertyDeclaration(node);
				this.InsideFluentOrInitializerExample = false;
			}
		}

		public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			if (!this.InsideFluentOrInitializerExample) return;
			var syntaxNode = node?.ChildNodes()?.LastOrDefault()?.WithAdditionalAnnotations();
			if (syntaxNode == null) return;
			var line = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
			var walker = new CodeWithDocumentationWalker(ClassDepth, line);
			walker.Visit(syntaxNode);
			this.Blocks.AddRange(walker.Blocks);
		}

		public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			if (this.ClassDepth == 1) this.InsideAutoIncludeMethodBlock = true;
			base.VisitMethodDeclaration(node);
			this.InsideAutoIncludeMethodBlock = false;
		}

		public override void VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			if (this.InsideAutoIncludeMethodBlock)
			{
				var line = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
				var allchildren = node.DescendantNodesAndTokens(descendIntoTrivia: true);
				if (allchildren.Any(a => a.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia))
				{
					var walker = new CodeWithDocumentationWalker(ClassDepth, line);
					walker.Visit(node.WithAdditionalAnnotations());
					this.Blocks.AddRange(walker.Blocks);
					return;
				}
				base.VisitExpressionStatement(node);
				this.Blocks.Add(new CodeBlock(node.WithoutLeadingTrivia().ToFullString(), line));
			}
			else base.VisitExpressionStatement(node);

		}

		public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			if (this.InsideAutoIncludeMethodBlock)
			{
				var allchildren = node.DescendantNodesAndTokens(descendIntoTrivia: true);
				var line = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
				if (allchildren.Any(a => a.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia))
				{
					var walker = new CodeWithDocumentationWalker(ClassDepth, line);
					walker.Visit(node.WithAdditionalAnnotations());
					this.Blocks.AddRange(walker.Blocks);
					return;
				}
				this.Blocks.Add(new CodeBlock(node.WithoutLeadingTrivia().ToFullString(), line));
			}
			base.VisitLocalDeclarationStatement(node);
		}

		public override void VisitXmlText(XmlTextSyntax node)
		{
			var text = node.TextTokens
				.Where(n => n.Kind() == SyntaxKind.XmlTextLiteralToken)
				.Aggregate(new StringBuilder(), (a, t) => a.AppendLine(t.Text.TrimStart()), a => a.ToString());

			var line = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
			this.Blocks.Add(new TextBlock(text, line));

			base.VisitXmlText(node);
		}

		public override void VisitTrivia(SyntaxTrivia trivia)
		{
			if (trivia.Kind() != SyntaxKind.MultiLineDocumentationCommentTrivia)
			{
				base.VisitTrivia(trivia);
				return;
			}

			this.InsideMultiLineDocumentation = true;
			base.VisitTrivia(trivia);
			this.InsideMultiLineDocumentation = false;
		}

		public override void VisitTrailingTrivia(SyntaxToken token)
		{
			//if (!this.InsideMultiLineDocumentation) return;
			//if (token.CSharpKind() != SyntaxKind.XmlTextLiteralToken) return;

			base.VisitTrailingTrivia(token);
		}

		public override void VisitToken(SyntaxToken token)
		{
			base.VisitToken(token);
		}

		public override void DefaultVisit(SyntaxNode node)
		{
			base.DefaultVisit(node);
		}

		public override void Visit(SyntaxNode node)
		{
			base.Visit(node);
		}
	}
}
