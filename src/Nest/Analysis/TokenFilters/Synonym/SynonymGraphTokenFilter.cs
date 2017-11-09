using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The synonym_graph token filter allows to easily handle synonyms,
	/// including multi-word synonyms correctly during the analysis process.
	/// </summary>
	public interface ISynonymGraphTokenFilter : ITokenFilter
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
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		bool? IgnoreCase { get; set; }

		[JsonProperty("expand")]
		bool? Expand { get; set; }

		[JsonProperty("tokenizer")]
		string Tokenizer { get; set; }
	}

	/// <inheritdoc/>
	public class SynonymGraphTokenFilter : TokenFilterBase, ISynonymGraphTokenFilter
	{
		public SynonymGraphTokenFilter() : base("synonym_graph") { }

		/// <inheritdoc/>
		public string SynonymsPath { get; set; }

		/// <inheritdoc/>
		public SynonymFormat? Format { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> Synonyms { get; set; }

		/// <inheritdoc/>
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc/>
		public bool? Expand { get; set; }

		/// <inheritdoc/>
		public string Tokenizer { get; set; }
	}
	///<inheritdoc/>
	public class SynonymGraphTokenFilterDescriptor
		: TokenFilterDescriptorBase<SynonymGraphTokenFilterDescriptor, ISynonymGraphTokenFilter>, ISynonymGraphTokenFilter
	{
		protected override string Type => "synonym_graph";

		bool? ISynonymGraphTokenFilter.IgnoreCase { get; set; }
		bool? ISynonymGraphTokenFilter.Expand { get; set; }
		string ISynonymGraphTokenFilter.Tokenizer { get; set; }
		string ISynonymGraphTokenFilter.SynonymsPath { get; set; }
		SynonymFormat? ISynonymGraphTokenFilter.Format{ get; set; }

		IEnumerable<string> ISynonymGraphTokenFilter.Synonyms { get; set; }

		///<inheritdoc/>
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		public SynonymGraphTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(a => a.IgnoreCase = ignoreCase);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor Expand(bool? expand = true) => Assign(a => a.Expand = expand);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor SynonymsPath(string path) => Assign(a => a.SynonymsPath = path);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor Format(SynonymFormat? format) => Assign(a => a.Format = format);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor Synonyms(IEnumerable<string> synonymGraphs) => Assign(a => a.Synonyms = synonymGraphs);

		///<inheritdoc/>
		public SynonymGraphTokenFilterDescriptor Synonyms(params string[] synonymGraphs) => Assign(a => a.Synonyms = synonymGraphs);

	}

}
