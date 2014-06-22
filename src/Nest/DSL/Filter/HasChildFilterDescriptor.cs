﻿using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HasChildFilterDescriptor<object>>))]
	public interface IHasChildFilter : IFilter
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}
	
	public class HasChildFilter : PlainFilter, IHasChildFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.HasChild = this;
		}

		public TypeNameMarker Type { get; set; }
		public string Scope { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class HasChildFilterDescriptor<T> : FilterBase, IHasChildFilter where T : class
	{
		bool IFilter.IsConditionless
		{
			get
			{
				var hf = ((IHasChildFilter)this);
				return hf.Query == null || hf.Query.IsConditionless || hf.Type.IsNullOrEmpty();
			}
		}

		TypeNameMarker IHasChildFilter.Type { get; set; }

		string IHasChildFilter.Scope { get; set;}
		
		IQueryContainer IHasChildFilter.Query { get; set; }

		public HasChildFilterDescriptor()
		{
			((IHasChildFilter)this).Type = TypeNameMarker.Create<T>();
		}

		public HasChildFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildFilter)this).Query = querySelector(q);
			return this;
		}
		
		public HasChildFilterDescriptor<T> Scope(string scope)
		{
			((IHasChildFilter)this).Scope = scope;
			return this;
		}

		public HasChildFilterDescriptor<T> Type(string type)
		{
			((IHasChildFilter)this).Type = type;
			return this;
		}
	}
}
