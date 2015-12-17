using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<NamedInnerHits, string, IInnerHitsContainer>))]
	public interface INamedInnerHits : IIsADictionary<string, IInnerHitsContainer> { }

	public class NamedInnerHits : IsADictionary<string, IInnerHitsContainer>, INamedInnerHits
	{
		public NamedInnerHits() : base() { }
		public NamedInnerHits(IDictionary<string, IInnerHitsContainer> container) : base(container) { }
		public NamedInnerHits(Dictionary<string, IInnerHitsContainer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, IInnerHitsContainer script) => this.BackingDictionary.Add(name, script);
	}

	public class NamedInnerHitsDescriptor<T>
		: IsADictionaryDescriptor<NamedInnerHitsDescriptor<T>, INamedInnerHits, string, IInnerHitsContainer>
		where T : class
	{
		public NamedInnerHitsDescriptor() : base(new NamedInnerHits()) { }

		public NamedInnerHitsDescriptor<T> Type(string name,Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			return AssignToInnerHit(name, globalInnerHit, (g, c) => c.Type = new Dictionary<TypeName, IGlobalInnerHit> { { typeof(T), globalInnerHit } });
		}
		
		public NamedInnerHitsDescriptor<T> Type<TOther>(string name, Func<GlobalInnerHitDescriptor<TOther>, IGlobalInnerHit> globalInnerHitsSelector = null) where TOther : class
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<TOther>());
			return AssignToInnerHit(name, globalInnerHit, (g, c) => c.Type = new Dictionary<TypeName, IGlobalInnerHit> { { typeof(TOther), globalInnerHit } });
		}

		public NamedInnerHitsDescriptor<T> Type(string name, TypeName type, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			return AssignToInnerHit(name, globalInnerHit, (g, c) => c.Type = new Dictionary<TypeName, IGlobalInnerHit> { { type, globalInnerHit } });
		}

		public NamedInnerHitsDescriptor<T> Path(string name, Field path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			return AssignToInnerHit(name, globalInnerHit, (g, c) => c.Path = new Dictionary<Field, IGlobalInnerHit> { { path, globalInnerHit } });
		}

		public NamedInnerHitsDescriptor<T> Path(string name, Expression<Func<T, object>> path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			return AssignToInnerHit(name, globalInnerHit, (g, c) => c.Path = new Dictionary<Field, IGlobalInnerHit> { { path, globalInnerHit } });
		}


		private NamedInnerHitsDescriptor<T> AssignToInnerHit(string name, IGlobalInnerHit innerHit, Action<IGlobalInnerHit, InnerHitsContainer> assign)
		{
			var c = new InnerHitsContainer();
			assign(innerHit, c);
			return Assign(name, c);
		}
	}
}
