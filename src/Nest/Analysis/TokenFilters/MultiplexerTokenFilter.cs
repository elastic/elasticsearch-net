// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type multiplexer will emit multiple tokens at the same position, each version of the token having
	/// been run through a different filter. Identical output tokens at the same position will be removed.
	/// </summary>
	public interface IMultiplexerTokenFilter : ITokenFilter
	{
		[DataMember(Name ="filters")]
		IEnumerable<string> Filters { get; set; }

		[DataMember(Name ="preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }
	}

	public class MultiplexerTokenFilter : TokenFilterBase, IMultiplexerTokenFilter
	{
		internal const string TokenType = "multiplexer";

		public MultiplexerTokenFilter() : base(TokenType) { }

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters" />
		public IEnumerable<string> Filters { get; set; }

		/// <inheritdoc cref="IMultiplexerTokenFilter.PreserveOriginal" />
		public bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc cref="IMultiplexerTokenFilter" />
	public class MultiplexerTokenFilterDescriptor
		: TokenFilterDescriptorBase<MultiplexerTokenFilterDescriptor, IMultiplexerTokenFilter>, IMultiplexerTokenFilter
	{
		protected override string Type => MultiplexerTokenFilter.TokenType;

		IEnumerable<string> IMultiplexerTokenFilter.Filters { get; set; }
		bool? IMultiplexerTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters" />
		public MultiplexerTokenFilterDescriptor Filters(IEnumerable<string> filters) => Assign(filters, (a, v) => a.Filters = v);

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters" />
		public MultiplexerTokenFilterDescriptor Filters(params string[] filters) => Assign(filters, (a, v) => a.Filters = v);

		/// <inheritdoc cref="IMultiplexerTokenFilter.PreserveOriginal" />
		public MultiplexerTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(preserve, (a, v) => a.PreserveOriginal = v);
	}
}
