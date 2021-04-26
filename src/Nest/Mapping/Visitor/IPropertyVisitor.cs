/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		void Visit(IDateNanosProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IBinaryProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(INestedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IObjectProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IGeoPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IGeoShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IShapeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IPointProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

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

		void Visit(IRankFeatureProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IRankFeaturesProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IFlattenedProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IHistogramProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IConstantKeywordProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(ISearchAsYouTypeProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IFieldAliasProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IWildcardProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		void Visit(IVersionProperty type, PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		IProperty Visit(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);

		bool SkipProperty(PropertyInfo propertyInfo, ElasticsearchPropertyAttributeBase attribute);
	}
}
