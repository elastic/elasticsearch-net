using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(IndexField))]
	public interface IIndexField : IFieldMapping
	{
		[DataMember(Name ="enabled")]
		bool? Enabled { get; set; }
	}

	public class IndexField : IIndexField
	{
		public bool? Enabled { get; set; }
	}

	public class IndexFieldDescriptor
		: DescriptorBase<IndexFieldDescriptor, IIndexField>, IIndexField
	{
		bool? IIndexField.Enabled { get; set; }

		public IndexFieldDescriptor Enabled(bool? enabled = true) => Assign(a => a.Enabled = enabled);
	}
}
