using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Tokenizers
{
	using FuncTokenizer = Func<string, TokenizersDescriptor, IPromise<ITokenizers>>;

	public static class TokenizerTests
	{
		public class EdgeNGramTests : TokenizerAssertionBase<EdgeNGramTests>
		{
			public override string Name => "endgen";

			public override ITokenizer Initializer => new EdgeNGramTokenizer
			{
				MaxGram = 2,
				MinGram = 1,
				TokenChars = new[] {TokenChar.Digit, TokenChar.Letter}
			};

			public override FuncTokenizer Fluent => (n, t) => t.EdgeNGram(n, e => e
				.MaxGram(2)
				.MinGram(1)
				.TokenChars(TokenChar.Digit, TokenChar.Letter)
			);

			public override object Json => new
			{
				min_gram = 1,
				max_gram = 2,
				token_chars = new[] {"digit", "letter"},
				type = "edge_ngram"
			};
		}

		public class NGramTests : TokenizerAssertionBase<NGramTests>
		{
			public override string Name => "ng";

			public override ITokenizer Initializer => new NGramTokenizer
			{
				MaxGram = 2,
				MinGram = 1,
				TokenChars = new[] {TokenChar.Digit, TokenChar.Letter}
			};

			public override FuncTokenizer Fluent => (n, t) => t.NGram(n, e => e
				.MaxGram(2)
				.MinGram(1)
				.TokenChars(TokenChar.Digit, TokenChar.Letter)
			);

			public override object Json => new
			{
				min_gram = 1,
				max_gram = 2,
				token_chars = new[] {"digit", "letter"},
				type = "ngram"
			};
		}

		public class PathHierarchyTests : TokenizerAssertionBase<PathHierarchyTests>
		{
			public override string Name => "path";

			public override ITokenizer Initializer => new PathHierarchyTokenizer
			{
				BufferSize = 2048,
				Delimiter = '|',
				Replacement = '-',
				Reverse = true,
				Skip = 1
			};

			public override FuncTokenizer Fluent => (n, t) => t.PathHierarchy(n, e => e
				.BufferSize(2048)
				.Delimiter('|')
				.Replacement('-')
				.Reverse()
				.Skip(1)
			);

			public override object Json => new
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
			public override string Name => "icu";
			private const string RuleFiles = "Latn:icu-files/KeywordTokenizer.rbbi";

			public override ITokenizer Initializer => new IcuTokenizer
			{
				RuleFiles = RuleFiles,
			};

			public override FuncTokenizer Fluent => (n, t) => t.Icu(n, e => e
				.RuleFiles(RuleFiles)
			);

			public override object Json => new
			{
				rule_files = RuleFiles,
				type = "icu_tokenizer"
			};
		}

		public class KuromojiTests : TokenizerAssertionBase<KuromojiTests>
		{
			public override string Name => "kuro";
			private const string Example = "/箱根山-箱根/成田空港-成田/";

			public override ITokenizer Initializer => new KuromojiTokenizer
			{
				Mode = KuromojiTokenizationMode.Extended,
				DiscardPunctuation = true,
				NBestExamples = Example,
				NBestCost = 1000
			};

			public override FuncTokenizer Fluent => (n, t) => t.Kuromoji(n, e => e
				.Mode(KuromojiTokenizationMode.Extended)
				.DiscardPunctuation()
				.NBestExamples(Example)
				.NBestCost(1000)
			);

			public override object Json => new
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
			public override string Name => "uax";
			public override ITokenizer Initializer => new UaxEmailUrlTokenizer {MaxTokenLength = 12};

			public override FuncTokenizer Fluent => (n, t) => t.UaxEmailUrl(n, e => e
				.MaxTokenLength(12)
			);

			public override object Json => new
			{
				max_token_length = 12,
				type = "uax_url_email"
			};
		}

		public class PatternTests : TokenizerAssertionBase<PatternTests>
		{
			public override string Name => "pat";

			public override ITokenizer Initializer => new PatternTokenizer
			{
				Flags = "CASE_INSENSITIVE",
				Group = 1,
				Pattern = @"\W+"
			};

			public override FuncTokenizer Fluent => (n, t) => t.Pattern(n, e => e
				.Flags("CASE_INSENSITIVE")
				.Group(1)
				.Pattern(@"\W+")
			);

			public override object Json => new
			{
				pattern = @"\W+",
				flags = "CASE_INSENSITIVE",
				group = 1,
				type = "pattern"
			};
		}

		public class WhitespaceTests : TokenizerAssertionBase<WhitespaceTests>
		{
			public override string Name => "ws";
			public override ITokenizer Initializer => new WhitespaceTokenizer();

			public override FuncTokenizer Fluent => (n, t) => t.Whitespace(n);

			public override object Json => new {type = "whitespace"};
		}

		public class StandardTests : TokenizerAssertionBase<StandardTests>
		{
			public override string Name => "stan";
			public override ITokenizer Initializer => new StandardTokenizer();

			public override FuncTokenizer Fluent => (n, t) => t.Standard(n);

			public override object Json => new {type = "standard"};
		}

		[SkipVersion("<6.4.0", "analysis-nori plugin introduced in 6.4.0")]
		public class NoriTests : TokenizerAssertionBase<NoriTests>
		{
			public override string Name => "nori";
			public override ITokenizer Initializer => new NoriTokenizer
			{
				DecompoundMode = NoriDecompoundMode.Mixed
			};

			public override FuncTokenizer Fluent => (n, t) => t.Nori(n, e => e
				.DecompoundMode(NoriDecompoundMode.Mixed)
			);

			public override object Json => new {type = "nori_tokenizer", decompound_mode = "mixed"};
		}
	}
}
