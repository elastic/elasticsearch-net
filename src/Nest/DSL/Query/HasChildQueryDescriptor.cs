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
		TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ChildScoreType? _ScoreType { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor _QueryDescriptor { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasChildQueryDescriptor<T> : IQuery, IHasChildQuery where T : class
	{
		TypeNameMarker IHasChildQuery._Type { get; set; }

		string IHasChildQuery._Scope { get; set; }

		ChildScoreType? IHasChildQuery._ScoreType { get; set; }

		IQueryDescriptor IHasChildQuery._QueryDescriptor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasChildQuery)this)._QueryDescriptor == null || ((IHasChildQuery)this)._QueryDescriptor.IsConditionless;
			}
		}

		public HasChildQueryDescriptor()
		{
			((IHasChildQuery)this)._Type = TypeNameMarker.Create<T>();
		}
	

		public HasChildQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildQuery)this)._QueryDescriptor = querySelector(q);
			return this;
		}
		public HasChildQueryDescriptor<T> Scope(string scope)
		{
			((IHasChildQuery)this)._Scope = scope;
			return this;
		}
		public HasChildQueryDescriptor<T> Type(string type)
		{
			((IHasChildQuery)this)._Type = type;
			return this;
		}

		public HasChildQueryDescriptor<T> Score(ChildScoreType? scoreType)
		{
			((IHasChildQuery)this)._ScoreType = scoreType;
			return this;
		}

	}
}
