using System;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Scrolling search, ideal for scrolling on the server as it allows to keep a query open on the serverside.
		/// Please consult the docs http://www.elasticsearch.org/guide/reference/api/search/scroll.html
		/// on the do's and don'ts!
		/// </summary>
		public IQueryResponse<dynamic> Scroll(string scrollTime, string scrollId)
		{
			return Scroll<dynamic>(scrollTime, scrollId);
		}

		public IQueryResponse<T> Scroll<T>(string scrollTime, string scrollId) where T : class
		{
			scrollId.ThrowIfNullOrEmpty("scrollId");
			scrollTime.ThrowIfNullOrEmpty("scrollTime");

			scrollTime = Uri.EscapeDataString(scrollTime);

			var path = "_search/scroll?scroll={0}".F(scrollTime);

			ConnectionStatus status = this.Connection.PostSync(path, scrollId);
			var r = this.ToParsedResponse<QueryResponse<T>>(status
				, extraConverters: new[] { new ConcreteTypeConverter(typeof(T), (d, h) => typeof(T)) }	
			);
			return r;
		}
	}
}