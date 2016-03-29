using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<TypeInnerHit, TypeName, IGlobalInnerHit>))]
	public interface ITypeInnerHit : IIsADictionary<TypeName, IGlobalInnerHit> { }

	public class TypeInnerHit : IsADictionaryBase<TypeName, IGlobalInnerHit>, ITypeInnerHit
	{
		public TypeInnerHit() {}

		public TypeInnerHit(IDictionary<TypeName, IGlobalInnerHit> container) : base(container) { }
		public TypeInnerHit(Dictionary<TypeName, IGlobalInnerHit> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName name, IGlobalInnerHit script) => this.BackingDictionary.Add(name, script);
	}

	public class TypeInnerHit<T> : TypeInnerHit
	{
		private readonly TypeName _name;
		public TypeInnerHit() : base()
		{
			_name = typeof(T);
			this.InnerHit = new GlobalInnerHit();
		}

		public IGlobalInnerHit InnerHit
		{
			get { return !this._name.IsNullOrEmpty() && this.BackingDictionary.ContainsKey(this._name) ? this.BackingDictionary[this._name] : null; }
			set { this.BackingDictionary[this._name] = value; }
		}
	}

	public class TypeInnerHitDescriptor<T>
		: IsADictionaryDescriptorBase<TypeInnerHitDescriptor<T>, ITypeInnerHit, TypeName, IGlobalInnerHit>
		where T : class
	{
		public TypeInnerHitDescriptor() : base(new TypeInnerHit()) { }

		public TypeInnerHitDescriptor<T> Type(TypeName name, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) => 
			this.Assign(name, globalInnerHitsSelector.InvokeOrDefault(new GlobalInnerHitDescriptor<T>()));

	}
}
