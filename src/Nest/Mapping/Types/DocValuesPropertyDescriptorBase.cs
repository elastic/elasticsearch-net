using System;
using Elasticsearch.Net;

namespace Nest
{
	/// <inheritdoc cref="IDocValuesProperty"/>
	public abstract class DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>
	: CorePropertyDescriptorBase<TDescriptor, TInterface, T>, IDocValuesProperty
	where TDescriptor : DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
	where TInterface : class, IDocValuesProperty
	where T : class
	{
		bool? IDocValuesProperty.DocValues { get; set; }

		protected DocValuesPropertyDescriptorBase(FieldType type) : base(type) { }

		/// <inheritdoc cref="IDocValuesProperty.DocValues"/>
		public TDescriptor DocValues(bool? docValues = true) => Assign(a => a.DocValues = docValues);
	}
}
