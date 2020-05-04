// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A filter that stems words using a Snowball-generated stemmer.
	/// </summary>
	public interface ISnowballTokenFilter : ITokenFilter
	{
		[DataMember(Name ="language")]
		SnowballLanguage? Language { get; set; }
	}

	/// <inheritdoc />
	public class SnowballTokenFilter : TokenFilterBase, ISnowballTokenFilter
	{
		public SnowballTokenFilter() : base("snowball") { }

		/// <inheritdoc />
		[DataMember(Name ="language")]
		public SnowballLanguage? Language { get; set; }
	}

	/// <inheritdoc />
	public class SnowballTokenFilterDescriptor
		: TokenFilterDescriptorBase<SnowballTokenFilterDescriptor, ISnowballTokenFilter>, ISnowballTokenFilter
	{
		protected override string Type => "snowball";

		SnowballLanguage? ISnowballTokenFilter.Language { get; set; }

		/// <inheritdoc />
		public SnowballTokenFilterDescriptor Language(SnowballLanguage? language) => Assign(language, (a, v) => a.Language = v);
	}
}
