using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(FieldNamesField))]
	public interface IFieldNamesField : IFieldMapping
	{
		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }
	}

	public class FieldNamesField : IFieldNamesField
	{
		public bool? Enabled { get; set; }
	}

	public class FieldNamesFieldDescriptor<T>
		: DescriptorBase<FieldNamesFieldDescriptor<T>, IFieldNamesField>, IFieldNamesField
	{
		bool? IFieldNamesField.Enabled { get; set; }

		public FieldNamesFieldDescriptor<T> Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);
	}
}
