using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorResponse : IResponse
	{
		IEnumerable<TermVectorsResponse> Documents { get; }
	}

	[JsonObject]
	public class MultiTermVectorResponse : BaseResponse, IMultiTermVectorResponse
	{
		[JsonProperty("docs")]
		public IEnumerable<TermVectorsResponse> Documents { get; internal set; }= new List<TermVectorsResponse>();
	}
}
