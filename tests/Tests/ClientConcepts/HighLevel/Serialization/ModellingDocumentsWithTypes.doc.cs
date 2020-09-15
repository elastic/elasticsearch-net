// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.Extensions;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Core.Client;
using Tests.Framework.DocumentationTests;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	/**[[modelling-documents-with-types]]
	 * === Modelling documents with types
	 *
	 * Elasticsearch provides search and aggregation capabilities on the documents that it is sent and indexes. These documents are sent as
	 * JSON objects within the request body of a HTTP request. It is natural to model documents within NEST and Elasticsearch.Net using
	 * https://en.wikipedia.org/wiki/Plain_Old_CLR_Object[POCOs (__Plain Old CLR Objects__)].
	 *
	 * This section provides an overview of how types and type hierarchies can be used to model documents.
	 */
	public class ModellingDocumentsWithTypes : DocumentationTestBase
	{
		/**[[default-behaviour]]
		 * ==== Default behaviour
		 *
		 * NEST's default behaviour is to serialize type property names as camelcase JSON object members.
		 * Given the POCO
		 */
		public class MyDocument
		{
			public string StringProperty { get; set; }
		}

		/**
		 * The following example demonstrates this behaviour
		 */
		[U]
		public void Default()
		{
			var indexResponse = Client.Index(
				new MyDocument { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * serializing the POCO property named `StringProperty` to the JSON object member named `stringProperty`
			 */
			// json
			var expected = new { stringProperty = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**[[default-field-name-inferrer]]
		 * ==== `DefaultFieldNameInferrer` setting
		 *
		 * Many different systems may be indexing documents into Elasticsearch, using a different
		 * convention than camelcase for JSON object members. How NEST serializes
		 * POCO property names can be globally controlled using `DefaultFieldNameInferrer` on
		 * `ConnectionSettings`. The following example defines a function that applies snake casing
		 * to a passed string, with the function called inside a delegate passed to `DefaultFieldNameInferrer`
		 */
		[U]
		public void DefaultFieldNameInferrer()
		{
			var settings = new ConnectionSettings();

			//hide
			settings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();

			static string ToSnakeCase(string s) // <1> function to convert a string to snake case
			{
				var builder = new StringBuilder(s.Length);
				for (int i = 0; i < s.Length; i++)
				{
					var c = s[i];
					if (char.IsUpper(c))
					{
						if (i == 0)
							builder.Append(char.ToLowerInvariant(c));
						else if (char.IsUpper(s[i - 1]))
							builder.Append(char.ToLowerInvariant(c));
						else
						{
							builder.Append("_");
							builder.Append(char.ToLowerInvariant(c));
						}
					}
					else
						builder.Append(c);
				}

				return builder.ToString();
			}

			settings.DefaultFieldNameInferrer(p => ToSnakeCase(p)); // <2> apply snake casing to **all** POCO properties

			var client = new ElasticClient(settings);

			var indexResponse = client.Index(
				new MyDocument { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * The above example serializes the `MyDocument` POCO to
			 */
			// json
			var expected = new { string_property = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**[[propertyname-attribute]]
		 * ==== `PropertyName` attribute
		 *
		 * Sometimes there may be a need to change only how specific POCO properties are serialized. The
		 * `PropertyName` attribute can be applied to POCO properties to control the name that the POCO
		 * property will serialize to and deserialize from. The following example uses the `PropertyName` attribute
		 * to control how the POCO property named `StringProperty` is serialized
		 */
		public class MyDocumentWithPropertyName
		{
			[PropertyName("string_property")]
			public string StringProperty { get; set; }
		}

		[U]
		public void PropertyName()
		{
			var indexResponse = Client.Index(
				new MyDocumentWithPropertyName { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * The above example serializes the `MyDocumentWithPropertyName` POCO to
			 */
			// json
			var expected = new { string_property = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**[[nest-property-attributes]]
		 * ==== NEST property attributes
		 *
		 * The `PropertyName` attribute can be used to control how a POCO property is serialized. NEST contains
		 * a collection of other attributes, such as `Text` attribute, that not only control how a POCO property is serialized,
		 * but also control how a POCO property is mapped when using <<attribute-mapping, Attribute mapping>>. The `Name` property of
		 * these attributes controls how a POCO property is serialized in a similar fashion to `PropertyName` attribute.
		 *
		 * The following example uses the `Text` attribute to control how the POCO property named `StringProperty` is serialized
		 */
		public class MyDocumentWithTextProperty
		{
			[Text(Name = "string_property")]
			public string StringProperty { get; set; }
		}

		[U]
		public void TextProperty()
		{
			var indexResponse = Client.Index(
				new MyDocumentWithTextProperty { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * The above example serializes the `MyDocumentWithTextProperty` POCO to
			 */
			// json
			var expected = new { string_property = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**[[data-member-attribute]]
		 * ==== DataMember attribute
		 *
		 * The `System.Runtime.Serialization.DataMember` attribute can be used to control how a POCO property is serialized. in a similar
		 * fashion to `PropertyName` attribute. The `DataMember` attribute may be preferred over `PropertyName` attribute in situations where
		 * the project in which the POCOs are defined does not have a dependency on NEST.
		 *
		 * The following example uses the `DataMember` attribute to control how the POCO property
		 * named `StringProperty` is serialized
		 */
		public class MyDocumentWithDataMember
		{
			[DataMember(Name = "string_property")]
			public string StringProperty { get; set; }
		}

		[U]
		public void DataMember()
		{
			var indexResponse = Client.Index(
				new MyDocumentWithDataMember { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * The above example serializes the `MyDocumentWithDataMember` POCO to
			 */
			// json
			var expected = new { string_property = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**[[default-mapping-for]]
		 * ==== `DefaultMappingFor<TDocument>` setting
		 *
		 * Whilst `DefaultFieldNameInferrer` applies a convention to all POCO properties, there may be occasions where
		 * only particular properties of a specific POCO are serialized differently. The `DefaultMappingFor<TDocument>` setting
		 * on `ConnectionSettings` can be used to change how properties are mapped for a type. The following example
		 * changes how the `StringProperty` is serialized for the `MyDocument` type
		 */
		[U]
		public void DefaultMappingFor()
		{
			var settings = new ConnectionSettings();

			//hide
			settings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();

			settings.DefaultMappingFor<MyDocument>(d => d
				.PropertyName(p => p.StringProperty, nameof(MyDocument.StringProperty)) // <1> serialize the `StringProperty` type as `"StringProperty"`
			);

			var client = new ElasticClient(settings);

			var indexResponse = client.Index(
				new MyDocument { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * The above example serializes the `MyDocument` POCO to
			 */
			// json
			var expected = new { StringProperty = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**
		 * `DefaultMappingFor<TDocument>`'s behaviour can be somewhat surprising when class hierarchies are involved. Consider the following
		 * POCOs
		 */
		public class MyBaseDocument
		{
			public string StringProperty { get; set; }
		}

		public class MyDerivedDocument : MyBaseDocument
		{
			public int IntProperty { get; set; }
		}

		/**
		 * When serializing an instance of `MyDerivedDocument` with
		 */
		[U]
		public void DerivedDocument()
		{
			var indexResponse = Client.Index(
				new MyDerivedDocument { StringProperty = "value", IntProperty = 2 },
				i => i.Index("my_documents"));

			/**
			 * it serializes to
			 */
			// json
			var expected = new { intProperty = 2, stringProperty = "value" };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));
		}

		/**
		 * Now, consider what happens when `DefaultMappingFor<TDocument>` is used to control how `MyDerivedDocument`
		 * is mapped
		 */
		[U]
		public void DerivedDocumentIgnoreStringProperty()
		{
			var settings = new ConnectionSettings();

			//hide
			settings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();

			settings.DefaultMappingFor<MyDerivedDocument>(d => d
				.PropertyName(p => p.IntProperty, nameof(MyDerivedDocument.IntProperty)) // <1> serialize the `IntProperty` type as `"IntProperty"`
				.Ignore(p => p.StringProperty) // <2> ignore `StringProperty`
			);

			var client = new ElasticClient(settings);

			var indexResponse = client.Index(
				new MyDerivedDocument { StringProperty = "value", IntProperty = 2 },
				i => i.Index("my_documents"));

			/**
			 * `MyDerivedDocument` serializes to
			 */
			// json
			var expected = new { IntProperty = 2 };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));

			/**
			 * showing that the POCO property named `IntProperty` is serialized to JSON object member named `"IntProperty"` and
			 * `StringProperty` has not been serialized (ignored). This shouldn't be surprising.
			 *
			 * Now, index an instance of the base class, `MyBaseDocument`
			 */
			var indexResponse2 = client.Index(
				new MyBaseDocument { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * This serializes to an empty JSON object
			 */
			// json
			var expected2 = new { };

			/**
			 * The `StringProperty` has not been serialized (ignored) for the base class, even though `DefaultMappingFor<TDocument>`
			 * was used with the derived class, `MyDerivedDocument`
			 */
			// hide
			indexResponse2.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected2));
		}

		/**
		 * This happens because `MyBaseDocument` is the _declaring type_ for the `StringProperty` member; when the `MemberInfo` for
		 * the `StringProperty` is retrieved from the expression `p => p.StringProperty`, the `DeclaringType` is `MyBaseDocument`.
		 * Since `DefaultMappingFor<TDocument>` persists property mappings for types in a dictionary keyed on `MemberInfo`, the
		 * `PropertyName()` mapping defined using `DefaultMappingFor<MyDerivedDocument>` also applies to the base type, `MyBaseDocument`.
		 *
		 * Consider a more involved example where the base type defines a member as `virtual`, and the derived type provides an
		 * `override` for the member
		 */
		public class MyBaseDocumentVirtualProperty
		{
			public virtual string StringProperty { get; set; }
		}

		public class MyDerivedDocumentOverrideProperty : MyBaseDocumentVirtualProperty
		{
			public override string StringProperty { get; set; }

			public int IntProperty { get; set; }
		}

		/**
		 * With a similar scenario to the last example, `DefaultMappingFor<TDocument>` is defined for the
		 * derived type, `MyDerivedDocumentOverrideProperty`
		 */
		[U]
		public void DerivedDocumentVirtualIgnoreStringProperty()
		{
			var settings = new ConnectionSettings();

			//hide
			settings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();

			settings.DefaultMappingFor<MyDerivedDocumentOverrideProperty>(d => d
				.PropertyName(p => p.IntProperty, nameof(MyDerivedDocumentOverrideProperty.IntProperty))
				.Ignore(p => p.StringProperty)
			);

			var client = new ElasticClient(settings);

			var indexResponse = client.Index(
				new MyDerivedDocumentOverrideProperty { StringProperty = "value", IntProperty = 2 },
				i => i.Index("my_documents"));

			/**
			 * The instance of `MyDerivedDocumentOverrideProperty` serializes to
			 */
			// json
			var expected = new { stringProperty = "value", IntProperty = 2 };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));

			/**
			 * Notably, the `StringProperty` member has been serialized and not ignored, even though the
			 * `DefaultMappingFor<MyDerivedDocumentOverrideProperty>` configuration specifies to ignore it.
			 *
			 * Serializing an instance of the base type, `MyBaseDocumentVirtualProperty`
			 */
			var indexResponse2 = client.Index(
				new MyBaseDocumentVirtualProperty { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * serializes to an empty JSON object
			 */
			// json
			var expected2 = new { };

			// hide
			indexResponse2.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected2));
		}


		/**
		 * This may be surprising.
		 *
		 * [IMPORTANT]
		 * --
		 * There is a difference in how `MemberInfo` that represent the members of a type are retrieved when using reflection, compared
		 * to how `MemberInfo` are determined from expressions.
		 *
		 * As an example, when retrieving `StringProperty` member on `MyDerivedDocumentOverrideProperty` using reflection, both
		 * `DeclaringType` and `ReflectedType` are `MyDerivedDocumentOverrideProperty`
		 *
		 * [source, csharp]
		 * ----
		 * var memberInfo = typeof(MyDerivedDocumentOverrideProperty).GetProperty("StringProperty");
		 * Console.WriteLine($"DeclaringType: {memberInfo.DeclaringType.Name}");
		 * Console.WriteLine($"ReflectedType: {memberInfo.ReflectedType.Name}");
		 * ----
		 *
		 * In contrast, when retrieving `StringProperty` member on `MyDerivedDocumentOverrideProperty` using an expression, both
		 * `DeclaringType` and `ReflectedType` are `MyBaseDocumentVirtualProperty`
		 *
		 * [source, csharp]
		 * ----
		 * public class MemberVisitor : ExpressionVisitor
		 * {
		 * 	   protected override Expression VisitMember(MemberExpression node)
		 * 	   {
		 * 	       Console.WriteLine($"DeclaringType: {node.Member.DeclaringType.Name}");
		 * 	       Console.WriteLine($"ReflectedType: {node.Member.ReflectedType.Name}");
		 * 	       return base.VisitMember(node);
		 * 	   }
		 * }
		 *
		 * Expression<Func<MyDerivedDocumentOverrideProperty, string>> memberExpression =
		 *     p => p.StringProperty;
		 *
	     * var visitor = new MemberVisitor();
	     * visitor.Visit(memberExpression);
		 * ----
		 *
		 * Crucially, this difference in how `MemberInfo` are retrieved explains the result of the previous example;
		 * The serialization implementation determines the members for a given type using reflection, whereas `DefaultMappingFor<TDocument>`
		 * determines the member in `PropertyName` using the expression passed.
		 * --
		 *
		 * As another example, consider a derived type that hides a base type member, using the `new` keyword
		 */
		public class MyDerivedDocumentShadowProperty : MyBaseDocument
		{
			public new string StringProperty { get; set; }
		}

		/**
		 * Now when configuring `DefaultMappingFor<TDocument>` for `MyDerivedDocumentShadowProperty`
		 */
		[U]
		public void DerivedDocumentShadowProperty()
		{
			var settings = new ConnectionSettings();

			//hide
			settings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();

			settings.DefaultMappingFor<MyDerivedDocumentShadowProperty>(d => d
				.Ignore(p => p.StringProperty)
			);

			var client = new ElasticClient(settings);

			var indexResponse = client.Index(
				new MyDerivedDocumentShadowProperty { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * an instance of `MyDerivedDocumentShadowProperty` serializes to
			 */
			// json
			var expected = new { };

			// hide
			indexResponse.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected));

			/**
			 * Whilst the base type `MyBaseDocument`
			 */

			var indexResponse2 = client.Index(
				new MyBaseDocument { StringProperty = "value" },
				i => i.Index("my_documents"));

			/**
			 * serializes to
			 */
			// json
			var expected2 = new { stringProperty = "value" };

			// hide
			indexResponse2.ApiCall.RequestBodyInBytes.Utf8String().Should().Be(JsonConvert.SerializeObject(expected2));
		}
		/**
		 * In summary, careful consideration should be made when using type hierarchies to represent documents
		 * that are indexed in Elasticsearch. It is generally recommended to stick to simple POCOs, where possible.
		 */
	}
}
