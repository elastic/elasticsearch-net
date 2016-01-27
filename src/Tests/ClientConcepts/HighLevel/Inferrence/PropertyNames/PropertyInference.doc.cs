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
		[U] public void PropertyNamesAreResolvedToLastToken()
		{
			Expression<Func<Project, object>> expression = p => p.Name.Suffix("raw");
			Expect("raw").WhenSerializing<PropertyName>(expression);
		}

		[U] public void StringsContainingDotsIsAnException()
		{
			Assert.Throws<ArgumentException>(() => Expect("exception!").WhenSerializing<PropertyName>("name.raw"));
		}
	}
}
