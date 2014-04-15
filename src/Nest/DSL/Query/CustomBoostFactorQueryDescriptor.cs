using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICustomBoostFactorQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		double? BoostFactor { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomBoostFactorQueryDescriptor<T> : IQuery, ICustomBoostFactorQuery where T : class
	{
		IQueryDescriptor ICustomBoostFactorQuery.Query { get; set; }

		double? ICustomBoostFactorQuery.BoostFactor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomBoostFactorQuery)this).Query == null || ((ICustomBoostFactorQuery)this).Query.IsConditionless;
			}
		}


		public CustomBoostFactorQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((ICustomBoostFactorQuery)this).Query = q;
			return this;
		}

		public CustomBoostFactorQueryDescriptor<T> BoostFactor(double boostFactor)
		{
			((ICustomBoostFactorQuery)this).BoostFactor = boostFactor;
			return this;
		}
	}
}
