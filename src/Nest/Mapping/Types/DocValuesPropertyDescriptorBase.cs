using System;

namespace Nest
{
	public abstract class DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>
		: CorePropertyDescriptorBase<TDescriptor, TInterface, T>, IDocValuesProperty
		where TDescriptor : DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IDocValuesProperty
		where T : class
	{
		[Obsolete("Please use overload taking FieldType")]
		protected DocValuesPropertyDescriptorBase(string type) : base(type) { }

		protected DocValuesPropertyDescriptorBase(FieldType type) : base(type) { }

		bool? IDocValuesProperty.DocValues { get; set; }

		public TDescriptor DocValues(bool docValues = true) => Assign(a => a.DocValues = docValues);
	}
}
