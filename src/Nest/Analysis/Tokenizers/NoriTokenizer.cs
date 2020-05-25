// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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
		/// Whether punctuation should be discarded from the output. Defaults to `true`.
		/// </summary>
		[DataMember(Name = "discard_punctuation")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? DiscardPunctuation { get; set; }

		/// <summary>
		/// The Nori tokenizer uses the mecab-ko-dic dictionary by default. A user_dictionary with custom nouns (NNG) may be
		/// appended to
		/// the default dictionary. This property allows you to specify this file on disk
		/// </summary>
		[DataMember(Name = "user_dictionary")]
		string UserDictionary { get; set; }

		/// <summary>
		/// The Nori tokenizer uses the mecab-ko-dic dictionary by default. A user_dictionary with custom nouns (NNG)
		/// can be specified inline with this property
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.6.0+
		/// </remarks>
		[DataMember(Name = "user_dictionary_rules")]
		IEnumerable<string> UserDictionaryRules { get; set; }
	}

	/// <inheritdoc cref="INoriTokenizer" />
	public class NoriTokenizer : TokenizerBase, INoriTokenizer
	{
		public NoriTokenizer() => Type = "nori_tokenizer";

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriDecompoundMode? DecompoundMode { get; set; }

		/// <inheritdoc cref="INoriTokenizer.DiscardPunctuation" />
		public bool? DiscardPunctuation { get; set; }

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public string UserDictionary { get; set; }

		/// <inheritdoc cref="INoriTokenizer.UserDictionaryRules" />
		public IEnumerable<string> UserDictionaryRules { get; set; }
	}

	/// <inheritdoc cref="INoriTokenizer" />
	public class NoriTokenizerDescriptor
		: TokenizerDescriptorBase<NoriTokenizerDescriptor, INoriTokenizer>, INoriTokenizer
	{
		protected override string Type => "nori_tokenizer";

		NoriDecompoundMode? INoriTokenizer.DecompoundMode { get; set; }
		string INoriTokenizer.UserDictionary { get; set; }
		IEnumerable<string> INoriTokenizer.UserDictionaryRules { get; set; }
		bool? INoriTokenizer.DiscardPunctuation { get; set; }

		/// <inheritdoc cref="INoriTokenizer.DecompoundMode" />
		public NoriTokenizerDescriptor DecompoundMode(NoriDecompoundMode? mode) => Assign(mode, (a, v) => a.DecompoundMode = v);

		/// <inheritdoc cref="INoriTokenizer.UserDictionary" />
		public NoriTokenizerDescriptor UserDictionary(string path) => Assign(path, (a, v) => a.UserDictionary = v);

		/// <inheritdoc cref="INoriTokenizer.UserDictionaryRules" />
		public NoriTokenizerDescriptor UserDictionaryRules(params string[] rules) => Assign(rules, (a, v) => a.UserDictionaryRules = v);

		/// <inheritdoc cref="INoriTokenizer.UserDictionaryRules" />
		public NoriTokenizerDescriptor UserDictionaryRules(IEnumerable<string> rules) => Assign(rules, (a, v) => a.UserDictionaryRules = v);

		/// <inheritdoc cref="INoriTokenizer.DiscardPunctuation" />
		public NoriTokenizerDescriptor DiscardPunctuation(bool? discard = true) => Assign(discard, (a, v) => a.DiscardPunctuation = v);
	}
}
