using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsciiDocNet;
using DocGenerator.AsciiDoc;
using DocGenerator.Walkers;
using Microsoft.CodeAnalysis;
using Document = Microsoft.CodeAnalysis.Document;

namespace DocGenerator.Documentation.Files
{
    public class CSharpDocumentationFile : DocumentationFile
    {
        private readonly Document _document;
        private readonly Dictionary<string, Project> _projects;

        public CSharpDocumentationFile(Document document, Dictionary<string, Project> projects)
            : base(new FileInfo(document.FilePath))
        {
            _document = document;
            _projects = projects;
        }

	    public override async Task SaveToDocumentationFolderAsync()
        {
            var converter = new DocConverter();
	        var blocks = await converter.ConvertAsync(_document).ConfigureAwait(false);

            if (!blocks.Any()) return;

            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                foreach (var block in blocks)
                    await writer.WriteLineAsync(block.ToAsciiDoc()).ConfigureAwait(false);
            }

            var destination = this.CreateDocumentationLocation();

            // Now add Asciidoc headers, rearrange sections, etc.
            var document = AsciiDocNet.Document.Parse(builder.ToString());
            var visitor = new GeneratedAsciidocVisitor(this.FileLocation, destination, _projects);
            document = visitor.Convert(document);

            // Write out document to file
            using (var writer = new StreamWriter(destination.FullName))
            {
                document.Accept(new AsciiDocVisitor(writer));
            }
        }
    }
}
