// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression.
	/// </summary>
	public interface IPatternTokenizer : ITokenizer
	{
		/// <summary>
		/// The regular expression flags.
		/// </summary>
		[DataMember(Name = "flags")]
		string Flags { get; set; }

		/// <summary>
		/// Which group to extract into tokens. Defaults to -1 (split).
		/// </summary>
		[DataMember(Name = "group")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Group { get; set; }

		/// <summary>
		/// The regular expression pattern, defaults to \W+.
		/// </summary>
		[DataMember(Name = "pattern")]
		string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class PatternTokenizer : TokenizerBase, IPatternTokenizer
	{
		public PatternTokenizer() => Type = "pattern";

		/// <inheritdoc />
		public string Flags { get; set; }

		/// <summary />
		public int? Group { get; set; }

		/// <inheritdoc />
		public string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class PatternTokenizerDescriptor
		: TokenizerDescriptorBase<PatternTokenizerDescriptor, IPatternTokenizer>, IPatternTokenizer
	{
		protected override string Type => "pattern";
		string IPatternTokenizer.Flags { get; set; }

		int? IPatternTokenizer.Group { get; set; }
		string IPatternTokenizer.Pattern { get; set; }

		/// <inheritdoc />
		public PatternTokenizerDescriptor Group(int? group) => Assign(group, (a, v) => a.Group = v);

		/// <inheritdoc />
		public PatternTokenizerDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);

		/// <inheritdoc />
		public PatternTokenizerDescriptor Flags(string flags) => Assign(flags, (a, v) => a.Flags = v);
	}
}
