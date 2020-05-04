// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	public class XPackInfoResponse : ResponseBase
	{
		[DataMember(Name ="build")]
		public XPackBuildInformation Build { get; internal set; }
		[DataMember(Name ="features")]
		public XPackFeatures Features { get; internal set; }
		[DataMember(Name ="license")]
		public MinimalLicenseInformation License { get; internal set; }
		[DataMember(Name ="tagline")]
		public string Tagline { get; internal set; }
	}

	public class XPackBuildInformation
	{
		[DataMember(Name ="date")]
		public DateTimeOffset Date { get; internal set; }

		[DataMember(Name ="hash")]
		public string Hash { get; internal set; }
	}

	public class MinimalLicenseInformation
	{
		[DataMember(Name ="expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; set; }

		[DataMember(Name ="mode")]
		public LicenseType Mode { get; internal set; }

		[DataMember(Name ="status")]
		public LicenseStatus Status { get; internal set; }

		[DataMember(Name ="type")]
		public LicenseType Type { get; internal set; }

		[DataMember(Name ="uid")]
		public string UID { get; internal set; }
	}

	public class XPackFeatures
	{
		[DataMember(Name = "analytics")]
		public XPackFeature Analytics { get; internal set; }

		[DataMember(Name = "ccr")]
		public XPackFeature Ccr { get; internal set; }

		[DataMember(Name = "enrich")]
		public XPackFeature Enrich { get; internal set; }

		[Obsolete("Changed to Transform in 7.5.0")]
		[DataMember(Name = "data_frame")]
		public XPackFeature DataFrame { get; internal set; }

		[DataMember(Name = "flattened")]
		public XPackFeature Flattened { get; internal set; }

		[DataMember(Name = "frozen_indices")]
		public XPackFeature FrozenIndices { get; internal set; }

		[DataMember(Name = "data_science")]
		public XPackFeature DataScience { get; internal set; }

		[DataMember(Name = "graph")]
		public XPackFeature Graph { get; internal set; }

		// TODO! Expand to fullname in 8.0?
		[DataMember(Name = "ilm")]
		public XPackFeature Ilm { get; internal set; }

		[DataMember(Name = "logstash")]
		public XPackFeature Logstash { get; internal set; }

		[DataMember(Name = "ml")]
		public XPackFeature MachineLearning { get; internal set; }

		[DataMember(Name = "monitoring")]
		public XPackFeature Monitoring { get; internal set; }

		[DataMember(Name = "rollup")]
		public XPackFeature Rollup { get; internal set; }

		[DataMember(Name = "security")]
		public XPackFeature Security { get; internal set; }

		[DataMember(Name = "slm")]
		public XPackFeature SnapshotLifecycleManagement { get; internal set; }

		[DataMember(Name = "spatial")]
		public XPackFeature Spatial { get; internal set; }

		[DataMember(Name = "sql")]
		public XPackFeature Sql { get; internal set; }

		[DataMember(Name = "transform")]
		public XPackFeature Transform { get; internal set; }

		[DataMember(Name = "vectors")]
		public XPackFeature Vectors { get; internal set; }

		[DataMember(Name = "voting_only")]
		public XPackFeature VotingOnly { get; internal set; }

		[DataMember(Name = "watcher")]
		public XPackFeature Watcher { get; internal set; }
	}

	public class XPackFeature
	{
		[DataMember(Name = "available")]
		public bool Available { get; internal set; }

		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="enabled")]
		public bool Enabled { get; internal set; }

		[DataMember(Name ="native_code_info")]
		public NativeCodeInformation NativeCodeInformation { get; internal set; }
	}

	public class NativeCodeInformation
	{
		[DataMember(Name ="build_hash")]
		public string BuildHash { get; internal set; }

		[DataMember(Name ="version")]
		public string Version { get; internal set; }
	}
}
