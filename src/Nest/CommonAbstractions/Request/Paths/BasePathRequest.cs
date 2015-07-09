using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class BasePathRequest<TParameters> : BaseRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{

		protected TOut Q<TOut>(string name) =>
			this.Request.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) =>
			this.Request.RequestParameters.AddQueryStringValue("source", value);

	}
}