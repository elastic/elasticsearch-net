using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ICatResponse<TCatRectord> : IResponse
		where TCatRectord : ICatRecord
	{
		IEnumerable<TCatRectord> Records { get; }
	}

	[JsonObject]
	public class CatResponse<TCatRecord> : BaseResponse, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IEnumerable<TCatRecord> Records { get; internal set; }

		public CatResponse(IElasticsearchResponse response)
		{
			this.IsValid = response.Success && response.HttpStatusCode == 200;
		}
	}

	public interface ICatRecord {}

	[JsonObject]
	public class CatAliasesRecord : ICatRecord
	{
		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("filter")]
		public string Filter { get; set; }

		[JsonProperty("indexRouting")]
		public string IndexRouting { get; set; }

		[JsonProperty("searchRouting")]
		public string SearchRouting { get; set; }

	}
}
