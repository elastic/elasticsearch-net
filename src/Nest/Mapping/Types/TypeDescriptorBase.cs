using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public abstract class TypeDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IElasticType
		where TDescriptor : TypeDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IElasticType
		where T : class
	{
		FieldName IElasticType.Name { get; set; }
		TypeName IElasticType.Type { get; set; }
		string IElasticType.IndexName { get; set; }
		bool IElasticType.Store { get; set; }
		bool IElasticType.DocValues { get; set; }
		SimilarityOption IElasticType.Similarity { get; set; }
		IEnumerable<FieldName> IElasticType.CopyTo { get; set; }
		IFielddata IElasticType.Fielddata { get; set; }
		IDictionary<FieldName, IElasticType> IElasticType.Fields { get; set; }

		public TDescriptor Name(FieldName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		public TDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		public TDescriptor Store(bool store = true) => Assign(a => a.Store = store);

		public TDescriptor DocValues(bool docValues = true) => Assign(a => a.DocValues = docValues);

		public TDescriptor Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> selector) => Assign(a =>
		{
			selector.ThrowIfNull(nameof(selector));
			var properties = selector(new PropertiesDescriptor<T>());
			foreach (var property in properties.Properties)
			{
				var value = property.Value as IElasticType;
				if (value == null)
					continue;
				a.Fields[property.Key] = value;
			}
		});

		public TDescriptor Similarity(SimilarityOption similarity) => Assign(a => a.Similarity = similarity);

		public TDescriptor CopyTo(IEnumerable<FieldName> copyTo) => Assign(a => a.CopyTo = copyTo);

		public TDescriptor Fielddata(Func<FielddataDescriptor, IFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new FielddataDescriptor()));
	}
}
