// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("scroll.json")]
	public partial interface IScrollRequest : ITypedSearchRequest
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

		Type ITypedSearchRequest.ClrType => null;
	}

	public partial class ScrollDescriptor<TInferDocument> where TInferDocument : class
	{
		public ScrollDescriptor(Time scroll, string scrollId) => ScrollId(scrollId).Scroll(scroll);

		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);

		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<TInferDocument> Scroll(Time scroll) => Assign(scroll, (a, v) => a.Scroll = v);

		public ScrollDescriptor<TInferDocument> ScrollId(string scrollId) => Assign(scrollId, (a, v) => a.ScrollId = v);
	}
}
