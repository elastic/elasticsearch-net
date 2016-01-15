using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.ClientConcepts.LowLevel;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;
using Field = Nest.Field;

namespace Tests.ClientConcepts.HighLevel.Inferrence.FieldNames
{
	public class FieldInferrence
	{
		/** # Strongly typed field access 
		 * 
		 * Several places in the elasticsearch API expect the path to a field from your original source document as a string.
		 * NEST allows you to use C# expressions to strongly type these field path strings. 
		 *
		 * These expressions are assigned to a type called `Field` and there are several ways to create a instance of that type
		 */

		/** Using the constructor directly is possible but rather involved */
		[U]
		public void UsingConstructors()
		{
			var fieldString = new Field { Name = "name" };

			/** especially when using C# expressions since these can not be simply new'ed*/
			Expression<Func<Project, object>> expression = p => p.Name;
			var fieldExpression = Field.Create(expression);

			Expect("name")
				.WhenSerializing(fieldExpression)
				.WhenSerializing(fieldString);
		}

		/** Therefor you can also implicitly convert strings and expressions to Field's */
		[U]
		public void ImplicitConversion()
		{
			Field fieldString = "name";

			/** but for expressions this is still rather involved */
			Expression<Func<Project, object>> expression = p => p.Name;
			Field fieldExpression = expression;

			Expect("name")
				.WhenSerializing(fieldExpression)
				.WhenSerializing(fieldString);
		}

		/** to ease creating Field's from expressions there is a static Property class you can use */
		[U]
		public void UsingStaticPropertyField()
		{
			Field fieldString = "name";

			/** but for expressions this is still rather involved */
			var fieldExpression = Field<Project>(p => p.Name);

			/** Using static imports in c# 6 this can be even shortened:
				using static Nest.Static; 
			*/
			fieldExpression = Field<Project>(p => p.Name);
			/** Now this is much much terser then our first example using the constructor! */

			Expect("name")
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldExpression);
		}

		/** By default NEST will camelCase all the field names to be more javascripty */
		[U]
		public void DefaultFieldNameInferrer()
		{
			/** using DefaultFieldNameInferrer() on ConnectionSettings you can change this behavior */
			var setup = WithConnectionSettings(s => s.DefaultFieldNameInferrer(p => p.ToUpper()));

			setup.Expect("NAME").WhenSerializing(Field<Project>(p => p.Name));

			/** However string are *always* passed along verbatim */
			setup.Expect("NaMe").WhenSerializing<Field>("NaMe");

			/** if you want the same behavior for expressions simply do nothing in the default inferrer */
			setup = WithConnectionSettings(s => s.DefaultFieldNameInferrer(p => p));
			setup.Expect("Name").WhenSerializing(Field<Project>(p => p.Name));
		}

		/** Complex field name expressions */

		[U]
		public void ComplexFieldNameExpressions()
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

		/** Annotations 
		* 
		* When using NEST's property attributes you can specify a new name for the properties
		*/
		public class BuiltIn
		{
			[String(Name = "naam")]
			public string Name { get; set; }
		}
		[U]
		public void BuiltInAnnotiatons()
		{
			Expect("naam").WhenSerializing(Field<BuiltIn>(p => p.Name));
		}

		/** 
		* Starting with NEST 2.x we also ask the serializer if it can resolve the property to a name.
		* Here we ask the default JsonNetSerializer and it takes JsonProperty into account
		*/
		public class SerializerSpecific
		{
			[JsonProperty("nameInJson")]
			public string Name { get; set; }
		}
		[U]
		public void SerializerSpecificAnnotations()
		{
			Expect("nameInJson").WhenSerializing(Field<SerializerSpecific>(p => p.Name));
		}

		/** 
		* If both are specified NEST takes precedence though 
		*/
		public class Both
		{
			[String(Name = "naam")]
			[JsonProperty("nameInJson")]
			public string Name { get; set; }
		}
		[U]
		public void NestAttributeTakesPrecedence()
		{
			Expect("naam").WhenSerializing(Field<Both>(p => p.Name));
			Expect(new
			{
				naam = "Martijn Laarman"
			}).WhenSerializing(new Both { Name = "Martijn Laarman" });
		}

		class A { public C C { get; set; } }
		class B { public C C { get; set; } }
		class C
		{
			public string Name { get; set; }
		}

		/**
		* Resolving field names is cached but this is per connection settings
		*/

		[U]
		public void ExpressionsAreCachedButSeeDifferentTypes()
		{
			var connectionSettings = TestClient.CreateSettings(forceInMemory: true);
			var client = new ElasticClient(connectionSettings);

			var fieldNameOnA = client.Infer.Field(Field<A>(p => p.C.Name));
			var fieldNameOnB = client.Infer.Field(Field<B>(p => p.C.Name));

			/**
			* Here we have to similary shaped expressions on coming from A and on from B
			* that will resolve to the same field name, as expected
			*/

			fieldNameOnA.Should().Be("c.name");
			fieldNameOnB.Should().Be("c.name");

			/**
			* now we create a new connectionsettings with a remap for C on class A to `d`
			* now when we resolve the field path for A will be different
			*/
			var newConnectionSettings = TestClient.CreateSettings(forceInMemory: true, modifySettings: s => s
				.InferMappingFor<A>(m => m
					.Rename(p => p.C, "d")
				)
			);
			var newClient = new ElasticClient(newConnectionSettings);

			fieldNameOnA = newClient.Infer.Field(Field<A>(p => p.C.Name));
			fieldNameOnB = newClient.Infer.Field(Field<B>(p => p.C.Name));

			fieldNameOnA.Should().Be("d.name");
			fieldNameOnB.Should().Be("c.name");

			/** however we didn't break inferrence on the first client instance using its separate connectionsettings */
			fieldNameOnA = client.Infer.Field(Field<A>(p => p.C.Name));
			fieldNameOnB = client.Infer.Field(Field<B>(p => p.C.Name));

			fieldNameOnA.Should().Be("c.name");
			fieldNameOnB.Should().Be("c.name");
		}

		/**
		* To wrap up lets showcase the precedence that field names are inferred
		* 1. A hard rename of the property on connection settings using Rename()
		* 2. A NEST property mapping
		* 3. Ask the serializer if the property has a verbatim value e.g it has an explicit JsonPropery attribute.
		* 4. Pass the MemberInfo's Name to the DefaultFieldNameInferrer which by default camelCases
		*
		* In the following example we have a class where each case wins
		*/

		class Precedence
		{
			/**
			* Eventhough this property has a NEST property mapping and a JsonProperty attribute
			* We are going to provide a hard rename for it on ConnectionSettings later that should win.
			*/
			[String(Name = "renamedIgnoresNest")]
			[JsonProperty("renamedIgnoresJsonProperty")]
			public string RenamedOnConnectionSettings { get; set; }

			/**
			* This property has both a NEST attribute and a JsonProperty, NEST should win.
			*/
			[String(Name = "nestAtt")]
			[JsonProperty("jsonProp")]
			public string NestAttribute { get; set; }

			/** We should take the json property into account by itself */
			[JsonProperty("jsonProp")]
			public string JsonProperty { get; set; }

			/** This property we are going to special case in a custom serializer to resolve to `ask` */
			public string AskSerializer { get; set; }

			/** We are going to register a DefaultFieldNameInferrer on ConnectionSettings 
			* that will uppercase all properties. 
			*/
			public string DefaultFieldNameInferrer { get; set; }

		}

		/** 
		* Here we create a custom converter that renames any property named `AskSerializer` to `ask`
		*/
		class CustomSerializer : JsonNetSerializer
		{
			public CustomSerializer(IConnectionSettingsValues settings) : base(settings) { }

			public override string CreatePropertyName(MemberInfo memberInfo)
			{
				if (memberInfo.Name == "AskSerializer") return "ask";
				return base.CreatePropertyName(memberInfo);
			}
		}

		[U]
		public void PrecedenceIsAsExpected()
		{
			var usingSettings = WithConnectionSettings(s => s
				/** here we provide an explicit rename of a property on connectionsettings */
				.InferMappingFor<Precedence>(m => m
					.Rename(p => p.RenamedOnConnectionSettings, "renamed")
				)
				/** All properties that are not mapped verbatim should be uppercased*/
				.DefaultFieldNameInferrer(p => p.ToUpperInvariant())
			).WithSerializer(s => new CustomSerializer(s));

			usingSettings.Expect("renamed").ForField(Field<Precedence>(p => p.RenamedOnConnectionSettings));
			usingSettings.Expect("nestAtt").ForField(Field<Precedence>(p => p.NestAttribute));
			usingSettings.Expect("jsonProp").ForField(Field<Precedence>(p => p.JsonProperty));
			usingSettings.Expect("ask").ForField(Field<Precedence>(p => p.AskSerializer));
			usingSettings.Expect("DEFAULTFIELDNAMEINFERRER").ForField(Field<Precedence>(p => p.DefaultFieldNameInferrer));
			
			/** The same rules apply when indexing an object */
			usingSettings.Expect(new [] 
			{
				"ask",
				"DEFAULTFIELDNAMEINFERRER",
				"jsonProp",
				"nestAtt",
				"renamed"
			}).AsPropertiesOf(new Precedence
			{
				RenamedOnConnectionSettings = "renamed on connection settings",
				NestAttribute = "using a nest attribute",
				JsonProperty = "the default serializer resolves json property attributes",
				AskSerializer = "serializer fiddled with this one",
				DefaultFieldNameInferrer = "shouting much?"
			});

		}
		/**
		*
		*/
	}
}
