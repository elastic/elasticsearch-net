using System;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldNamesFieldMapping>))]
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
		private IFieldNamesFieldMapping Self => this;

		bool IFieldNamesFieldMapping.Enabled { get; set;}

		public FieldNamesFieldMappingDescriptor<T> Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
		
	}
}