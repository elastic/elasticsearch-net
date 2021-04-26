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
	/// A token filter of type asciifolding that converts alphabetic, numeric, and symbolic Unicode characters which are
	/// <para> not in the first 127 ASCII characters (the “Basic Latin” Unicode block) into their ASCII equivalents, if one exists.</para>
	/// </summary>
	public interface IAsciiFoldingTokenFilter : ITokenFilter
	{
		[DataMember(Name ="preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class AsciiFoldingTokenFilter : TokenFilterBase, IAsciiFoldingTokenFilter
	{
		public AsciiFoldingTokenFilter() : base("asciifolding") { }

		public bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class AsciiFoldingTokenFilterDescriptor
		: TokenFilterDescriptorBase<AsciiFoldingTokenFilterDescriptor, IAsciiFoldingTokenFilter>, IAsciiFoldingTokenFilter
	{
		protected override string Type => "asciifolding";

		bool? IAsciiFoldingTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc />
		public AsciiFoldingTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(preserve, (a, v) => a.PreserveOriginal = v);
	}
}
