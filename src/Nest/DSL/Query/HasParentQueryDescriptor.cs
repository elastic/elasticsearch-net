using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
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
		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return false; } }
		public TypeNameMarker Type { get; set; }
		public ParentScoreType? ScoreType { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class HasParentQueryDescriptor<T> : IHasParentQuery where T : class
	{
		private IHasParentQuery Self { get { return this; }}

		TypeNameMarker IHasParentQuery.Type { get; set; }

		ParentScoreType? IHasParentQuery.ScoreType { get; set; }
		
		string IQuery.Name { get; set; }

		IQueryContainer IHasParentQuery.Query { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		public HasParentQueryDescriptor()
		{
			Self.Type = TypeNameMarker.Create<T>();
		}

		public HasParentQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public HasParentQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}
		public HasParentQueryDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		public HasParentQueryDescriptor<T> Score(ParentScoreType? scoreType = ParentScoreType.Score)
		{
			Self.ScoreType = scoreType;
			return this;
		}

	}
}
