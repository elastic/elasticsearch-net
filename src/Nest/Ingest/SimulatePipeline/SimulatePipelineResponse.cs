// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class SimulatePipelineResponse : ResponseBase
	{
		[DataMember(Name ="docs")]
		public IReadOnlyCollection<PipelineSimulation> Documents { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;
	}

	[DataContract]
	public class PipelineSimulation
	{
		[DataMember(Name ="doc")]
		public DocumentSimulation Document { get; internal set; }

		[DataMember(Name ="processor_results")]
		public IReadOnlyCollection<PipelineSimulation> ProcessorResults { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;

		[DataMember(Name ="tag")]
		public string Tag { get; internal set; }
	}

	[DataContract]
	public class DocumentSimulation
	{
		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="_ingest")]
		public Ingest Ingest { get; internal set; }

		[DataMember(Name ="_parent")]
		public string Parent { get; internal set; }

		[DataMember(Name ="_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="_source")]
		public ILazyDocument Source { get; internal set; }
	}

	[DataContract]
	public class Ingest
	{
		[DataMember(Name ="timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
