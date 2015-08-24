using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// The synonym token filter allows to easily handle synonyms during the analysis process. 
	/// </summary>
	public interface ISynonymTokenFilter : ITokenFilter
	{
		/// <summary>
		///  a path a synonyms file relative to the node's `config` location.
		/// </summary>
		[JsonProperty("synonyms_path")]
		string SynonymsPath { get; set; }

		[JsonProperty("format")]
		SynonymFormat? Format { get; set; }

		[JsonProperty("synonyms")]
		IEnumerable<string> Synonyms { get; set; }

		[JsonProperty("ignore_case")]
		bool? IgnoreCase { get; set; }

		[JsonProperty("expand")]
		bool? Expand { get; set; }

		[JsonProperty("tokenizer")]
		string Tokenizer { get; set; }
	}

	/// <inheritdoc/>
	public class SynonymTokenFilter : TokenFilterBase, ISynonymTokenFilter
	{
		public SynonymTokenFilter() : base("synonym") { }

		/// <inheritdoc/>
		public string SynonymsPath { get; set; }

		/// <inheritdoc/>
		public SynonymFormat? Format { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> Synonyms { get; set; }

		/// <inheritdoc/>
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc/>
		public bool? Expand { get; set; }

		/// <inheritdoc/>
		public string Tokenizer { get; set; }
	}
}
