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
	/// Limits the number of tokens that are indexed per document and field.
	/// </summary>
	public interface ILimitTokenCountTokenFilter : ITokenFilter
	{
		/// <summary>
		/// If set to true the filter exhaust the stream even if max_token_count tokens have been consumed already.
		/// </summary>
		[DataMember(Name ="consume_all_tokens")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? ConsumeAllTokens { get; set; }

		/// <summary>
		/// The maximum number of tokens that should be indexed per document and field.
		/// </summary>
		[DataMember(Name ="max_token_count")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenCount { get; set; }
	}

	/// <inheritdoc />
	public class LimitTokenCountTokenFilter : TokenFilterBase, ILimitTokenCountTokenFilter
	{
		public LimitTokenCountTokenFilter() : base("limit") { }

		/// <inheritdoc />
		public bool? ConsumeAllTokens { get; set; }

		/// <inheritdoc />
		public int? MaxTokenCount { get; set; }
	}

	/// <inheritdoc />
	public class LimitTokenCountTokenFilterDescriptor
		: TokenFilterDescriptorBase<LimitTokenCountTokenFilterDescriptor, ILimitTokenCountTokenFilter>, ILimitTokenCountTokenFilter
	{
		protected override string Type => "limit";
		bool? ILimitTokenCountTokenFilter.ConsumeAllTokens { get; set; }

		int? ILimitTokenCountTokenFilter.MaxTokenCount { get; set; }

		/// <inheritdoc />
		public LimitTokenCountTokenFilterDescriptor ConsumeAllToken(bool? consumeAllTokens = true) =>
			Assign(consumeAllTokens, (a, v) => a.ConsumeAllTokens = v);

		/// <inheritdoc />
		public LimitTokenCountTokenFilterDescriptor MaxTokenCount(int? maxTokenCount) => Assign(maxTokenCount, (a, v) => a.MaxTokenCount = v);
	}
}
