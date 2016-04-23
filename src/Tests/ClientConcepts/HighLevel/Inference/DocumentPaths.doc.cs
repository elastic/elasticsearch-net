using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class DocumentPaths
	{
		/**== Document Paths
		 *
		 * Many API's in Elasticsearch describe a path to a document. In NEST, besides generating a constructor that takes
		 * and Index, Type and Id separately, we also generate a constructor taking a `DocumentPath` that allows you to describe the path
		 * to your document more succinctly
		 */

		/** === Creating new instances */
		[U] public void FromId()
		{
			/** here we create a new document path based on Project with the id 1 */
			IDocumentPath path = new DocumentPath<Project>(1);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect(1).WhenSerializing(path.Id);

			/** You can still override the inferred index and type name*/
			path = new DocumentPath<Project>(1).Type("project1");
			Expect("project1").WhenSerializing(path.Type);

			path = new DocumentPath<Project>(1).Index("project1");
			Expect("project1").WhenSerializing(path.Index);

			/** and there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(1);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect(1).WhenSerializing(path.Id);
		}

		/** === Creating from a document type instance
		 * if you have an instance of your document you can use it as well generate document paths
		 */
		[U] public void FromObject()
		{
			var project = new Project { Name = "hello-world" };

			/** here we create a new document path based on the instance of `Project`, project */
			IDocumentPath path = new DocumentPath<Project>(project);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("hello-world").WhenSerializing(path.Id);

			/** You can still override the inferred index and type name*/
			path = new DocumentPath<Project>(project).Type("project1");
			Expect("project1").WhenSerializing(path.Type);

			path = new DocumentPath<Project>(project).Index("project1");
			Expect("project1").WhenSerializing(path.Index);

			/** and again, there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(project);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("hello-world").WhenSerializing(path.Id);

			DocumentPath<Project> p = project;
		}

		/** === An example with requests */
		[U] public void UsingWithRequests()
		{
			/* Given the following CLR type that describes a document */
			var project = new Project { Name = "hello-world" };

			/** we can see an example of how `DocumentPath` helps your describe your requests more tersely */
			var request = new IndexRequest<Project>(2) { Document = project };
			request = new IndexRequest<Project>(project) { };

			/** when comparing with the full blown constructor and passing document manually,
			* `DocumentPath<T>`'s benefits become apparent.
			*/
			request = new IndexRequest<Project>(IndexName.From<Project>(), TypeName.From<Project>(), 2)
			{
				Document = project
			};
		}
	}
}
