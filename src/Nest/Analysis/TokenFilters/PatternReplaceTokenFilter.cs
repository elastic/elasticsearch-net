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
	/// The pattern_replace token filter allows to easily handle string replacements based on a regular expression.
	/// </summary>
	public interface IPatternReplaceTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The flags for the regular expression
		/// </summary>
		[DataMember(Name ="flags")]
		string Flags { get; set; }

		/// <summary>
		/// The regular expression
		/// </summary>
		[DataMember(Name ="pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// Replacement string
		/// </summary>
		[DataMember(Name ="replacement")]
		string Replacement { get; set; }
	}

	/// <inheritdoc />
	public class PatternReplaceTokenFilter : TokenFilterBase, IPatternReplaceTokenFilter
	{
		public PatternReplaceTokenFilter() : base("pattern_replace") { }

		/// <inheritdoc />
		public string Flags { get; set; }

		/// <inheritdoc />
		public string Pattern { get; set; }

		/// <inheritdoc />
		public string Replacement { get; set; }
	}

	/// <inheritdoc />
	public class PatternReplaceTokenFilterDescriptor
		: TokenFilterDescriptorBase<PatternReplaceTokenFilterDescriptor, IPatternReplaceTokenFilter>, IPatternReplaceTokenFilter
	{
		protected override string Type => "pattern_replace";

		string IPatternReplaceTokenFilter.Pattern { get; set; }
		string IPatternReplaceTokenFilter.Replacement { get; set; }
		string IPatternReplaceTokenFilter.Flags { get; set; }

		/// <inheritdoc cref="IPatternReplaceTokenFilter.Flags" />
		public PatternReplaceTokenFilterDescriptor Flags(string flags) => Assign(flags, (a, v) => a.Flags = v);

		/// <inheritdoc cref="IPatternReplaceTokenFilter.Pattern" />
		public PatternReplaceTokenFilterDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		/// <inheritdoc cref="IPatternReplaceTokenFilter.Replacement" />
		public PatternReplaceTokenFilterDescriptor Replacement(string replacement) => Assign(replacement, (a, v) => a.Replacement = v);
	}
}
