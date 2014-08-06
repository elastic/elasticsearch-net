using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiPercolateResponse : IResponse
	{
		[JsonProperty("responses")]
		IEnumerable<PercolateResponse> Responses { get; }
	}

	[JsonObject]
	public class MultiPercolateResponse : BaseResponse, IMultiPercolateResponse
	{
		public IEnumerable<PercolateResponse> Responses { get; private set; }
	}
}
