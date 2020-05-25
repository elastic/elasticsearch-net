// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The synonym token filter allows to easily handle synonyms during the analysis process.
	/// </summary>
	public interface ISynonymTokenFilter : ITokenFilter
	{
		[DataMember(Name ="expand")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Expand { get; set; }

		[DataMember(Name ="format")]
		SynonymFormat? Format { get; set; }

		/// <summary>
		/// If `true` ignores exceptions while parsing the synonym configuration. It is important
		/// to note that only those synonym rules which cannot get parsed are ignored.
		/// </summary>
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

		/// <summary>
		/// Whether this token filter can reload changes to synonym files
		/// on demand.
		/// Marking as updateable means this component is only usable at search time
		/// </summary>
		/// <remarks>
		/// Supported in Elasticsearch 7.3.0+
		/// </remarks>
		[DataMember(Name = "updateable")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Updateable { get; set; }
	}

	/// <inheritdoc />
	public class SynonymTokenFilter : TokenFilterBase, ISynonymTokenFilter
	{
		public SynonymTokenFilter() : base("synonym") { }

		/// <inheritdoc />
		public bool? Expand { get; set; }

		/// <inheritdoc />
		public SynonymFormat? Format { get; set; }

		/// <inheritdoc cref="ISynonymTokenFilter.Lenient" />
		public bool? Lenient { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Synonyms { get; set; }

		/// <inheritdoc />
		public string SynonymsPath { get; set; }

		/// <inheritdoc />
		public string Tokenizer { get; set; }

		/// <inheritdoc />
		public bool? Updateable { get; set; }
	}

	/// <inheritdoc />
	public class SynonymTokenFilterDescriptor
		: TokenFilterDescriptorBase<SynonymTokenFilterDescriptor, ISynonymTokenFilter>, ISynonymTokenFilter
	{
		protected override string Type => "synonym";
		bool? ISynonymTokenFilter.Expand { get; set; }
		SynonymFormat? ISynonymTokenFilter.Format { get; set; }
		bool? ISynonymTokenFilter.Lenient { get; set; }
		IEnumerable<string> ISynonymTokenFilter.Synonyms { get; set; }
		string ISynonymTokenFilter.SynonymsPath { get; set; }
		string ISynonymTokenFilter.Tokenizer { get; set; }
		bool? ISynonymTokenFilter.Updateable { get; set; }

		/// <inheritdoc cref="ISynonymTokenFilter.Expand"/>
		public SynonymTokenFilterDescriptor Expand(bool? expand = true) => Assign(expand, (a, v) => a.Expand = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Lenient" />
		public SynonymTokenFilterDescriptor Lenient(bool? lenient = true) => Assign(lenient, (a, v) => a.Lenient = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Tokenizer"/>
		public SynonymTokenFilterDescriptor Tokenizer(string tokenizer) => Assign(tokenizer, (a, v) => a.Tokenizer = v);

		/// <inheritdoc cref="ISynonymTokenFilter.SynonymsPath"/>
		public SynonymTokenFilterDescriptor SynonymsPath(string path) => Assign(path, (a, v) => a.SynonymsPath = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Format"/>
		public SynonymTokenFilterDescriptor Format(SynonymFormat? format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Synonyms"/>
		public SynonymTokenFilterDescriptor Synonyms(IEnumerable<string> synonyms) => Assign(synonyms, (a, v) => a.Synonyms = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Synonyms"/>
		public SynonymTokenFilterDescriptor Synonyms(params string[] synonyms) => Assign(synonyms, (a, v) => a.Synonyms = v);

		/// <inheritdoc cref="ISynonymTokenFilter.Updateable"/>
		public SynonymTokenFilterDescriptor Updateable(bool? updateable = true) => Assign(updateable, (a, v) => a.Updateable = v);
	}
}
