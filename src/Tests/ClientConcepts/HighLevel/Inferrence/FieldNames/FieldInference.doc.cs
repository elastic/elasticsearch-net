using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;
using static Nest.Static;

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
			var fieldString = new FieldName {Name = "name"};

			/** especially when using C# expressions since these can not be simply new'ed*/
			Expression<Func<Project, object>> expression = p => p.Name;
			var fieldExpression = FieldName.Create(expression);

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
			var fieldExpression = Field<Project>(p=>p.Name);

			/** Using static imports in c# 6 this can be even shortened:
				using static Nest.Static; 
			*/
			fieldExpression = Field<Project>(p=>p.Name);
			/** Now this is much much terser then our first example using the constructor! */

			Expect("name")
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldExpression);
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
			/** You can follow your property expression to any depth, here we are traversing to the LeadDeveloper's (Person) FirstName */
			Expect("leadDeveloper.firstName").WhenSerializing(Field<Project>(p => p.LeadDeveloper.FirstName));
			/** When dealing with collection index access is ingnored allowing you to traverse into properties of collections */
			Expect("curatedTags").WhenSerializing(Field<Project>(p => p.CuratedTags[0]));
			/** Similarly .First() also works, remember these are expressions and not actual code that will be executed */
			Expect("curatedTags").WhenSerializing(Field<Project>(p => p.CuratedTags.First()));
			Expect("curatedTags.added").WhenSerializing(Field<Project>(p => p.CuratedTags[0].Added));
			Expect("curatedTags.name").WhenSerializing(Field<Project>(p => p.CuratedTags.First().Name));
			
			/** When we see an indexer on a dictionary we assume they describe property names */
			Expect("metadata.hardcoded").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"]));
			Expect("metadata.hardcoded.created").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"].Created));

			/** A cool feature here is that we'll evaluate variables passed to these indexers */
			var variable = "var";
			Expect("metadata.var").WhenSerializing(Field<Project>(p => p.Metadata[variable]));
			Expect("metadata.var.created").WhenSerializing(Field<Project>(p => p.Metadata[variable].Created));


			/** If you are using elasticearch's multifield mapping (you really should!) these "virtual" sub fields 
			* do not always map back on to your POCO, by calling .Suffix() you describe the sub fields that do not live in your c# objects
			*/
			Expect("leadDeveloper.firstName.raw").WhenSerializing(Field<Project>(p => p.LeadDeveloper.FirstName.Suffix("raw")));
			Expect("curatedTags.raw").WhenSerializing(Field<Project>(p => p.CuratedTags[0].Suffix("raw")));
			Expect("curatedTags.raw").WhenSerializing(Field<Project>(p => p.CuratedTags.First().Suffix("raw")));
			Expect("curatedTags.added.raw").WhenSerializing(Field<Project>(p => p.CuratedTags[0].Added.Suffix("raw")));
			Expect("metadata.hardcoded.raw").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"].Suffix("raw")));
			Expect("metadata.hardcoded.created.raw").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"].Created.Suffix("raw")));

			/**
			* You can even chain them to any depth!
			*/
			Expect("curatedTags.name.raw.evendeeper").WhenSerializing(Field<Project>(p => p.CuratedTags.First().Name.Suffix("raw").Suffix("evendeeper")));


			/** Variables passed to suffix will be evaluated as well */
			var suffix = "unanalyzed";
			Expect("metadata.var.unanalyzed").WhenSerializing(Field<Project>(p => p.Metadata[variable].Suffix(suffix)));
			Expect("metadata.var.created.unanalyzed").WhenSerializing(Field<Project>(p => p.Metadata[variable].Created.Suffix(suffix)));

		}
	}
}
