using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;

namespace Nest
{
	/// <summary>
	/// </summary>
	public interface IRequest {}
	public interface IRequest<TParameters> : IPathInfo<TParameters>, IRequest
		where TParameters : IRequestParameters, new()
	{
		/// <summary>
		/// Used to describe request parameters not part of the body. e.q query string or 
		/// connection configuration overrides
		/// </summary>
		TParameters RequestParameters { get; set; }

		/// <summary>
		/// 
		/// </summary>
		IRequestConfiguration RequestConfiguration { get; set; }
	}
}