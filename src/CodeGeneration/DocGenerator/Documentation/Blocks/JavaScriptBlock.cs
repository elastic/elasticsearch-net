using System.Text;

namespace DocGenerator.Documentation.Blocks
{
    public class JavaScriptBlock : CodeBlock
    {
        public JavaScriptBlock(string text, int startingLine, int depth, string memberName = null)
            : base(text, startingLine, depth, "javascript", memberName)
        {
        }

        public string Title { get; set; }

        public override string ToAsciiDoc()
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrEmpty(Title))
                builder.AppendLine("." + Title);
            builder.AppendLine(!string.IsNullOrEmpty(MemberName)
                ? $"[source, {Language.ToLowerInvariant()}, method=\"{MemberName.ToLowerInvariant()}\"]"
                : $"[source, {Language.ToLowerInvariant()}]");
            builder.AppendLine("----");
            builder.AppendLine(Value);
            builder.AppendLine("----");
            return builder.ToString();
        }
    }
}