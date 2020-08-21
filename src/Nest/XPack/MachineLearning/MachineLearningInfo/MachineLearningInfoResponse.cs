// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
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

		/// <summary>
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "daily_model_snapshot_retention_after_days")]
		public long DailyModelSnapshotRetentionAfterDays { get; internal set; }

		[DataMember(Name = "categorization_analyzer")]
		public CategorizationAnalyzer CategorizationAnalyzer { get; internal set; }
	}

	public class CategorizationAnalyzer
	{
		[DataMember(Name = "tokenizer")]
		public string Tokenizer { get; internal set; }

		[DataMember(Name = "filter")]
		public IReadOnlyCollection<ITokenFilter> Filter { get; internal set; }
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

		/// <summary>
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "effective_max_model_memory_limit")]
		public string EffectiveMaxModelMemoryLimit { get; internal set; }
	}
}
