// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Tests.Core.Client;
using Tests.Domain;
using static Tests.Core.Serialization.SerializationTestHelper;
using static Elastic.Clients.Elasticsearch.Infer;
using Field = Elastic.Clients.Elasticsearch.Field;
using Tests.Core.Client.Settings;
using System.Text.Json.Serialization;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class FieldInference
	{
		/**[[field-inference]]
		 * === Field inference
		 *
		 * Several places in the Elasticsearch API expect the path to a field from your original source document, as a string value.
		 * NEST allows you to use C# expressions to strongly type these field path strings.
		 *
		 * These expressions are assigned to a type called `Field`, and there are several ways to create an instance of one
		 */

		/**
		* ==== Constructor
		* Using the constructor directly is possible _but_ can get rather involved when resolving from a member access lambda expression
		*/
		[U]
		public void UsingConstructors()
		{
			var fieldString = new Field("name");

			var fieldProperty = new Field(typeof(Project).GetProperty(nameof(Project.Name)));

			Expression<Func<Project, object>> expression = p => p.Name;
			var fieldExpression = new Field(expression);

			Expect("name")
				.WhenSerializing(fieldExpression)
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldProperty);
		}

		[U]
		public void UsingConstructorAlsoSetsComparisonValue()
		{
			/** When using the constructor and passing a value for `Name`, `Property` or `Expression`,
			* `ComparisonValue` is also set on the `Field` instance; this is used when
			*
			* - determining `Field` equality
			* - getting the hash code for a `Field` instance
			*/
			var fieldStringWithBoostTwo = new Field("name^2");
			var fieldStringWithBoostThree = new Field("name^3");

			Expression<Func<Project, object>> expression = p => p.Name;
			var fieldExpression = new Field(expression);

			var fieldProperty = new Field(typeof(Project).GetProperty(nameof(Project.Name)));

			fieldStringWithBoostTwo.GetHashCode().Should().NotBe(0);
			fieldStringWithBoostThree.GetHashCode().Should().NotBe(0);
			fieldExpression.GetHashCode().Should().NotBe(0);
			fieldProperty.GetHashCode().Should().NotBe(0);

			fieldStringWithBoostTwo.Should().Be(fieldStringWithBoostTwo); //<1> <<field-name-with-boost,Fields can constructed with a name that contains a boost>>
		}

		/**
		* [[field-name-with-boost]]
		*==== Field Names with Boost
		*
		* When specifying a `Field` name, the name can include a boost value; NEST will split the name and boost
		* value and set the `Boost` property; a boost value as part of the string takes precedence over a boost
		* value that may also be passed as the second constructor argument
		*/
		[U]
		public void NameCanSpecifyBoost()
		{
			Field fieldString = "name^2";
			var fieldStringConstructor = new Field("name^2");
			var fieldStringCreate = new Field("name^2", 3); //<1> NEST will take the boost from the name

			fieldString.Name.Should().Be("name");
			fieldStringConstructor.Name.Should().Be("name");
			fieldStringCreate.Name.Should().Be("name");
			fieldString.Boost.Should().Be(2);
			fieldStringConstructor.Boost.Should().Be(2);
			fieldStringCreate.Boost.Should().Be(2);
		}

		/**
		* ==== Implicit Conversion
		* As well as using the constructor, you can also implicitly convert `string`, `PropertyInfo` and member access lambda expressions to a `Field`.
		* For expressions however, this is _still_ rather involved as the expression first needs to be assigned to a variable that explicitly specifies
		* the expression delegate type.
		*/
		[U]
		public void ImplicitConversion()
		{
			Field fieldString = "name";

			Field fieldProperty = typeof(Project).GetProperty(nameof(Project.Name));

			Expression<Func<Project, object>> expression = p => p.Name;
			Field fieldExpression = expression;

			Expect("name")
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldProperty)
				.WhenSerializing(fieldExpression);
		}

		/**
		* [[nest-infer]]
		* ==== Using Nest.Infer methods
		* To ease creating a `Field` instance from expressions, there is a static `Infer` class you can use
		*
		* [TIP]
		* ====
		* This example uses the https://msdn.microsoft.com/en-us/library/sf0df423.aspx#Anchor_0[static import] `using static Nest.Infer;`
		 * in the using directives to shorthand `Nest.Infer.Field<T>()`
		* to simply `Field<T>()`. Be sure to include this static import if copying any of these examples.
		* ====
		*
		*/
		[U]
		public void UsingStaticPropertyField()
		{
			Field fieldString = "name";

			/** but for expressions this is still rather involved */
			var fieldExpression = Field<Project>(p => p.Name);

			/** this can be even shortened even further using a static import.
			* Now that is much terser than our first example using the constructor!
			*/
			fieldExpression = Field<Project>(p => p.Name);

			Expect("name")
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldExpression);

			/** You can specify boosts in the field using a string, as well as using `Nest.Infer.Field` */
			fieldString = "name^2.1";
			fieldString.Boost.Should().Be(2.1);

			fieldExpression = Field<Project>(p => p.Name, 2.1);
			fieldExpression.Boost.Should().Be(2.1);

			Expect("name^2.1")
				.WhenSerializing(fieldString)
				.WhenSerializing(fieldExpression);
		}

		/**
		 * [[camel-casing]]
		* ==== Field name casing
		* By default, NEST https://en.wikipedia.org/wiki/Camel_case[camelcases] **all** field names to better align with typical
		* JavaScript and JSON conventions
		*/
		[U]
		public void DefaultFieldNameInferrer()
		{
			/** using `DefaultFieldNameInferrer()` on ConnectionSettings you can change this behavior */
			var setup = WithConnectionSettings(s => s.DefaultFieldNameInferrer(p => p.ToUpper()));

			setup.Expect("NAME").WhenSerializing(Field<Project>(p => p.Name));

			/** A `Field` constructed from a `string` however is *always* passed along verbatim */
			setup.Expect("NaMe").WhenSerializing<Field>("NaMe");

			/** If you'd like NEST to not change the casing of field names at all,
			 * simply pass a Func<string,string> to `DefaultFieldNameInferrer` that simply returns the
			 * input string
			 */
			setup = WithConnectionSettings(s => s.DefaultFieldNameInferrer(p => p));
			setup.Expect("Name").WhenSerializing(Field<Project>(p => p.Name));
		}

		/**
		 * ==== Complex field name expressions */
		[U]
		public void ComplexFieldNameExpressions()
		{
			/** You can follow your property expression to any depth. Here we are traversing to the `LeadDeveloper` `FirstName` */
			Expect("leadDeveloper.firstName").WhenSerializing(Field<Project>(p => p.LeadDeveloper.FirstName));

			/** When dealing with collection indexers, the indexer access is ignored allowing you to traverse into properties of collections */
			Expect("curatedTags").WhenSerializing(Field<Project>(p => p.CuratedTags[0]));

			/** Similarly, LINQ's `.First()` method also works */
			Expect("curatedTags").WhenSerializing(Field<Project>(p => p.CuratedTags.First()));
			Expect("curatedTags.added").WhenSerializing(Field<Project>(p => p.CuratedTags[0].Added));
			Expect("curatedTags.name").WhenSerializing(Field<Project>(p => p.CuratedTags.First().Name));

			/** NOTE: Remember, these are _expressions_ to access members, and not actual code that will be executed
			*
			* An indexer on a dictionary is assumed to describe a property name */
			Expect("metadata.hardcoded").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"]));
			Expect("metadata.hardcoded.created").WhenSerializing(Field<Project>(p => p.Metadata["hardcoded"].Created));

			/** A cool feature here is that NEST will evaluate variables passed to an indexer */
			var variable = "var";
			Expect("metadata.var").WhenSerializing(Field<Project>(p => p.Metadata[variable]));
			Expect("metadata.var.created").WhenSerializing(Field<Project>(p => p.Metadata[variable].Created));

			/**
			* If you are using Elasticearch's multi-fields, which you really should as they allow
			* you to analyze a string in a number of different ways, these __"virtual"__ sub fields
			* do not always map back on to your POCO. By calling `.Suffix()` on expressions, you describe the sub fields that
			* should be mapped and <<auto-map, how they are mapped>>
			*/
			Expect("leadDeveloper.firstName.raw").WhenSerializing(
				Field<Project>(p => p.LeadDeveloper.FirstName.Suffix("raw")));

			Expect("curatedTags.raw").WhenSerializing(
				Field<Project>(p => p.CuratedTags[0].Suffix("raw")));

			Expect("curatedTags.raw").WhenSerializing(
				Field<Project>(p => p.CuratedTags.First().Suffix("raw")));

			Expect("curatedTags.added.raw").WhenSerializing(
				Field<Project>(p => p.CuratedTags[0].Added.Suffix("raw")));

			Expect("metadata.hardcoded.raw").WhenSerializing(
				Field<Project>(p => p.Metadata["hardcoded"].Suffix("raw")));

			Expect("metadata.hardcoded.created.raw").WhenSerializing(
				Field<Project>(p => p.Metadata["hardcoded"].Created.Suffix("raw")));

			/**
			* You can even chain `.Suffix()` calls to any depth!
			*/
			Expect("curatedTags.name.raw.evendeeper").WhenSerializing(
				Field<Project>(p => p.CuratedTags.First().Name.Suffix("raw").Suffix("evendeeper")));

			/** Variables passed to suffix will be evaluated as well */
			var suffix = "unanalyzed";
			Expect("metadata.var.unanalyzed").WhenSerializing(
				Field<Project>(p => p.Metadata[variable].Suffix(suffix)));

			Expect("metadata.var.created.unanalyzed").WhenSerializing(
				Field<Project>(p => p.Metadata[variable].Created.Suffix(suffix)));
		}

		/**
		* Suffixes can also be appended to expressions using `.AppendSuffix()`. This is useful in cases where you want to apply the same suffix
		* to a list of fields.
		*/
		[U]
		public void AppendingSuffixToExpressions()
		{
			/** Here we have a list of expressions */
			var expressions = new List<Expression<Func<Project, object>>>
			{
				p => p.Name,
				p => p.Description,
				p => p.CuratedTags.First().Name,
				p => p.LeadDeveloper.FirstName,
				p => p.Metadata["hardcoded"]
			};

			/** and we want to append the suffix "raw" to each */
			var fieldExpressions =
				expressions.Select<Expression<Func<Project, object>>, Field>(e => e.AppendSuffix("raw")).ToList();

			Expect("name.raw").WhenSerializing(fieldExpressions[0]);
			Expect("description.raw").WhenSerializing(fieldExpressions[1]);
			Expect("curatedTags.name.raw").WhenSerializing(fieldExpressions[2]);
			Expect("leadDeveloper.firstName.raw").WhenSerializing(fieldExpressions[3]);
			Expect("metadata.hardcoded.raw").WhenSerializing(fieldExpressions[4]);

			/** or we might even want to chain multiple `.AppendSuffix()` calls */
			var multiSuffixFieldExpressions =
				expressions.Select<Expression<Func<Project, object>>, Field>(e => e.AppendSuffix("raw").AppendSuffix("evendeeper")).ToList();

			Expect("name.raw.evendeeper").WhenSerializing(multiSuffixFieldExpressions[0]);
			Expect("description.raw.evendeeper").WhenSerializing(multiSuffixFieldExpressions[1]);
			Expect("curatedTags.name.raw.evendeeper").WhenSerializing(multiSuffixFieldExpressions[2]);
			Expect("leadDeveloper.firstName.raw.evendeeper").WhenSerializing(multiSuffixFieldExpressions[3]);
			Expect("metadata.hardcoded.raw.evendeeper").WhenSerializing(multiSuffixFieldExpressions[4]);
		}

		/**
		* [[member-expressions-only]]
		* ==== Member Expressions only
		* The expression passed to Field should only be a MemberExpression
		* https://docs.microsoft.com/en-us/dotnet/api/system.linq.expressions.memberexpression?view=netframework-4.7.2
		*
		*/
		[U]
		public void OnlyMemberExpressionAllowed()
		{
			var fieldExpression = Field<Project>(p => p.Name + 2);
		}

		///**[[field-name-attribute]]
		//* ==== Attribute based naming
		//*
		//* Using NEST's property attributes you can specify a new name for the properties
		//*/
		public class SystemTextJsonAttribute
		{
			[JsonPropertyName("naam")]
			public string Name { get; set; }
		}

		[U]
		public void SystemTextJsonAttributes()
		{
			Expect("naam").WhenSerializing(Field<SystemTextJsonAttribute>(p => p.Name));
			Expect(new
			{
				naam = "Martijn Laarman"
			}).WhenSerializing(new SystemTextJsonAttribute { Name = "Martijn Laarman" });
		}

		///**[[data-member-field-attribute]]
		// * ==== DataMember attributes
		// *
		// * If a property has a `System.Runtime.Serialization.DataMemberAttribute` applied, this can be used to resolve
		// * a field value for a property
		// */
		//public class DataMember
		//{
		//	[DataMember(Name = "nameFromDataMember")]
		//	public string Name { get; set; }
		//}

		//[U]
		//public void DataAnnotationAttribute()
		//{
		//	Expect("nameFromDataMember").WhenSerializing(Field<DataMember>(p => p.Name));
		//}

		///**[[serializer-specific-field-attribute]]
		//* ==== Serializer specific attributes
		//*
		//* NEST can also use a serializer specific attribute to resolve a field value for a property.
		//* In this example, the {nuget}/NEST.JsonNetSerializer[`JsonNetSerializer`] is hooked up as the
		//* <<custom-serialization, custom serializer>> for the client and we use the `JsonPropertyAttribute` to resolve a field
		//*/
		//public class SerializerSpecific
		//{
		//	[PropertyName("nameInJson"), JsonProperty("nameInJson")]
		//	public string Name { get; set; }
		//}

		//[U]
		//public void SerializerSpecificAnnotations()
		//{
		//	Expect("nameInJson").WhenSerializing(Field<SerializerSpecific>(p => p.Name));
		//}

		///**
		//* If both a NEST property attribute and a serializer specific attribute are present on a property,
		//* **NEST attributes take precedence**
		//*/
		//public class Both
		//{
		//	[Text(Name = "naam")]
		//	[PropertyName("nameInJson"), DataMember(Name = "nameInJson")]
		//	public string Name { get; set; }
		//}

		//[U]
		//public void NestAttributeTakesPrecedence()
		//{
		//	Expect("naam").WhenSerializing(Field<Both>(p => p.Name));
		//	Expect(new
		//	{
		//		naam = "Martijn Laarman"
		//	}).WhenSerializing(new Both { Name = "Martijn Laarman" });
		//}

		/**
		* [[field-inference-caching]]
		*==== Field Inference Caching
		*
		* Resolution of field names is cached _per_ `ConnectionSettings` instance. To demonstrate,
		* take the following simple POCOs
		*/
		class A { public C C { get; set; } }
		class B { public C C { get; set; } }
		class C
		{
			public string Name { get; set; }
		}

		[U]
		public void ExpressionsAreCachedButSeeDifferentTypes()
		{
			var client = TestClient.Default;

			var fieldNameOnA = client.Infer.Field(Field<A>(p => p.C.Name));
			var fieldNameOnB = client.Infer.Field(Field<B>(p => p.C.Name));

			/**
			* Here we have two similarly shaped expressions, one coming from A and one from B
			* that will resolve to the same field name, as expected
			*/

			fieldNameOnA.Should().Be("c.name");
			fieldNameOnB.Should().Be("c.name");

			/**
			* now we create a new connection settings with a re-map for `C` on class `A` to `"d"`
			* now when we resolve the field path for property `C` on `A`, it will be different than
			* for property `C` on `B`
			*/
			var newConnectionSettings = new TestElasticsearchClientSettings()
				.DefaultMappingFor<A>(m => m
					.PropertyName(p => p.C, "d")
				);

			var newClient = new ElasticsearchClient(newConnectionSettings);

			fieldNameOnA = newClient.Infer.Field(Field<A>(p => p.C.Name));
			fieldNameOnB = newClient.Infer.Field(Field<B>(p => p.C.Name));

			fieldNameOnA.Should().Be("d.name");
			fieldNameOnB.Should().Be("c.name");

			/** however we didn't break inference on the first client instance using its separate connection settings */
			fieldNameOnA = client.Infer.Field(Field<A>(p => p.C.Name));
			fieldNameOnB = client.Infer.Field(Field<B>(p => p.C.Name));

			fieldNameOnA.Should().Be("c.name");
			fieldNameOnB.Should().Be("c.name");
		}

		///**
		//* [[field-inference-precedence]]
		//* ==== Inference Precedence
		//* To wrap up, the precedence in which field names are inferred is:
		//*
		//* . A naming of the property on `ConnectionSettings` using `.PropertyName()`
		//* . A NEST `PropertyNameAttribute`
		//* . Ask the serializer if the property has a verbatim value, e.g. it has a `JsonPropertyAttribute` if using {nuget}/NEST.JsonNetSerializer[`JsonNetSerializer`]
		//* . See if the `MemberInfo` has a `DataMemberAttribute` applied
		//* . Pass the `MemberInfo` to the `DefaultFieldNameInferrer`, which by default will camel case the `Name` property
		//*
		//* The following example class will demonstrate this precedence
		//*/
		//private class Precedence
		//{
		//	[Text(Name = "renamedIgnoresNest")]
		//	[PropertyName("renamedIgnoresJsonProperty"),JsonProperty("renamedIgnoresJsonProperty")]
		//	public string RenamedOnConnectionSettings { get; set; } //<1> Even though this property has various attributes applied we provide an override on ConnectionSettings later that takes precedence.

		//	[Text(Name = "nestAtt")]
		//	[PropertyName("nestProp"),JsonProperty("jsonProp")]
		//	public string NestAttribute { get; set; } //<2> Has a `TextAttribute`, `PropertyNameAttribute` and a `JsonPropertyAttribute` - the `TextAttribute` takes precedence.

		//	[PropertyName("nestProp"),JsonProperty("jsonProp")]
		//	public string NestProperty { get; set; } //<3> Has both a `PropertyNameAttribute` and a `JsonPropertyAttribute` - the `PropertyNameAttribute` takes precedence.

		//	[DataMember(Name ="jsonProp")]
		//	public string JsonProperty { get; set; } //<4> `JsonPropertyAttribute` takes precedence.

		//	[PropertyName("dontaskme"),JsonProperty("dontaskme")]
		//	public string AskSerializer { get; set; } //<5> This property we are going to hard code in our custom serializer to resolve to ask.

		//	[DataMember(Name = "data")]
		//	public string DataMember { get; set; }

		//	public string DefaultFieldNameInferrer { get; set; } //<6>  We are going to register a DefaultFieldNameInferrer on ConnectionSettings that will uppercase all properties.
		//}

		/**
		* We'll create a custom `IPropertyMappingProvider` that renames any property named `AskSerializer` to `ask`.
		* and hook it up when creating the Connection Settings in the following section.
		*/
		//private class CustomPropertyMappingProvider : PropertyMappingProvider
		//{
		//	public override PropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		//	{
		//		return memberInfo.Name == nameof(Precedence.AskSerializer)
		//			? new PropertyMapping { Name = "ask" }
		//			: base.CreatePropertyMapping(memberInfo);
		//	}
		//}

		//[U]
		//[ProjectReferenceOnly]
		//public void PrecedenceIsAsExpected()
		//{
		//	/** Now, when we create the Connection Settings to use to configure the client, we'll add
		//	 * - a default mapping for the `Precedence` type
		//	 * - our `CustomPropertyMappingProvider`
		//	 * - a delegate to perform default field name inference
		//	*/
		//	var usingSettings = WithConnectionSettings(s => s

		//		.DefaultMappingFor<Precedence>(m => m
		//			.PropertyName(p => p.RenamedOnConnectionSettings, "renamed") // <1> Rename on the mapping for the `Precedence` type
		//		)
		//		.DefaultFieldNameInferrer(p => p.ToUpperInvariant()) // <2> Default inference for a field, if no other rules apply or are specified for a given field
		//	).WithPropertyMappingProvider(new CustomPropertyMappingProvider()); // <3> Hook up the custom `IPropertyMappingProvider`

		//	usingSettings.Expect("renamed").ForField(Field<Precedence>(p => p.RenamedOnConnectionSettings));
		//	usingSettings.Expect("nestAtt").ForField(Field<Precedence>(p => p.NestAttribute));
		//	usingSettings.Expect("nestProp").ForField(Field<Precedence>(p => p.NestProperty));
		//	usingSettings.Expect("jsonProp").ForField(Field<Precedence>(p => p.JsonProperty));
		//	usingSettings.Expect("ask").ForField(Field<Precedence>(p => p.AskSerializer));
		//	usingSettings.Expect("data").ForField(Field<Precedence>(p => p.DataMember));
		//	usingSettings.Expect("DEFAULTFIELDNAMEINFERRER").ForField(Field<Precedence>(p => p.DefaultFieldNameInferrer));


		//	/** The same naming rules also apply when indexing a document */
		//	usingSettings.Expect(new []
		//	{
		//		"ask",
		//		"DEFAULTFIELDNAMEINFERRER",
		//		"jsonProp",
		//		"nestProp",
		//		"nestAtt",
		//		"renamed",
		//		"data"
		//	}).AsPropertiesOf(new Precedence
		//	{
		//		RenamedOnConnectionSettings = "renamed on connection settings",
		//		NestAttribute = "using a nest attribute",
		//		NestProperty = "using a nest property",
		//		JsonProperty = "the default serializer resolves json property attributes",
		//		AskSerializer = "serializer fiddled with this one",
		//		DefaultFieldNameInferrer = "shouting much?",
		//		DataMember = "using a DataMember attribute"
		//	});
		//}

		/**
		 *[[inherited-field-inference]]
		 * ==== Overriding inherited field inference
		 *
		 * Properties inherited from a base type can be ignored and renamed using `DefaultMappingFor<T>` for
		 * a given type, on Connection Settings.
		 *
		 * To demonstrate, the `IgnoreMe` property on `Parent` can be ignored on the `Child` type, and the
		 * `Description` property renamed, using `DefaultMappingFor<Child>(...)`
		 */
		public class Parent
		{
			public int Id { get; set; }
			public string Description { get; set; }
			public string IgnoreMe { get; set; }
		}

		public class Child : Parent { }

		//[U]
		//public void CodeBasedConfigurationInherits()
		//{
		//	var usingSettings = WithConnectionSettings(s => s
		//		.DefaultMappingFor<Child>(m => m
		//			.PropertyName(p => p.Description, "desc")
		//			.Ignore(p => p.IgnoreMe)
		//		)
		//	);
		//	usingSettings.Expect(new []
		//	{
		//		"id",
		//		"desc",
		//	}).AsPropertiesOf(new Child
		//	{
		//		Id = 1,
		//		Description = "this property will be renamed for Child",
		//		IgnoreMe = "this property will be ignored (won't be serialized) for Child",
		//	});

		//}


		//public class SourceModel
		//{
		//	[PropertyName("gexo")]
		//	public GeoModel Geo { get; set; }
		//}

		public class GeoModel
		{
			[DataMember(Name = "country_iso_code")]
			public string CountryIsoCode { get; set; }
		}

		///// this tests runs both during development as wel as during canary builds which reference
		///// all the artifacts as nuget packages instead of project references
		//[U]
		//public void JsonPropertyIsPickedWhenSerializerIsInternalized()
		//{
		//	var usingSettings = WithConnectionSettings(s => s)
		//		.WithSourceSerializer(JsonNetSerializer.Default);

		//	usingSettings.Expect("gexo").ForField(Field<SourceModel>(p=>p.Geo));
		//	usingSettings.Expect("country_iso_code").ForField(Field<GeoModel>(p=>p.CountryIsoCode));
		//	usingSettings.Expect(new []
		//	{
		//		"country_iso_code",
		//	}).AsPropertiesOf(new GeoModel { CountryIsoCode = "nl" });
		//	usingSettings.Expect(new []
		//	{
		//		"gexo",
		//	}).AsPropertiesOf(new SourceModel { Geo = new GeoModel { CountryIsoCode = "nl" } });

		//	usingSettings.Expect("gexo.country_iso_code").ForField(Field<SourceModel>(p=>p.Geo.CountryIsoCode));
		//}
	}
}
