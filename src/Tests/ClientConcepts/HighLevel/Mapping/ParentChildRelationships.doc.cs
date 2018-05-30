using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Mapping
{
	/**[[parent-child-relationships]]
	* === Parent/Child relationships
	*
	* Prior to Elasticsearch 6.x you could have multiple types in a single index. Through the special `_parent` field mapping of a given type,
	* one could create 1 to N relationships of parent => children documents. This worked because when indexing children, you passed a
	* `_parent` id which acted as the routing key, ensuring a parent, its children and any ancestors all lived on the same shard.
	*
	* Starting with 6.x indices, multiple types are no longer suppported in a single index. One reason for this is that if for instance
	* two types have the same `name` property, this property needed to be mapped exactly the same for both types, but all the APIs act as if you can map
	* them individually, often causing confusion. Essentially, `_type` always acted as a discriminating field within an index but was often explained
	* as being more special than this.
	*
	* So how do you create a parent join, now that indices no longer allow you store different types in the same index and therefor also
	* not on the same shard?
	*
	*/
	public class ParentChildRelationships : DocumentationTestBase
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
		* In the following example we setup our client and give our types prefered index and type names.  Starting with NEST 6.x we can
		* also give a type a preferred `RelationName` as can be seen on the `DefaultMappingFor<MyParent>`.
		*
		* Also note that we give `MyChild` and `MyParent` the same default `doc` type name to make sure they end up in the same index
		* under the same type.
		*
		*/
		[U]
		public void SimpleParentChildMapping()
		{
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, new InMemoryConnection()) // <1> for the purposes of this example, an in memory connection is used which doesn't actually send a request. In your application, you'd use the default connection or your own implementation that actually sends a request.
				.DefaultMappingFor<MyDocument>(m => m.IndexName("index").TypeName("doc"))
				.DefaultMappingFor<MyChild>(m => m.IndexName("index").TypeName("doc"))
				.DefaultMappingFor<MyParent>(m => m.IndexName("index").TypeName("doc").RelationName("parent"));

			var client = new ElasticClient(connectionSettings);

			// hide
			connectionSettings.DisableDirectStreaming();

			/**
			* With the `ConnectionSettings` set up, we can proceed to map `MyParent` and `MyChild` as part of the create index request.
			*/
			var createIndexResponse = client.CreateIndex("index", c => c
				.Index<MyDocument>()
				.Mappings(ms => ms
					.Map<MyDocument>(m => m
						.RoutingField(r => r.Required()) // <1> recommended to make the routing field mandatory so you can not accidentally forget
						.AutoMap<MyParent>() // <2> Map all of the `MyParent` properties
						.AutoMap<MyChild>() // <3> Map all of the `MyChild` properties
						.Properties(props => props
							.Join(j => j // <4> Additionally map the `JoinField` since it is not automatically mapped by `AutoMap()`
								.Name(p => p.MyJoinField)
								.Relations(r => r
									.Join<MyParent, MyChild>()
								)
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
						_routing = new { required = true },
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
				.Expect(expected).FromRequest(createIndexResponse);
		}

		/**
		* ==== Indexing parents or children
		*
		* Now that we have our join field mapping set up on the index, we can proceed to index parent and child documents.
		*/
		[U] public void Indexing()
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
				MyJoinField = "myparent" // <2> this lets the join data type know this is a root document of type `myparent`
			};
			var indexParent = client.IndexDocument<MyDocument>(parentDocument);

			//json
			var expected = new
			{
				id = 1,
				parentProperty = "a parent prop",
				myJoinField = "myparent"
			};

			// hide
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

			// hide
			Expect(childJson).FromRequest(indexChild);

			/**
			 * The mapping already links `myparent` as the parent type so we only need supply the parent id.
			 * In fact there are many ways to create join field:
			 */
			Expect("myparent").WhenSerializing(JoinField.Root(typeof(MyParent)));
			Expect("myparent").WhenSerializing(JoinField.Root(Relation<MyParent>()));
			Expect("myparent").WhenSerializing(JoinField.Root<MyParent>());
			Expect("myparent").WhenSerializing(JoinField.Root("myparent"));

			var childLink = new { name = "mychild", parent = "1" };
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
		public void Inference()
		{
			// hide
			var client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming().PrettyJson());
			var infer = client.Infer;
			var parent = new MyParent {Id = 1337, MyJoinField = JoinField.Root<MyParent>()};
			infer.Routing(parent).Should().Be("1337");

			var child = new MyChild {Id = 1338, MyJoinField = JoinField.Link<MyChild>(parentId: "1337")};
			infer.Routing(child).Should().Be("1337");

			child = new MyChild {Id = 1339, MyJoinField = JoinField.Link<MyChild, MyParent>(parent)};
			infer.Routing(child).Should().Be("1337");

			/**
			 * here we index `parent` and rather than fishing out the parent id by inspecting `parent` we just pass the instance
			 * to `Routing` which can infer the correct routing key based on the JoinField property on the instance
			 */
			var indexResponse = client.Index(parent, i => i.Routing(Routing.From(parent)));
			indexResponse.ApiCall.Uri.Query.Should().Contain("routing=1337");

			/**
			 * The same goes for when we index a child, we can pass the instance directly to `Routing` and NEST will use the parent id
			 * already specified on `child`. Here we use the static import `using static Nest.Infer` and it's `Route()` static method to
			 * create an instance of `Routing`
			 */
			indexResponse = client.Index(child, i => i.Routing(Route(child)));
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
		 * If you use multiple levels of parent and child relations e.g `A => B => C`, when you index `C`, you
		 * need to provide the id of `A` as the routing key *but* the id of `B` to set up the relation on the join field.
		 * In this case, NEST `JoinRouting` helper is unable to resolve to the id of `A` and will return the id of `B`.
		 *
		 */
	}
}
