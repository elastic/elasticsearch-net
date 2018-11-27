using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SizeField))]
	public interface ISizeField : IFieldMapping
	{
		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }
	}

	public class SizeField : ISizeField
	{
		public bool? Enabled { get; set; }
	}

	public class SizeFieldDescriptor
		: DescriptorBase<SizeFieldDescriptor, ISizeField>, ISizeField
	{
		bool? ISizeField.Enabled { get; set; }

		public SizeFieldDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);
	}
}
