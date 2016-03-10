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
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Formatting;
#if !DOTNETCORE
using Microsoft.CodeAnalysis.MSBuild;
#endif
using Microsoft.CodeAnalysis.Text;
using Nest.Litterateur.Documentation.Blocks;
using Newtonsoft.Json;

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
				var language = "csharp";
				_code = node.WithoutLeadingTrivia().WithTrailingTrivia().ToFullString();
				_code = _code.RemoveNumberOfLeadingTabsAfterNewline(repeatedTabs);

#if !DOTNETCORE
				// if this is ExpectJson node, get the json for the anonymous type
				if (node.Kind() == SyntaxKind.AnonymousObjectCreationExpression)
				{
					_code = GetJsonForAnonymousType();
					language = "javascript";
				}
#endif

				var nodeLine = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line;
				var line = _lineNumberOverride ?? nodeLine;
				var codeBlocks = ParseCodeBlocks(_code, line, language);

				base.Visit(node);

				var nodeHasLeadingTriva = node.HasLeadingTrivia && node.GetLeadingTrivia()
					.Any(c => c.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia);
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
					var codeBlocks = ParseCodeBlocks(_code, line, "csharp");

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
				.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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

		private List<CodeBlock> ParseCodeBlocks(string code, int line, string language)
		{
			return Regex.Split(code, @"\/\*\*.*?\*\/", RegexOptions.Singleline)
				.Select(b => b.TrimStart('\r', '\n').TrimEnd('\r', '\n', '\t'))
				.Where(b => !string.IsNullOrEmpty(b) && b != ";")
				.Select(b => new CodeBlock(b, line, language))
				.ToList();
		}

#if !DOTNETCORE
		private string GetJsonForAnonymousType()
		{
			var text =
				$@"
					using System; 
					using Newtonsoft.Json; 

					namespace Temporary
					{{
						public class Json 
						{{
							public string Write()
							{{
								var o = " +
				_code + $@";
								var json = JsonConvert.SerializeObject(o, Formatting.Indented);
								return json;
							}}
						}}
					}}";

			var syntaxTree = CSharpSyntaxTree.ParseText(text);
			string assemblyName = Path.GetRandomFileName();

			var references = new MetadataReference[]
			{
				MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
				MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location),
				MetadataReference.CreateFromFile(typeof(JsonConvert).GetTypeInfo().Assembly.Location),
			};

			CSharpCompilation compilation = 
				CSharpCompilation.Create(
				assemblyName, 
				new[] { syntaxTree }, 
				references, 
				new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

			using (var ms = new MemoryStream())
			{
				var result = compilation.Emit(ms);

				if (!result.Success)
				{
					var failures = result.Diagnostics.Where(diagnostic =>
						diagnostic.IsWarningAsError ||
						diagnostic.Severity == DiagnosticSeverity.Error);

					var builder = new StringBuilder();
					foreach (var diagnostic in failures)
					{
						builder.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
					}

					throw new Exception(builder.ToString());
				}

				ms.Seek(0, SeekOrigin.Begin);
				Assembly assembly = Assembly.Load(ms.ToArray());

				Type type = assembly.GetType("Temporary.Json");
				object obj = Activator.CreateInstance(type);
				var output = type.InvokeMember("Write",
					BindingFlags.Default | BindingFlags.InvokeMethod,
					null,
					obj,
					new object[] { });

				return output.ToString();
			}
		}
#endif
	}
}
