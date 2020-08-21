// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	/// <inheritdoc cref="ICoreProperty" />
	public abstract class CorePropertyDescriptorBase<TDescriptor, TInterface, T>
		: PropertyDescriptorBase<TDescriptor, TInterface, T>, ICoreProperty
		where TDescriptor : CorePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICoreProperty
		where T : class
	{
		protected CorePropertyDescriptorBase(FieldType type) : base(type) { }

		Fields ICoreProperty.CopyTo { get; set; }
		IProperties ICoreProperty.Fields { get; set; }
		string ICoreProperty.Similarity { get; set; }
		bool? ICoreProperty.Store { get; set; }

		/// <inheritdoc cref="ICoreProperty.Store" />
		public TDescriptor Store(bool? store = true) => Assign(store, (a, v) => a.Store = v);

		/// <inheritdoc cref="ICoreProperty.Fields" />
		public TDescriptor Fields(Func<PropertiesDescriptor<T>, IPromise<IProperties>> selector) =>
			Assign(selector, (a, v) => a.Fields = v?.Invoke(new PropertiesDescriptor<T>())?.Value);

		/// <inheritdoc cref="ICoreProperty.Similarity" />
		public TDescriptor Similarity(string similarity) => Assign(similarity, (a, v) => a.Similarity = v);

		/// <inheritdoc cref="ICoreProperty.CopyTo" />
		public TDescriptor CopyTo(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.CopyTo = v?.Invoke(new FieldsDescriptor<T>())?.Value);
	}
}
