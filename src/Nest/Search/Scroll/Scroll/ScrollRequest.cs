using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IScrollRequest : ICovariantSearchRequest
	{
		[JsonProperty("scroll")]
		Time Scroll { get; set; }

		[JsonProperty("scroll_id")]
		string ScrollId { get; set; }
	}

	public partial class ScrollRequest
	{
		public ScrollRequest(string scrollId, Time scroll)
		{
			Scroll = scroll;
			ScrollId = scrollId;
		}

		public Types CovariantTypes { get; set; }

		public Time Scroll { get; set; }

		public string ScrollId { get; set; }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => _clrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => CovariantTypes;
	}

	public partial class ScrollDescriptor<T> where T : class
	{
		private Types _covariantTypes = null;
		Type ICovariantSearchRequest.ClrType => typeof(T);
		Types ICovariantSearchRequest.ElasticsearchTypes => _covariantTypes;

		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }
		Func<dynamic, Hit<dynamic>, Type> ICovariantSearchRequest.TypeSelector { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(Time scroll) => Assign(a => a.Scroll = scroll);

		public ScrollDescriptor<T> ScrollId(string scrollId) => Assign(a => a.ScrollId = scrollId);

		public ScrollDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) =>
			Assign(a => a.TypeSelector = typeSelector);

		public ScrollDescriptor<T> CovariantTypes(Types covariantTypes) => Assign(a => _covariantTypes = covariantTypes);
	}
}
