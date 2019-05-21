using System;
using System.Runtime.Serialization;

namespace Nest
{
	public partial interface IScrollRequest : ICovariantSearchRequest
	{
		[DataMember(Name ="scroll")]
		Time Scroll { get; set; }

		[DataMember(Name ="scroll_id")]
		string ScrollId { get; set; }
	}

	public partial class ScrollRequest
	{
		public ScrollRequest(string scrollId, Time scroll)
		{
			Scroll = scroll;
			ScrollId = scrollId;
		}

		public Time Scroll { get; set; }

		public string ScrollId { get; set; }

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => _clrType;
	}

	public partial class ScrollDescriptor<TDocument> where TDocument : class
	{
		Type ICovariantSearchRequest.ClrType => typeof(TDocument);

		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<TDocument> Scroll(Time scroll) => Assign(scroll, (a, v) => a.Scroll = v);

		public ScrollDescriptor<TDocument> ScrollId(string scrollId) => Assign(scrollId, (a, v) => a.ScrollId = v);
	}
}
