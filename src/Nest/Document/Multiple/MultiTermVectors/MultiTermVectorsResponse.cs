using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorsResponse : IResponse
	{
		IEnumerable<TermVectorsResponse> Documents { get; }
	}

	[JsonObject]
	public class MultiTermVectorsResponse : ResponseBase, IMultiTermVectorsResponse
	{
		// TODO For 3.0 we should create a separate term vector object rather than using TermVectorsResponse
		// since it contains general response data that isn't relevant (i.e. ApiCall, StatusCode, etc...)
		[JsonProperty("docs")]
		public IEnumerable<TermVectorsResponse> Documents { get; internal set; } = new List<TermVectorsResponse>();
	}
}
