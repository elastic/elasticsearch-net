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

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Retrieve results for machine learning job influencers.
	/// </summary>
	[MapsApi("ml.get_influencers.json")]
	public partial interface IGetInfluencersRequest
	{
		/// <summary>
		/// If true, the results are sorted in descending order.
		/// </summary>
		bool? Descending { get; set; }

		/// <summary>
		/// Returns influencers with timestamps earlier than this time.
		/// </summary>
		[DataMember(Name = "end")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// If true, the output excludes interim results. By default, interim results are included.
		/// </summary>
		[DataMember(Name = "exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// Returns influencers with anomaly scores higher than this value.
		/// </summary>
		[DataMember(Name = "influencer_score")]
		double? InfluencerScore { get; set; }

		/// <summary>
		/// Specifies pagination for the influencers.
		/// </summary>
		[DataMember(Name = "page")]
		IPage Page { get; set; }

		/// <summary>
		/// Specifies the sort field for the requested influencers. By default, the influencers are sorted by the
		/// <see cref="InfluencerScore" /> value.
		/// </summary>
		[DataMember(Name = "sort")]
		Field Sort { get; set; }

		/// <summary>
		/// Returns influencers with timestamps after this time.
		/// </summary>
		[DataMember(Name = "start")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	public partial class GetInfluencersRequest
	{
		/// <inheritdoc />
		public bool? Descending { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }

		/// <inheritdoc />
		public bool? ExcludeInterim { get; set; }

		/// <inheritdoc />
		public double? InfluencerScore { get; set; }

		/// <inheritdoc />
		public IPage Page { get; set; }

		/// <inheritdoc />
		public Field Sort { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	public partial class GetInfluencersDescriptor
	{
		bool? IGetInfluencersRequest.Descending { get; set; }
		DateTimeOffset? IGetInfluencersRequest.End { get; set; }
		bool? IGetInfluencersRequest.ExcludeInterim { get; set; }
		double? IGetInfluencersRequest.InfluencerScore { get; set; }
		IPage IGetInfluencersRequest.Page { get; set; }
		Field IGetInfluencersRequest.Sort { get; set; }
		DateTimeOffset? IGetInfluencersRequest.Start { get; set; }

		/// <inheritdoc />
		public GetInfluencersDescriptor InfluencerScore(double? influencerScore) => Assign(influencerScore, (a, v) => a.InfluencerScore = v);

		/// <inheritdoc />
		public GetInfluencersDescriptor Desc(bool? descending = true) => Assign(descending, (a, v) => a.Descending = v);

		/// <inheritdoc />
		public GetInfluencersDescriptor End(DateTimeOffset? end) => Assign(end, (a, v) => a.End = v);

		/// <inheritdoc />
		public GetInfluencersDescriptor ExcludeInterim(bool? excludeInterim = true) => Assign(excludeInterim, (a, v) => a.ExcludeInterim = v);

		/// <inheritdoc />
		public GetInfluencersDescriptor Page(Func<PageDescriptor, IPage> selector) => Assign(selector, (a, v) => a.Page = v?.Invoke(new PageDescriptor()));

		/// <inheritdoc />
		public GetInfluencersDescriptor Sort(Field field) => Assign(field, (a, v) => a.Sort = v);

		/// <inheritdoc />
		public GetInfluencersDescriptor Start(DateTimeOffset? end) => Assign(end, (a, v) => a.Start = v);
	}
}
