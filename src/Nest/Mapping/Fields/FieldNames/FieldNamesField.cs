using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<FieldNamesField>))]
	public interface IFieldNamesField : ISpecialField
	{
		[JsonProperty("enabled")]
		bool Enabled { get; set; }
	}

	public class FieldNamesField : IFieldNamesField
	{
		public bool Enabled { get; set; }
	}

	public class FieldNamesFieldDescriptor<T> 
		: DescriptorBase<FieldNamesFieldDescriptor<T>, IFieldNamesField>, IFieldNamesField
	{
		bool IFieldNamesField.Enabled { get; set;}

		public FieldNamesFieldDescriptor<T> Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);
	}
}