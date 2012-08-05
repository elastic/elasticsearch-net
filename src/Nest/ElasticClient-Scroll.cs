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
			scrollId.ThrowIfNullOrEmpty("scrollId");
			scrollTime.ThrowIfNullOrEmpty("scrollTime");

			scrollId = Uri.EscapeDataString(scrollId);
			scrollTime = Uri.EscapeDataString(scrollTime);

			var path = "_search/scroll?scroll={0}&scroll_id={1}".F(scrollTime, scrollId);

			ConnectionStatus status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<QueryResponse<dynamic>>(status);
			return r;
		}
	}
}