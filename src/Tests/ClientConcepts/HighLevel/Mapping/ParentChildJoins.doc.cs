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
	/**[[multi-fields]]
	* === Parent Child joins using the join mapping
	*
	* Prior to Elasticsearch 6.x you could have multiple types in a single index. Through the special _parent field mapping of a given type
	* one could create 1 to N relationship of parent => children documents. This worked because when indexing children you passed a
	* `_parent` id which would act as the routing key making sure a parent and its (grand)children all lived on the same shard.
	*
	* Starting with 6.x indices you may no longer have multiple types in a single index. One reason for this is that if for instance
	* two types * have the same `name` property they need to be mapped exactly the same but all the API's acted as if you could map
	* them individually which often lead to confusion.
	*
	* So how do you create a parent join now that indices no longer allow you store different types in the same index and therefor also
	* not on the same shard?
	*
	*/

	public class ParentChildJoins : DocumentationTestBase
	{
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
		* ==== Default mapping for String properties
		*
		* When using <<auto-map, Auto Mapping>>, the inferred mapping for a `string`
		* POCO type is a `text` datatype with multi fields including a `keyword` sub field
		*/
		[U]
		public void SimpleParentChildMapping()
		{
			var descriptor = new CreateIndexDescriptor(Index<MyDocument>())
				.Mappings(ms => ms
					.Map<MyDocument>(m => m
						.AutoMap<MyParent>() // <1> Map all of the MyParent properties
						.AutoMap<MyChild>() // <2> Map all of the MyChild properties
						.Properties(props => props
							.Join(j => j // <3> Automap does not automatically translate `JoinField` since we are only allowed to have one.
								.Name(p=>p.MyJoinField)
								.Relations(r => r
									.Join<MyDocument, MyChild>()
								)
							)
						)
					)
				);

			/**
			 */
			//json
			var expected = new
			{
				mappings = new
				{
					mydocument = new
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
									mydocument = "mychild"
								}
							}
						}
					}
				}
			};

			//hide
			Expect(expected).WhenSerializing((ICreateIndexRequest) descriptor);
		}

		/**
		 * Now that we have our join field set up we'll index our two subclasses under the same type `mydocument`
		 */

		[U]
		public void Indexing()
		{
			// hide
			var client = TestClient.GetInMemoryClient(c => c.DisableDirectStreaming().PrettyJson());
			var parentDocument = new MyParent
			{
				Id = 1,
				ParentProperty = "a parent prop",
				MyJoinField = JoinField.Root<MyParent>() // <1> this lets the join data type know this is a root document of type `myparent`
			};
			var indexParent = client.Index<MyDocument>(parentDocument);
			//json
			var expected = new
			{
				id = 1,
				parentProperty = "a parent prop",
				myJoinField = "myparent"
			};
			Expect(expected).FromRequest(indexParent);

			/**
			* Note: you can simply implicitly convert from string to indicate the root name for the join field
			*/
			JoinField parentJoinField = "aparent";

			/**
			 * Linking the child document to its parent follows a similar pattern.
			 * Here we create a link by inferring the id from our parent instance `parentDocument`
			 */
			var indexChild = client.Index<MyDocument>(new MyChild
			{
				MyJoinField = JoinField.Link<MyChild, MyParent>(parentDocument)
			});
			/**
			 * or here we are simply stating this document is of type `mychild` and should be linked
			 * to parent id 1 from `parentDocument`.
			 */
			indexChild = client.Index<MyDocument>(new MyChild
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
			Expect("myparent").WhenSerializing(JoinField.Root(Type<MyParent>()));
			Expect("myparent").WhenSerializing(JoinField.Root<MyParent>());
			Expect("myparent").WhenSerializing(JoinField.Root("myparent"));

			var childLink = new {name = "mychild", parent = "1"};
			Expect(childLink).WhenSerializing(JoinField.Link<MyChild>(1));
			Expect(childLink).WhenSerializing(JoinField.Link<MyChild, MyParent>(parentDocument));
			Expect(childLink).WhenSerializing(JoinField.Link("mychild", 1));
			Expect(childLink).WhenSerializing(JoinField.Link(typeof(MyChild), 1));

		}
	}
}
