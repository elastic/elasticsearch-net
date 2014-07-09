using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
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

		[JsonProperty("score_type")]
		[JsonConverter(typeof (StringEnumConverter))]
		ParentScoreType? ScoreType { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

	}

	public class HasParentQuery : PlainQuery, IHasParentQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.HasParent = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public TypeNameMarker Type { get; set; }
		public ParentScoreType? ScoreType { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class HasParentQueryDescriptor<T> : IHasParentQuery where T : class
	{
		TypeNameMarker IHasParentQuery.Type { get; set; }

		ParentScoreType? IHasParentQuery.ScoreType { get; set; }

		IQueryContainer IHasParentQuery.Query { get; set; }

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
		

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentQuery)this).Query = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			((IHasParentQuery)this).Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.Score)
		{
			((IHasParentQuery)this).ScoreType = scoreType;
			return this;
		}

	}
}
