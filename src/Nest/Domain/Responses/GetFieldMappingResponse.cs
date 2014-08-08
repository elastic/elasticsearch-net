using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{

	[JsonConverter(typeof(ElasticTypesConverter))]
	public class ElasticsearchPropertyMappings : Dictionary<PropertyNameMarker, IElasticType>
	{
		
	}

	public class FieldMappings : Dictionary<string, Dictionary<string, ElasticsearchPropertyMappings>> {}

	public interface IGetFieldMappingResponse : IResponse
	{
		FieldMappings FieldMappings { get; set; }
	}

	public class GetFieldMappingResponse : BaseResponse, IGetFieldMappingResponse
	{
		internal GetFieldMappingResponse(IElasticsearchResponse status, FieldMappings dict)
		{
			this.FieldMappings = dict ?? new FieldMappings();
			this.IsValid = status.Success && dict != null && dict.Count > 0;
		}

		public FieldMappings FieldMappings { get; set; }
	}
}