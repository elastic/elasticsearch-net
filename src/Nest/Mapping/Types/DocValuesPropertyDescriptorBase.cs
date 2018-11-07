namespace Nest
{
	/// <inheritdoc cref="IDocValuesProperty" />
	public abstract class DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>
		: CorePropertyDescriptorBase<TDescriptor, TInterface, T>, IDocValuesProperty
		where TDescriptor : DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IDocValuesProperty
		where T : class
	{
		protected DocValuesPropertyDescriptorBase(FieldType type) : base(type) { }

		bool? IDocValuesProperty.DocValues { get; set; }

		/// <inheritdoc cref="IDocValuesProperty.DocValues" />
		public TDescriptor DocValues(bool? docValues = true) => Assign(a => a.DocValues = docValues);
	}
}
