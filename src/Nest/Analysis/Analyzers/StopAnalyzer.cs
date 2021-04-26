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

namespace Nest
{
	/// <summary>
	/// An analyzer of type stop that is built using a Lower Case Tokenizer, with Stop Token Filter.
	/// </summary>
	public interface IStopAnalyzer : IAnalyzer
	{
		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to the english stop words.
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a stopwords file configuration.
		/// </summary>
		[DataMember(Name ="stopwords_path")]
		string StopwordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopAnalyzer : AnalyzerBase, IStopAnalyzer
	{
		public StopAnalyzer() : base("stop") { }

		/// <inheritdoc />
		public StopWords StopWords { get; set; }

		/// <inheritdoc />
		public string StopwordsPath { get; set; }
	}

	/// <inheritdoc />
	public class StopAnalyzerDescriptor : AnalyzerDescriptorBase<StopAnalyzerDescriptor, IStopAnalyzer>, IStopAnalyzer
	{
		protected override string Type => "stop";

		StopWords IStopAnalyzer.StopWords { get; set; }
		string IStopAnalyzer.StopwordsPath { get; set; }

		public StopAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public StopAnalyzerDescriptor StopwordsPath(string path) => Assign(path, (a, v) => a.StopwordsPath = v);
	}
}
