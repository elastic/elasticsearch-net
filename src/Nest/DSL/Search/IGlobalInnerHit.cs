using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGlobalInnerHit
	{
		[JsonProperty(PropertyName = "query")]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHit : IGlobalInnerHit
	{
		public IQueryContainer Query { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHitDescriptor<T> : IGlobalInnerHit where T : class
	{
		private IGlobalInnerHit Self { get { return this; } }

		IQueryContainer IGlobalInnerHit.Query { get; set; }
		IDictionary<string, IInnerHitsContainer> IGlobalInnerHit.InnerHits { get; set; }

		public GlobalInnerHitDescriptor<T> Query(Func<QueryDescriptor<T>, IQueryContainer> querySelector)
		{
			Self.Query = querySelector == null ? null : querySelector(new QueryDescriptor<T>());
			return this;
		}
	}
}