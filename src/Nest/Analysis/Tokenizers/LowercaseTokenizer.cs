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

namespace Nest
{
	/// <summary>
	/// A tokenizer of type lowercase that performs the function of Letter Tokenizer and Lower Case Token Filter together.
	/// <para>It divides text at non-letters and converts them to lower case. </para>
	/// <para>While it is functionally equivalent to the combination of Letter Tokenizer and Lower Case Token Filter, </para>
	/// <para>there is a performance advantage to doing the two tasks at once, hence this (redundant) implementation.</para>
	/// </summary>
	public interface ILowercaseTokenizer : ITokenizer { }

	/// <inheritdoc />
	public class LowercaseTokenizer : TokenizerBase, ILowercaseTokenizer
	{
		public LowercaseTokenizer() => Type = "lowercase";
	}

	/// <inheritdoc />
	public class LowercaseTokenizerDescriptor
		: TokenizerDescriptorBase<LowercaseTokenizerDescriptor, ILowercaseTokenizer>, ILowercaseTokenizer
	{
		protected override string Type => "lowercase";
	}
}
