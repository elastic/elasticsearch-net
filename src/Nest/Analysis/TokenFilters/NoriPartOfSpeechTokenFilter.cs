// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary> The nori_part_of_speech token filter removes tokens that match a set of part-of-speech tags. </summary>
	public interface INoriPartOfSpeechTokenFilter : ITokenFilter
	{
		/// <summary> An array of part-of-speech tags that should be removed. </summary>
		[DataMember(Name ="stoptags")]
		IEnumerable<string> StopTags { get; set; }
	}

	/// <inheritdoc cref="INoriPartOfSpeechTokenFilter" />
	public class NoriPartOfSpeechTokenFilter : TokenFilterBase, INoriPartOfSpeechTokenFilter
	{
		public NoriPartOfSpeechTokenFilter() : base("nori_part_of_speech") { }

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public IEnumerable<string> StopTags { get; set; }
	}

	/// <inheritdoc cref="INoriPartOfSpeechTokenFilter" />
	public class NoriPartOfSpeechTokenFilterDescriptor
		: TokenFilterDescriptorBase<NoriPartOfSpeechTokenFilterDescriptor, INoriPartOfSpeechTokenFilter>, INoriPartOfSpeechTokenFilter
	{
		protected override string Type => "nori_part_of_speech";

		IEnumerable<string> INoriPartOfSpeechTokenFilter.StopTags { get; set; }

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public NoriPartOfSpeechTokenFilterDescriptor StopTags(IEnumerable<string> stopTags) => Assign(stopTags, (a, v) => a.StopTags = v);

		///<inheritdoc cref="INoriPartOfSpeechTokenFilter.StopTags" />
		public NoriPartOfSpeechTokenFilterDescriptor StopTags(params string[] stopTags) => Assign(stopTags, (a, v) => a.StopTags = v);
	}
}
