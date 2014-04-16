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
	public interface INestedFilterDescriptor : IFilterBase
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
	public class NestedFilterDescriptor<T> : FilterBase, INestedFilterDescriptor where T : class
	{
		NestedScore? INestedFilterDescriptor._Score { get; set; }

		IQueryDescriptor INestedFilterDescriptor._QueryDescriptor { get; set; }

		PropertyPathMarker INestedFilterDescriptor._Path { get; set; }

		string INestedFilterDescriptor._Scope { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((INestedFilterDescriptor)this)._QueryDescriptor == null 
					|| ((INestedFilterDescriptor)this)._QueryDescriptor.IsConditionless;
			}
		}

		public NestedFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((INestedFilterDescriptor)this)._QueryDescriptor = querySelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Score(NestedScore score)
		{
			((INestedFilterDescriptor)this)._Score = score;
			return this;
		}
		public NestedFilterDescriptor<T> Path(string path)
		{
			((INestedFilterDescriptor)this)._Path = path;
			return this;
		}
		public NestedFilterDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			((INestedFilterDescriptor)this)._Path = objectPath;
			return this;
		}
		public NestedFilterDescriptor<T> Scope(string scope)
		{
			((INestedFilterDescriptor)this)._Scope = scope;
			return this;
		}
	}
}
