using System;
using Elasticsearch.Net;

namespace Nest
{
	/// <inheritdoc cref="ICoreProperty"/>
	public abstract class CorePropertyDescriptorBase<TDescriptor, TInterface, T>
		: PropertyDescriptorBase<TDescriptor, TInterface, T>, ICoreProperty
		where TDescriptor : CorePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICoreProperty
		where T : class
	{
		bool? ICoreProperty.Store { get; set; }
		Union<SimilarityOption, string> ICoreProperty.Similarity { get; set; }
		Fields ICoreProperty.CopyTo { get; set; }
		IProperties ICoreProperty.Fields { get; set; }

		protected CorePropertyDescriptorBase(FieldType type) : base(type) {}

		/// <inheritdoc cref="ICoreProperty.Store"/>
		public TDescriptor Store(bool? store = true) => Assign(a => a.Store = store);

		/// <inheritdoc cref="ICoreProperty.Fields"/>
		public TDescriptor Fields(Func<PropertiesDescriptor<T>, IPromise<IProperties>> selector) => Assign(a => a.Fields = selector?.Invoke(new PropertiesDescriptor<T>())?.Value);

		/// <inheritdoc cref="ICoreProperty.Similarity"/>
		public TDescriptor Similarity(SimilarityOption? similarity) => Assign(a => a.Similarity = similarity);

		/// <inheritdoc cref="ICoreProperty.Similarity"/>
		public TDescriptor Similarity(string similarity) => Assign(a => a.Similarity = similarity);

		/// <inheritdoc cref="ICoreProperty.CopyTo"/>
		public TDescriptor CopyTo(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) => Assign(a => a.CopyTo = fields?.Invoke(new FieldsDescriptor<T>())?.Value);
	}
}
