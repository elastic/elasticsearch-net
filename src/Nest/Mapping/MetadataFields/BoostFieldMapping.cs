using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<BoostFieldMapping>))]
	public interface IBoostFieldMapping : ISpecialField
	{
		[JsonProperty("name")]
		PropertyNameMarker Name { get; set; }

		[JsonProperty("null_value")]
		double NullValue { get; set; }
	}

	public class BoostFieldMapping : IBoostFieldMapping
	{
		public PropertyNameMarker Name { get; set; }

		public double NullValue { get; set; }
	}


	public class BoostFieldMappingDescriptor<T> : IBoostFieldMapping
	{
		private IBoostFieldMapping Self { get { return this; } }

		PropertyNameMarker IBoostFieldMapping.Name { get; set; }
		
		double IBoostFieldMapping.NullValue { get; set; }

		public BoostFieldMappingDescriptor<T> NullValue(double boost)
		{
			Self.NullValue = boost;
			return this;
		}
		public BoostFieldMappingDescriptor<T> Name(string path)
		{
			Self.Name = path;
			return this;
		}
		public BoostFieldMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			Self.Name = objectPath;
			return this;
		}

	}
}