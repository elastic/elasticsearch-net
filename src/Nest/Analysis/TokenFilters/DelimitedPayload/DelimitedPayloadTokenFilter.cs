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

namespace Nest
{
	/// <summary>
	/// Splits tokens into tokens and payload whenever a delimiter character is found.
	/// </summary>
	public interface IDelimitedPayloadTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Character used for splitting the tokens.
		/// </summary>
		[DataMember(Name ="delimiter")]
		char? Delimiter { get; set; }

		/// <summary>
		/// The type of the payload. int for integer, float for float and identity for characters.
		/// </summary>
		[DataMember(Name ="encoding")]
		DelimitedPayloadEncoding? Encoding { get; set; }
	}

	/// <inheritdoc />
	public class DelimitedPayloadTokenFilter : TokenFilterBase, IDelimitedPayloadTokenFilter
	{
		public DelimitedPayloadTokenFilter() : base("delimited_payload") { }

		/// <inheritdoc />
		public char? Delimiter { get; set; }

		/// <inheritdoc />
		public DelimitedPayloadEncoding? Encoding { get; set; }
	}

	/// <inheritdoc />
	public class DelimitedPayloadTokenFilterDescriptor
		: TokenFilterDescriptorBase<DelimitedPayloadTokenFilterDescriptor, IDelimitedPayloadTokenFilter>, IDelimitedPayloadTokenFilter
	{
		protected override string Type => "delimited_payload";

		char? IDelimitedPayloadTokenFilter.Delimiter { get; set; }
		DelimitedPayloadEncoding? IDelimitedPayloadTokenFilter.Encoding { get; set; }

		/// <inheritdoc />
		public DelimitedPayloadTokenFilterDescriptor Delimiter(char? delimiter) => Assign(delimiter, (a, v) => a.Delimiter = v);

		/// <inheritdoc />
		public DelimitedPayloadTokenFilterDescriptor Encoding(DelimitedPayloadEncoding? encoding) => Assign(encoding, (a, v) => a.Encoding = v);
	}
}
