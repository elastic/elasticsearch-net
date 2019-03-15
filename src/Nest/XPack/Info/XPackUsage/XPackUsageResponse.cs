using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IXPackUsageResponse : IResponse
	{
		[DataMember(Name = "watcher")]
		AlertingUsage Alerting { get; }

		[DataMember(Name = "graph")]
		XPackUsage Graph { get; }

		[DataMember(Name = "ml")]
		MachineLearningUsage MachineLearning { get; }

		[DataMember(Name = "monitoring")]
		MonitoringUsage Monitoring { get; }

		[DataMember(Name = "security")]
		SecurityUsage Security { get; }
	}

	public class XPackUsageResponse : ResponseBase, IXPackUsageResponse
	{
		[DataMember(Name = "watcher")]
		public AlertingUsage Alerting { get; internal set; }

		[DataMember(Name = "graph")]
		public XPackUsage Graph { get; internal set; }

		[DataMember(Name = "ml")]
		public MachineLearningUsage MachineLearning { get; internal set; }

		[DataMember(Name = "monitoring")]
		public MonitoringUsage Monitoring { get; internal set; }

		[DataMember(Name = "security")]
		public SecurityUsage Security { get; internal set; }
	}

	public class XPackUsage
	{
		[DataMember(Name = "available")]
		public bool Available { get; internal set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; internal set; }
	}

	public class SecurityUsage : XPackUsage
	{
		[DataMember(Name = "anonymous")]
		public SecurityFeatureToggle Anonymous { get; internal set; }

		[DataMember(Name = "audit")]
		public AuditUsage Audit { get; internal set; }

		[DataMember(Name = "ipfilter")]
		public IpFilterUsage IpFilter { get; internal set; }

		[DataMember(Name = "realms")]
		public IReadOnlyDictionary<string, RealmUsage> Realms { get; internal set; } = EmptyReadOnly<string, RealmUsage>.Dictionary;

		[DataMember(Name = "roles")]
		public IReadOnlyDictionary<string, RoleUsage> Roles { get; internal set; } = EmptyReadOnly<string, RoleUsage>.Dictionary;

		[DataMember(Name = "ssl")]
		public SslUsage Ssl { get; internal set; }

		[DataMember(Name = "system_key")]
		public SecurityFeatureToggle SystemKey { get; internal set; }

		public class AuditUsage : SecurityFeatureToggle
		{
			[DataMember(Name = "outputs")]
			public IReadOnlyCollection<string> Outputs { get; internal set; } = EmptyReadOnly<string>.Collection;
		}

		public class IpFilterUsage
		{
			[DataMember(Name = "http")]
			public bool Http { get; internal set; }

			[DataMember(Name = "transport")]
			public bool Transport { get; internal set; }
		}

		public class RealmUsage : XPackUsage
		{
			[DataMember(Name = "name")]
			public IReadOnlyCollection<string> Name { get; internal set; } = EmptyReadOnly<string>.Collection;

			[DataMember(Name = "order")]
			public IReadOnlyCollection<long> Order { get; internal set; } = EmptyReadOnly<long>.Collection;

			[DataMember(Name = "size")]
			public IReadOnlyCollection<long> Size { get; internal set; } = EmptyReadOnly<long>.Collection;
		}

		public class RoleUsage
		{
			[DataMember(Name = "dls")]
			public bool Dls { get; internal set; }

			[DataMember(Name = "fls")]
			public bool Fls { get; internal set; }

			[DataMember(Name = "size")]
			public long Size { get; internal set; }
		}

		public class SslUsage
		{
			[DataMember(Name = "http")]
			public SecurityFeatureToggle Http { get; internal set; }

			[DataMember(Name = "transport")]
			public SecurityFeatureToggle Transport { get; internal set; }
		}

		public class SecurityFeatureToggle
		{
			[DataMember(Name = "enabled")]
			public bool Enabled { get; internal set; }
		}
	}

	public class AlertingUsage : XPackUsage
	{
		[DataMember(Name = "count")]
		public AlertingCount Count { get; internal set; }

		[DataMember(Name = "execution")]
		public AlertingExecution Execution { get; internal set; }

		public class AlertingExecution
		{
			[DataMember(Name = "actions")]
			public IReadOnlyDictionary<string, ExecutionAction> Actions { get; internal set; } = EmptyReadOnly<string, ExecutionAction>.Dictionary;
		}

		public class ExecutionAction
		{
			[DataMember(Name = "total")]
			public long Total { get; internal set; }

			[DataMember(Name = "total_in_ms")]
			public long TotalInMilliseconds { get; internal set; }
		}

		public class AlertingCount
		{
			[DataMember(Name = "active")]
			public long Active { get; internal set; }

			[DataMember(Name = "total")]
			public long Total { get; internal set; }
		}
	}

	public class MonitoringUsage : XPackUsage
	{
		[DataMember(Name = "enabled_exporters")]
		public IReadOnlyDictionary<string, long> EnabledExporters { get; set; } = EmptyReadOnly<string, long>.Dictionary;
	}

	public class MachineLearningUsage : XPackUsage
	{
		[DataMember(Name = "datafeeds")]
		public IReadOnlyDictionary<string, DataFeed> Datafeeds { get; set; } = EmptyReadOnly<string, DataFeed>.Dictionary;

		[DataMember(Name = "jobs")]
		public IReadOnlyDictionary<string, Job> Jobs { get; set; } = EmptyReadOnly<string, Job>.Dictionary;

		public class DataFeed
		{
			[DataMember(Name = "count")]
			public long Count { get; internal set; }
		}

		public class Job
		{
			[DataMember(Name = "count")]
			public long Count { get; internal set; }

			[DataMember(Name = "detectors")]
			public JobStatistics Detectors { get; internal set; }

			[DataMember(Name = "forecasts")]
			public ForecastStatistics Forecasts { get; internal set; }

			[DataMember(Name = "model_size")]
			public JobStatistics ModelSize { get; internal set; }
		}

		public class JobStatistics
		{
			[DataMember(Name = "avg")]
			public double Average { get; internal set; }

			[DataMember(Name = "max")]
			public double Maximum { get; internal set; }

			[DataMember(Name = "min")]
			public double Minimum { get; internal set; }

			[DataMember(Name = "total")]
			public double Total { get; internal set; }
		}

		public class ForecastStatistics
		{
			/// <summary>
			/// The number of jobs that have at least one forecast.
			/// </summary>
			[DataMember(Name = "forecasted_jobs")]
			public long Jobs { get; internal set; }

			/// <summary>
			/// Statistics about the memory usage: minimum, maximum, average and total.
			/// </summary>
			[DataMember(Name = "memory_bytes")]
			public JobStatistics MemoryBytes { get; internal set; }

			/// <summary>
			/// Statistics about the forecast runtime in milliseconds: minimum, maximum, average and total.
			/// </summary>
			[DataMember(Name = "processing_time_ms")]
			public JobStatistics ProcessingTimeMilliseconds { get; internal set; }

			/// <summary>
			/// Statistics about the number of forecast records: minimum, maximum, average and total.
			/// </summary>
			[DataMember(Name = "records")]
			public JobStatistics Records { get; internal set; }

			/// <summary>
			/// Counts per forecast status.
			/// </summary>
			[DataMember(Name = "status")]
			public IReadOnlyDictionary<string, long> Status { get; internal set; } = EmptyReadOnly<string, long>.Dictionary;

			/// <summary>
			/// The number of forecasts currently available for this model.
			/// </summary>
			[DataMember(Name = "total")]
			public long Total { get; internal set; }
		}
	}
}
