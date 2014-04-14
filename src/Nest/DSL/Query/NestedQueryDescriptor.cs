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
	public interface INestedQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? _Score { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor _QueryDescriptor { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker _Path { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedQueryDescriptor<T> : IQuery, INestedQuery where T : class
	{
		NestedScore? INestedQuery._Score { get; set; }

		IQueryDescriptor INestedQuery._QueryDescriptor { get; set; }

		PropertyPathMarker INestedQuery._Path { get; set; }

		string INestedQuery._Scope { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((INestedQuery)this)._QueryDescriptor == null || ((INestedQuery)this)._QueryDescriptor.IsConditionless;
			}
		}

		public NestedQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((INestedQuery)this)._QueryDescriptor = querySelector(q);
			return this;
		}

		public NestedQueryDescriptor<T> Score(NestedScore score)
		{
			((INestedQuery)this)._Score = score;
			return this;
		}
		public NestedQueryDescriptor<T> Path(string path)
		{
			((INestedQuery)this)._Path = path;
			return this;
		}
		public NestedQueryDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			((INestedQuery)this)._Path = objectPath;
			return this;
		}
		public NestedQueryDescriptor<T> Scope(string scope)
		{
			((INestedQuery)this)._Scope = scope;
			return this;
		}
	}
}
