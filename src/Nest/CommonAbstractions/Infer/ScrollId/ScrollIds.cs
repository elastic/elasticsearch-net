using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public class ScrollIds : IUrlParameter
	{
		private readonly IEnumerable<ScrollId> _scrollIds;
		public ScrollIds(IEnumerable<string> scrollIds)
		{
			if (!scrollIds.HasAny()) throw new ArgumentException("can not create ScrollIds an empty enumerable of strings", nameof(scrollIds));
			this._scrollIds = scrollIds.Select(s => (ScrollId)s);
		}

		public static ScrollIds Parse(string scrollIds)
		{
			if (scrollIds.IsNullOrEmpty()) throw new ArgumentException("can not create ScrollIds from an empty string", nameof(scrollIds));
			var scrolls = scrollIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return new ScrollIds(scrolls);
		}

		public string GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", this._scrollIds.Select(s => s.GetString(settings)));

		public static implicit operator ScrollIds(string scrollIds) => Parse(scrollIds);
	}
}