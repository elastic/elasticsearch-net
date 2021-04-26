/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type standard that is built of using Standard Tokenizer, with Standard Token Filter, Lower Case Token Filter, and Stop Token
	/// Filter.
	/// </summary>
	public interface IStandardAnalyzer : IAnalyzer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class StandardAnalyzer : AnalyzerBase, IStandardAnalyzer
	{
		public StandardAnalyzer() : base("standard") { }

		/// <inheritdoc />
		public int? MaxTokenLength { get; set; }

		/// <inheritdoc />
		public StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class StandardAnalyzerDescriptor : AnalyzerDescriptorBase<StandardAnalyzerDescriptor, IStandardAnalyzer>, IStandardAnalyzer
	{
		protected override string Type => "standard";
		int? IStandardAnalyzer.MaxTokenLength { get; set; }

		StopWords IStandardAnalyzer.StopWords { get; set; }

		public StandardAnalyzerDescriptor StopWords(params string[] stopWords) =>
			Assign(stopWords, (a, v) => a.StopWords = v);

		public StandardAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public StandardAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public StandardAnalyzerDescriptor MaxTokenLength(int? maxTokenLength) =>
			Assign(maxTokenLength, (a, v) => a.MaxTokenLength = v);
	}
}
