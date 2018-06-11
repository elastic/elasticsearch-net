using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An evaluation metric for the Ranking Evaluation API
	/// </summary>
	public interface IEvaluationMetric
	{
	}

	/// <summary>
	/// For every query in the test suite, this metric calculates the reciprocal of the rank of
	/// the first relevant document. For example finding the first relevant result in position 3 means
	/// the reciprocal rank is 1/3. The reciprocal rank for each query is averaged across all queries in
	/// the test suite to give the mean reciprocal rank.
	/// </summary>
	public class MeanReciprocalRank : IEvaluationMetric
	{
		/// <summary>
		/// The maximum number of documents retrieved per query.
		/// This value will act in place of the usual size parameter in the query.
		/// Defaults to <c>10</c>.
		/// </summary>
		[JsonProperty("k")]
		public int? K { get; set; }

		/// <summary>
		/// The rating threshold above which documents are considered
		/// to be "relevant". Defaults to 1.
		/// </summary>
		[JsonProperty("relevant_rating_threshold")]
		public int? RelevantRatingThreshold { get; set; }
	}

	/// <summary>
	/// Measures the number of relevant results in the top k search results.
	/// Its a form of the well known Precision metric that only looks at the top k documents.
	/// It is the fraction of relevant documents in those first k search. A precision at 10 (P@10)
	/// value of 0.6 then means six out of the 10 top hits are relevant with respect to the users information need.
	/// </summary>
	public class PrecisionAtK : IEvaluationMetric
	{
		/// <summary>
		/// The maximum number of documents retrieved per query.
		/// This value will act in place of the usual size parameter in the query.
		/// Defaults to <c>10</c>.
		/// </summary>
		[JsonProperty("k")]
		public int? K { get; set; }

		/// <summary>
		/// The rating threshold above which documents are considered
		/// to be "relevant". Defaults to 1.
		/// </summary>
		[JsonProperty("relevant_rating_threshold")]
		public int? RelevantRatingThreshold { get; set; }

		/// <summary>
		/// Controls how unlabeled documents in the search results are counted.
		/// If set to <c>true</c>, unlabeled documents are ignored and neither count as relevant
		/// or irrelevant. Set to <c>false</c> (the default), they are treated as irrelevant.
		/// </summary>
		[JsonProperty("ignore_unlabeled")]
		public bool? IgnoreUnlabeled { get; set; }
	}

	/// <summary>
	/// In contrast to <see cref="PrecisionAtK"/> and <see cref="MeanReciprocalRank"/>,
	/// <see cref="DiscountedCumulativeGain"/> takes both the rank and the rating of the search results
	/// into account. The assumption is that highly relevant documents are more useful for the user
	/// when appearing at the top of the result list. Therefore, the DCG formula reduces the contribution
	/// that high ratings for documents on lower search ranks have on the overall DCG metric.
	/// </summary>
	public class DiscountedCumulativeGain : IEvaluationMetric
	{
		/// <summary>
		/// The maximum number of documents retrieved per query.
		/// This value will act in place of the usual size parameter in the query.
		/// Defaults to <c>10</c>.
		/// </summary>
		[JsonProperty("k")]
		public int? K { get; set; }

		/// <summary>
		/// If set to <c>true</c>, this metric will calculate the Normalized DCG.
		/// </summary>
		[JsonProperty("normalize")]
		public bool? Normalize { get; set; }
	}
}
