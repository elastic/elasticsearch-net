using System.Collections.Generic;
using System.Linq;
using DocGenerator.Documentation.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DocGenerator.Walkers
{
    public class UsageTestsWalker : CSharpDocumentationFileWalker
    {
        public UsageTestsWalker(IList<IDocumentationBlock> blocks) : base(blocks)
        {
        }

        private static readonly string[] ConvertToJson = {
            "ExpectJson",
            "QueryJson",
	        "AggregationJson"
        };

        private static readonly string[] MembersOfInterest = {
            "ExpectJson",
            "QueryJson",
	        "AggregationJson",
            "Fluent",
            "Initializer",
            "QueryFluent",
            "QueryInitializer",
            "ExpectResponse",
	        "FluentAggs",
	        "InitializerAggs"
        };

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (MembersOfInterest.Contains(node.Identifier.Text))
            {
                base.VisitPropertyDeclaration(node);
            }
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (MembersOfInterest.Contains(node.Identifier.Text))
            {
                base.VisitMethodDeclaration(node);
            }
        }

        protected override bool SerializePropertyDeclarationToJson(PropertyDeclarationSyntax node) =>
            SerializeToJson(node, node.Identifier.Text);

        protected override bool SerializeMethodDeclarationToJson(MethodDeclarationSyntax node) =>
            SerializeToJson(node, node.Identifier.Text);

        private bool SerializeToJson(SyntaxNode node, string memberName)
        {
            if (!ConvertToJson.Contains(memberName)) return false;

	        if (node.TryGetJsonForSyntaxNode(out var json))
            {
                var startingLine = node.StartingLine();
                Blocks.Add(new JavaScriptBlock(json, startingLine, ClassDepth, memberName));
            }

            return true;
        }
    }
}
