using Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers;
using NUnit.Framework;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Indices.Analysis.Analyzers
{
	[TestFixture]
	public class AnalyzerTests : BaseAnalysisTests
	{
		[Test]
		public void CustomAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("myCustom", new CustomAnalyzer
					{
						Tokenizer = "myTokenizer",
						Filter = new string[] { "myTokenFilter1", "myTokenFilter2" },
						CharFilter = new string[] { "my_html" },
						Alias = new string[] { "alias1", "alias2" }
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void KeywordAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("keyword", new KeywordAnalyzer())));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void LanguageAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("arabic", new LanguageAnalyzer(Language.Arabic)
					{
						StopWords = new string[] { "foo" },
						StemExclusionList = new string[] { "bar" },
						StopwordsPath = "/path/to/stopwords"
					})));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void PatternAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("whitespace", new PatternAnalyzer
					{
						Pattern = "\\\\s+",
						Lowercase = false,
						Flags = "g"
					})));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void SimpleAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("simple", new SimpleAnalyzer())));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void SnowballAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("snowball", new SnowballAnalyzer
					{
						Language = "English",
						StopWords = "these,are,some,stopwords"
					})));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void StandardAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("standard", new StandardAnalyzer())));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void StopAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("stop", new StopAnalyzer
					{
						StopWords = new string[] { "these","are","some","stopwords"},
						StopwordsPath = "/path/to/stopwords"
					})));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void WhitespaceAnalyzerTest()
		{
			var result = this.Analysis(a => a
				.Analyzers(aa => aa.Add("whitespace", new WhitespaceAnalyzer())));

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
