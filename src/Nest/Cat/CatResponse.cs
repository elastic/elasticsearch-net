using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ICatResponse<out TCatRecord> : IResponse
		where TCatRecord : ICatRecord
	{
		IEnumerable<TCatRecord> Records { get; }
	}

	[JsonObject]
	public class CatResponse<TCatRecord> : BaseResponse, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IEnumerable<TCatRecord> Records { get; internal set; }
	}
}
