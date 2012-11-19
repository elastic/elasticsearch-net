using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomBoostFactorQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		internal double? _BoostFactor { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return this._Query == null || this._Query.IsConditionlessQueryDescriptor;
			}
		}


		public CustomBoostFactorQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q;
			return this;
		}

		public CustomBoostFactorQueryDescriptor<T> BoostFactor(double boostFactor)
		{
			boostFactor.ThrowIfNull("boostFactor");
			this._BoostFactor = boostFactor;
			return this;
		}
	}
}
