using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocGenerator.Documentation.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DocGenerator.Walkers
{
    /// <summary>
    /// Walks a C# syntax tree, extracting out code and multiline comments for use
    /// within asciidoc documentation
    /// </summary>
    public class CSharpDocumentationFileWalker : CSharpSyntaxWalker
    {
        protected readonly IList<IDocumentationBlock> Blocks;

        public CSharpDocumentationFileWalker(IList<IDocumentationBlock> blocks) : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            Blocks = blocks;
        }

        protected int ClassDepth { get; set; }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if (node.ShouldBeHidden()) return;

            if (node.ChildNodes().All(childNode => childNode.IsKind(SyntaxKind.PropertyDeclaration) ||
                                                   childNode.IsKind(SyntaxKind.AttributeList)))
            {
                // simple nested interface	
                AddNestedType(node);
            }
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.ShouldBeHidden()) return;

            ++ClassDepth;
            if (ClassDepth == 1)
            {
                // walk the top level class
                base.VisitClassDeclaration(node);
            }
            else if (node.ChildNodes().All(childNode => childNode.IsKind(SyntaxKind.PropertyDeclaration) ||
                                                        childNode.IsKind(SyntaxKind.AttributeList)))
            {
                // we have a simple nested POCO class inside of a class
                AddNestedType(node);
            }
            else
            {
                var methods = node.ChildNodes().OfType<MethodDeclarationSyntax>();
                if (!methods.Any(m => m.AttributeLists.SelectMany(a => a.Attributes).Any()))
                {
                    // nested class with methods that are not unit or integration tests 
                    // e.g. example PropertyVisitor in Automap.doc.cs
                    AddNestedType(node);
                }
            }
            --ClassDepth;
        }

        protected virtual bool SerializePropertyDeclarationToJson(PropertyDeclarationSyntax node) => false;

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var trivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia()
                : default(SyntaxTriviaList);

            if (node.ShouldBeHidden(trivia)) return;

            // multiline comment on property
            AddMultiLineDocumentationComment(trivia);

            // allow derived types to determine if this method should be json serialized
            if (SerializePropertyDeclarationToJson(node)) return;

            var memberName = node.Identifier.Text;

            // decide whether we should strip the modifier/accessor and return type, based on if
            // this is a property on the top level class
            if (ClassDepth == 1)
            {
                var arrowExpressionClauseSyntax =
                    node.ChildNodes().OfType<ArrowExpressionClauseSyntax>().FirstOrDefault();

                if (arrowExpressionClauseSyntax != null)
                {
                    var firstChildNode = arrowExpressionClauseSyntax.ChildNodes().First();
                    Blocks.Add(new CSharpBlock(firstChildNode, ClassDepth, memberName));
                }
                else
                {
                    // property getter
                    var blockNode = node.DescendantNodes().OfType<BlockSyntax>().FirstOrDefault();

                    if (blockNode != null)
                    {
                        AddBlockChildNodes(blockNode, memberName);
                    }
                }
            }
            else
            {
                // assume this is a nested class' property
                Blocks.Add(new CSharpBlock(node, ClassDepth, memberName));
            }

            if (node.HasTrailingTrivia)
            {
                trivia = node.GetTrailingTrivia();
                AddMultiLineDocumentationComment(trivia);
            }         
        }

        protected virtual bool SerializeMethodDeclarationToJson(MethodDeclarationSyntax node) => false;

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var leadingTrivia = node.HasLeadingTrivia
                ? node.GetLeadingTrivia()
                : default(SyntaxTriviaList);

            // multiline comment on method
            AddMultiLineDocumentationComment(leadingTrivia);

            if (node.ShouldBeHidden(leadingTrivia)) return;

            // allow derived types to determine if this method should be json serialized
            if (SerializeMethodDeclarationToJson(node)) return;

            var memberName = node.Identifier.Text;

            foreach(var blockNode in node.ChildNodes().OfType<BlockSyntax>())
            {
                AddBlockChildNodes(blockNode, memberName);
            }

            foreach (var syntax in node.ChildNodes().Where(c => c.IsKind(SyntaxKind.ArrowExpressionClause)))
            {
                var syntaxNode = syntax.ChildNodes().First();
                Blocks.Add(new CSharpBlock(syntaxNode, ClassDepth, memberName));
            }
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            if (!trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
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
            var line = trivia.SyntaxTree.GetLineSpan(trivia.Span).StartLinePosition.Line;

            Blocks.Add(new TextBlock(text, line));
        }

        private void AddNestedType(BaseTypeDeclarationSyntax node)
        {
            var blockAdded = false;

            if (node.HasLeadingTrivia)
            {
                var leadingTrivia = node.GetLeadingTrivia();

                if (node.ShouldBeHidden(leadingTrivia))
                {
                    return;
                }

                // inline multiline comment
                AddMultiLineDocumentationComment(leadingTrivia);

                if (node.ShouldBeConvertedToJson(leadingTrivia))
                {
                    string json;
                    if (node.TryGetJsonForSyntaxNode(out json))
                    {
                        var startingLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
                        Blocks.Add(new JavaScriptBlock(json, startingLine, ClassDepth, node.Identifier.Text));
                        blockAdded = true;
                    }
                }
            }

            if (!blockAdded)
            {
                Blocks.Add(new CSharpBlock(node, ClassDepth));
            }
        }

        private void AddBlockChildNodes(BlockSyntax node, string memberName)
        {
            CSharpBlock codeBlock = null;
            foreach (var blockChildNode in node.ChildNodes())
            {
                if (blockChildNode.HasLeadingTrivia)
                {
                    var trivia = blockChildNode.GetLeadingTrivia();

                    // inside method multiline comment
                    AddMultiLineDocumentationComment(trivia, () =>
                    {
                        // flush what we may have collected so far
                        if (codeBlock != null)
                        {
                            Blocks.Add(codeBlock);
                            codeBlock = null;
                        }
                    });

                    if (blockChildNode.ShouldBeHidden(trivia))
                        continue;

                    if (blockChildNode.ShouldBeConvertedToJson(trivia))
                    {
                        string json;
                        if (blockChildNode.TryGetJsonForSyntaxNode(out json))
                        {
                            // flush what we may have collected so far
                            if (codeBlock != null)
                            {
                                Blocks.Add(codeBlock);
                                codeBlock = null;
                            }

                            var startingLine = blockChildNode.StartingLine();
                            Blocks.Add(new JavaScriptBlock(json, startingLine, ClassDepth, memberName));
                            continue;
                        }
                    }
                }

                if (codeBlock == null)
                {
                    codeBlock = new CSharpBlock(blockChildNode, ClassDepth, memberName);
                }
                else
                {
                    codeBlock.AddNode(blockChildNode);
                }

                if (blockChildNode.HasTrailingTrivia)
                {
                    var trivia = blockChildNode.GetTrailingTrivia();
                    AddMultiLineDocumentationComment(trivia, () =>
                    {
                        // flush what we may have collected so far
                        if (codeBlock != null)
                        {
                            Blocks.Add(codeBlock);
                            codeBlock = null;
                        }
                    });
                }
            }

            if (codeBlock != null)
            {
                Blocks.Add(codeBlock);
            }
        }

        private void AddMultiLineDocumentationComment(SyntaxTriviaList leadingTrivia, Action actionIfFound = null)
        {
            if (leadingTrivia.Any(l => l.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)))
            {
                actionIfFound?.Invoke();

                foreach (var trivia in leadingTrivia)
                {
                    VisitTrivia(trivia);
                }
            }
        }
    }
}