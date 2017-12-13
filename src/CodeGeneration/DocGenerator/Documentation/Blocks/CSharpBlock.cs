using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DocGenerator.Documentation.Blocks
{
    public class CSharpBlock : CodeBlock
    {
        private static readonly Regex Callout = new Regex(@"//[ \t]*(?<callout>\<\d+\>)[ \t]*(?<text>\S.*)", RegexOptions.Compiled);
        private static readonly Regex CalloutReplacer = new Regex(@"//[ \t]*\<(\d+)\>.*", RegexOptions.Compiled);

        private List<string> Callouts { get; } = new List<string>();

        public CSharpBlock(SyntaxNode node, int depth, string memberName = null)
            : base(node.WithoutLeadingTrivia().ToFullStringWithoutPragmaWarningDirectiveTrivia(),
                node.StartingLine(),
                node.IsKind(SyntaxKind.ClassDeclaration) ? depth : depth + 2,
                "csharp",
                memberName)
        {
        }

        public void AddNode(SyntaxNode node) => Lines.Add(node.WithLeadingEndOfLineTrivia().ToFullStringWithoutPragmaWarningDirectiveTrivia());

        public override string ToAsciiDoc()
        {
            var builder = new StringBuilder();

            // method attribute is used to add section titles in GeneratedAsciidocVisitor
            builder.AppendLine(!string.IsNullOrEmpty(MemberName)
                ? $"[source, {Language.ToLowerInvariant()}, method=\"{MemberName.ToLowerInvariant()}\"]"
                : $"[source, {Language.ToLowerInvariant()}]");

            builder.AppendLine("----");

            var code = ExtractCallOutsFromCode(Value);

            code = code.RemoveNumberOfLeadingTabsOrSpacesAfterNewline(Depth);
            builder.AppendLine(code);

            builder.AppendLine("----");
            foreach (var callout in Callouts)
            {
                builder.AppendLine(callout);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Extracts the callouts from code. The callout comment is defined inline within
        /// source code to play nicely with C# semantics, but needs to be extracted and placed after the
        /// source block delimiter to be valid asciidoc.
        /// </summary>
        private string ExtractCallOutsFromCode(string value)
        {
            var matches = Callout.Matches(value);
            var callouts = new List<string>();

            foreach (Match match in matches)
            {
                callouts.Add($"{match.Groups["callout"].Value} {match.Groups["text"].Value}");
            }

            if (callouts.Any())
            {
                value = CalloutReplacer.Replace(value, "//<$1>");
                Callouts.AddRange(callouts);
            }

            return value.Trim();
        }
    }
}
