using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IElasticsearchProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IElasticsearchProperty
		where T : class
	{
		FieldName IElasticsearchProperty.Name { get; set; }
		TypeName IElasticsearchProperty.Type { get; set; }
		string IElasticsearchProperty.IndexName { get; set; }
		bool? IElasticsearchProperty.Store { get; set; }
		bool? IElasticsearchProperty.DocValues { get; set; }
		SimilarityOption? IElasticsearchProperty.Similarity { get; set; }
		IEnumerable<FieldName> IElasticsearchProperty.CopyTo { get; set; }
		IProperties IElasticsearchProperty.Fields { get; set; }

		public PropertyDescriptorBase(string type) { ((IElasticsearchProperty)this).Type = type; }

		public TDescriptor Name(FieldName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		public TDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		public TDescriptor Store(bool store = true) => Assign(a => a.Store = store);

		public TDescriptor DocValues(bool docValues = true) => Assign(a => a.DocValues = docValues);

		public TDescriptor Fields(Func<PropertiesDescriptor<T>, IProperties> selector) => Assign(a => a.Fields = selector?.Invoke(new PropertiesDescriptor<T>()));

		public TDescriptor Similarity(SimilarityOption similarity) => Assign(a => a.Similarity = similarity);

		public TDescriptor CopyTo(IEnumerable<FieldName> copyTo) => Assign(a => a.CopyTo = copyTo);
	}
}
