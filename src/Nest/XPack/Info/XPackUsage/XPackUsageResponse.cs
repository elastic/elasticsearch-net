using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IXPackUsageResponse : IResponse
	{
		[JsonProperty("watcher")]
		AlertingUsage Alerting { get; }

		[JsonProperty("graph")]
		XPackUsage Graph { get; }

		[JsonProperty("ml")]
		MachineLearningUsage MachineLearning { get; }

		[JsonProperty("monitoring")]
		MonitoringUsage Monitoring { get; }

		[JsonProperty("security")]
		SecurityUsage Security { get; }
	}

	public class XPackUsageResponse : ResponseBase, IXPackUsageResponse
	{
		[JsonProperty("watcher")]
		public AlertingUsage Alerting { get; internal set; }

		[JsonProperty("graph")]
		public XPackUsage Graph { get; internal set; }

		[JsonProperty("ml")]
		public MachineLearningUsage MachineLearning { get; internal set; }

		[JsonProperty("monitoring")]
		public MonitoringUsage Monitoring { get; internal set; }

		[JsonProperty("security")]
		public SecurityUsage Security { get; internal set; }
	}

	public class XPackUsage
	{
		public bool Available { get; internal set; }
		public bool Enabled { get; internal set; }
	}

	public class SecurityUsage : XPackUsage
	{
		[JsonProperty("anonymous")]
		public SecurityFeatureToggle Anonymous { get; internal set; }

		[JsonProperty("audit")]
		public AuditUsage Audit { get; internal set; }

		[JsonProperty("ipfilter")]
		public IpFilterUsage IpFilter { get; internal set; }

		[JsonProperty("realms")]
		public IReadOnlyDictionary<string, RealmUsage> Realms { get; internal set; } = EmptyReadOnly<string, RealmUsage>.Dictionary;

		[JsonProperty("roles")]
		public IReadOnlyDictionary<string, RoleUsage> Roles { get; internal set; } = EmptyReadOnly<string, RoleUsage>.Dictionary;

		[JsonProperty("ssl")]
		public SslUsage Ssl { get; internal set; }

		[JsonProperty("system_key")]
		public SecurityFeatureToggle SystemKey { get; internal set; }

		public class AuditUsage : SecurityFeatureToggle
		{
			[JsonProperty("outputs")]
			public IReadOnlyCollection<string> Outputs { get; internal set; } = EmptyReadOnly<string>.Collection;
		}

		public class IpFilterUsage
		{
			[JsonProperty("http")]
			public bool Http { get; internal set; }

			[JsonProperty("transport")]
			public bool Transport { get; internal set; }
		}

		public class RealmUsage : XPackUsage
		{
			[JsonProperty("name")]
			public IReadOnlyCollection<string> Name { get; internal set; } = EmptyReadOnly<string>.Collection;

			[JsonProperty("order")]
			public IReadOnlyCollection<long> Order { get; internal set; } = EmptyReadOnly<long>.Collection;

			[JsonProperty("size")]
			public IReadOnlyCollection<long> Size { get; internal set; } = EmptyReadOnly<long>.Collection;
		}

		public class RoleUsage
		{
			[JsonProperty("dls")]
			public bool Dls { get; internal set; }

			[JsonProperty("fls")]
			public bool Fls { get; internal set; }

			[JsonProperty("size")]
			public long Size { get; internal set; }
		}

		public class SslUsage
		{
			[JsonProperty("http")]
			public SecurityFeatureToggle Http { get; internal set; }

			[JsonProperty("transport")]
			public SecurityFeatureToggle Transport { get; internal set; }
		}

		public class SecurityFeatureToggle
		{
			[JsonProperty("enabled")]
			public bool Enabled { get; internal set; }
		}
	}

	public class AlertingUsage : XPackUsage
	{
		[JsonProperty("count")]
		public AlertingCount Count { get; internal set; }

		[JsonProperty("execution")]
		public AlertingExecution Execution { get; internal set; }

		public class AlertingExecution
		{
			[JsonProperty("actions")]
			public IReadOnlyDictionary<string, ExecutionAction> Actions { get; internal set; } = EmptyReadOnly<string, ExecutionAction>.Dictionary;
		}

		public class ExecutionAction
		{
			[JsonProperty("total")]
			public long Total { get; internal set; }

			[JsonProperty("total_in_ms")]
			public long TotalInMilliseconds { get; internal set; }
		}

		public class AlertingCount
		{
			[JsonProperty("active")]
			public long Active { get; internal set; }

			[JsonProperty("total")]
			public long Total { get; internal set; }
		}
	}

	public class MonitoringUsage : XPackUsage
	{
		[JsonProperty("enabled_exporters")]
		public IReadOnlyDictionary<string, long> EnabledExporters { get; set; } = EmptyReadOnly<string, long>.Dictionary;
	}

	public class MachineLearningUsage : XPackUsage
	{
	    /// <remarks>Valid only for Elasticsearch 6.5.0+</remarks>
		[JsonProperty("node_count")]
		public int NodeCount { get; internal set; }

		[JsonProperty("datafeeds")]
		public IReadOnlyDictionary<string, DataFeed> Datafeeds { get; set; } = EmptyReadOnly<string, DataFeed>.Dictionary;

		[JsonProperty("jobs")]
		public IReadOnlyDictionary<string, Job> Jobs { get; set; } = EmptyReadOnly<string, Job>.Dictionary;

		public class DataFeed
		{
			[JsonProperty("count")]
			public long Count { get; internal set; }
		}

		public class Job
		{
			[JsonProperty("count")]
			public long Count { get; internal set; }

			[JsonProperty("detectors")]
			public JobStatistics Detectors { get; internal set; }

			[JsonProperty("model_size")]
			public JobStatistics ModelSize { get; internal set; }

			[JsonProperty("forecasts")]
			public ForecastStatistics Forecasts { get; internal set; }
		}

		public class JobStatistics
		{
			[JsonProperty("avg")]
			public double Average { get; internal set; }

			[JsonProperty("max")]
			public double Maximum { get; internal set; }

			[JsonProperty("min")]
			public double Minimum { get; internal set; }

			[JsonProperty("total")]
			public double Total { get; internal set; }
		}

		public class ForecastStatistics
		{
			/// <summary>
			/// The number of forecasts currently available for this model.
			/// </summary>
			[JsonProperty("total")]
			public long Total { get; internal set; }

			/// <summary>
			/// The number of jobs that have at least one forecast.
			/// </summary>
			[JsonProperty("forecasted_jobs")]
			public long Jobs { get; internal set; }

			/// <summary>
			/// Statistics about the memory usage: minimum, maximum, average and total.
			/// </summary>
			[JsonProperty("memory_bytes")]
			public JobStatistics MemoryBytes { get; internal set; }

			/// <summary>
			/// Statistics about the forecast runtime in milliseconds: minimum, maximum, average and total.
			/// </summary>
			[JsonProperty("processing_time_ms")]
			public JobStatistics ProcessingTimeMilliseconds { get; internal set; }

			/// <summary>
			/// Statistics about the number of forecast records: minimum, maximum, average and total.
			/// </summary>
			[JsonProperty("records")]
			public JobStatistics Records { get; internal set; }

			/// <summary>
			/// Counts per forecast status.
			/// </summary>
			[JsonProperty("status")]
			public IReadOnlyDictionary<string, long> Status { get; internal set; }
				= EmptyReadOnly<string, long>.Dictionary;
		}
	}
}
