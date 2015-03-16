using System;
using System.Collections.Generic;
using System.Linq;
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

		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return false; } }
		public IQueryContainer Query { get; set; }
		public double? BoostFactor { get; set; }
	}

	public class CustomBoostFactorQueryDescriptor<T> : ICustomBoostFactorQuery where T : class
	{
		private ICustomBoostFactorQuery Self  { get { return this; }}

		IQueryContainer ICustomBoostFactorQuery.Query { get; set; }

		double? ICustomBoostFactorQuery.BoostFactor { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		public CustomBoostFactorQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public CustomBoostFactorQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		public CustomBoostFactorQueryDescriptor<T> BoostFactor(double boostFactor)
		{
			Self.BoostFactor = boostFactor;
			return this;
		}
	}
}
