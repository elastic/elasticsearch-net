using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INestedQuery : IQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor QueryDescriptor { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }

		[JsonProperty("_scope")]
		string Scope { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedQueryDescriptor<T> : INestedQuery where T : class
	{
		NestedScore? INestedQuery.Score { get; set; }

		IQueryDescriptor INestedQuery.QueryDescriptor { get; set; }

		PropertyPathMarker INestedQuery.Path { get; set; }

		string INestedQuery.Scope { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((INestedQuery)this).QueryDescriptor == null || ((INestedQuery)this).QueryDescriptor.IsConditionless;
			}
		}

		public NestedQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((INestedQuery)this).QueryDescriptor = querySelector(q);
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
