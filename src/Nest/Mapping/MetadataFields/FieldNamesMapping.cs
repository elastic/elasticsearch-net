using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<FieldNamesFieldMapping>))]
	public interface IFieldNamesFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool Enabled { get; set; }
	}

	public class FieldNamesFieldMapping : IFieldNamesFieldMapping
	{
		public bool Enabled { get; set; }
	}


	public class FieldNamesFieldMappingDescriptor<T> : IFieldNamesFieldMapping
	{
		private IFieldNamesFieldMapping Self { get { return this; } }

		bool IFieldNamesFieldMapping.Enabled { get; set;}

		public FieldNamesFieldMappingDescriptor<T> Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
		
	}
}