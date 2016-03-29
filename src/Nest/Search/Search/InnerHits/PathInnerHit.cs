using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<PathInnerHit, Field, IGlobalInnerHit>))]
	public interface IPathInnerHit : IIsADictionary<Field, IGlobalInnerHit> { }

	public class PathInnerHit : IsADictionaryBase<Field, IGlobalInnerHit>, IPathInnerHit
	{
		public PathInnerHit() {}
		public PathInnerHit(IDictionary<Field, IGlobalInnerHit> container) : base(container) { }
		public PathInnerHit(Dictionary<Field, IGlobalInnerHit> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(Field name, IGlobalInnerHit script) => this.BackingDictionary.Add(name, script);
	}

	public class PathInnerHit<T> : PathInnerHit
	{
		private readonly Field _path;
		public PathInnerHit(Field path) : base() { _path = path; }
		public PathInnerHit(Expression<Func<T, object>> path) : base() { _path = path; }

		public IGlobalInnerHit InnerHit
		{
			get { return this._path != null && this.BackingDictionary.ContainsKey(this._path) ? this.BackingDictionary[this._path] : null; }
			set { this.BackingDictionary[this._path] = value; }
		}
	}

	public class PathInnerHitDescriptor<T>
		: IsADictionaryDescriptorBase<PathInnerHitDescriptor<T>, IPathInnerHit, Field, IGlobalInnerHit>
		where T : class
	{
		public PathInnerHitDescriptor() : base(new PathInnerHit()) { }

		public PathInnerHitDescriptor<T> Path(Field name,Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) => 
			this.Assign(name, globalInnerHitsSelector.InvokeOrDefault(new GlobalInnerHitDescriptor<T>()));

		public PathInnerHitDescriptor<T> Path(Expression<Func<T, object>> name,Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) => 
			this.Assign(name, globalInnerHitsSelector.InvokeOrDefault(new GlobalInnerHitDescriptor<T>()));

	}
}
