using System;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Scrolling search, ideal for scrolling on the server as it allows to keep a query open on the serverside.
		/// Please consult the docs http://www.elasticsearch.org/guide/reference/api/search/scroll.html
		/// on the do's and don'ts!
		/// </summary>
		public IQueryResponse<T> Scroll<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) 
			where T : class
		{
			string scrollId;
			var pathInfo = GetPathInfo(scrollSelector, out scrollId);
			return this.RawDispatch.ScrollDispatch(pathInfo, scrollId)
				.Deserialize<QueryResponse<T>>();
		}
		public Task<IQueryResponse<T>> ScrollAsync<T>(
			Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector) 
			where T : class
		{
			string scrollId;
			var pathInfo = GetPathInfo(scrollSelector, out scrollId);
			return this.RawDispatch.ScrollDispatchAsync(pathInfo, scrollId)
				.ContinueWith<IQueryResponse<T>>(t => t.Result.Deserialize<QueryResponse<T>>());
		}
		private ElasticSearchPathInfo<ScrollQueryString> GetPathInfo<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector, out string scrollId) where T : class
		{
			scrollSelector.ThrowIfNull("scrollSelector");
			var scrollDescriptor = scrollSelector(new ScrollDescriptor<T>());
			scrollId = scrollDescriptor._QueryString._scroll_id;
			scrollId.ThrowIfNullOrEmpty("scrollId");

			var pathInfo = scrollDescriptor.ToPathInfo(this._connectionSettings);
			return pathInfo;
		}
	}
}