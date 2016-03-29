using System.Collections.Generic;
using Newtonsoft.Json;

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
	///<inheritdoc/>
	public class SynonymTokenFilterDescriptor 
		: TokenFilterDescriptorBase<SynonymTokenFilterDescriptor, ISynonymTokenFilter>, ISynonymTokenFilter
	{
		protected override string Type => "synonym";

		bool? ISynonymTokenFilter.IgnoreCase { get; set; }
		bool? ISynonymTokenFilter.Expand { get; set; }
		string ISynonymTokenFilter.Tokenizer { get; set; }
		string ISynonymTokenFilter.SynonymsPath { get; set; }
		SynonymFormat? ISynonymTokenFilter.Format { get; set; }
		IEnumerable<string> ISynonymTokenFilter.Synonyms { get; set; }

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(a => a.IgnoreCase = ignoreCase);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor Expand(bool? expand = true) => Assign(a => a.Expand = expand);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor SynonymsPath(string path) => Assign(a => a.SynonymsPath = path);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor Format(SynonymFormat? format) => Assign(a => a.Format = format);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor Synonyms(IEnumerable<string> synonyms) => Assign(a => a.Synonyms = synonyms);

		///<inheritdoc/>
		public SynonymTokenFilterDescriptor Synonyms(params string[] synonyms) => Assign(a => a.Synonyms = synonyms);

	}

}