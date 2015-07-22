using System;
using System.Linq.Expressions;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;
using static Nest.Property;

namespace Tests.ClientConcepts.HighLevel.Inferrence.FieldNames
{
	public class FieldNameInferrence
	{
		/** # Strongly typed field access 
		 * 
		 * Several places in the elasticsearch API expect the path to a field from your original source document as a string.
		 * NEST allows you to use C# expressions to strongly type these field path strings. 
		 *
		 * These expressions are assigned to a type called `FieldName` and there are several ways to create a instance of that type
		 */

		/** Using the constructor directly is possible but rather involved */
		[U] public void UsingConstructors()
		{
			var fieldString = new FieldName() {Name = "name"};

			/** especially when using C# expressions since these can not be simply new'ed*/
			Expression<Func<Project, object>> expression = p => p.Name;
			var fieldExpression = new FieldName() { Expression = expression };

			Expect("name")
				.WhenSerializing(fieldExpression)
				.WhenSerializing(fieldString);
		}
		
		/** Therefor you can also implicitly convert strings and expressions to FieldName's */
		[U] public void ImplicitConversion()
		{
			FieldName fieldString = "name";

			/** but for expressions this is still rather involved */
			Expression<Func<Project, object>> expression = p => p.Name;
			FieldName fieldExpression = expression;

			Expect("name")
				.WhenSerializing(fieldExpression)
				.WhenSerializing(fieldString);
		}

		/** to ease creating FieldName's from expressions there is a static Property class you can use */
		[U] public void UsingStaticPropertyField()
		{
			FieldName fieldString = "name";

			/** but for expressions this is still rather involved */
			var fieldExpression = Property.Field<Project>(p=>p.Name);

			/** Using static imports in c# 6 this can be even shortened:
				using static Nest.Property; 
			*/
			fieldExpression = Field<Project>(p=>p.Name);
			/** Now this is much much terser then our first example using the constructor! */

			Expect("name").WhenSerializing(fieldExpression);
		}
		
		/** By default NEST will camelCase all the field names to be more javascripty */
		[U] public void SetDefaultFieldNameInferrer()
		{
			/** using SetDefaultFieldNameInferrer on ConnectionSettings you can change this behavior */
			var setup = WithConnectionSettings(s => s.SetDefaultFieldNameInferrer(p => p.ToUpper()));

			setup.Expect("NAME").WhenSerializing(Field<Project>(p => p.Name));

			/** However string are *always* passed along verbatim */
			setup.Expect("NaMe").WhenSerializing<FieldName>("NaMe");

			/** if you want the same behavior for expressions simply do nothing in the default inferrer */
			setup = WithConnectionSettings(s => s.SetDefaultFieldNameInferrer(p => p));
			setup.Expect("Name").WhenSerializing(Field<Project>(p => p.Name));
		}

		/** Complex field name expressions */

		[U] public void ComplexFieldNameExpressions()
		{
			Expect("leadDeveloper.firstName").WhenSerializing(Field<Project>(p => p.LeadDeveloper.FirstName));
		}
	}
}
