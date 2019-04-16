using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The synonym_graph token filter allows to easily handle synonyms,
	/// including multi-word synonyms correctly during the analysis process.
	/// </summary>
	public interface ISynonymGraphTokenFilter : ITokenFilter
	{
		[DataMember(Name ="expand")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Expand { get; set; }

		[DataMember(Name ="format")]
		SynonymFormat? Format { get; set; }

		[DataMember(Name ="ignore_case")]
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? IgnoreCase { get; set; }

		/// <inheritdoc cref="ISynonymTokenFilter.Lenient" />
		[DataMember(Name ="lenient")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Lenient { get; set; }

		[DataMember(Name ="synonyms")]
		IEnumerable<string> Synonyms { get; set; }

		/// <summary>
		///  a path a synonyms file relative to the node's `config` location.
		/// </summary>
		[DataMember(Name ="synonyms_path")]
		string SynonymsPath { get; set; }

		[DataMember(Name ="tokenizer")]
		string Tokenizer { get; set; }
	}

	/// <inheritdoc />
	public class SynonymGraphTokenFilter : TokenFilterBase, ISynonymGraphTokenFilter
	{
		public SynonymGraphTokenFilter() : base("synonym_graph") { }

		/// <inheritdoc />
		public bool? Expand { get; set; }

		/// <inheritdoc />
		public SynonymFormat? Format { get; set; }

		/// <inheritdoc />
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc cref="ISynonymTokenFilter.Lenient" />
		public bool? Lenient { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Synonyms { get; set; }

		/// <inheritdoc />
		public string SynonymsPath { get; set; }

		/// <inheritdoc />
		public string Tokenizer { get; set; }
	}

	/// <inheritdoc />
	public class SynonymGraphTokenFilterDescriptor
		: TokenFilterDescriptorBase<SynonymGraphTokenFilterDescriptor, ISynonymGraphTokenFilter>, ISynonymGraphTokenFilter
	{
		protected override string Type => "synonym_graph";
		bool? ISynonymGraphTokenFilter.Expand { get; set; }
		SynonymFormat? ISynonymGraphTokenFilter.Format { get; set; }

		bool? ISynonymGraphTokenFilter.IgnoreCase { get; set; }
		bool? ISynonymGraphTokenFilter.Lenient { get; set; }

		IEnumerable<string> ISynonymGraphTokenFilter.Synonyms { get; set; }
		string ISynonymGraphTokenFilter.SynonymsPath { get; set; }
		string ISynonymGraphTokenFilter.Tokenizer { get; set; }

		/// <inheritdoc />
		[Obsolete("Will be removed in Elasticsearch 7.x, if you need to ignore case add a lowercase filter before this synonym filter")]
		public SynonymGraphTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(a => a.IgnoreCase = ignoreCase);

		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor Expand(bool? expand = true) => Assign(a => a.Expand = expand);

		/// <inheritdoc cref="ISynonymTokenFilter.Lenient" />
		public SynonymGraphTokenFilterDescriptor Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);


		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);

		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor SynonymsPath(string path) => Assign(a => a.SynonymsPath = path);

		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor Format(SynonymFormat? format) => Assign(a => a.Format = format);

		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor Synonyms(IEnumerable<string> synonymGraphs) => Assign(a => a.Synonyms = synonymGraphs);

		/// <inheritdoc />
		public SynonymGraphTokenFilterDescriptor Synonyms(params string[] synonymGraphs) => Assign(a => a.Synonyms = synonymGraphs);
	}
}
