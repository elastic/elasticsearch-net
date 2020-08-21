// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
