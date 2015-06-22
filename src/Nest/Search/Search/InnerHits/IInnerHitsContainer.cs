using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInnerHitsContainer
	{
		[JsonProperty(PropertyName = "type")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<TypeNameMarker, IGlobalInnerHit> Type { get; set; }

		[JsonProperty(PropertyName = "path")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, IGlobalInnerHit> Path { get; set; }
	}

	public class InnerHitsContainer : IInnerHitsContainer
	{
		public IDictionary<TypeNameMarker, IGlobalInnerHit> Type { get; set; }
		public IDictionary<PropertyPathMarker, IGlobalInnerHit> Path { get; set; }
	}

	public class InnerHitsContainerDescriptor<T> : IInnerHitsContainer where T : class
	{
		private IInnerHitsContainer Self { get { return this; }}

		IDictionary<TypeNameMarker, IGlobalInnerHit> IInnerHitsContainer.Type { get; set; }
		IDictionary<PropertyPathMarker, IGlobalInnerHit> IInnerHitsContainer.Path { get; set; }

		public InnerHitsContainerDescriptor<T> Type(Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			Self.Type = new Dictionary<TypeNameMarker, IGlobalInnerHit> {{typeof(T), globalInnerHit}};
			return this;
		}
		
		public InnerHitsContainerDescriptor<T> Type<TOther>(Func<GlobalInnerHitDescriptor<TOther>, IGlobalInnerHit> globalInnerHitsSelector = null) where TOther : class
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<TOther>());
			Self.Type = new Dictionary<TypeNameMarker, IGlobalInnerHit> {{typeof(TOther), globalInnerHit}};
			return this;
		}

		public InnerHitsContainerDescriptor<T> Path(string path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			Self.Path = new Dictionary<PropertyPathMarker, IGlobalInnerHit> {{ path, globalInnerHit}};
			return this;
		}

		public InnerHitsContainerDescriptor<T> Path(Expression<Func<T, object>> path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector = null) 
		{
			var globalInnerHit = globalInnerHitsSelector == null ? new GlobalInnerHit() : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			Self.Path = new Dictionary<PropertyPathMarker, IGlobalInnerHit> {{ path, globalInnerHit}};
			return this;
		}
	}
}