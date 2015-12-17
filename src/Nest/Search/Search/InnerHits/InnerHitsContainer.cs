using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<InnerHitsContainer>))]
	public interface IInnerHitsContainer
	{
		[JsonProperty(PropertyName = "type")]
		ITypeInnerHit Type { get; set; }

		[JsonProperty(PropertyName = "path")]
		IPathInnerHit Path { get; set; }
	}

	public class InnerHitsContainer : IInnerHitsContainer
	{
		public ITypeInnerHit Type { get; set; }
		public IPathInnerHit Path { get; set; }
	}

	public class InnerHitsContainerDescriptor<T> : DescriptorBase<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>, IInnerHitsContainer 
		where T : class
	{
		ITypeInnerHit IInnerHitsContainer.Type { get; set; }
		IPathInnerHit IInnerHitsContainer.Path { get; set; }

		public InnerHitsContainerDescriptor<T> Type<TOther>(Func<GlobalInnerHitDescriptor<TOther>, IGlobalInnerHit> selector = null) where TOther : class => 
			Assign(a => a.Type = new TypeInnerHitDescriptor<TOther>().Type(typeof(TOther), selector)?.PromisedValue);
	
		public InnerHitsContainerDescriptor<T> Type(TypeName type, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null) => 
			Assign(a => a.Type = new TypeInnerHitDescriptor<T>().Type(type, selector)?.PromisedValue);

		public InnerHitsContainerDescriptor<T> Path(Field path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null)  =>
			Assign(a => a.Path = new PathInnerHitDescriptor<T>().Path(path, selector)?.PromisedValue);

		public InnerHitsContainerDescriptor<T> Path(Expression<Func<T, object>> path, Func<GlobalInnerHitDescriptor<T>, IGlobalInnerHit> selector = null)  =>
			Assign(a => a.Path = new PathInnerHitDescriptor<T>().Path(path, selector)?.PromisedValue);
	}
}