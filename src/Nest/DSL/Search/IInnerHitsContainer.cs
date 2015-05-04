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

	public class InnerHitsContainerDescriptor : IInnerHitsContainer
	{
		private IInnerHitsContainer Self { get { return this; }}

		IDictionary<TypeNameMarker, IGlobalInnerHit> IInnerHitsContainer.Type { get; set; }
		IDictionary<PropertyPathMarker, IGlobalInnerHit> IInnerHitsContainer.Path { get; set; }

		public void Type<T>(Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector) where T : class
		{
			var globalInnerHit = globalInnerHitsSelector == null ? null : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			if (globalInnerHit == null)
			{
				Self.Type = null;
				return;
			}
			Self.Type = new Dictionary<TypeNameMarker, IGlobalInnerHit> {{typeof(T), globalInnerHit}};
		}
		
		public void Path<T>(string path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector) where T : class
		{
			var globalInnerHit = globalInnerHitsSelector == null ? null : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			if (globalInnerHit == null)
			{
				Self.Path = null;
				return;
			}
			Self.Path = new Dictionary<PropertyPathMarker, IGlobalInnerHit> {{ path, globalInnerHit}};
		}

		public void Path<T, TPathReturn>(Expression<Func<T, TPathReturn>> path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> globalInnerHitsSelector) where T : class
		{
			var globalInnerHit = globalInnerHitsSelector == null ? null : globalInnerHitsSelector(new GlobalInnerHitDescriptor<T>());
			if (globalInnerHit == null)
			{
				Self.Path = null;
				return;
			}
			Self.Path = new Dictionary<PropertyPathMarker, IGlobalInnerHit> {{ path, globalInnerHit}};
		}
	}
}