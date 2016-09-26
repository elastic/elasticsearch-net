using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISimulatePipelineResponse : IResponse
	{
		[JsonProperty("docs")]
		List<PipelineSimulation> Documents { get; set; }
	}

	public class SimulatePipelineResponse : ResponseBase, ISimulatePipelineResponse
	{
		public List<PipelineSimulation> Documents { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class PipelineSimulation
	{
		[JsonProperty("processor_results")]
		public List<PipelineSimulation> ProcessorResults { get; internal set; }

		[JsonProperty("tag")]
		public string Tag { get; internal set; }

		[JsonProperty("doc")]
		public DocumentSimulation Document { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class DocumentSimulation
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_parent")]
		public string Parent { get; internal set; }

		[JsonProperty("_routing")]
		public string Routing { get; internal set; }

		[JsonProperty("_source")]
		public ILazyDocument Source { get; internal set; }

		[JsonProperty("_ingest")]
		public Ingest Ingest { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class Ingest
	{
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
