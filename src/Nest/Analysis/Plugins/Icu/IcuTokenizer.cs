using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Tokenizes text into words on word boundaries, as defined in UAX #29: Unicode Text Segmentation. It behaves much
	/// like the standard tokenizer, but adds better support for some Asian languages by using a dictionary-based approach
	/// to identify words in Thai, Lao, Chinese, Japanese, and Korean, and using custom rules to break Myanmar and Khmer
	/// text into syllables.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuTokenizer : ITokenizer
	{
		/// <summary>
		/// You can customize the icu-tokenizer behavior by specifying per-script rule files,
		/// see the RBBI rules syntax reference for a more detailed explanation.
		/// </summary>
		[JsonProperty("rule_files")]
		string RuleFiles { get; set; }
	}

	/// <inheritdoc/>
	public class IcuTokenizer : TokenizerBase, IIcuTokenizer
    {
		public IcuTokenizer() { Type = "icu_tokenizer"; }

		/// <inheritdoc/>
		public string RuleFiles { get; set; }
    }

	/// <inheritdoc/>
	public class IcuTokenizerDescriptor
		: TokenizerDescriptorBase<IcuTokenizerDescriptor, IIcuTokenizer>, IIcuTokenizer
	{
		protected override string Type => "icu_tokenizer";

		string IIcuTokenizer.RuleFiles { get; set; }

		/// <inheritdoc/>
		public IcuTokenizerDescriptor RuleFiles(string ruleFiles) => Assign(a => a.RuleFiles = ruleFiles);
	}
}
