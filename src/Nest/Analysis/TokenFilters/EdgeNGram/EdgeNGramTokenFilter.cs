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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type edgeNGram.
	/// </summary>
	public interface IEdgeNGramTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Defaults to 2.
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
		/// Either front or back. Defaults to front.
		/// </summary>
		[DataMember(Name ="side")]
		EdgeNGramSide? Side { get; set; }

		/// <summary>
		/// Emits original token when set to <c>true</c>. Defaults to <c>false</c>.
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class EdgeNGramTokenFilter : TokenFilterBase, IEdgeNGramTokenFilter
	{
		public EdgeNGramTokenFilter() : base("edge_ngram") { }

		/// <inheritdoc />
		public int? MaxGram { get; set; }

		/// <inheritdoc />
		public int? MinGram { get; set; }

		/// <inheritdoc />
		public EdgeNGramSide? Side { get; set; }

		/// <inheritdoc />
		public bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class EdgeNGramTokenFilterDescriptor
		: TokenFilterDescriptorBase<EdgeNGramTokenFilterDescriptor, IEdgeNGramTokenFilter>, IEdgeNGramTokenFilter
	{
		protected override string Type => "edge_ngram";
		int? IEdgeNGramTokenFilter.MaxGram { get; set; }
		int? IEdgeNGramTokenFilter.MinGram { get; set; }
		EdgeNGramSide? IEdgeNGramTokenFilter.Side { get; set; }
		bool? IEdgeNGramTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc cref="IEdgeNGramTokenFilter.MinGram" />
		public EdgeNGramTokenFilterDescriptor MinGram(int? minGram) => Assign(minGram, (a, v) => a.MinGram = v);

		/// <inheritdoc cref="IEdgeNGramTokenFilter.MaxGram" />
		public EdgeNGramTokenFilterDescriptor MaxGram(int? maxGram) => Assign(maxGram, (a, v) => a.MaxGram = v);

		/// <inheritdoc cref="IEdgeNGramTokenFilter.Side" />
		public EdgeNGramTokenFilterDescriptor Side(EdgeNGramSide? side) => Assign(side, (a, v) => a.Side = v);

		/// <inheritdoc cref="IEdgeNGramTokenFilter.PreserveOriginal" />
		public EdgeNGramTokenFilterDescriptor PreserveOriginal(bool? preserveOriginal = true) =>
			Assign(preserveOriginal, (a, v) => a.PreserveOriginal = v);
	}
}
