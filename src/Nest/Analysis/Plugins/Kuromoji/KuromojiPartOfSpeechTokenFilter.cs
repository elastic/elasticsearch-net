// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The kuromoji_part_of_speech token filter removes tokens that match a set of part-of-speech tags.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiPartOfSpeechTokenFilter : ITokenFilter
	{
		/// <summary>
		/// An array of part-of-speech tags that should be removed. It defaults to the stoptags.txt file embedded
		/// in the lucene-analyzer-kuromoji.jar.
		/// </summary>
		[DataMember(Name ="stoptags")]
		IEnumerable<string> StopTags { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiPartOfSpeechTokenFilter : TokenFilterBase, IKuromojiPartOfSpeechTokenFilter
	{
		public KuromojiPartOfSpeechTokenFilter() : base("kuromoji_part_of_speech") { }

		/// <inheritdoc />
		public IEnumerable<string> StopTags { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiPartOfSpeechTokenFilterDescriptor
		: TokenFilterDescriptorBase<KuromojiPartOfSpeechTokenFilterDescriptor, IKuromojiPartOfSpeechTokenFilter>, IKuromojiPartOfSpeechTokenFilter
	{
		protected override string Type => "kuromoji_part_of_speech";

		IEnumerable<string> IKuromojiPartOfSpeechTokenFilter.StopTags { get; set; }

		/// <inheritdoc />
		public KuromojiPartOfSpeechTokenFilterDescriptor StopTags(IEnumerable<string> stopTags) => Assign(stopTags, (a, v) => a.StopTags = v);

		/// <inheritdoc />
		public KuromojiPartOfSpeechTokenFilterDescriptor StopTags(params string[] stopTags) => Assign(stopTags, (a, v) => a.StopTags = v);
	}
}
