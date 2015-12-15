using System;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.Tokenizers
{
	/**
	 */

	public class TokenizerUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
		protected override object ExpectJson => new
		{
			analysis = new
			{
				tokenizer = new
				{
					endgen = new
					{
						min_gram = 1,
						max_gram = 2,
						token_chars = new[] { "digit", "letter" },
						type = "edge_ngram"
					},
					ng = new
					{
						min_gram = 1,
						max_gram = 2,
						token_chars = new[] { "digit", "letter" },
						type = "ngram"
					},
					path = new
					{
						delimiter = "|",
						replacement = "-",
						buffer_size = 2048,
						reverse = true,
						skip = 1,
						type = "path_hierarchy"
					},
					pattern = new
					{
						pattern = @"\W+",
						flags = "CASE_INSENSITIVE",
						group = 1,
						type = "pattern"
					},
					standard = new
					{
						type = "standard"
					},
					uax = new
					{
						max_token_length = 12,
						type = "uax_url_email"
					},
					whitespace = new
					{
						type = "whitespace"
					}
				}
			}
		};

		/**
		 * 
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;
		public static Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> FluentExample => s => s
			.Analysis(analysis => analysis
				.Tokenizers(tokenizer => tokenizer
					.EdgeNGram("endgen", t => t
						.MaxGram(2)
						.MinGram(1)
						.TokenChars(TokenChar.Digit, TokenChar.Letter)
					)
					.NGram("ng", t => t
						.MaxGram(2)
						.MinGram(1)
						.TokenChars(TokenChar.Digit, TokenChar.Letter)
					)
					.PathHierarchy("path", t => t
						.BufferSize(2048)
						.Delimiter('|')
						.Replacement('-')
						.Reverse()
						.Skip(1)
					)
					.Pattern("pattern", t => t
						.Flags("CASE_INSENSITIVE")
						.Group(1)
						.Pattern(@"\W+")
					)
					.Standard("standard")
					.UaxEmailUrl("uax", t => t.MaxTokenLength(12))
					.Whitespace("whitespace")
				)
			);

		/**
		 */
		protected override IndexSettings Initializer => InitializerExample;
		public static IndexSettings InitializerExample =>
			new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Tokenizers = new Nest.Tokenizers
					{
							{ "endgen", new EdgeNGramTokenizer
							{
								MaxGram = 2,
								MinGram = 1,
								TokenChars = new [] { TokenChar.Digit, TokenChar.Letter }
							}},
							{ "ng", new NGramTokenizer
							{
								MaxGram = 2,
								MinGram = 1,
								TokenChars = new [] { TokenChar.Digit, TokenChar.Letter }
							}},
							{ "path", new PathHierarchyTokenizer
							{
								BufferSize = 2048,
								Delimiter = '|',
								Replacement = '-',
								Reverse = true,
								Skip = 1
							} },
							{ "pattern", new PatternTokenizer
							{
								Flags = "CASE_INSENSITIVE",
								Group = 1,
								Pattern = @"\W+"
							} },
							{ "standard", new StandardTokenizer() },
							{ "uax", new UaxEmailUrlTokenizer { MaxTokenLength = 12 } },
							{ "whitespace", new WhitespaceTokenizer() },
					}
				}
			};
	}
}
