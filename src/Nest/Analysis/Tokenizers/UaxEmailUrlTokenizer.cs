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
	/// A tokenizer of type uax_url_email which works exactly like the standard tokenizer, but tokenizes emails and urls as single tokens
	/// </summary>
	public interface IUaxEmailUrlTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }
	}

	/// <summary />
	public class UaxEmailUrlTokenizer : TokenizerBase, IUaxEmailUrlTokenizer
	{
		public UaxEmailUrlTokenizer() => Type = "uax_url_email";

		/// <summary />
		public int? MaxTokenLength { get; set; }
	}

	/// <summary />
	public class UaxEmailUrlTokenizerDescriptor
		: TokenizerDescriptorBase<UaxEmailUrlTokenizerDescriptor, IUaxEmailUrlTokenizer>, IUaxEmailUrlTokenizer
	{
		protected override string Type => "uax_url_email";

		int? IUaxEmailUrlTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc />
		public UaxEmailUrlTokenizerDescriptor MaxTokenLength(int? maxLength) => Assign(maxLength, (a, v) => a.MaxTokenLength = v);
	}
}
