// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobConfiguration
	{
		/// <inheritdoc cref="ICreateRollupJobRequest.Cron" />
		[DataMember(Name ="cron")]
		public string Cron { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.Groups" />
		[DataMember(Name ="groups")]
		public IRollupGroupings Groups { get; internal set; }

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.IndexPattern" />
		[DataMember(Name ="index_pattern")]
		public string IndexPattern { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.Metrics" />
		[DataMember(Name ="metrics")]
		public IEnumerable<IRollupFieldMetric> Metrics { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.PageSize" />
		[DataMember(Name ="page_size")]
		public long? PageSize { get; internal set; }

		/// <inheritdoc cref="ICreateRollupJobRequest.RollupIndex" />
		[DataMember(Name ="rollup_index")]
		public IndexName RollupIndex { get; internal set; }

		[DataMember(Name ="timeout")]
		public Time Timeout { get; internal set; }
	}
}
