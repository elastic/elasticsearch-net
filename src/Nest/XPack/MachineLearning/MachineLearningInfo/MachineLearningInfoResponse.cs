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
