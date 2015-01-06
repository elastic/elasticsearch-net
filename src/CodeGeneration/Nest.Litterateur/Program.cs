using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nest.Litterateur
{
	class Program
	{
		static Object x = 1;
		static void Main(string[] args)
		{
			var code = File.ReadAllText(@"c:\Projects\elasticsearch-net\src\Tests\Nest.Tests.Literate\ElasticsearchNet\Connecting.cs");
			var ast = CSharpSyntaxTree.ParseText(code);
			var walker = new CodeGenerationWalker();
			walker.Visit(ast.GetRoot());

		}
	}

	class CodeGenerationWalker : CSharpSyntaxWalker
	{
		public CodeGenerationWalker() : base(SyntaxWalkerDepth.StructuredTrivia) { }

		private int ClassDepth { get; set; }
		private bool InsideMultiLineDocumentation { get; set; }

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			++ClassDepth;
			base.VisitClassDeclaration(node);
			--ClassDepth;
		}

		public override void VisitXmlText(XmlTextSyntax node)
		{
			var text = node.TextTokens.ToFullString();
			base.VisitXmlText(node);
		}

		public override void VisitTrivia(SyntaxTrivia trivia)
		{
			if (trivia.CSharpKind() != SyntaxKind.MultiLineDocumentationCommentTrivia) return;

			this.InsideMultiLineDocumentation = true;
			base.VisitTrivia(trivia);
			this.InsideMultiLineDocumentation = false;
		}

		public override void VisitTrailingTrivia(SyntaxToken token)
		{
			if (!this.InsideMultiLineDocumentation) return;
			if (token.CSharpKind() != SyntaxKind.XmlTextLiteralToken) return;

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
