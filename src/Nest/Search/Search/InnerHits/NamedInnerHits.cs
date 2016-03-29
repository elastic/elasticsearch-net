using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<NamedInnerHits, string, IInnerHitsContainer>))]
	public interface INamedInnerHits : IIsADictionary<string, IInnerHitsContainer> { }

	public class NamedInnerHits : IsADictionaryBase<string, IInnerHitsContainer>, INamedInnerHits
	{
		public NamedInnerHits() : base() { }
		public NamedInnerHits(IDictionary<string, IInnerHitsContainer> container) : base(container) { }
		public NamedInnerHits(Dictionary<string, IInnerHitsContainer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, IInnerHitsContainer container) => this.BackingDictionary.Add(name, container);

		public void Add(string name, ITypeInnerHit typeInnerHit) => this.BackingDictionary.Add(name, new InnerHitsContainer { Type = typeInnerHit });

		public void Add(string name, IPathInnerHit pathInnerHit) => this.BackingDictionary.Add(name, new InnerHitsContainer { Path = pathInnerHit });
	}

	public class NamedInnerHitsDescriptor<T>
		: IsADictionaryDescriptorBase<NamedInnerHitsDescriptor<T>, INamedInnerHits, string, IInnerHitsContainer>
		where T : class
	{
		public NamedInnerHitsDescriptor() : base(new NamedInnerHits()) { }

		public NamedInnerHitsDescriptor<T> Type(string name, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null) =>
			Assign(name, new InnerHitsContainerDescriptor<T>().Type(selector));
		
		public NamedInnerHitsDescriptor<T> Type(string name, TypeName type, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null) =>
			Assign(name, new InnerHitsContainerDescriptor<T>().Type(type,selector));

		public NamedInnerHitsDescriptor<T> Type<TOther>(string name, Func<GlobalInnerHitDescriptor<TOther>, IGlobalInnerHit> selector = null) where TOther : class =>
			Assign(name, new InnerHitsContainerDescriptor<T>().Type<TOther>(selector));

		public NamedInnerHitsDescriptor<T> Path(string name, Field path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null)  =>
			Assign(name, new InnerHitsContainerDescriptor<T>().Path(path, selector));

		public NamedInnerHitsDescriptor<T> Path(string name, Expression<Func<T, object>> path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null)  =>
			Assign(name, new InnerHitsContainerDescriptor<T>().Path(path, selector));

	}
}
