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
	/// An analyzer of type snowball that uses the standard tokenizer, with standard filter, lowercase filter, stop filter, and snowball filter.
	/// <para>
	/// The Snowball Analyzer is a stemming analyzer from Lucene that is originally based on the snowball project from
	/// snowball.tartarus.org.
	/// </para>
	/// </summary>
	public interface ISnowballAnalyzer : IAnalyzer
	{
		[DataMember(Name ="language")]
		SnowballLanguage? Language { get; set; }

		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class SnowballAnalyzer : AnalyzerBase, ISnowballAnalyzer
	{
		public SnowballAnalyzer() : base("snowball") { }

		public SnowballLanguage? Language { get; set; }

		public StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class SnowballAnalyzerDescriptor : AnalyzerDescriptorBase<SnowballAnalyzerDescriptor, ISnowballAnalyzer>, ISnowballAnalyzer
	{
		protected override string Type => "snowball";
		SnowballLanguage? ISnowballAnalyzer.Language { get; set; }

		StopWords ISnowballAnalyzer.StopWords { get; set; }

		public SnowballAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public SnowballAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) => Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public SnowballAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public SnowballAnalyzerDescriptor Language(SnowballLanguage? language) => Assign(language, (a, v) => a.Language = v);
	}
}
