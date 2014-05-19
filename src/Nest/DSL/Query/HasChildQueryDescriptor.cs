using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HasChildQueryDescriptor<object>>))]
	public interface IHasChildQuery : IQuery
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ChildScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryDescriptor Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasChildQueryDescriptor<T> : IHasChildQuery where T : class
	{
		TypeNameMarker IHasChildQuery.Type { get; set; }

		string IHasChildQuery.Scope { get; set; }

		ChildScoreType? IHasChildQuery.ScoreType { get; set; }

		IQueryDescriptor IHasChildQuery.Query { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasChildQuery)this).Query == null || ((IHasChildQuery)this).Query.IsConditionless;
			}
		}

		public HasChildQueryDescriptor()
		{
			((IHasChildQuery)this).Type = TypeNameMarker.Create<T>();
		}
	

		public HasChildQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildQuery)this).Query = querySelector(q);
			return this;
		}
		public HasChildQueryDescriptor<T> Scope(string scope)
		{
			((IHasChildQuery)this).Scope = scope;
			return this;
		}
		public HasChildQueryDescriptor<T> Type(string type)
		{
			((IHasChildQuery)this).Type = type;
			return this;
		}

		public HasChildQueryDescriptor<T> Score(ChildScoreType? scoreType)
		{
			((IHasChildQuery)this).ScoreType = scoreType;
			return this;
		}

	}
}
