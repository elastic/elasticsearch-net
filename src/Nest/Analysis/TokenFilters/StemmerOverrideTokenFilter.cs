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

namespace Nest
{
	/// <summary>
	/// Overrides stemming algorithms, by applying a custom mapping, then protecting these terms from being modified by stemmers. Must be placed
	/// before any stemming filters.
	/// </summary>
	public interface IStemmerOverrideTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of mapping rules to use.
		/// </summary>
		[DataMember(Name ="rules")]
		IEnumerable<string> Rules { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a list of mappings.
		/// </summary>
		[DataMember(Name ="rules_path")]
		string RulesPath { get; set; }
	}

	/// <inheritdoc />
	public class StemmerOverrideTokenFilter : TokenFilterBase, IStemmerOverrideTokenFilter
	{
		public StemmerOverrideTokenFilter() : base("stemmer_override") { }

		/// <inheritdoc />
		public IEnumerable<string> Rules { get; set; }

		/// <inheritdoc />
		public string RulesPath { get; set; }
	}

	/// <inheritdoc />
	public class StemmerOverrideTokenFilterDescriptor
		: TokenFilterDescriptorBase<StemmerOverrideTokenFilterDescriptor, IStemmerOverrideTokenFilter>, IStemmerOverrideTokenFilter
	{
		protected override string Type => "stemmer_override";

		IEnumerable<string> IStemmerOverrideTokenFilter.Rules { get; set; }
		string IStemmerOverrideTokenFilter.RulesPath { get; set; }

		/// <inheritdoc />
		public StemmerOverrideTokenFilterDescriptor Rules(IEnumerable<string> rules) => Assign(rules, (a, v) => a.Rules = v);

		/// <inheritdoc />
		public StemmerOverrideTokenFilterDescriptor Rules(params string[] rules) => Assign(rules, (a, v) => a.Rules = v);

		/// <inheritdoc />
		public StemmerOverrideTokenFilterDescriptor RulesPath(string path) => Assign(path, (a, v) => a.RulesPath = v);
	}
}
