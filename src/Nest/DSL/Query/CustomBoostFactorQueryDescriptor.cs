using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICustomBoostFactorQuery
	{
		[JsonProperty(PropertyName = "query")]
		BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		double? _BoostFactor { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomBoostFactorQueryDescriptor<T> : IQuery, ICustomBoostFactorQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		BaseQuery ICustomBoostFactorQuery._Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		double? ICustomBoostFactorQuery._BoostFactor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomBoostFactorQuery)this)._Query == null || ((ICustomBoostFactorQuery)this)._Query.IsConditionless;
			}
		}


		public CustomBoostFactorQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((ICustomBoostFactorQuery)this)._Query = q;
			return this;
		}

		public CustomBoostFactorQueryDescriptor<T> BoostFactor(double boostFactor)
		{
			((ICustomBoostFactorQuery)this)._BoostFactor = boostFactor;
			return this;
		}
	}
}
