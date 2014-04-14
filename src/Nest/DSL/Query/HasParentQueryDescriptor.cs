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
	public interface IHasParentQuery
	{
		[JsonProperty("type")]
		TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? _ScoreType { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor _QueryDescriptor { get; set; }

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasParentQueryDescriptor<T> : IQuery, IHasParentQuery where T : class
	{
		TypeNameMarker IHasParentQuery._Type { get; set; }

		string IHasParentQuery._Scope { get; set; }

		ParentScoreType? IHasParentQuery._ScoreType { get; set; }

		IQueryDescriptor IHasParentQuery._QueryDescriptor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasParentQuery)this)._QueryDescriptor == null || ((IHasParentQuery)this)._QueryDescriptor.IsConditionless;
			}
		}

		public HasParentQueryDescriptor()
		{
			((IHasParentQuery)this)._Type = TypeNameMarker.Create<T>();
		}
		

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentQuery)this)._QueryDescriptor = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Scope(string scope)
		{
			((IHasParentQuery)this)._Scope = scope;
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			((IHasParentQuery)this)._Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.score)
		{
			((IHasParentQuery)this)._ScoreType = scoreType;
			return this;
		}

	}
}
