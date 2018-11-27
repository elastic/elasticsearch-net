using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// An analyzer of type pattern that can flexibly separate text into terms via a regular expression.
	/// </summary>
	public interface IPatternAnalyzer : IAnalyzer
	{
		[DataMember(Name ="flags")]
		string Flags { get; set; }

		[DataMember(Name ="lowercase")]
		bool? Lowercase { get; set; }

		[DataMember(Name ="pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to an empty list
		/// </summary>
		[DataMember(Name ="stopwords")]
		[JsonConverter(typeof(StopWordsJsonConverter))]
		StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class PatternAnalyzer : AnalyzerBase, IPatternAnalyzer
	{
		public PatternAnalyzer() : base("pattern") { }

		public string Flags { get; set; }

		public bool? Lowercase { get; set; }

		public string Pattern { get; set; }

		public StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class PatternAnalyzerDescriptor : AnalyzerDescriptorBase<PatternAnalyzerDescriptor, IPatternAnalyzer>, IPatternAnalyzer
	{
		protected override string Type => "pattern";
		string IPatternAnalyzer.Flags { get; set; }
		bool? IPatternAnalyzer.Lowercase { get; set; }
		string IPatternAnalyzer.Pattern { get; set; }

		StopWords IPatternAnalyzer.StopWords { get; set; }

		public PatternAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(a => a.StopWords = stopWords);

		public PatternAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());

		public PatternAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(a => a.StopWords = stopWords);

		public PatternAnalyzerDescriptor Pattern(string pattern) => Assign(a => a.Pattern = pattern);

		public PatternAnalyzerDescriptor Flags(string flags) => Assign(a => a.Flags = flags);

		public PatternAnalyzerDescriptor Lowercase(bool? lowercase = true) => Assign(a => a.Lowercase = lowercase);
	}
}
