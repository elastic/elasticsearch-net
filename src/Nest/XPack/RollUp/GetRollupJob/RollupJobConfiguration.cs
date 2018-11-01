using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class RollupJobConfiguration
	{
		/// <inheritdoc cref="ICreateRollupJobRequest.Cron" />
		[JsonProperty("cron")]
		public string Cron { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.Groups" />
		[JsonProperty("groups")]
		public IRollupGroupings Groups { get; internal set; }

		[JsonProperty("id")]
		public string Id { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.IndexPattern" />
		[JsonProperty("index_pattern")]
		public string IndexPattern { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.Metrics" />
		[JsonProperty("metrics")]
		public IEnumerable<IRollupFieldMetric> Metrics { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.PageSize" />
		[JsonProperty("page_size")]
		public long? PageSize { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.RollupIndex" />
		[JsonProperty("rollup_index")]
		public IndexName RollupIndex { get; internal set; }

		[JsonProperty("timeout")]
		public Time Timeout { get; internal set; }
	}
}
