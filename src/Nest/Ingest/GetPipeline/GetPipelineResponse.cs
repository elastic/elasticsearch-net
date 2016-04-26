using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IGetPipelineResponse : IResponse
	{
		Pipeline Source { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetPipelineResponse : ResponseBase, IGetPipelineResponse
	{
		[JsonProperty("_source")]
		public Pipeline Source { get; internal set; }

		[JsonProperty("_version")]
		public int Version { get; internal set; }
	}
}
