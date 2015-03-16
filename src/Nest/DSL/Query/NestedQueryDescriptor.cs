using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<NestedQueryDescriptor<object>>))]
	public interface INestedQuery : IQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterDescriptor<object>>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }

	}

	public class NestedQuery : PlainQuery, INestedQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Nested = this;
		}
		
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public NestedScore? Score { get; set; }
		public IFilterContainer Filter { get; set; }
		public IQueryContainer Query { get; set; }
		public PropertyPathMarker Path { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedQueryDescriptor<T> : INestedQuery where T : class
	{
		private INestedQuery Self { get { return this; } }

		NestedScore? INestedQuery.Score { get; set; }

		IFilterContainer INestedQuery.Filter { get; set; }

		IQueryContainer INestedQuery.Query { get; set; }

		PropertyPathMarker INestedQuery.Path { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		string IQuery.Name { get; set; }

		public NestedQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		public NestedQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var q = new FilterDescriptor<T>();
			Self.Filter = filterSelector(q);
			return this;
		}

		public NestedQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}

		public NestedQueryDescriptor<T> Score(NestedScore score)
		{
			Self.Score = score;
			return this;
		}
		public NestedQueryDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public NestedQueryDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}
	}
}
