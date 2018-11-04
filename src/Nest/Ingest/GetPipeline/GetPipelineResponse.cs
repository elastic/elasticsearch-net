using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGetPipelineResponse : IResponse
	{
		[JsonProperty("pipelines")]
		IReadOnlyDictionary<string, IPipeline> Pipelines { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetPipelineResponse, string, IPipeline>))]
	public class GetPipelineResponse : DictionaryResponseBase<string, IPipeline>, IGetPipelineResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IPipeline> Pipelines => Self.BackingDictionary;
	}
}
