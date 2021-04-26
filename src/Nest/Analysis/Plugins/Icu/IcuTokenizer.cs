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
	/// Tokenizes text into words on word boundaries, as defined in UAX #29: Unicode Text Segmentation. It behaves much
	/// like the standard tokenizer, but adds better support for some Asian languages by using a dictionary-based approach
	/// to identify words in Thai, Lao, Chinese, Japanese, and Korean, and using custom rules to break Myanmar and Khmer
	/// text into syllables.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuTokenizer : ITokenizer
	{
		/// <summary>
		/// You can customize the icu-tokenizer behavior by specifying per-script rule files,
		/// see the RBBI rules syntax reference for a more detailed explanation.
		/// </summary>
		[DataMember(Name ="rule_files")]
		string RuleFiles { get; set; }
	}

	/// <inheritdoc />
	public class IcuTokenizer : TokenizerBase, IIcuTokenizer
	{
		public IcuTokenizer() => Type = "icu_tokenizer";

		/// <inheritdoc />
		public string RuleFiles { get; set; }
	}

	/// <inheritdoc />
	public class IcuTokenizerDescriptor
		: TokenizerDescriptorBase<IcuTokenizerDescriptor, IIcuTokenizer>, IIcuTokenizer
	{
		protected override string Type => "icu_tokenizer";

		string IIcuTokenizer.RuleFiles { get; set; }

		/// <inheritdoc />
		public IcuTokenizerDescriptor RuleFiles(string ruleFiles) => Assign(ruleFiles, (a, v) => a.RuleFiles = v);
	}
}
