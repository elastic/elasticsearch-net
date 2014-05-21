using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

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

		[JsonProperty("_scope")]
		string Scope { get; set; }

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedQueryDescriptor<T> : INestedQuery where T : class
	{
		NestedScore? INestedQuery.Score { get; set; }

		IFilterContainer INestedQuery.Filter { get; set; }

		IQueryContainer INestedQuery.Query { get; set; }

		PropertyPathMarker INestedQuery.Path { get; set; }

		string INestedQuery.Scope { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((INestedQuery)this).Query == null || ((INestedQuery)this).Query.IsConditionless;
			}
		}

		public NestedQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var q = new FilterDescriptor<T>();
			((INestedQuery)this).Filter = filterSelector(q);
			return this;
		}

		public NestedQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((INestedQuery)this).Query = querySelector(q);
			return this;
		}

		public NestedQueryDescriptor<T> Score(NestedScore score)
		{
			((INestedQuery)this).Score = score;
			return this;
		}
		public NestedQueryDescriptor<T> Path(string path)
		{
			((INestedQuery)this).Path = path;
			return this;
		}
		public NestedQueryDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			((INestedQuery)this).Path = objectPath;
			return this;
		}
		public NestedQueryDescriptor<T> Scope(string scope)
		{
			((INestedQuery)this).Scope = scope;
			return this;
		}
	}
}
