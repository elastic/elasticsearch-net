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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The kuromoji_iteration_mark normalizes Japanese horizontal iteration marks (odoriji) to their expanded form.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiIterationMarkCharFilter : ICharFilter
	{
		[DataMember(Name ="normalize_kana")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? NormalizeKana { get; set; }

		[DataMember(Name ="normalize_kanji")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? NormalizeKanji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiIterationMarkCharFilter : CharFilterBase, IKuromojiIterationMarkCharFilter
	{
		public KuromojiIterationMarkCharFilter() : base("kuromoji_iteration_mark") { }

		/// <inheritdoc />
		public bool? NormalizeKana { get; set; }

		/// <inheritdoc />
		public bool? NormalizeKanji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiIterationMarkCharFilterDescriptor
		: CharFilterDescriptorBase<KuromojiIterationMarkCharFilterDescriptor, IKuromojiIterationMarkCharFilter>, IKuromojiIterationMarkCharFilter
	{
		protected override string Type => "kuromoji_iteration_mark";
		bool? IKuromojiIterationMarkCharFilter.NormalizeKana { get; set; }
		bool? IKuromojiIterationMarkCharFilter.NormalizeKanji { get; set; }

		/// <inheritdoc />
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKanji(bool? normalize = true) =>
			Assign(normalize, (a, v) => a.NormalizeKanji = v);

		/// <inheritdoc />
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKana(bool? normalize = true) =>
			Assign(normalize, (a, v) => a.NormalizeKana = v);
	}
}
