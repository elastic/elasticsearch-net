using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Property;

namespace Tests
{
	public class TokenFiltersTests
	{
		/**
		 */

		public class Usage : GeneralUsageBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
		{
			protected override object ExpectJson => new
			{
			};

			/**
			 * 
			 */
			protected override Func<IndexSettingsDescriptor, IIndexSettings> Fluent => s => s
				.Analysis(analysis => analysis
					.TokenFilters(tf => tf
						.AsciiFolding("myAscii", t => t.PreserveOriginal())
						.CommonGrams("myCommonGrams", t => t
							.CommonWords("x", "y", "z")
							.IgnoreCase()
							.QueryMode()
						)
						.DelimitedPayload("mydp", t => t
							.Delimiter('-')
							.Encoding(DelimitedPayloadEncoding.Identity)
						)
						.DictionaryDecompounder("dcc", t => t
							.MaxSubwordSize(2)
							.MinSubwordSize(2)
							.MinWordSize(2)
							.OnlyLongestMatch()
							.WordList("x", "y", "z")
						)
						.EdgeNGram("etf", t => t
							.MaxGram(2)
							.MinGram(1)
						)
						.Elision("elision", t => t
							.Articles("a", "b", "c")
						)
						.Hunspell("hunspell", t => t
							.Dedup()
							.Dictionary("path_to_dict")
							.IgnoreCase()
							.Locale("en")
							.LongestOnly()
						)
						.HyphenationDecompounder("hypdecomp", t => t
							.MaxSubwordSize(2)
							.MinSubwordSize(2)
							.MinWordSize(2)
							.OnlyLongestMatch()
							.WordList("x", "y", "z")
						)
						.KeepTypes("keeptypes", t => t
							.Types("<NUM>", "<SOMETHINGELSE>")
						)
						.KeepWords("keepwords", t => t
							.KeepWords("a", "b", "c")
							.KeepWordsCase()
						)
						.KeywordMarker("marker", t => t
							.IgnoreCase()
							.Keywords("a", "b")
						)
						.KeywordRepeat("repeat")
						.KStem("kstem")
						.Length("length", t => t
							.Max(200)
							.Min(10)
						)
						.LimitTokenCount("limit", t => t
							.ConsumeAllToken()
							.MaxTokenCount(12)
						)
						.Lowercase("lc")
						.NGram("ngram", t => t
							.MinGram(3)
							.MaxGram(30)
						)
						.PatternCapture("pc", t => t
							.Patterns(@"\d", @"\w")
							.PreserveOriginal()
						)
						.PatternReplace("pr", t => t
							.Pattern(@"(\d|\w)")
							.Replacement("replacement")
						)
						.Phonetic("phone", t => t
							.Encoder(PhoneticEncoder.Beidermorse)
							.Replace()
						)
						.PorterStem("porter")
						.Reverse("rev")
						.Shingle("shing", t => t
							.FillerToken("x")
							.MaxShingleSize(12)
							.MinShingleSize(8)
							.OutputUnigrams()
							.OutputUnigramsIfNoShingles()
							.TokenSeparator("|")
						)
						.Snowball("snow", t => t.Language(SnowballLanguage.Dutch))
						.Standard("standard")
						.Stemmer("stem", t => t.Language("arabic"))
						.StemmerOverride("stemo", t => t.RulesPath("analysis/custom_stems.txt"))
						.Stop("stop", t => t
							.IgnoreCase()
							.RemoveTrailing()
							.StopWords("x", "y", "z")
						)
						.Synonym("syn", t => t
							.Expand()
							.Format(SynonymFormat.WordNet)
							.IgnoreCase()
							.SynonymsPath("analysis/stopwords.txt")
							.Synonyms("x=>y", "z=>s")
							.Tokenizer("whitespace")
						)
						.Trim("trimmer")
						.Truncate("truncer", t => t.Length(100))
						.Unique("uq", t => t.OnlyOnSamePosition())
						.Uppercase("upper")
						.WordDelimiter("wd", t => t
							.CatenateAll()
							.CatenateNumbers()
							.CatenateWords()
							.GenerateNumberParts()
							.GenerateWordParts()
							.PreserveOriginal()
							.ProtectedWords("x", "y", "z")
							.SplitOnCaseChange()
							.SplitOnNumerics()
							.StemEnglishPossessive()
						)
					)
				);

			/**
			 */
			protected override IndexSettings Initializer =>
				new IndexSettings
				{
					Analysis = new Analysis
					{
					}
				};
		}
	}
}
