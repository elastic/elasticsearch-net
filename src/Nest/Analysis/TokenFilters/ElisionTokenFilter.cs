// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter which removes elisions. For example, “l’avion” (the plane) will tokenized as “avion” (plane).
	/// </summary>
	public interface IElisionTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Accepts articles setting which is a set of stop words articles
		/// </summary>
		[DataMember(Name = "articles")]
		IEnumerable<string> Articles { get; set; }

		/// <summary>
		/// Whether articles should be handled case-insensitively. Defaults to <c>false</c>.
		/// </summary>
		[DataMember(Name = "articles_case")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? ArticlesCase { get; set; }
	}

	/// <inheritdoc cref="IElisionTokenFilter" />
	public class ElisionTokenFilter : TokenFilterBase, IElisionTokenFilter
	{
		public ElisionTokenFilter() : base("elision") { }

		/// <inheritdoc />
		public IEnumerable<string> Articles { get; set; }

		/// <inheritdoc />
		public bool? ArticlesCase { get; set; }
	}

	/// <inheritdoc cref="IElisionTokenFilter" />
	public class ElisionTokenFilterDescriptor
		: TokenFilterDescriptorBase<ElisionTokenFilterDescriptor, IElisionTokenFilter>, IElisionTokenFilter
	{
		protected override string Type => "elision";

		IEnumerable<string> IElisionTokenFilter.Articles { get; set; }
		bool? IElisionTokenFilter.ArticlesCase { get; set; }

		/// <inheritdoc cref="IElisionTokenFilter.Articles"/>
		public ElisionTokenFilterDescriptor Articles(IEnumerable<string> articles) => Assign(articles, (a, v) => a.Articles = v);

		/// <inheritdoc cref="IElisionTokenFilter.Articles"/>
		public ElisionTokenFilterDescriptor Articles(params string[] articles) => Assign(articles, (a, v) => a.Articles = v);

		/// <inheritdoc cref="IElisionTokenFilter.ArticlesCase"/>
		public ElisionTokenFilterDescriptor ArticlesCase(bool? articlesCase = true) => Assign(articlesCase, (a, v) => a.ArticlesCase = v);
	}
}
