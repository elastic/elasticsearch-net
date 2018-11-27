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

	public partial class ScrollDescriptor<T> where T : class
	{
		Type ICovariantSearchRequest.ClrType => typeof(T);

		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(Time scroll) => Assign(a => a.Scroll = scroll);

		public ScrollDescriptor<T> ScrollId(string scrollId) => Assign(a => a.ScrollId = scrollId);
	}
}
