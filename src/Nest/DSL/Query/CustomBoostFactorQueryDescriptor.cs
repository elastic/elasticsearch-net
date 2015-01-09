using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CustomBoostFactorQueryDescriptor<object>>))]
	public interface ICustomBoostFactorQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "boost_factor")]
		double? BoostFactor { get; set; }
	}

	public class CustomBoostFactorQuery : PlainQuery, ICustomBoostFactorQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.CustomBoostFactor = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public IQueryContainer Query { get; set; }
		public double? BoostFactor { get; set; }
	}

	public class CustomBoostFactorQueryDescriptor<T> : ICustomBoostFactorQuery where T : class
	{
		IQueryContainer ICustomBoostFactorQuery.Query { get; set; }

		double? ICustomBoostFactorQuery.BoostFactor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomBoostFactorQuery)this).Query == null || ((ICustomBoostFactorQuery)this).Query.IsConditionless;
			}
		}


		public CustomBoostFactorQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
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
