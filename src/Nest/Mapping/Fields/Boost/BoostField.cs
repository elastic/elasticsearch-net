using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoostField>))]
	public interface IBoostField : ISpecialField
	{
		[JsonProperty("name")]
		Field Name { get; set; }

		[JsonProperty("null_value")]
		double NullValue { get; set; }
	}

	public class BoostField : IBoostField
	{
		public Field Name { get; set; }

		public double NullValue { get; set; }
	}


	public class BoostFieldDescriptor<T> 
		: DescriptorBase<BoostFieldDescriptor<T>, IBoostField>, IBoostField
	{
		Field IBoostField.Name { get; set; }
		double IBoostField.NullValue { get; set; }

		public BoostFieldDescriptor<T> NullValue(double nullValue) => Assign(a => a.NullValue = nullValue);
		public BoostFieldDescriptor<T> Name(string name) => Assign(a => a.Name = name);
		public BoostFieldDescriptor<T> Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);
	}
}