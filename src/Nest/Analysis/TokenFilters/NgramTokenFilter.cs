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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type nGram.
	/// </summary>
	public interface INGramTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Defaults to 2
		/// </summary>
		[DataMember(Name ="max_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxGram { get; set; }

		/// <summary>
		/// Defaults to 1.
		/// </summary>
		[DataMember(Name ="min_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinGram { get; set; }

		/// <summary>
		/// Emits original token when set to `true`. Defaults to `false`.
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenFilter : TokenFilterBase, INGramTokenFilter
	{
		public NGramTokenFilter() : base("ngram") { }

		/// <inheritdoc />
		public int? MaxGram { get; set; }

		/// <inheritdoc />
		public int? MinGram { get; set; }

		/// <inheritdoc />
		public bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenFilterDescriptor
		: TokenFilterDescriptorBase<NGramTokenFilterDescriptor, INGramTokenFilter>, INGramTokenFilter
	{
		protected override string Type => "ngram";
		int? INGramTokenFilter.MaxGram { get; set; }

		int? INGramTokenFilter.MinGram { get; set; }

		bool? INGramTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc cref="INGramTokenFilter.MinGram" />
		public NGramTokenFilterDescriptor MinGram(int? minGram) => Assign(minGram, (a, v) => a.MinGram = v);

		/// <inheritdoc cref="INGramTokenFilter.MaxGram" />
		public NGramTokenFilterDescriptor MaxGram(int? maxGram) => Assign(maxGram, (a, v) => a.MaxGram = v);

		/// <inheritdoc cref="INGramTokenFilter.PreserveOriginal" />
		public NGramTokenFilterDescriptor PreserveOriginal(bool? preserveOriginal = true) =>
			Assign(preserveOriginal, (a, v) => a.PreserveOriginal = v);
	}
}
