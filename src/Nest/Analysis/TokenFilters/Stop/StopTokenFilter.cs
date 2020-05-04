// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type stop that removes stop words from token streams.
	/// </summary>
	public interface IStopTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Set to true to lower case all words first. Defaults to false.
		/// </summary>
		[DataMember(Name ="ignore_case")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? IgnoreCase { get; set; }

		/// <summary>
		/// Set to false in order to not ignore the last term of a search if it is a stop word.
		/// This is very useful for  the completion suggester as a query like green a can
		/// be extended to green apple even though  you remove stop words in general. Defaults to true.
		/// </summary>
		[DataMember(Name ="remove_trailing")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? RemoveTrailing { get; set; }

		/// <summary>
		/// A list of stop words to use. Defaults to `_english_` stop words.
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords
		/// file configuration. Each stop word should be in its own "line"
		/// (separated by a line break). The file must be UTF-8 encoded.
		/// </summary>
		[DataMember(Name ="stopwords_path")]
		string StopWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopTokenFilter : TokenFilterBase, IStopTokenFilter
	{
		public StopTokenFilter() : base("stop") { }

		/// <inheritdoc />
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc />
		public bool? RemoveTrailing { get; set; }

		/// <inheritdoc />
		public StopWords StopWords { get; set; }

		/// <inheritdoc />
		public string StopWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopTokenFilterDescriptor
		: TokenFilterDescriptorBase<StopTokenFilterDescriptor, IStopTokenFilter>, IStopTokenFilter
	{
		protected override string Type => "stop";

		bool? IStopTokenFilter.IgnoreCase { get; set; }
		bool? IStopTokenFilter.RemoveTrailing { get; set; }
		StopWords IStopTokenFilter.StopWords { get; set; }
		string IStopTokenFilter.StopWordsPath { get; set; }

		/// <inheritdoc />
		public StopTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(ignoreCase, (a, v) => a.IgnoreCase = v);

		/// <inheritdoc />
		public StopTokenFilterDescriptor RemoveTrailing(bool? removeTrailing = true) => Assign(removeTrailing, (a, v) => a.RemoveTrailing = v);

		/// <inheritdoc />
		public StopTokenFilterDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		/// <inheritdoc />
		public StopTokenFilterDescriptor StopWords(IEnumerable<string> stopWords) => Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		/// <inheritdoc />
		public StopTokenFilterDescriptor StopWords(params string[] stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		/// <inheritdoc />
		public StopTokenFilterDescriptor StopWordsPath(string path) => Assign(path, (a, v) => a.StopWordsPath = v);
	}
}
