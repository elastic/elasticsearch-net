// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	///  A token filter of type lowercase that normalizes token text to lower case.
	/// <para> Lowercase token filter supports Greek and Turkish lowercase token filters through the language parameter.</para>
	/// </summary>
	public interface ILowercaseTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Lowercase token filter supports Greek, Irish, and Turkish lowercase token filters through the language parameter
		/// </summary>
		[DataMember(Name ="language")]
		string Language { get; set; }
	}

	/// <inheritdoc />
	public class LowercaseTokenFilter : TokenFilterBase, ILowercaseTokenFilter
	{
		public LowercaseTokenFilter() : base("lowercase") { }

		/// <inheritdoc />
		public string Language { get; set; }
	}

	/// <inheritdoc />
	public class LowercaseTokenFilterDescriptor
		: TokenFilterDescriptorBase<LowercaseTokenFilterDescriptor, ILowercaseTokenFilter>, ILowercaseTokenFilter
	{
		protected override string Type => "lowercase";

		string ILowercaseTokenFilter.Language { get; set; }

		/// <inheritdoc />
		public LowercaseTokenFilterDescriptor Language(string language) => Assign(language, (a, v) => a.Language = v);
	}
}
