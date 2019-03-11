using Newtonsoft.Json;

namespace Nest
{
	public interface IMachineLearningInfoResponse : IResponse
	{
		[JsonProperty("defaults")]
		Defaults Defaults { get; }

		[JsonProperty("limits")]
		Limits Limits { get; }

		[JsonProperty("upgrade_mode")]
		bool? UpgradeMode { get; }
	}

	public class MachineLearningInfoResponse : ResponseBase, IMachineLearningInfoResponse
	{
		[JsonProperty("defaults")]
		public Defaults Defaults { get; internal set; }

		[JsonProperty("limits")]
		public Limits Limits { get; internal set; }

		[JsonProperty("upgrade_mode")]
		public bool? UpgradeMode { get; internal set; }
	}

	public class Defaults
	{
		[JsonProperty("anomaly_detectors")]
		public AnomalyDetectors AnomalyDetectors { get; internal set; }

		[JsonProperty("datafeeds")]
		public Datafeeds Datafeeds { get; internal set; }
	}

	public class AnomalyDetectors
	{
		[JsonProperty("model_memory_limit")]
		public string ModelMemoryLimit { get; internal set; }

		[JsonProperty("categorization_examples_limit")]
		public int CategorizationExamplesLimit { get; internal set; }

		[JsonProperty("model_snapshot_retention_days")]
		public int ModelSnapshotRetentionDays { get; internal set; }
	}

	public class Datafeeds
	{
		[JsonProperty("scroll_size")]
		public int ScrollSize { get; internal set; }
	}

	public class Limits
	{
		[JsonProperty("max_model_memory_limit")]
		public string MaxModelMemoryLimit { get; internal set; }
	}
}
