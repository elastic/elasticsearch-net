using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IProperty
		where T : class
	{
		PropertyName IProperty.Name { get; set; }
		TypeName IProperty.Type { get; set; }
		string IProperty.IndexName { get; set; }
		bool? IProperty.Store { get; set; }
		bool? IProperty.DocValues { get; set; }
		SimilarityOption? IProperty.Similarity { get; set; }
		IEnumerable<FieldName> IProperty.CopyTo { get; set; }
		IProperties IProperty.Fields { get; set; }

		public PropertyDescriptorBase(string type) { ((IProperty)this).Type = type; }

		public TDescriptor Name(PropertyName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		public TDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		public TDescriptor Store(bool store = true) => Assign(a => a.Store = store);

		public TDescriptor DocValues(bool docValues = true) => Assign(a => a.DocValues = docValues);

		public TDescriptor Fields(Func<PropertiesDescriptor<T>, IProperties> selector) => Assign(a => a.Fields = selector?.Invoke(new PropertiesDescriptor<T>()));

		public TDescriptor Similarity(SimilarityOption similarity) => Assign(a => a.Similarity = similarity);

		public TDescriptor CopyTo(IEnumerable<FieldName> copyTo) => Assign(a => a.CopyTo = copyTo);
	}
}
