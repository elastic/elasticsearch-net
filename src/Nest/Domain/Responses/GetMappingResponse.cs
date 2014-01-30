using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		RootObjectMapping Mapping { get; }
	}

	public class GetMappingResponse : BaseResponse, IGetMappingResponse
	{
		public GetMappingResponse()
		{
			this.IsValid = true;
		}

		internal GetMappingResponse(ConnectionStatus status, Dictionary<string, RootObjectMapping> dict)
		{
			this.IsValid = status.Success;
			if (dict == null || dict.Count <= 0) return;
			var mapping = dict.First();
			mapping.Value.TypeNameMarker = mapping.Key;
			this.Mapping = mapping.Value;
		}

		public RootObjectMapping Mapping { get; internal set; }
	}
}