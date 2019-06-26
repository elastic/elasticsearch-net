using System;
using System.Runtime.Serialization;

namespace Nest
{
	public partial interface IScrollRequest : ISearchTypeInformation
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

		Type ISearchTypeInformation.ClrType => null;
	}

	public partial class ScrollDescriptor<TInferDocument> where TInferDocument : class
	{
		public ScrollDescriptor(Time scroll, string scrollId) => ScrollId(scrollId).Scroll(scroll);

		Type ISearchTypeInformation.ClrType => typeof(TInferDocument);

		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<TInferDocument> Scroll(Time scroll) => Assign(scroll, (a, v) => a.Scroll = v);

		public ScrollDescriptor<TInferDocument> ScrollId(string scrollId) => Assign(scrollId, (a, v) => a.ScrollId = v);
	}
}
