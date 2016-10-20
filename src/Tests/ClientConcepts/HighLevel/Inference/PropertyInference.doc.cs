using System;
using System.Linq.Expressions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;
using Xunit;
using FluentAssertions;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[property-inference]]
	* == Property Name Inference
	*/
	public class PropertyNames : IntegrationDocumentationTestBase , IClusterFixture<WritableCluster>
	{
		public PropertyNames(WritableCluster cluster) : base(cluster) { }

		/**=== Appending suffixes to a Lambda expression body
		 * Suffixes can be appended to the body of a lambda expression, useful in cases where
		 * you have a POCO property mapped as a multi field
		 * and want to use strongly typed access based on the property, yet append a suffix to the
		 * generated field name in order to access a particular multi field.
		 *
		 * The `.Suffix()` extension method can be used for this purpose and when serializing expressions suffixed
		 * in this way, the serialized field name resolves to the last token
		 */
		[U] public void PropertyNamesAreResolvedToLastTokenUsingSuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/**=== Appending suffixes to a Lambda expression
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
