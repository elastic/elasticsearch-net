// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class NodeIngestStats
	{
		/// <summary> Per pipeline ingest statistics </summary>
		[DataMember(Name = "pipelines")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IngestStats>))]
		public IReadOnlyDictionary<string, IngestStats> Pipelines { get; internal set; }
			= EmptyReadOnly<string, IngestStats>.Dictionary;

		/// <summary> Overall global ingest statistics </summary>
		[DataMember(Name = "total")]
		public IngestStats Total { get; set; }
	}
}
