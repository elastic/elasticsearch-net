using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<BoostField>))]
	public interface IBoostField : ISpecialField
	{
		[JsonProperty("name")]
		FieldName Name { get; set; }

		[JsonProperty("null_value")]
		double NullValue { get; set; }
	}

	public class BoostField : IBoostField
	{
		public FieldName Name { get; set; }

		public double NullValue { get; set; }
	}


	public class BoostFieldDescriptor<T> : IBoostField
	{
		private IBoostField Self => this;

		FieldName IBoostField.Name { get; set; }
		
		double IBoostField.NullValue { get; set; }

		public BoostFieldDescriptor<T> NullValue(double boost)
		{
			Self.NullValue = boost;
			return this;
		}
		public BoostFieldDescriptor<T> Name(string path)
		{
			Self.Name = path;
			return this;
		}
		public BoostFieldDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			Self.Name = objectPath;
			return this;
		}

	}
}