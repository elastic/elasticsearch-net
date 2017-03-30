using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RewriteMultiTerm
	{
		[EnumMember(Value = "constant_score_auto")]
		ConstantScoreDefault,
		[EnumMember(Value = "scoring_boolean")]
		ScoringBoolean,
		[EnumMember(Value = "constant_score_boolean")]
		ConstantScoreBoolean,
		[EnumMember(Value = "constant_score_filter")]
		ConstantScoreFilter,
		[EnumMember(Value = "top_terms_N")]
		TopTermsN,
		[EnumMember(Value = "top_terms_boost_N")]
		TopTermsBoostN
	}

	/// <summary>
	/// Controls how a multi term query such as a wildcard or prefix query, is rewritten.
	/// </summary>
	[JsonConverter(typeof(MultiTermQueryRewriteConverter))]
	public class MultiTermQueryRewrite : IEquatable<MultiTermQueryRewrite>
	{
		private static readonly char[] DigitCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		private readonly string _value;

		/// <summary>
		/// The type of multi term rewrite to perform
		/// </summary>
		public RewriteMultiTerm Rewrite { get; }

		/// <summary>
		/// The size of the top scoring terms to use
		/// </summary>
		public int? Size { get; }

		internal MultiTermQueryRewrite(RewriteMultiTerm rewrite, int? size = null)
		{
			switch (rewrite)
			{
				case RewriteMultiTerm.ConstantScoreDefault:
				case RewriteMultiTerm.ScoringBoolean:
				case RewriteMultiTerm.ConstantScoreBoolean:
				case RewriteMultiTerm.ConstantScoreFilter:
					_value = rewrite.ToEnumValue();
					break;
				case RewriteMultiTerm.TopTermsN:
				case RewriteMultiTerm.TopTermsBoostN:
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
		public static MultiTermQueryRewrite ConstantScoreDefault { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ConstantScoreDefault);

		/// <summary>
		/// A rewrite method that first creates a private Filter by visiting each term in sequence and marking 
		/// all docs for that term. Matching documents are assigned a constant score equal to the query’s boost.
		/// </summary>
		public static MultiTermQueryRewrite ConstantScoreFilter { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ConstantScoreFilter);

		/// <summary>
		/// A rewrite method that first translates each term into a should clause in a boolean query, and keeps the scores
		///  as computed by the query. Note that typically such scores are meaningless to the user, and require non-trivial
		///  CPU to compute, so it’s almost always better to use constant_score_auto. This rewrite method will hit too many
		///  clauses failure if it exceeds the boolean query limit (defaults to 1024).
		/// </summary>
		public static MultiTermQueryRewrite ScoringBoolean { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ScoringBoolean);

		/// <summary>
		/// Similar to scoring_boolean except scores are not computed. Instead, each matching document receives a constant
		///  score equal to the query’s boost. This rewrite method will hit too many clauses failure if it exceeds the
		/// boolean query limit (defaults to 1024).
		/// </summary>
		public static MultiTermQueryRewrite ConstantScoreBoolean { get; } = new MultiTermQueryRewrite(RewriteMultiTerm.ConstantScoreBoolean);

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, and keeps the scores
		/// as computed by the query. This rewrite method only uses the top scoring terms so it will not overflow boolean
		///  max clause count. <param name="size" /> controls the size of the top scoring terms to use.
		/// </summary>
		public static MultiTermQueryRewrite TopTerms(int size) => new MultiTermQueryRewrite(RewriteMultiTerm.TopTermsN, size);

		/// <summary>
		/// A rewrite method that first translates each term into should clause in boolean query, but the scores are only
		/// computed as the boost. This rewrite method only uses the top scoring terms so it will not overflow the boolean
		///  max clause count. <param name="size" /> controls the size of the top scoring terms to use.
		/// </summary>
		public static MultiTermQueryRewrite TopTermsBoost(int size) => new MultiTermQueryRewrite(RewriteMultiTerm.TopTermsBoostN, size);

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
				case RewriteMultiTerm.ConstantScoreDefault:
					return ConstantScoreDefault;
				case RewriteMultiTerm.ScoringBoolean:
					return ScoringBoolean;
				case RewriteMultiTerm.ConstantScoreBoolean:
					return ConstantScoreBoolean;
				case RewriteMultiTerm.ConstantScoreFilter:
					return ConstantScoreFilter;
				case RewriteMultiTerm.TopTermsN:
					return TopTerms(size);
				case RewriteMultiTerm.TopTermsBoostN:
					return TopTermsBoost(size);
				default:
					throw new InvalidOperationException($"Unsupported {nameof(RewriteMultiTerm)} value: '{rewriteMultiTerm}'");
			}
		}

		public override string ToString() => this._value;

		public bool Equals(MultiTermQueryRewrite other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Rewrite == other.Rewrite && Size == other.Size;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			var value = obj as string;
			if (value != null)
				return string.Equals(value, _value);

			return obj.GetType() == this.GetType() && Equals((MultiTermQueryRewrite)obj);
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

	internal class MultiTermQueryRewriteConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var multiTerm = (MultiTermQueryRewrite)value;
			writer.WriteValue(multiTerm?.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			if (reader.TokenType != JsonToken.String)
				throw new JsonSerializationException($"Invalid token type {reader.TokenType} to deserialize {nameof(MultiTermQueryRewrite)} from");

			return MultiTermQueryRewrite.Create((string)reader.Value);
		}

		public override bool CanConvert(Type objectType) => typeof(MultiTermQueryRewrite).IsAssignableFrom(objectType);
	}
}
