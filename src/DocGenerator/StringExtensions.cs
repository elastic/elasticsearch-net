// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;

namespace DocGenerator
{
	public static class StringExtensions
	{
		private static readonly Regex LeadingMultiLineComment = new Regex(@"^(?<value>[ \t]*\/\*)", RegexOptions.Compiled);

		private static readonly Dictionary<string, string> Substitutions = new Dictionary<string, string>
		{
			{ "FixedDate", "new DateTime(2015, 06, 06, 12, 01, 02, 123)" },
			{ "FirstNameToFind", "\"pierce\"" },
			{ "Project.First.Suggest.Context.Values.SelectMany(v => v).First()", "\"red\"" },
			{ "Project.First.Suggest.Contexts.Values.SelectMany(v => v).First()", "\"red\"" },
			{ "Project.Instance.Name", "\"Durgan LLC\"" },
			{
				"Project.InstanceAnonymous", "new {name = \"Koch, Collier and Mohr\", state = \"BellyUp\",startedOn = " +
				"\"2015-01-01T00:00:00\",lastActivity = \"0001-01-01T00:00:00\",leadDeveloper = " +
				"new { gender = \"Male\", id = 0, firstName = \"Martijn\", lastName = \"Laarman\" }," +
				"location = new { lat = 42.1523, lon = -80.321 }}"
			},
			{ "_templateString", "\"{ \\\"match\\\": { \\\"text\\\": \\\"{{query_string}}\\\" } }\"" },
			{
				"base.QueryJson",
				"new{ @bool = new { must = new[] { new { match_all = new { } } }, must_not = new[] { new { match_all = new { } } }, should = new[] { new { match_all = new { } } }, filter = new[] { new { match_all = new { } } }, minimum_should_match = 1, boost = 2.0, } }"
			},
			{ "ExpectedTerms", "new [] { \"term1\", \"term2\" }" },
			{ "_ctxNumberofCommits", "\"_source.numberOfCommits > 0\"" },
			{ "Project.First.Name", "\"Lesch Group\"" },
			{ "Project.First.NumberOfCommits", "775" },
			{ "IntervalsPrefix", "\"lorem\"" },
			{ "IntervalsFuzzy", "\"lorem\"" },
			{ "LastNameSearch", "\"Stokes\"" },
			{ "_first.Language", "\"painless\"" },
			{ "_first.Init", "\"state.map = [:]\"" },
			{ "_first.Map", "\"if (state.map.containsKey(doc['state'].value)) state.map[doc['state'].value] += 1; else state.map[doc['state'].value] = 1;\"" },
			{ "_first.Reduce", "\"def reduce = [:]; for (map in states){ for (entry in map.entrySet()) { if (reduce.containsKey(entry.getKey())) reduce[entry.getKey()] += entry.getValue(); else reduce[entry.getKey()] = entry.getValue(); }} return reduce;\"" },
			{ "_first.Combine", "\"return state.map;\"" },
			{ "_second.Language", "\"painless\"" },
			{ "_second.Init", "\"state.commits = []\"" },
			{ "_second.Combine", "\"def sum = 0.0; for (c in state.commits) { sum += c } return sum\"" },
			{ "_second.Map", "\"if (doc['state'].value == \\\"Stable\\\") { state.commits.add(doc['numberOfCommits'].value) }\"" },
			{ "_second.Reduce", "\"def sum = 0.0; for (a in states) { sum += a } return sum\"" },
			{ "_script.Init", "\"state.commits = []\"" },
			{ "_script.Map", "\"if (doc['state'].value == \\\"Stable\\\") { state.commits.add(doc['numberOfCommits'].value) }\"" },
			{ "_script.Combine", "\"def sum = 0.0; for (c in state.commits) { sum += c } return sum\"" },
			{ "_script.Reduce", "\"def sum = 0.0; for (a in states) { sum += a } return sum\"" },
			{ "EnvelopeCoordinates", @"new [] { new [] { -45.0, 45.0 }, new [] { 45.0, -45.0 }}" },
			{ "CircleCoordinates", @"new [] { 45.0, -45.0 }" },
			{ "MultiPointCoordinates", @"new [] { new [] {38.897676, -77.03653}, new [] {38.889939, -77.009051} }" },
			{
				"MultiLineStringCoordinates", @"new[]
											{
												new [] { new [] { 2.0, 12.0 }, new [] { 2.0, 13.0 },new [] { 3.0, 13.0 }, new []{ 3.0, 12.0 } },
												new [] { new [] { 0.0, 10.0 }, new [] { 0.0, 11.0 },new [] { 1.0, 11.0 }, new []{ 1.0, 10.0 } },
												new [] { new [] { 0.2, 10.2 }, new [] { 0.2, 10.8 },new [] { 0.8, 10.8 }, new []{ 0.8, 12.0 } },
											}"
			},
			{
				"MultiPolygonCoordinates", @"new[]
											{
												new []
												{
													new []
													{
														new [] { 10.0, -17.0},
														new [] {15.0, 16.0},
														new [] {0.0, 12.0},
														new [] {-15.0, 16.0},
														new [] { -10.0, -17.0},
														new [] { 10.0, -17.0}
													},
													new []
													{
														new [] {8.2  , 18.2},
														new [] { 8.2 , -18.8},
														new [] { -8.8, -10.8},
														new [] {8.8  , 18.2}
													}
												},
												new []
												{
													new []
													{
														new [] { 8.0, -15.0},
														new [] {15.0, 16.0},
														new [] {0.0, 12.0},
														new [] {-15.0, 16.0},
														new [] { -10.0, -17.0},
														new [] { 8.0, -15.0}
													}
												}
											}"
			},
			{
				"PolygonCoordinates", @"new[]{
										new []{ new [] {10.0, -17.0}, new [] {15.0, 16.0}, new [] {0.0, 12.0}, new [] {-15.0, 16.0}, new [] {-10.0, -17.0},new [] {10.0, -17.0}},
										new []{ new [] {8.2, 18.2}, new [] {8.2, -18.8}, new [] {-8.8, -10.8}, new [] {8.8, 18.2}}
									}"
			},
			{ "LineStringCoordinates", @"new [] { new [] {38.897676, -77.03653}, new [] {38.889939, -77.009051} }" },
			{ "PointCoordinates", "new[] { 38.897676, -77.03653 }" },
			{
				"this._polygonCoordinates", @"new[]{
										new []{ new [] {10.0, -17.0}, new [] {15.0, 16.0}, new [] {0.0, 12.0}, new [] {-15.0, 16.0}, new [] {-10.0, -17.0},new [] {10.0, -17.0}},
										new []{ new [] {8.2, 18.2}, new [] {8.2, -18.8}, new [] {-8.8, -10.8}, new [] {8.8, 18.2}}
									}"
			},
			{ "ProjectFilterExpectedJson", "new {term = new {type = new {value = \"project\"}}}" },
			{ "_scriptScoreSource", "\"decayNumericLinear(params.origin, params.scale, params.offset, params.decay, doc['numberOfCommits'].value)\""}
		};

		private static readonly Regex LeadingSpacesAndAsterisk = new Regex(@"^(?<value>[ \t]*\*\s?).*", RegexOptions.Compiled);

		// TODO: Total Hack of replacements in anonymous types that represent json. This can be resolved by referencing tests assembly when building the dynamic assembly,

		// but might want to put doc generation at same directory level as Tests to reference project directly.

		private static readonly Regex TrailingMultiLineComment = new Regex(@"(?<value>\*\/[ \t]*)$", RegexOptions.Compiled);

		public static string PascalToHyphen(this string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return Regex.Replace(
					Regex.Replace(
						Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1-$2"), @"([a-z\d])([A-Z])", "$1-$2")
					, @"[-\s]+", "-", RegexOptions.Compiled)
				.TrimEnd('-')
				.ToLower();
		}

		public static string LowercaseHyphenToPascal(this string lowercaseHyphenatedInput) =>
			Regex.Replace(lowercaseHyphenatedInput.Replace("-", " "), @"\b([a-z])", m => m.Captures[0].Value.ToUpper());

		public static string TrimEnd(this string input, string trim)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return input.EndsWith(trim, StringComparison.OrdinalIgnoreCase)
				? input.Substring(0, input.Length - trim.Length)
				: input;
		}

		public static string RemoveLeadingAndTrailingMultiLineComments(this string input)
		{
			var match = LeadingMultiLineComment.Match(input);

			if (match.Success) input = input.Substring(match.Groups["value"].Value.Length);

			match = TrailingMultiLineComment.Match(input);

			if (match.Success) input = input.Substring(0, input.Length - match.Groups["value"].Value.Length);

			return input;
		}

		public static string RemoveLeadingSpacesAndAsterisk(this string input)
		{
			var match = LeadingSpacesAndAsterisk.Match(input);
			if (match.Success) input = input.Substring(match.Groups["value"].Value.Length);

			return input;
		}

		/// <summary>
		///  Removes the specified number of tabs (or spaces, assuming 4 spaces = 1 tab)
		///  from each line of the input
		/// </summary>
		public static string RemoveNumberOfLeadingTabsOrSpacesAfterNewline(this string input, int numberOfTabs)
		{
			var leadingCharacterIndex = input.IndexOf("\t", StringComparison.OrdinalIgnoreCase);

			if (leadingCharacterIndex == -1)
			{
				leadingCharacterIndex = input.IndexOf(" ", StringComparison.OrdinalIgnoreCase);

				if (leadingCharacterIndex == -1) return input;
			}

			var count = 0;
			var firstNonTabCharacter = char.MinValue;

			for (var i = leadingCharacterIndex; i < input.Length; i++)
			{
				if (input[i] != '\t' && input[i] != ' ')
				{
					firstNonTabCharacter = input[i];
					count = i - leadingCharacterIndex;
					break;
				}
			}

			if (firstNonTabCharacter == '{' && numberOfTabs != count) numberOfTabs = count;

			return Regex.Replace(
				Regex.Replace(
					input,
					$"(?<tabs>[\n|\r\n]+\t{{{numberOfTabs}}})",
					m => m.Value.Replace("\t", string.Empty)
				),
				$"(?<spaces>[\n|\r\n]+\\s{{{numberOfTabs * 4}}})",
				m => m.Value.Replace(" ", string.Empty)
			);
		}

		public static string[] SplitOnNewLines(this string input, StringSplitOptions options) => input.Split(new[] { "\r\n", "\n" }, options);

		public static bool TryGetJsonForExpressionSyntax(this string anonymousTypeString, out string json)
		{
			json = null;

			foreach (var substitution in Substitutions)
				anonymousTypeString = anonymousTypeString.Replace(substitution.Key, substitution.Value);

			var text =
				$@"
					using System;
                    using System.Collections.Generic;
					using System.ComponentModel;
					using System.Runtime.Serialization;
					using Newtonsoft.Json.Linq;
					using Newtonsoft.Json;

					namespace Temporary
					{{
						public class Json
						{{
							public string Write()
							{{
								var o = {anonymousTypeString};
								var json = JsonConvert.SerializeObject(o, Formatting.Indented);
								return json;
							}}
						}}
					}}";

			var syntaxTree = CSharpSyntaxTree.ParseText(text);
			var assemblyName = Path.GetRandomFileName();
			var references = new List<MetadataReference>
			{
				MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(JsonConvert).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(ITypedList).Assembly.Location),
			};
			var systemReferences = new[]
			{
				"System.Runtime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.ObjectModel, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Dynamic.Runtime, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Linq.Expressions, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"
			};
			foreach (var r in systemReferences)
			{
				var location = Assembly.Load(r).Location;
				references.Add(MetadataReference.CreateFromFile(location));
			}

			var compilation =
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

					var builder = new StringBuilder($"Unable to serialize the following C# anonymous type string to json: {anonymousTypeString}");
					foreach (var diagnostic in failures) builder.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
					builder.AppendLine(new string('-', 30));

					Console.Error.WriteLine(builder.ToString());
					return false;
				}

				ms.Seek(0, SeekOrigin.Begin);

				var assembly = Assembly.Load(ms.ToArray());
				var type = assembly.GetType("Temporary.Json");
				var obj = Activator.CreateInstance(type);

				var output = type.InvokeMember("Write",
					BindingFlags.Default | BindingFlags.InvokeMethod,
					null,
					obj,
					new object[] { });

				json = output.ToString();
				return true;
			}
		}

		public static string ReplaceArityWithGenericSignature(this string value)
		{
			var indexOfBackTick = value.IndexOf("`", StringComparison.Ordinal);

			if (indexOfBackTick == -1)
				return value;

			var arity = value[indexOfBackTick + 1];
			value = value.Substring(0, indexOfBackTick);

			return Enumerable.Range(1, int.Parse(arity.ToString()))
				.Aggregate(value + "<", (l, i) => l = l + (i == 1 ? "T" : $"T{i}")) + ">";
		}
	}
}
