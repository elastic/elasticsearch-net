using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FieldNamesField>))]
	public interface IFieldNamesField : IFieldMapping
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