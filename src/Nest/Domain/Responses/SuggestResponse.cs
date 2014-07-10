using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface ISuggestResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		IDictionary<string, Suggest[]> Suggestions { get; set; }
	}

	[JsonObject]
	public class SuggestResponse : BaseResponse, ISuggestResponse
	{
		public SuggestResponse()
		{
			this.IsValid = true;
			this.Suggestions = new Dictionary<string, Suggest[]>();
		}
		
		public ShardsMetaData Shards { get; internal set; }

		public IDictionary<string, Suggest[]> Suggestions { get; set;}
	}
}