using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGetPipelineResponse : IResponse
	{
		[DataMember(Name ="pipelines")]
		IReadOnlyDictionary<string, IPipeline> Pipelines { get; }
	}

	[JsonFormatter(typeof(DictionaryResponseFormatter<GetPipelineResponse, string, IPipeline>))]
	public class GetPipelineResponse : DictionaryResponseBase<string, IPipeline>, IGetPipelineResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IPipeline> Pipelines => Self.BackingDictionary;
	}
}
