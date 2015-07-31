using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<RoutingField>))]
	public interface IRoutingField : ISpecialField
	{
		[JsonProperty("required")]
		bool? Required { get; set; }

		[JsonProperty("path")]
		FieldName Path { get; set; }
	}

	public class RoutingField : IRoutingField
	{
		public bool? Required { get; set; }

		public FieldName Path { get; set; }
	}

	public class RoutingFieldDescriptor<T> : IRoutingField
	{
		private IRoutingField Self => this;

		bool? IRoutingField.Required { get; set;}

		FieldName IRoutingField.Path { get; set; }

		public RoutingFieldDescriptor<T> Required(bool required = true)
		{
			Self.Required = required;
			return this;
		}
		public RoutingFieldDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public RoutingFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}
	}
}