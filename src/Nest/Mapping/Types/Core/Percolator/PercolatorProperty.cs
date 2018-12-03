using System.Diagnostics;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
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
