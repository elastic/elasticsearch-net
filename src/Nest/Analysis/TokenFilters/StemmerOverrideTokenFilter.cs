using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Overrides stemming algorithms, by applying a custom mapping, then protecting these terms from being modified by stemmers. Must be placed before any stemming filters.
	/// </summary>
	public interface IStemmerOverrideTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of mapping rules to use.
		/// </summary>
		[JsonProperty("rules")]
		IEnumerable<string> Rules { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a list of mappings.
		/// </summary>
		[JsonProperty("rules_path")]
		string RulesPath { get; set; }
	}
	/// <inheritdoc/>
	public class StemmerOverrideTokenFilter : TokenFilterBase, IStemmerOverrideTokenFilter
	{
		public StemmerOverrideTokenFilter() : base("stemmer_override") { }

		/// <inheritdoc/>
		public IEnumerable<string> Rules { get; set; }

		/// <inheritdoc/>
		public string RulesPath { get; set; }

	}
	///<inheritdoc/>
	public class StemmerOverrideTokenFilterDescriptor 
		: TokenFilterDescriptorBase<StemmerOverrideTokenFilterDescriptor, IStemmerOverrideTokenFilter>, IStemmerOverrideTokenFilter
	{
		protected override string Type => "stemmer_override";

		IEnumerable<string> IStemmerOverrideTokenFilter.Rules { get; set; }
		string IStemmerOverrideTokenFilter.RulesPath { get; set; }

		///<inheritdoc/>
		public StemmerOverrideTokenFilterDescriptor Rules(IEnumerable<string> rules) => Assign(a => a.Rules = rules);

		///<inheritdoc/>
		public StemmerOverrideTokenFilterDescriptor Rules(params string[] rules) => Assign(a => a.Rules = rules);

		///<inheritdoc/>
		public StemmerOverrideTokenFilterDescriptor RulesPath(string path) => Assign(a => a.RulesPath = path);

	}

}