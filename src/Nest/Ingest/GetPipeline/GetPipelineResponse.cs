using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGetPipelineResponse : IResponse
	{
		[JsonProperty("pipelines")]
		List<PipelineInfo> Pipelines { get; }
	}

	public class GetPipelineResponse : ResponseBase, IGetPipelineResponse
	{
		public List<PipelineInfo> Pipelines { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class PipelineInfo
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("config")]
		public Pipeline Config { get; internal set; }
	}
}
