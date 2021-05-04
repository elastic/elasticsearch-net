// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		public TDescriptor DocValues(bool? docValues = true) => Assign(docValues, (a, v) => a.DocValues = v);
	}
}
