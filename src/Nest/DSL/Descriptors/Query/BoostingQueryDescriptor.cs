using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class BoostingQueryDescriptor<T> where T : class
	{
		[JsonProperty("positive")]
		internal QueryDescriptor<T> _PositiveQuery { get; set; }

		[JsonProperty("negative")]
		internal QueryDescriptor<T> _NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		internal double? _NegativeBoost { get; set; }

		public BoostingQueryDescriptor<T> NegativeBoost(double boost)
		{
			this._NegativeBoost = boost;
			return this;
		}

		public BoostingQueryDescriptor<T> Positive(Action<QueryDescriptor<T>> selector)
		{
			var query = new QueryDescriptor<T>();
			selector(query);
			this._PositiveQuery = query;
			return this;
		}
		public BoostingQueryDescriptor<T> Negative(Action<QueryDescriptor<T>> selector)
		{
			var query = new QueryDescriptor<T>();
			selector(query);
			this._NegativeQuery = query;
			return this;
		}
	}
}
