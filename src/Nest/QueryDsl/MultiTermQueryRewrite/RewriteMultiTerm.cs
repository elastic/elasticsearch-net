// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Multi term query rewrite method
	/// </summary>
	[StringEnum]
	public enum RewriteMultiTerm
	{
		/// <summary>
		///  A rewrite method that performs like constant_score_boolean when there are few matching terms and otherwise
		///  visits all matching terms in sequence and marks documents for that term. Matching documents are assigned a
		///  constant score equal to the query’s boost.
		/// </summary>
		[EnumMember(Value = "constant_score")]
		ConstantScore,

		/// <summary>
		/// A rewrite method that first translates each term into a should clause in a boolean query, and keeps the scores
		///  as computed by the query. Note that typically such scores are meaningless to the user, and require non-trivial
		///  CPU to compute. This rewrite method will hit too many
		///  clauses failure if it exceeds the boolean query limit (defaults to 1024).
		/// </summary>
		[EnumMember(Value = "scoring_boolean")]
		ScoringBoolean,

		/// <summary>
		/// Similar to scoring_boolean except scores are not computed. Instead, each matching document receives a constant
		///  score equal to the query’s boost. This rewrite method will hit too many clauses failure if it exceeds the
		/// boolean query limit (defaults to 1024).
		/// </summary>
		[EnumMember(Value = "constant_score_boolean")]
		ConstantScoreBoolean,

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, and keeps the scores
		/// as computed by the query. This rewrite method only uses the top scoring terms so it will not overflow boolean
		///  max clause count. The N controls the size of the top scoring terms to use.
		/// </summary>
		[EnumMember(Value = "top_terms_N")]
		TopTermsN,

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, but the scores are only
		/// computed as the boost. This rewrite method only uses the top scoring terms so it will not overflow the boolean
		///  max clause count. The N controls the size of the top scoring terms to use.
		/// </summary>
		[EnumMember(Value = "top_terms_boost_N")]
		TopTermsBoostN,

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, but all term queries compute
		///  scores as if they had the same frequency. In practice the frequency which is used is the maximum frequency of all
		///  matching terms. This rewrite method only uses the top scoring terms so it will not overflow boolean max clause count.
		/// The N controls the size of the top scoring terms to use.
		/// </summary>
		[EnumMember(Value = "top_terms_blended_freqs_N")]
		TopTermsBlendedFreqsN
	}

	/// <summary>
	/// Controls how a multi term query such as a wildcard or prefix query, is rewritten.
	/// </summary>
	[JsonFormatter(typeof(MultiTermQueryRewriteFormatter))]
	public class MultiTermQueryRewrite : IEquatable<MultiTermQueryRewrite>
	{
		private static readonly char[] DigitCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		private readonly string _value;

		internal MultiTermQueryRewrite(RewriteMultiTerm rewrite, int? size = null)
		{
			switch (rewrite)
			{
				case RewriteMultiTerm.ConstantScore:
				case RewriteMultiTerm.ScoringBoolean:
				case RewriteMultiTerm.ConstantScoreBoolean:
					_value = rewrite.ToEnumValue();
					break;
				case RewriteMultiTerm.TopTermsN:
				case RewriteMultiTerm.TopTermsBoostN:
				case RewriteMultiTerm.TopTermsBlendedFreqsN:
					if (size == null)
						throw new ArgumentException($"{nameof(size)} must be specified with {nameof(RewriteMultiTerm)}.{rewrite}");

					var rewriteType = rewrite.ToEnumValue();
					rewriteType = rewriteType.Substring(0, rewriteType.Length - 1);
					_value = rewriteType + size;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(rewrite));
			}

			Rewrite = rewrite;
			Size = size;
		}

		/// <summary>
		///  A rewrite method that performs like constant_score_boolean when there are few matching terms and otherwise
		///  visits all matching terms in sequence and marks documents for that term. Matching documents are assigned a
		///  constant score equal to the query’s boost.
		/// </summary>
		public static MultiTermQueryRewrite ConstantScore { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ConstantScore);

		/// <summary>
		/// Similar to scoring_boolean except scores are not computed. Instead, each matching document receives a constant
		///  score equal to the query’s boost. This rewrite method will hit too many clauses failure if it exceeds the
		/// boolean query limit (defaults to 1024).
		/// </summary>
		public static MultiTermQueryRewrite ConstantScoreBoolean { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ConstantScoreBoolean);

		/// <summary>
		/// The type of multi term rewrite to perform
		/// </summary>
		public RewriteMultiTerm Rewrite { get; }

		/// <summary>
		/// A rewrite method that first translates each term into a should clause in a boolean query, and keeps the scores
		///  as computed by the query. Note that typically such scores are meaningless to the user, and require non-trivial
		///  CPU to compute. This rewrite method will hit too many
		///  clauses failure if it exceeds the boolean query limit (defaults to 1024).
		/// </summary>
		public static MultiTermQueryRewrite ScoringBoolean { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ScoringBoolean);

		/// <summary>
		/// The size of the top scoring terms to use
		/// </summary>
		public int? Size { get; }

		public bool Equals(MultiTermQueryRewrite other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return Rewrite == other.Rewrite && Size == other.Size;
		}

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, and keeps the scores
		/// as computed by the query. This rewrite method only uses the top scoring terms so it will not overflow boolean
		///  max clause count.
		/// <param name="size" />
		/// controls the size of the top scoring terms to use.
		/// </summary>
		public static MultiTermQueryRewrite TopTerms(int size) => new MultiTermQueryRewrite(RewriteMultiTerm.TopTermsN, size);

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, but the scores are only
		/// computed as the boost. This rewrite method only uses the top scoring terms so it will not overflow the boolean
		///  max clause count.
		/// <param name="size" />
		/// controls the size of the top scoring terms to use.
		/// </summary>
		public static MultiTermQueryRewrite TopTermsBoost(int size) => new MultiTermQueryRewrite(RewriteMultiTerm.TopTermsBoostN, size);

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, but all term queries compute
		///  scores as if they had the same frequency. In practice the frequency which is used is the maximum frequency of all
		///  matching terms. This rewrite method only uses the top scoring terms so it will not overflow boolean max clause count.
		/// <param name="size" />
		/// controls the size of the top scoring terms to use.
		/// </summary>
		public static MultiTermQueryRewrite TopTermsBlendedFreqs(int size) => new MultiTermQueryRewrite(RewriteMultiTerm.TopTermsBlendedFreqsN, size);

		internal static MultiTermQueryRewrite Create(string value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			var rewriteType = value;
			var size = 0;
			var firstDigitIndex = value.IndexOfAny(DigitCharacters);

			if (firstDigitIndex > -1)
			{
				rewriteType = $"{value.Substring(0, firstDigitIndex)}N";
				size = int.Parse(value.Substring(firstDigitIndex));
			}

			var rewriteMultiTerm = rewriteType.ToEnum<RewriteMultiTerm>();
			if (rewriteMultiTerm == null)
				throw new InvalidOperationException($"Unsupported {nameof(RewriteMultiTerm)} value: '{rewriteType}'");

			switch (rewriteMultiTerm)
			{
				case RewriteMultiTerm.ConstantScore:
					return ConstantScore;
				case RewriteMultiTerm.ScoringBoolean:
					return ScoringBoolean;
				case RewriteMultiTerm.ConstantScoreBoolean:
					return ConstantScoreBoolean;
				case RewriteMultiTerm.TopTermsN:
					return TopTerms(size);
				case RewriteMultiTerm.TopTermsBoostN:
					return TopTermsBoost(size);
				case RewriteMultiTerm.TopTermsBlendedFreqsN:
					return TopTermsBlendedFreqs(size);
				default:
					throw new InvalidOperationException($"Unsupported {nameof(RewriteMultiTerm)} value: '{rewriteMultiTerm}'");
			}
		}

		public override string ToString() => _value;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			var value = obj as string;
			if (value != null)
				return string.Equals(value, _value);

			return obj.GetType() == GetType() && Equals((MultiTermQueryRewrite)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((int)Rewrite * 397) ^ Size.GetHashCode();
			}
		}

		public static bool operator ==(MultiTermQueryRewrite left, MultiTermQueryRewrite right) => Equals(left, right);

		public static bool operator !=(MultiTermQueryRewrite left, MultiTermQueryRewrite right) => !Equals(left, right);
	}
}
