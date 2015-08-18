using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoostFieldMapping>))]
	public interface IBoostFieldMapping : ISpecialField
	{
		[JsonProperty("name")]
		FieldName Name { get; set; }

		[JsonProperty("null_value")]
		double NullValue { get; set; }
	}

	public class BoostFieldMapping : IBoostFieldMapping
	{
		public FieldName Name { get; set; }

		public double NullValue { get; set; }
	}


	public class BoostFieldMappingDescriptor<T> : IBoostFieldMapping
	{
		private IBoostFieldMapping Self => this;

		FieldName IBoostFieldMapping.Name { get; set; }
		
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