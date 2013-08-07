using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A set of analyzers aimed at analyzing specific language text. 
	/// </summary>
	public class LanguageAnalyzer : AnalyzerBase
	{
		public LanguageAnalyzer(Language language)
		{
			language.ThrowIfNull("language");
			var name = Enum.GetName(typeof (Language), language);
			if (name == null)
				language.ThrowIfNull("language");
			
			var langName = name.ToLowerInvariant();
			Type = langName;
		}

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
		[JsonProperty("stopwords")]
		public IEnumerable<string> StopWords { get; set; }

		[JsonProperty("stem_exclusion ")]
		public IEnumerable<string> StemExclusionList { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords file configuration.
		/// </summary>
		[JsonProperty("stopwords_path")]
		public string StopwordsPath { get; set; }
	}
}