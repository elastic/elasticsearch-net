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
	[JsonConverter(typeof(ReadAsTypeConverter<HasParentQueryDescriptor<object>>))]
	public interface IHasParentQuery : IQuery
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryDescriptor Query { get; set; }

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasParentQueryDescriptor<T> : IHasParentQuery where T : class
	{
		TypeNameMarker IHasParentQuery.Type { get; set; }

		string IHasParentQuery.Scope { get; set; }

		ParentScoreType? IHasParentQuery.ScoreType { get; set; }

		IQueryDescriptor IHasParentQuery.Query { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IHasParentQuery)this).Query == null || ((IHasParentQuery)this).Query.IsConditionless;
			}
		}

		public HasParentQueryDescriptor()
		{
			((IHasParentQuery)this).Type = TypeNameMarker.Create<T>();
		}
		

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentQuery)this).Query = querySelector(q);
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
