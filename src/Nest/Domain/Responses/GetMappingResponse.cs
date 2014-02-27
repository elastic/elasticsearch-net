using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		RootObjectMapping Mapping { get; }
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, RootObjectMapping>>>
	{
		
	}

	public class GetMappingResponse : BaseResponse, IGetMappingResponse
	{
		public GetMappingResponse()
		{
			this.IsValid = true;
		}

		internal GetMappingResponse(ElasticsearchResponse status, GetRootObjectMappingWrapping dict)
		{
			this.ConnectionStatus = status;
			this.IsValid = status.Success && dict != null && dict.Count > 0;
			if (!this.IsValid) return;
			var firstNode = dict.First();
			if (!firstNode.Value.HasAny())
				return;
			var mappingNode = firstNode.Value["mappings"];
			if (mappingNode == null)
			{
				this.IsValid = false;
				return;
			}
			var mapping = mappingNode.First();
			if (mapping.Value == null)
			{
				this.IsValid = false;
				return;
			}
			mapping.Value.Name = mapping.Key;
			this.Mapping = mapping.Value;
		}

		public RootObjectMapping Mapping { get; internal set; }
	}
}