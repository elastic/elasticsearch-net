using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class BasePathRequest<TParameters> : BaseRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		
		[JsonIgnore]
		public IRequestConfiguration RequestConfiguration
		{	
			get { return base._requestConfiguration; }
			set { base._requestConfiguration = value; }
		}
	}
}