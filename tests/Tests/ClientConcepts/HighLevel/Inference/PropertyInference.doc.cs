// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
