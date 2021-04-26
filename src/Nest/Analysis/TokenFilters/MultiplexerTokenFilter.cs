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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
