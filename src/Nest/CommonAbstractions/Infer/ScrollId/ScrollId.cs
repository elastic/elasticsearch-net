using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public class ScrollId : IUrlParameter
	{
		private readonly string _scrollId;
		public ScrollId(string id) { this._scrollId = id; }

		public string GetString(IConnectionConfigurationValues settings) => _scrollId;
		public static implicit operator ScrollId(string id) => new ScrollId(id);
	}
}