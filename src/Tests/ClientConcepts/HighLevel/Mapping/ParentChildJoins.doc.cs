using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
	/**[[parent-child-joins]]
	* === Parent Child joins using the join mapping
	*
	* Prior to Elasticsearch 6.x you could have multiple types in a single index. Through the special _parent field mapping of a given type
	* one could create 1 to N relationship of parent => children documents. This worked because when indexing children you passed a
	* `_parent` id which would act as the routing key making sure a parent and its (grand)children all lived on the same shard.
	*
	* Starting with 6.x indices, you may no longer have multiple types in a single index. One reason for this is that if for instance
	* two types * have the same `name` property they need to be mapped exactly the same but all the API's acted as if you could map
	* them individually which often lead to confusion. `_type` always acted as a discriminating column but was often explained as a table.
	*
	* So how do you create a parent join now that indices no longer allow you store different types in the same index and therefor also
	* not on the same shard?
	*
	*/
	public class ParentChildJoins : DocumentationTestBase
	{
		/**
		* ==== Parent And Child example
		*
		* In the following contrived example we create two .NET types called `MyParent` and `MyChild` both extending from `MyDocument`.
		* There is no requirement that the parent and child .NET types are related at all. The types could be plain POCO's or `MyChild`
		* could be a subclass of `MyParent`. The only requirement is that they have **a** property where the property type is `JoinField`.
		*
		* As per Elasticsearch's constraints you should only have **a single** property of type JoinField on your POCO.
		*
		*/
		public abstract class MyDocument
		{
			public int Id { get; set; }
			public JoinField MyJoinField { get; set; }
		}

		public class MyParent : MyDocument
		{
			[Text]
			public string ParentProperty { get; set; }
		}

		public class MyChild : MyDocument
		{
			[Text]
			public string ChildProperty { get; set; }
		}

		/**
		* ==== Parent And Child mapping
		*
		* In the following example we setup our client and give our types prefered index and type names. What's new is that we can
		* also give a type a preferred `RelationName` as can be seen on the `InferMappingFor<MyParent>`.
		*
		* Also note that we give `MyChild` and `MyParent` the same default `doc` type name to make sure they end up in the same index
		* under the same type.
		*
		*/
		[U]
		public void SimpleParentChildMapping()
		{

			var connectionSettings = new ConnectionSettings()
				.InferMappingFor<MyDocument>(m => m.IndexName("index").TypeName("doc"))
				.InferMappingFor<MyChild>(m => m.IndexName("index").TypeName("doc"))
				.InferMappingFor<MyParent>(m => m.IndexName("index").TypeName("doc").RelationName("parent"));

			/**
			* with the `connectionSettings` all setup we can proceed to map `MyParent` and `MyChild` as part of the create index request.
			*/
			var descriptor = new CreateIndexDescriptor(Index<MyDocument>())
				.Mappings(ms => ms
					.Map<MyDocument>(m => m
						.AutoMap<MyParent>() // <1> Map all of the MyParent properties
						.AutoMap<MyChild>() // <2> Map all of the MyChild properties
						.Properties(props => props
							.Join(j => j // <3> Automap does not automatically translate `JoinField` since we are only allowed to have one.
								.Name(p=>p.MyJoinField)
								.Relations(r => r
									.Join<MyParent, MyChild>()
								)
							)
						)
					)
				);

			/**
			* We call `AutoMap()` for both types to discover properties of both .NET types. `AutoMap()` won't automatically setup the
			* join field mapping though because NEST can not infer all the `Relations` that are required by your domain.
			*
			* In this case we setup `MyChild` to be child of `MyParent`. `.Join()` has many overloads so be sure to check them out if you
			* need to map not one but multiple children.
			*
			*/

			//json
			var expected = new
			{
				mappings = new
				{
					doc = new
					{
						properties = new
						{
							parentProperty = new {type = "text"},
							childProperty = new {type = "text"},
							id = new {type = "integer"},
							myJoinField = new
							{
								type = "join",
								relations = new
								{
									parent = "mychild"
								}
							}
						}
					}
				}
			};

			/**
			* Note how `MyParent`'s relation name is `parent` because of the mapping on connection settings. This also comes in handy
			* later when doing strongly typed `has_child` and `has_parent` queries.
			*/

			//hide
			WithConnectionSettings(s => connectionSettings)
				.Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
		}

		/**
		* ==== Indexing parents or children
		*
		* Now that we have our join field mapping set up on the index we can proceed to index parents and children documents.
		*/

		[U]
		public void Indexing()
		{
			// hide
			var client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming().PrettyJson());
			/**
			* To mark a document with the relation name of the parent `MyParent` all of the following three ways are equivalent.
			*
			* In the first we explicitly call `JoinField.Root` to mark this document as the root of a parent child relationship namely
			* that of `MyParent`. In the following examples we rely on implicit conversion from `string` and `Type` to do the same.
			*/
			var parentDocument = new MyParent
			{
				Id = 1,
				ParentProperty = "a parent prop",
				MyJoinField = JoinField.Root<MyParent>()
			};
			parentDocument = new MyParent
			{
				Id = 1,
				ParentProperty = "a parent prop",
				MyJoinField = typeof(MyParent) // <1> this lets the join data type know this is a root document of type `myparent`
			};
			parentDocument = new MyParent
			{
				Id = 1,
				ParentProperty = "a parent prop",
				MyJoinField = "parent" // <1> this lets the join data type know this is a root document of type `myparent`
			};
			var indexParent = client.IndexDocument<MyDocument>(parentDocument);
			//json
			var expected = new
			{
				id = 1,
				parentProperty = "a parent prop",
				myJoinField = "myparent"
			};
			Expect(expected).FromRequest(indexParent);


			/**
			 * Linking the child document to its parent follows a similar pattern.
			 * Here we create a link by inferring the id from our parent instance `parentDocument`
			 */
			var indexChild = client.IndexDocument<MyDocument>(new MyChild
			{
				MyJoinField = JoinField.Link<MyChild, MyParent>(parentDocument)
			});
			/**
			 * or here we are simply stating this document is of type `mychild` and should be linked
			 * to parent id 1 from `parentDocument`.
			 */
			indexChild = client.IndexDocument<MyDocument>(new MyChild
			{
				Id = 2,
				MyJoinField = JoinField.Link<MyChild>(1)
			});
			//json
			var childJson = new
			{
				id = 2,
				myJoinField = new
				{
					name = "mychild",
					parent = "1"
				}
			};
			Expect(childJson).FromRequest(indexChild);
			/**
			 * The mapping already links `myparent` as the parent type so we only need to suply the parent id.
			 * In fact there are many ways to create join field:
			 */

			Expect("myparent").WhenSerializing(JoinField.Root(typeof(MyParent)));
			Expect("myparent").WhenSerializing(JoinField.Root(Relation<MyParent>()));
			Expect("myparent").WhenSerializing(JoinField.Root<MyParent>());
			Expect("myparent").WhenSerializing(JoinField.Root("myparent"));

			var childLink = new {name = "mychild", parent = "1"};
			Expect(childLink).WhenSerializing(JoinField.Link<MyChild>(1));
			Expect(childLink).WhenSerializing(JoinField.Link<MyChild, MyParent>(parentDocument));
			Expect(childLink).WhenSerializing(JoinField.Link("mychild", 1));
			Expect(childLink).WhenSerializing(JoinField.Link(typeof(MyChild), 1));
		}

		/**
		 * ==== Routing parent child documents
		 *
		 * A parent and all of it's (grand)children still need to live on the same shard so you still need to take care of specifying routing.
		 *
		 * In the past you would have to provide the parent id on the request using `parent=<parentid>` this was always an alias for routing
		 * and thus in Elasticsearch 6.x you need to provide `routing=<parentid>` instead.
		 *
		 * NEST has a handy helper to infer the correct routing value given a document that is smart enough to find the join field and infer
		 * correct parent.
		 */

		[U]
		public void Inferrence()
		{
			// hide
			var client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming().PrettyJson());
			var infer = client.Infer;
			var parent = new MyParent {Id = 1337, MyJoinField = JoinField.Root<MyParent>()};
			infer.JoinRouting(parent).Should().Be("1337");

			var child = new MyChild {Id = 1338, MyJoinField = JoinField.Link<MyChild>(parentId: "1337")};
			infer.JoinRouting(child).Should().Be("1337");

			child = new MyChild {Id = 1339, MyJoinField = JoinField.Link<MyChild, MyParent>(parent)};
			infer.JoinRouting(child).Should().Be("1337");

			/**
			 * here we index `parent` and rather than fishing out the parent id by inspecting `parent` we just pass the instance
			 * to `Routing` which can infer the correct routing key based on the JoinField property on the instance
			 */
			var indexResponse = client.Index(parent, i => i.Routing(parent));
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=1337");

			/**
			 * The same goes for when we index a child, we can pass the instance directly to `Routing` and NEST will use the parent id
			 * already specified on `child`
			 */
			indexResponse = client.Index(child, i => i.Routing(child));
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=1337");

			/** Wouldn't be handy if NEST does this automatically? It does! */
			indexResponse = client.IndexDocument(child);
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=1337");

			/** You can always override the default inferred routing though */
			indexResponse = client.Index(child, i => i.Routing("explicit"));
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=explicit");

			indexResponse = client.Index(child, i => i.Routing(null));
			indexResponse.ApiCall.Uri.Query.Should().NotContain("routing");

			/**
			 * This works for both the fluent and object initializer syntax
			 */

			var indexRequest = new IndexRequest<MyChild>(child);
			indexResponse = client.Index(indexRequest);
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=1337");
			/**
			 * Its important to note that the routing is resolved at request time, not instantation time
			 * here we update the `child`'s `JoinField` after already creating the index request for `child`
			 */
			child.MyJoinField = JoinField.Link<MyChild>(parentId: "something-else");
			indexResponse = client.Index(indexRequest);
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=something-else");
		}

		/** [NOTE]
		 * --
		 * If you use multiple levels of parent and child relations e.g `A => B => C` when you index `C` you
		 * need to provide the id of `A` as the routing key but the id of `B` to set up the relation on the join field. In this case NEST
		 * `JoinRouting` helper is unable to resolve to the id of `A` and will return the id of `B`.
		 *
		 */
	}
}
