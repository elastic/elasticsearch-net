using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHasChildQuery
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ChildScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor QueryDescriptor { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasChildQueryDescriptor<T> : IQuery, IHasChildQuery where T : class
	{
		TypeNameMarker IHasChildQuery.Type { get; set; }

		string IHasChildQuery.Scope { get; set; }

		ChildScoreType? IHasChildQuery.ScoreType { get; set; }

		IQueryDescriptor IHasChildQuery.QueryDescriptor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasChildQuery)this).QueryDescriptor == null || ((IHasChildQuery)this).QueryDescriptor.IsConditionless;
			}
		}

		public HasChildQueryDescriptor()
		{
			((IHasChildQuery)this).Type = TypeNameMarker.Create<T>();
		}
	

		public HasChildQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildQuery)this).QueryDescriptor = querySelector(q);
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
