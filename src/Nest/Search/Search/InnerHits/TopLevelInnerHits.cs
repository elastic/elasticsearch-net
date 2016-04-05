using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<TopLevelInnerHits, string, ITopLevelInnerHit>))]
	public interface ITopLevelInnerHits : IIsADictionary<string, ITopLevelInnerHit> { }

	public class TopLevelInnerHits : IsADictionaryBase<string, ITopLevelInnerHit>, ITopLevelInnerHits
	{
		public TopLevelInnerHits() : base() { }
		public TopLevelInnerHits(IDictionary<string, ITopLevelInnerHit> container) : base(container) { }
		public TopLevelInnerHits(Dictionary<string, ITopLevelInnerHit> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ITopLevelInnerHit innerHit) => this.BackingDictionary.Add(name, innerHit);
	}

	public class TopLevelInnerHitsDescriptor<T>
		: IsADictionaryDescriptorBase<TopLevelInnerHitsDescriptor<T>, ITopLevelInnerHits, string, ITopLevelInnerHit>
		where T : class
	{
		public TopLevelInnerHitsDescriptor() : base(new TopLevelInnerHits()) { }

		public TopLevelInnerHitsDescriptor<T> Type(string name, TypeName type, Func<TopLevelInnerHit<T>, ITopLevelInnerHit> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new TopLevelInnerHit<T>().Type(type)));

		public TopLevelInnerHitsDescriptor<T> Type<TOther>(string name, Func<TopLevelInnerHit<T>, ITopLevelInnerHit> selector = null) where TOther : class =>
			Assign(name, selector.InvokeOrDefault(new TopLevelInnerHit<T>().Type<TOther>()));

		public TopLevelInnerHitsDescriptor<T> Path(string name, Field path, Func<TopLevelInnerHit<T>, ITopLevelInnerHit> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new TopLevelInnerHit<T>().Path(path)));

		public TopLevelInnerHitsDescriptor<T> Path(string name, Expression<Func<T, object>> path, Func<TopLevelInnerHit<T>, ITopLevelInnerHit> selector = null) =>
			Assign(name, selector.InvokeOrDefault(new TopLevelInnerHit<T>().Path(path)));
	}
}
