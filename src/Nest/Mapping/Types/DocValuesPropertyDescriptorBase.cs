namespace Nest
{
	public abstract class DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>
	: CorePropertyDescriptorBase<TDescriptor, TInterface, T>, IDocValuesProperty
	where TDescriptor : DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
	where TInterface : class, IDocValuesProperty
	where T : class
	{
		bool? IDocValuesProperty.DocValues { get; set; }

		protected DocValuesPropertyDescriptorBase(string type) : base(type) { }

		public TDescriptor DocValues(bool docValues = true) => Assign(a => a.DocValues = docValues);
	}
}
