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

using System;
using System.Linq.Expressions;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using static Tests.Core.Serialization.SerializationTestHelper;
using Xunit;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[property-inference]]
	* === Property name inference
	*/
	public class PropertyNames : DocumentationTestBase , IClusterFixture<WritableCluster>
	{
		/**
		 * ==== Appending suffixes to a lambda expression body
		 * Suffixes can be appended to the body of a lambda expression, which is useful in cases where
		 * you have a POCO property mapped as a <<multi-fields, multi-field>>
		 * and want to use strongly typed access with a lambda expression, yet append a suffix to the
		 * generated field name, in order to access a particular sub field of the multi-field.
		 *
		 * The `.Suffix()` extension method can be used for this purpose and when serializing expressions suffixed
		 * in this way, the serialized property name resolves to the last token
		 */
		[U] public void PropertyNamesAreResolvedToLastTokenUsingSuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/**
		 * ==== Appending suffixes to a Lambda expression
		 * Alternatively, suffixes can be applied to a lambda expression directly using
		 * the `.ApplySuffix()` extension method. Again, the serialized field name
		 * resolves to the last token
		 */
		[U] public void PropertyNamesAreResolvedToLastTokenUsingAppendSuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name;
			expression = expression.AppendSuffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

	}
}
