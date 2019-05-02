using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetPipelineResponse, string, IPipeline>))]
	public class GetPipelineResponse : DictionaryResponseBase<string, IPipeline>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IPipeline> Pipelines => Self.BackingDictionary;
	}
}
