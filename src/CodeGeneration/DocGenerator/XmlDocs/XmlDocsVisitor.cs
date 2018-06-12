using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AsciiDocNet;
using NuDoq;

namespace DocGenerator.XmlDocs
{
    /// <summary>
    /// Visits XML Documentation file to build an AsciiDoc
    /// collection of labeled list items to include in documentation
    /// </summary>
    /// <seealso cref="NuDoq.Visitor" />
    public class XmlDocsVisitor : Visitor
    {
        private LabeledListItem _labeledListItem;
        private readonly Type _type;

        // AsciiDocNet does not currently have a type for list item continuations, so mimic here
        // for the moment
        private const string ListItemContinuation = "\r\n+\r\n";

        public List<LabeledListItem> LabeledListItems { get; } = new List<LabeledListItem>();

        public XmlDocsVisitor(Type type)
        {
            _type = type;
        }

        public override void VisitText(Text text)
        {
            var content = text.Content.Trim();
            if (!_labeledListItem.Any())
                _labeledListItem.Add(new Paragraph(content));
            else
            {
                var paragraph = _labeledListItem.Last() as Paragraph;

                if (paragraph == null)
                    _labeledListItem.Add(new Paragraph(content));
                else
                {
                    var literal = paragraph.Last() as TextLiteral;

                    if (literal != null && literal.Text == ListItemContinuation)
                        paragraph.Add(new TextLiteral(content));
                    else
                        paragraph.Add(new TextLiteral(" " + content));
                }
            }
        }

        public override void VisitParam(Param param)
        {
            // TODO: add to docs. Omit for moment.
        }

        public override void VisitPara(Para para)
        {
            var paragraph = _labeledListItem.LastOrDefault() as Paragraph;
            paragraph?.Add(new TextLiteral(ListItemContinuation));
            base.VisitPara(para);
        }

        public override void VisitC(C code)
        {
            var content = EncloseInMarks(code.Content.Trim());
            if (!_labeledListItem.Any())
            {
                _labeledListItem.Add(new Paragraph(content));
            }
            else
            {
                var paragraph = _labeledListItem.Last() as Paragraph;
                if (paragraph == null)
                    _labeledListItem.Add(new Paragraph(content));
                else
                    paragraph.Add(new TextLiteral(" " + content));
            }
        }

        public override void VisitSee(See see)
        {
            var content = EncloseInMarks(ExtractLastTokenAndFillGenericParameters((see.Cref ?? see.Content).Trim()));
            if (!_labeledListItem.Any())
            {
                _labeledListItem.Add(new Paragraph(content));
            }
            else
            {
                var paragraph = _labeledListItem.Last() as Paragraph;

                if (paragraph == null)
                    _labeledListItem.Add(new Paragraph(content));
                else
                    paragraph.Add(new TextLiteral(" " + content));
            }
        }

        private string ExtractLastTokenAndFillGenericParameters(string value)
        {
            if (value == null)
                return string.Empty;

            var endOfToken = value.IndexOf("(", StringComparison.Ordinal);
            if (endOfToken == -1)
                endOfToken = value.Length;

            var index = 0;

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == '.')
                    index = i + 1;
                else if (value[i] == '(')
                    break;
            }

            var length = endOfToken - index;
            var lastToken = value.Substring(index, length);

            return lastToken.ReplaceArityWithGenericSignature();
        }

        private string EncloseInMarks(string value) => $"`{value}`";

        public override void VisitMember(Member member)
        {
            if (member.Info != null)
            {
                if (member.Info.DeclaringType == _type &&
                    member.Info.MemberType.HasFlag(MemberTypes.Method))
                {
                    var methodInfo = member.Info as MethodInfo;

                    if (methodInfo != null && methodInfo.IsPublic)
                    {
                        if (_labeledListItem != null)
                            LabeledListItems.Add(_labeledListItem);

                        _labeledListItem = new LabeledListItem(EncloseInMarks(methodInfo.Name), 0);
                        base.VisitMember(member);
                    }
                }
            }
        }
    }
}
