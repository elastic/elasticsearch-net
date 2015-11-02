using System;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RoutingField>))]
	public interface IRoutingField : ISpecialField
	{
		[JsonProperty("required")]
		bool? Required { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public class RoutingField : IRoutingField
	{
		public bool? Required { get; set; }

		public Field Path { get; set; }
	}

	public class RoutingFieldDescriptor<T> 
		: DescriptorBase<RoutingFieldDescriptor<T>, IRoutingField>, IRoutingField
	{
		bool? IRoutingField.Required { get; set;}
		Field IRoutingField.Path { get; set; }

		public RoutingFieldDescriptor<T> Required(bool required = true) => Assign(a => a.Required = required);

		public RoutingFieldDescriptor<T> Path(string path) => Assign(a => a.Path = path);

		public RoutingFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}