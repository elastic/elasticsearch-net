using System;
using System.Linq.Expressions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;
using Xunit;

namespace Tests.ClientConcepts.HighLevel.Inferrence.PropertyNames
{
	public class PropertyNames
	{
		/** 
		 * Property names resolve to the last token. An example using the `.Suffix()` extension
		 */
		[U] public void PropertyNamesAreResolvedToLastTokenUsingSuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/** 
		 * And an example using the `.ApplySuffix()` extension on lambda expressions
		 */
		[U]
		public void PropertyNamesAreResolvedToLastTokenUsingApplySuffix()
		{
			Expression<Func<Project, object>> expression = p => p.Name;
			expression.AppendSuffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		/** Property names cannot contain a `.` in order to prevent the potential for collision with a field that 
		 * may have https://www.elastic.co/guide/en/elasticsearch/reference/current/_multi_fields.html[`multi_fields`] 
		 */
		[U] public void StringsContainingDotsIsAnException()
		{
			Assert.Throws<ArgumentException>(() => Expect("exception!").WhenSerializing<PropertyName>("name.raw"));
		}
	}
}
