using System;
using Nest;

namespace Tests.Analysis.Tokenizers
{
	using FuncTokenizer = Func<string, TokenizersDescriptor, IPromise<ITokenizers>>;

	public static class TokenizerTests
	{
		public class EdgeNGramTests : TokenizerAssertionBase<EdgeNGramTests>
		{
			protected override string Name => "endgen";

			protected override ITokenizer Initializer => new EdgeNGramTokenizer
			{
				MaxGram = 2,
				MinGram = 1,
				TokenChars = new[] {TokenChar.Digit, TokenChar.Letter}
			};

			protected override FuncTokenizer Fluent => (n, t) => t.EdgeNGram(n, e => e
				.MaxGram(2)
				.MinGram(1)
				.TokenChars(TokenChar.Digit, TokenChar.Letter)
			);

			protected override object Json => new
			{
				min_gram = 1,
				max_gram = 2,
				token_chars = new[] {"digit", "letter"},
				type = "edge_ngram"
			};
		}

		public class NGramTests : TokenizerAssertionBase<NGramTests>
		{
			protected override string Name => "ng";

			protected override ITokenizer Initializer => new NGramTokenizer
			{
				MaxGram = 2,
				MinGram = 1,
				TokenChars = new[] {TokenChar.Digit, TokenChar.Letter}
			};

			protected override FuncTokenizer Fluent => (n, t) => t.NGram(n, e => e
				.MaxGram(2)
				.MinGram(1)
				.TokenChars(TokenChar.Digit, TokenChar.Letter)
			);

			protected override object Json => new
			{
				min_gram = 1,
				max_gram = 2,
				token_chars = new[] {"digit", "letter"},
				type = "ngram"
			};
		}

		public class PathHierarchyTests : TokenizerAssertionBase<PathHierarchyTests>
		{
			protected override string Name => "path";

			protected override ITokenizer Initializer => new PathHierarchyTokenizer
			{
				BufferSize = 2048,
				Delimiter = '|',
				Replacement = '-',
				Reverse = true,
				Skip = 1
			};

			protected override FuncTokenizer Fluent => (n, t) => t.PathHierarchy(n, e => e
				.BufferSize(2048)
				.Delimiter('|')
				.Replacement('-')
				.Reverse()
				.Skip(1)
			);

			protected override object Json => new
			{
				delimiter = "|",
				replacement = "-",
				buffer_size = 2048,
				reverse = true,
				skip = 1,
				type = "path_hierarchy"
			};
		}

		public class IcuTests : TokenizerAssertionBase<IcuTests>
		{
			protected override string Name => "icu";
			private const string RuleFiles = "Latn:icu-files/KeywordTokenizer.rbbi";

			protected override ITokenizer Initializer => new IcuTokenizer
			{
				RuleFiles = RuleFiles,
			};

			protected override FuncTokenizer Fluent => (n, t) => t.Icu(n, e => e
				.RuleFiles(RuleFiles)
			);

			protected override object Json => new
			{
				rule_files = RuleFiles,
				type = "icu_tokenizer"
			};
		}

		public class KuromojiTests : TokenizerAssertionBase<KuromojiTests>
		{
			protected override string Name => "kuro";
			private const string Example = "/箱根山-箱根/成田空港-成田/";

			protected override ITokenizer Initializer => new KuromojiTokenizer
			{
				Mode = KuromojiTokenizationMode.Extended,
				DiscardPunctuation = true,
				NBestExamples = Example,
				NBestCost = 1000
			};

			protected override FuncTokenizer Fluent => (n, t) => t.Kuromoji(n, e => e
				.Mode(KuromojiTokenizationMode.Extended)
				.DiscardPunctuation()
				.NBestExamples(Example)
				.NBestCost(1000)
			);

			protected override object Json => new
			{
				discard_punctuation = true,
				mode = "extended",
				nbest_cost = 1000,
				nbest_examples = Example,
				type = "kuromoji_tokenizer"
			};
		}

		public class UaxTests : TokenizerAssertionBase<UaxTests>
		{
			protected override string Name => "uax";
			protected override ITokenizer Initializer => new UaxEmailUrlTokenizer {MaxTokenLength = 12};

			protected override FuncTokenizer Fluent => (n, t) => t.UaxEmailUrl(n, e => e
				.MaxTokenLength(12)
			);

			protected override object Json => new
			{
				max_token_length = 12,
				type = "uax_url_email"
			};
		}

		public class PatternTests : TokenizerAssertionBase<PatternTests>
		{
			protected override string Name => "pat";

			protected override ITokenizer Initializer => new PatternTokenizer
			{
				Flags = "CASE_INSENSITIVE",
				Group = 1,
				Pattern = @"\W+"
			};

			protected override FuncTokenizer Fluent => (n, t) => t.Pattern(n, e => e
				.Flags("CASE_INSENSITIVE")
				.Group(1)
				.Pattern(@"\W+")
			);

			protected override object Json => new
			{
				pattern = @"\W+",
				flags = "CASE_INSENSITIVE",
				group = 1,
				type = "pattern"
			};
		}

		public class WhitespaceTests : TokenizerAssertionBase<WhitespaceTests>
		{
			protected override string Name => "ws";
			protected override ITokenizer Initializer => new WhitespaceTokenizer();

			protected override FuncTokenizer Fluent => (n, t) => t.Whitespace(n);

			protected override object Json => new {type = "whitespace"};
		}

		public class StandardTests : TokenizerAssertionBase<StandardTests>
		{
			protected override string Name => "ws";
			protected override ITokenizer Initializer => new StandardTokenizer();

			protected override FuncTokenizer Fluent => (n, t) => t.Standard(n);

			protected override object Json => new {type = "standard"};
		}
	}
}
