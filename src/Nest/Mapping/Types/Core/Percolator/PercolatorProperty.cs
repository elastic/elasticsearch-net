using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IPercolatorProperty : IProperty { }

	[DebuggerDisplay("{DebugDisplay}")]
	public class PercolatorProperty : PropertyBase, IPercolatorProperty
	{
		public PercolatorProperty() : base(FieldType.Percolator) { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class PercolatorPropertyDescriptor<T>
		: PropertyDescriptorBase<PercolatorPropertyDescriptor<T>, IPercolatorProperty, T>, IPercolatorProperty
		where T : class
	{
		public PercolatorPropertyDescriptor() : base(FieldType.Percolator) { }
	}
}
