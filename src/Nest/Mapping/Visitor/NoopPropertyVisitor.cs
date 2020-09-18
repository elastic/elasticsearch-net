// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;

namespace Nest
{
	public class NoopPropertyVisitor : IPropertyVisitor
	{
		public virtual bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => false;

		public virtual void Visit(IBooleanProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(ICompletionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IMurmur3HashProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(ITokenCountProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IPercolatorProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IIntegerRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IFloatRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(ILongRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IDoubleRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IDateRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IIpRangeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IJoinProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IRankFeatureProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IRankFeaturesProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IIpProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IDateProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IDateNanosProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(INumberProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(ITextProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IFlattenedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IHistogramProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IConstantKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(ISearchAsYouTypeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IFieldAliasProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual void Visit(IWildcardProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) { }

		public virtual IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute) => null;

		public virtual void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute)
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
				case IDateNanosProperty dateNanosType:
					Visit(dateNanosType, propertyInfo, attribute);
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
				case IShapeProperty shapeType:
					Visit(shapeType, propertyInfo, attribute);
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
				case IRankFeatureProperty rankFeature:
					Visit(rankFeature, propertyInfo, attribute);
					break;
				case IRankFeaturesProperty rankFeatures:
					Visit(rankFeatures, propertyInfo, attribute);
					break;
				case IHistogramProperty histogram:
					Visit(histogram, propertyInfo, attribute);
					break;
				case IConstantKeywordProperty constantKeyword:
					Visit(constantKeyword, propertyInfo, attribute);
					break;
				case IPointProperty point:
					Visit(point, propertyInfo, attribute);
          break;
				case ISearchAsYouTypeProperty searchAsYouType:
					Visit(searchAsYouType, propertyInfo, attribute);
					break;
				case IWildcardProperty wildcard:
					Visit(wildcard, propertyInfo, attribute);
					break;
				case IFieldAliasProperty fieldAlias:
					Visit(fieldAlias, propertyInfo, attribute);
					break;
			}
		}
	}
}
