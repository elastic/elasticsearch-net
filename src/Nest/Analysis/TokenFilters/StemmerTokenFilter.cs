// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A filter that stems words (similar to snowball, but with more options).
	/// </summary>
	public interface IStemmerTokenFilter : ITokenFilter
	{
		[DataMember(Name ="language")]
		string Language { get; set; }
	}

	public class StemmerTokenFilter : TokenFilterBase, IStemmerTokenFilter
	{
		public StemmerTokenFilter() : base("stemmer") { }

		public string Language { get; set; }
	}

	/// <inheritdoc />
	public class StemmerTokenFilterDescriptor
		: TokenFilterDescriptorBase<StemmerTokenFilterDescriptor, IStemmerTokenFilter>, IStemmerTokenFilter
	{
		protected override string Type => "stemmer";

		string IStemmerTokenFilter.Language { get; set; }

		/// <inheritdoc />
		public StemmerTokenFilterDescriptor Language(string language) => Assign(language, (a, v) => a.Language = v);
	}
}
