using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class CustomBoostFactorQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal QueryDescriptor<T> _Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		internal double? _BoostFactor { get; set; }

		public CustomBoostFactorQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			querySelector(query);

			this._Query = query;
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
