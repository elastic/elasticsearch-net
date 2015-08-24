using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The pattern_replace char filter allows the use of a regex to manipulate the characters in a string before analysis. 
	/// </summary>
	public interface IPatternReplaceCharFilter : ICharFilter
	{
		[JsonProperty("pattern")]
		string Pattern { get; set; }

		[JsonProperty("replacement")]
		string Replacement { get; set; }
	}
	
	public class PatternReplaceCharFilter : CharFilterBase, ICharFilter
	{
		public PatternReplaceCharFilter() : base("pattern_replace") { }

		public string Pattern { get; set; }

		public string Replacement { get; set; }
	}
	public class PatternReplaceCharFilterDescriptor 
		: CharFilterDescriptorBase<PatternReplaceCharFilterDescriptor, IPatternReplaceCharFilter>, IPatternReplaceCharFilter
	{
		protected override string Type => "pattern_replace";
		string IPatternReplaceCharFilter.Pattern { get; set; }
		string IPatternReplaceCharFilter.Replacement { get; set; }

		public PatternReplaceCharFilterDescriptor Pattern(string pattern) =>
			Assign(a => a.Pattern = pattern);

		public PatternReplaceCharFilterDescriptor Replacement(string replacement) =>
			Assign(a => a.Replacement = replacement);

	}
}
