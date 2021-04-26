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
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The fingerprint token filter emits a single token which is useful
	/// for fingerprinting a body of text, and/or providing a token that can be
	/// clustered on. It does this by sorting the tokens, deduplicating and
	/// then concatenating them back into a single token.
	/// </summary>
	public interface IFingerprintTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_output_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxOutputSize { get; set; }

		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		[DataMember(Name ="separator")]
		string Separator { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintTokenFilter : TokenFilterBase, IFingerprintTokenFilter
	{
		public FingerprintTokenFilter() : base("fingerprint") { }

		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		public int? MaxOutputSize { get; set; }

		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		public string Separator { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintTokenFilterDescriptor
		: TokenFilterDescriptorBase<FingerprintTokenFilterDescriptor, IFingerprintTokenFilter>, IFingerprintTokenFilter
	{
		protected override string Type => "fingerprint";
		int? IFingerprintTokenFilter.MaxOutputSize { get; set; }

		string IFingerprintTokenFilter.Separator { get; set; }

		public FingerprintTokenFilterDescriptor Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);

		public FingerprintTokenFilterDescriptor MaxOutputSize(int? maxOutputSize) => Assign(maxOutputSize, (a, v) => a.MaxOutputSize = v);
	}
}
