using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRequest
	{
		IRequestConfiguration RequestConfiguration { get; set; }
	}

	public interface IRequest<TParameters> : IPathInfo<TParameters>, IRequest
		where TParameters : IRequestParameters, new()
	{
		/// <summary>
		/// Used to describe request parameters not part of the body. e.q query string or 
		/// connection configuration overrides
		/// </summary>
		TParameters RequestParameters { get; set; }

        IElasticsearchPathInfo<TParameters> PathInfo { get; set; }
	}
}