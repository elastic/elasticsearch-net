using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<RoutingFieldMapping>))]
	public interface IRoutingFieldMapping : ISpecialField
	{
		[JsonProperty("required")]
		bool? Required { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }
	}

	public class RoutingFieldMapping : IRoutingFieldMapping
	{
		public bool? Required { get; set; }

		public PropertyPathMarker Path { get; set; }
	}


	public class RoutingFieldMappingDescriptor<T> : IRoutingFieldMapping
	{
		private IRoutingFieldMapping Self => this;

		bool? IRoutingFieldMapping.Required { get; set;}

		PropertyPathMarker IRoutingFieldMapping.Path { get; set; }

		public RoutingFieldMappingDescriptor<T> Required(bool required = true)
		{
			Self.Required = required;
			return this;
		}
		public RoutingFieldMappingDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public RoutingFieldMappingDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}

	}
}