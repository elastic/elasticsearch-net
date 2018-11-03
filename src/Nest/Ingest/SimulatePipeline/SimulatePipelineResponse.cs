using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISimulatePipelineResponse : IResponse
	{
		[JsonProperty("docs")]
		IReadOnlyCollection<PipelineSimulation> Documents { get; }
	}

	public class SimulatePipelineResponse : ResponseBase, ISimulatePipelineResponse
	{
		public IReadOnlyCollection<PipelineSimulation> Documents { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class PipelineSimulation
	{
		[JsonProperty("doc")]
		public DocumentSimulation Document { get; internal set; }

		[JsonProperty("processor_results")]
		public IReadOnlyCollection<PipelineSimulation> ProcessorResults { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;

		[JsonProperty("tag")]
		public string Tag { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class DocumentSimulation
	{
		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_ingest")]
		public Ingest Ingest { get; internal set; }

		[JsonProperty("_parent")]
		public string Parent { get; internal set; }

		[JsonProperty("_routing")]
		public string Routing { get; internal set; }

		[JsonProperty("_source")]
		public ILazyDocument Source { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class Ingest
	{
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
