// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The fingerprint analyzer implements a fingerprinting algorithm which
	/// is used by the OpenRefine project to assist in clustering.
	/// </summary>
	public interface IFingerprintAnalyzer : IAnalyzer
	{
		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_output_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxOutputSize { get; set; }

		/// <summary>
		/// If true, emits both the original and folded version of tokens
		/// that contain extended characters. Defaults to false
		/// </summary>
		[DataMember(Name ="preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }

		/// <summary>
		/// The character that separates the tokens after concatenation. Defaults to a space.
		/// </summary>
		[DataMember(Name ="separator")]
		string Separator { get; set; }

		/// <summary>
		/// A list of stop words to use. Defaults to an empty list
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// A path(either relative to config location, or absolute) to a stopwords
		/// file configuration.Each stop word should be in its own "line"
		/// (separated by a line break). The file must be UTF-8 encoded.
		/// </summary>
		[DataMember(Name ="stopwords_path")]
		string StopWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintAnalyzer : AnalyzerBase, IFingerprintAnalyzer
	{
		public FingerprintAnalyzer() : base("fingerprint") { }

		public int? MaxOutputSize { get; set; }

		public bool? PreserveOriginal { get; set; }

		public string Separator { get; set; }

		public StopWords StopWords { get; set; }

		public string StopWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintAnalyzerDescriptor : AnalyzerDescriptorBase<FingerprintAnalyzerDescriptor, IFingerprintAnalyzer>, IFingerprintAnalyzer
	{
		protected override string Type => "fingerprint";
		int? IFingerprintAnalyzer.MaxOutputSize { get; set; }
		bool? IFingerprintAnalyzer.PreserveOriginal { get; set; }

		string IFingerprintAnalyzer.Separator { get; set; }
		StopWords IFingerprintAnalyzer.StopWords { get; set; }
		string IFingerprintAnalyzer.StopWordsPath { get; set; }

		public FingerprintAnalyzerDescriptor Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);

		public FingerprintAnalyzerDescriptor MaxOutputSize(int? maxOutputSize) => Assign(maxOutputSize, (a, v) => a.MaxOutputSize = v);

		public FingerprintAnalyzerDescriptor PreserveOriginal(bool? preserveOriginal = true) => Assign(preserveOriginal, (a, v) => a.PreserveOriginal = v);

		public FingerprintAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public FingerprintAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public FingerprintAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public FingerprintAnalyzerDescriptor StopWordsPath(string stopWordsPath) => Assign(stopWordsPath, (a, v) => a.StopWordsPath = v);
	}
}
