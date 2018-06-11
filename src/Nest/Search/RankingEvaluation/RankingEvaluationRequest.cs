using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRankingEvaluationRequest
	{
		/// <summary>
		/// A collection of templates to use to define typical search requests
		/// </summary>
		[JsonProperty("templates")]
		IEnumerable<RatedRequestTemplate> Templates { get; set; }

		/// <summary>
		/// A collection of typical search requests, together with their provided ratings
		/// </summary>
		[JsonProperty("requests")]
		IEnumerable<RatedRequest> Requests { get; set; }

		/// <summary>
		/// The evaluation metric to calculate
		/// </summary>
		[JsonProperty("metric")]
		IEvaluationMetric Metric { get; set; }

		/// <summary>
		/// The maximum number of concurrent searches. Defaults to <c>10</c>.
		/// </summary>
		[JsonProperty("max_concurrent_searches")]
		int? MaxConcurrentSearches { get; set; }
	}

	/// <inheritdoc cref="IRankingEvaluationRequest"/>
	public partial class RankingEvaluationRequest : IRankingEvaluationRequest
	{
		/// <inheritdoc />
		public IEnumerable<RatedRequestTemplate> Templates { get; set; }

		/// <inheritdoc />
		public IEnumerable<RatedRequest> Requests { get; set; }

		/// <inheritdoc />
		public IEvaluationMetric Metric { get; set; }

		/// <inheritdoc />
		public int? MaxConcurrentSearches { get; set; }
	}

	[DescriptorFor("RankEval")]
	public partial class RankingEvaluationDescriptor : IRankingEvaluationRequest
	{
		IEnumerable<RatedRequestTemplate> IRankingEvaluationRequest.Templates { get; set; }
		IEnumerable<RatedRequest> IRankingEvaluationRequest.Requests { get; set; }
		IEvaluationMetric IRankingEvaluationRequest.Metric { get; set; }
		int? IRankingEvaluationRequest.MaxConcurrentSearches { get; set; }
	}
}
