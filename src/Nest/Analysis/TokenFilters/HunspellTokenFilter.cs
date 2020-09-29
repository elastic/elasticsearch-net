// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	///  Basic support for hunspell stemming.
	/// <para> Hunspell dictionaries will be picked up from a dedicated hunspell directory on the filesystem.</para>
	/// </summary>
	public interface IHunspellTokenFilter : ITokenFilter
	{
		/// <summary>
		/// If only unique terms should be returned, this needs to be set to true.
		/// </summary>
		[DataMember(Name ="dedup")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Dedup { get; set; }

		/// <summary>
		/// The name of a dictionary.The path to your hunspell dictionaries should be configured via
		/// `indices.analysis.hunspell.dictionary.location` before.
		/// </summary>
		[DataMember(Name ="dictionary")]
		string Dictionary { get; set; }

		/// <summary>
		/// A locale for this filter. If this is unset, the lang or language are used instead - so one of these has to be set.
		/// </summary>
		[DataMember(Name ="locale")]
		string Locale { get; set; }

		/// <summary>
		/// If only the longest term should be returned, set this to true.
		/// </summary>
		[DataMember(Name ="longest_only")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? LongestOnly { get; set; }
	}

	/// <inheritdoc />
	public class HunspellTokenFilter : TokenFilterBase, IHunspellTokenFilter
	{
		public HunspellTokenFilter() : base("hunspell") { }

		/// <inheritdoc />
		public bool? Dedup { get; set; }

		/// <inheritdoc />
		public string Dictionary { get; set; }

		/// <inheritdoc />
		public string Locale { get; set; }

		/// <inheritdoc />
		public bool? LongestOnly { get; set; }
	}

	/// <inheritdoc />
	public class HunspellTokenFilterDescriptor
		: TokenFilterDescriptorBase<HunspellTokenFilterDescriptor, IHunspellTokenFilter>, IHunspellTokenFilter
	{
		protected override string Type => "hunspell";
		bool? IHunspellTokenFilter.Dedup { get; set; }
		string IHunspellTokenFilter.Dictionary { get; set; }
		string IHunspellTokenFilter.Locale { get; set; }

		bool? IHunspellTokenFilter.LongestOnly { get; set; }

		/// <inheritdoc />
		public HunspellTokenFilterDescriptor LongestOnly(bool? longestOnly = true) => Assign(longestOnly, (a, v) => a.LongestOnly = v);

		/// <inheritdoc />
		public HunspellTokenFilterDescriptor Dedup(bool? dedup = true) => Assign(dedup, (a, v) => a.Dedup = v);

		/// <inheritdoc />
		public HunspellTokenFilterDescriptor Locale(string locale) => Assign(locale, (a, v) => a.Locale = v);

		/// <inheritdoc />
		public HunspellTokenFilterDescriptor Dictionary(string dictionary) => Assign(dictionary, (a, v) => a.Dictionary = v);
	}
}
