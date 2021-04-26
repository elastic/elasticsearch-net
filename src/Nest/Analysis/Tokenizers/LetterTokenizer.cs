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
	/// A tokenizer of type letter that divides text at non-letters. That’s to say, it defines tokens as maximal strings of adjacent letters.
	/// <para>
	/// Note, this does a decent job for most European languages, but does a terrible job for some Asian languages, where words are not
	/// separated by spaces.
	/// </para>
	/// </summary>
	public interface ILetterTokenizer : ITokenizer { }

	/// <inheritdoc />
	public class LetterTokenizer : TokenizerBase, ILetterTokenizer
	{
		public LetterTokenizer() => Type = "letter";
	}

	/// <inheritdoc />
	public class LetterTokenizerDescriptor
		: TokenizerDescriptorBase<LetterTokenizerDescriptor, ILetterTokenizer>, ILetterTokenizer
	{
		protected override string Type => "letter";
	}
}
