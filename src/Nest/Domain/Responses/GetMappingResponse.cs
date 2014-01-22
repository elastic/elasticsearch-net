using System.Collections.Generic;
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
		public RootObjectMapping Mapping { get; internal set; }
	}
}