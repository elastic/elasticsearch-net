/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ExamplesGenerator
{
	/// <summary>
	/// Reads the implemented examples from the Examples project
	/// </summary>
	internal static class CSharpParser
	{
		private static readonly Regex StartTag = new Regex(@"// tag::(?<hash>.*?)\[\]");
		private static readonly Regex EndTag = new Regex(@"// end::(?<hash>.*?)\[\]");

		private static Func<SyntaxTrivia, bool> SingleLineTriviaMatches(Regex regex) =>
			t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) && regex.IsMatch(t.ToFullString());

		public static IEnumerable<ImplementedExample> ParseImplementedExamples()
		{
			foreach (var path in Directory.EnumerateFiles(ExampleLocation.ExamplesCSharpProject.FullName, "*Page.cs", SearchOption.AllDirectories))
			{
				var compilationUnit = CSharpSyntaxTree.ParseText(File.ReadAllText(path)).GetCompilationUnitRoot();

				var classDeclaration = compilationUnit
					.Members.OfType<NamespaceDeclarationSyntax>()
					.Single()
					.Members.OfType<ClassDeclarationSyntax>()
					.Single();

				var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>();

				foreach (var methodDeclaration in methodDeclarations)
				{
					var uAttribute = methodDeclaration.AttributeLists.Single(a => a.Attributes.Any(aa => aa.Name.ToString() == "U")).Attributes.Single();

					// No Skip property present on [U] attribute
					if (uAttribute.ArgumentList == null)
					{
						var descriptionAttributeSyntax = methodDeclaration.AttributeLists.SingleOrDefault(a => a.Attributes.Any(aa => aa.Name.ToString() == "Description"));

						var referenceFileAndLineNumber = "";
						if (descriptionAttributeSyntax != null)
							referenceFileAndLineNumber =  descriptionAttributeSyntax.Attributes.Single().ArgumentList.Arguments.First().ToString().Trim('"');

						// opening tag comment is leading trivia on the first statement
						var openTagComment = methodDeclaration.Body.Statements.First()
							.GetLeadingTrivia().Single(SingleLineTriviaMatches(StartTag));

						var hash = StartTag.Match(openTagComment.ToFullString()).Groups["hash"].Value;

						var statements = new List<StatementSyntax>();

						for (var i = 0; i < methodDeclaration.Body.Statements.Count; i++)
						{
							var statement = methodDeclaration.Body.Statements[i];

							// end tag can be reported as leading trivia on the first match example assertion, which denotes the end of
							// statements we're interested in
							if (statement.GetLeadingTrivia().Any(SingleLineTriviaMatches(EndTag)))
								break;

							statements.Add(i == 0 ? statement.ReplaceTrivia(openTagComment, default(SyntaxTrivia)) : statement);

							if (statement.GetTrailingTrivia().Any(SingleLineTriviaMatches(EndTag)))
							{
								statements.Add(statement.WithoutTrailingTrivia());
								break;
							}
						}

						// create a new block with collected statements. We need a SyntaxNode to work with
						var body = Block(statements);

						var method = methodDeclaration.Identifier.Text;
						var startLineNumber = methodDeclaration.SyntaxTree.GetLineSpan(methodDeclaration.Span).StartLinePosition.Line + 1;
						var endLineNumber = methodDeclaration.SyntaxTree.GetLineSpan(methodDeclaration.Span).EndLinePosition.Line + 1;

						yield return new ImplementedExample(referenceFileAndLineNumber, method, startLineNumber, endLineNumber, path, hash, body);
					}
				}
			}
		}
	}
}
