using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary> The decompound mode determines how the tokenizer handles compound tokens. </summary>
	[StringEnum]
	public enum NoriDecompoundMode
	{
		/// <summary> Decomposes compounds and discards the original form (default). </summary>
		[EnumMember(Value = "discard")]
		Discard,

		/// <summary> No decomposition for compounds </summary>
		[EnumMember(Value = "none")]
		None,

		/// <summary> Decomposes compounds and keeps the original form </summary>
		[EnumMember(Value = "mixed")]
		Mixed
	}

	/// <summary> Tokenizer that ships with the analysis-nori plugin</summary>
	public interface INoriTokenizer : ITokenizer
	{
		/// <summary>
		/// The regular expression pattern, defaults to \W+.
		/// </summary>
		[DataMember(Name = "decompound_mode")]
		NoriDecompoundMode? DecompoundMode { get; set; }

		/// <summary>
		/// The Nori tokenizer uses the mecab-ko-dic dictionary by default. A user_dictionary with custom nouns (NNG) may be
		/// appended to
		/// the default dictionary. This property allows you to specify this file on disk
		/// </summary>
		[DataMember(Name = "user_dictionary")]
		string UserDictionary { get; set; }
	}

	/// <inheritdoc cref="INoriTokenizer" />
	public class NoriTokenizer : TokenizerBase, INoriTokenizer
	{
		public NoriTokenizer() => Type = "nori_tokenizer";

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriDecompoundMode? DecompoundMode { get; set; }

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public string UserDictionary { get; set; }
	}

	/// <inheritdoc cref="INoriTokenizer" />
	public class NoriTokenizerDescriptor
		: TokenizerDescriptorBase<NoriTokenizerDescriptor, INoriTokenizer>, INoriTokenizer
	{
		protected override string Type => "nori_tokenizer";

		NoriDecompoundMode? INoriTokenizer.DecompoundMode { get; set; }
		string INoriTokenizer.UserDictionary { get; set; }

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriTokenizerDescriptor DecompoundMode(NoriDecompoundMode? mode) => Assign(a => a.DecompoundMode = mode);

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public NoriTokenizerDescriptor UserDictionary(string path) => Assign(a => a.UserDictionary = path);
	}
}
