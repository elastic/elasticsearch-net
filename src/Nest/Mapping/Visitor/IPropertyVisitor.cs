using System.Reflection;

namespace Nest
{
	public interface IPropertyVisitor
	{
		void Visit(ITextProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IPercolatorProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IIntegerRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IFloatRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(ILongRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IDoubleRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IDateRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IIpRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		void Visit(IJoinProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
		bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
	}
}
