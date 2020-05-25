// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
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
