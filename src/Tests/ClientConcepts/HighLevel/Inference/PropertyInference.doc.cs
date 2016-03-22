using System;
using System.Linq.Expressions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;
using Xunit;

namespace Tests.ClientConcepts.HighLevel.Inferrence.PropertyNames
{
	/**[[property-inference]]
	* == Property Name Inference 
	*/
	public class PropertyNames
	{
		/**=== Using `.Suffix()` extension method on `object`
		 * Property names resolve to the last token. An example using the `.Suffix()` extension
		 */
		[U] public void PropertyNamesAreResolvedToLastTokenUsingSuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/**=== `.ApplySuffix()` extension  method on Expression Delegates
		 * And an example using the `.ApplySuffix()` extension on lambda expressions
		 */
		[U]
		public void PropertyNamesAreResolvedToLastTokenUsingApplySuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name;
			expression.AppendSuffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/** Property names cannot contain a ``.``. in order to prevent the potential for collision with a field that 
		 * may have {ref_current}/_multi_fields.html[`multi_fields`] 
		 */
		[U] public void StringsContainingDotsIsAnException()
		{
			Assert.Throws<ArgumentException>(() => Expect("exception!").WhenSerializing<PropertyName>("name.raw"));
		}
	}
}
