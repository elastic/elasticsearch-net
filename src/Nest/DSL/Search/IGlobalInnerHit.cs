using System;
using System.Collections.Generic;
using System.Linq;
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

		public GlobalInnerHitDescriptor<T> InnerHits(
			Func<
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>, 
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>
			> innerHitsSelector)
		{
			if (innerHitsSelector == null)
			{
				Self.InnerHits = null;
				return this;
			}
			var containers = innerHitsSelector(new FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>())
				.Where(kv => kv.Value != null)
				.Select(kv => new {Key = kv.Key, Value = kv.Value(new InnerHitsContainerDescriptor<T>())})
				.Where(kv => kv.Value != null)
				.ToDictionary(kv => kv.Key, kv => kv.Value);
			if (containers == null || containers.Count == 0)
			{
				Self.InnerHits = null;
				return this;
			}
			Self.InnerHits = containers;
			return this;
		}
	}
}