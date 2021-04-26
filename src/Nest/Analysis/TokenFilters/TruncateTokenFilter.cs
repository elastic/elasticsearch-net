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
	/// The truncate token filter can be used to truncate tokens into a specific length. This can come in handy with keyword (single token)
	/// <para> based mapped fields that are used for sorting in order to reduce memory usage.</para>
	/// </summary>
	public interface ITruncateTokenFilter : ITokenFilter
	{
		/// <summary>
		/// length parameter which control the number of characters to truncate to, defaults to 10.
		/// </summary>
		[DataMember(Name ="length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Length { get; set; }
	}

	/// <inheritdoc />
	public class TruncateTokenFilter : TokenFilterBase, ITruncateTokenFilter
	{
		public TruncateTokenFilter() : base("truncate") { }

		/// <inheritdoc />
		public int? Length { get; set; }
	}

	/// <inheritdoc />
	public class TruncateTokenFilterDescriptor
		: TokenFilterDescriptorBase<TruncateTokenFilterDescriptor, ITruncateTokenFilter>, ITruncateTokenFilter
	{
		protected override string Type => "truncate";

		int? ITruncateTokenFilter.Length { get; set; }

		/// <inheritdoc />
		public TruncateTokenFilterDescriptor Length(int? length) => Assign(length, (a, v) => a.Length = v);
	}
}
