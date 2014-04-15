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
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor QueryDescriptor { get; set; }

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasParentQueryDescriptor<T> : IQuery, IHasParentQuery where T : class
	{
		TypeNameMarker IHasParentQuery.Type { get; set; }

		string IHasParentQuery.Scope { get; set; }

		ParentScoreType? IHasParentQuery.ScoreType { get; set; }

		IQueryDescriptor IHasParentQuery.QueryDescriptor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasParentQuery)this).QueryDescriptor == null || ((IHasParentQuery)this).QueryDescriptor.IsConditionless;
			}
		}

		public HasParentQueryDescriptor()
		{
			((IHasParentQuery)this).Type = TypeNameMarker.Create<T>();
		}
		

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentQuery)this).QueryDescriptor = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Scope(string scope)
		{
			((IHasParentQuery)this).Scope = scope;
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			((IHasParentQuery)this).Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.score)
		{
			((IHasParentQuery)this).ScoreType = scoreType;
			return this;
		}

	}
}
