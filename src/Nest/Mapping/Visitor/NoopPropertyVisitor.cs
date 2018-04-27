using System.Reflection;

namespace Nest
{
	public class NoopPropertyVisitor : IPropertyVisitor
	{
		public virtual void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IPercolatorProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IIntegerRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IFloatRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(ILongRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IDoubleRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IDateRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IIpRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public void Visit(IJoinProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(ITextProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual void Visit(IKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
		}

		public virtual IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => null;

		public virtual bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => false;

		public void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
		{
			switch (type)
			{
				case INestedProperty nestedType:
					Visit(nestedType, propertyInfo, attribute);
					break;
				case IObjectProperty objectType:
					Visit(objectType, propertyInfo, attribute);
					break;
				case IBinaryProperty binaryType:
					Visit(binaryType, propertyInfo, attribute);
					break;
				case IBooleanProperty booleanType:
					Visit(booleanType, propertyInfo, attribute);
					break;
				case IDateProperty dateType:
					Visit(dateType, propertyInfo, attribute);
					break;
				case INumberProperty numberType:
					Visit(numberType, propertyInfo, attribute);
					break;
				case ITextProperty textType:
					Visit(textType, propertyInfo, attribute);
					break;
				case IKeywordProperty keywordType:
					Visit(keywordType, propertyInfo, attribute);
					break;
				case IGeoShapeProperty geoShapeType:
					Visit(geoShapeType, propertyInfo, attribute);
					break;
				case IGeoPointProperty geoPointType:
					Visit(geoPointType, propertyInfo, attribute);
					break;
				case ICompletionProperty completionType:
					Visit(completionType, propertyInfo, attribute);
					break;
				case IIpProperty ipType:
					Visit(ipType, propertyInfo, attribute);
					break;
				case IMurmur3HashProperty murmurType:
					Visit(murmurType, propertyInfo, attribute);
					break;
				case ITokenCountProperty tokenCountType:
					Visit(tokenCountType, propertyInfo, attribute);
					break;
				case IPercolatorProperty percolatorType:
					Visit(percolatorType, propertyInfo, attribute);
					break;
				case IJoinProperty joinType:
					Visit(joinType, propertyInfo, attribute);
					break;
				case IIntegerRangeProperty integerRangeType:
					Visit(integerRangeType, propertyInfo, attribute);
					break;
				case ILongRangeProperty longRangeType:
					Visit(longRangeType, propertyInfo, attribute);
					break;
				case IDoubleRangeProperty doubleRangeType:
					Visit(doubleRangeType, propertyInfo, attribute);
					break;
				case IFloatRangeProperty floatRangeType:
					Visit(floatRangeType, propertyInfo, attribute);
					break;
				case IDateRangeProperty dateRangeType:
					Visit(dateRangeType, propertyInfo, attribute);
					break;
				case IIpRangeProperty ipRangeType:
					Visit(ipRangeType, propertyInfo, attribute);
					break;
			}
		}
	}
}
