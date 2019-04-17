using System.Runtime.Serialization;

namespace Nest
{
	public class MachineLearningInfoResponse : ResponseBase
	{
		[DataMember(Name = "defaults")]
		public Defaults Defaults { get; internal set; }

		[DataMember(Name = "limits")]
		public Limits Limits { get; internal set; }

		[DataMember(Name = "upgrade_mode")]
		public bool? UpgradeMode { get; internal set; }
	}

	public class Defaults
	{
		[DataMember(Name = "anomaly_detectors")]
		public AnomalyDetectors AnomalyDetectors { get; internal set; }

		[DataMember(Name = "datafeeds")]
		public Datafeeds Datafeeds { get; internal set; }
	}

	public class AnomalyDetectors
	{
		[DataMember(Name = "model_memory_limit")]
		public string ModelMemoryLimit { get; internal set; }

		[DataMember(Name = "categorization_examples_limit")]
		public int CategorizationExamplesLimit { get; internal set; }

		[DataMember(Name = "model_snapshot_retention_days")]
		public int ModelSnapshotRetentionDays { get; internal set; }
	}

	public class Datafeeds
	{
		[DataMember(Name = "scroll_size")]
		public int ScrollSize { get; internal set; }
	}

	public class Limits
	{
		[DataMember(Name = "max_model_memory_limit")]
		public string MaxModelMemoryLimit { get; internal set; }
	}
}
