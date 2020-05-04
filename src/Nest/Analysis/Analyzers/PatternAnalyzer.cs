// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type pattern that can flexibly separate text into terms via a regular expression.
	/// </summary>
	public interface IPatternAnalyzer : IAnalyzer
	{
		[DataMember(Name ="flags")]
		string Flags { get; set; }

		[DataMember(Name ="lowercase")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Lowercase { get; set; }

		[DataMember(Name ="pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// A list of stopword to initialize the stop filter with. Defaults to an empty list
		/// </summary>
		[DataMember(Name ="stopwords")]
		StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class PatternAnalyzer : AnalyzerBase, IPatternAnalyzer
	{
		public PatternAnalyzer() : base("pattern") { }

		public string Flags { get; set; }

		public bool? Lowercase { get; set; }

		public string Pattern { get; set; }

		public StopWords StopWords { get; set; }
	}

	/// <inheritdoc />
	public class PatternAnalyzerDescriptor : AnalyzerDescriptorBase<PatternAnalyzerDescriptor, IPatternAnalyzer>, IPatternAnalyzer
	{
		protected override string Type => "pattern";
		string IPatternAnalyzer.Flags { get; set; }
		bool? IPatternAnalyzer.Lowercase { get; set; }
		string IPatternAnalyzer.Pattern { get; set; }

		StopWords IPatternAnalyzer.StopWords { get; set; }

		public PatternAnalyzerDescriptor StopWords(params string[] stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public PatternAnalyzerDescriptor StopWords(IEnumerable<string> stopWords) =>
			Assign(stopWords.ToListOrNullIfEmpty(), (a, v) => a.StopWords = v);

		public PatternAnalyzerDescriptor StopWords(StopWords stopWords) => Assign(stopWords, (a, v) => a.StopWords = v);

		public PatternAnalyzerDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		public PatternAnalyzerDescriptor Flags(string flags) => Assign(flags, (a, v) => a.Flags = v);

		public PatternAnalyzerDescriptor Lowercase(bool? lowercase = true) => Assign(lowercase, (a, v) => a.Lowercase = v);
	}
}
