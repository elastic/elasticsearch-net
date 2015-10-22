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

namespace Tests.ClientConcepts.HighLevel.Inferrence.DocumentPaths
{
	public class DocumentPaths
	{
		/** # DocumentPaths
		 * Many API's in elasticsearch describe a path to a document. In NEST besides generating a constructor that takes
		 * and Index, Type and Id seperately we also generate a constructor taking a DocumentPath that allows you to describe the path
		 * to your document more succintly 
		 */

		/** Manually newing */
		[U] public void FromId()
		{
			/** here we create a new document path based on Project with the id 1 */
			IDocumentPath path = new DocumentPath<Project>(1);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("1").WhenSerializing(path.Id);

			/** You can still override the inferred index and type name*/
			path = new DocumentPath<Project>(1).Type("project1");
			Expect("project1").WhenSerializing(path.Type);

			path = new DocumentPath<Project>(1).Index("project1");
			Expect("project1").WhenSerializing(path.Index);
			
			/** there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(1); 
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("1").WhenSerializing(path.Id);
		}

		//** if you have an instance of your document you can use it as well generate document paths */
		[U] public void FromObject()
		{
			var project = new Project { Name = "hello-world" };

			/** here we create a new document path based on a Project */
			IDocumentPath path = new DocumentPath<Project>(project);
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("hello-world").WhenSerializing(path.Id);

			/** You can still override the inferred index and type name*/
			path = new DocumentPath<Project>(project).Type("project1");
			Expect("project1").WhenSerializing(path.Type);

			path = new DocumentPath<Project>(project).Index("project1");
			Expect("project1").WhenSerializing(path.Index);
			
			/** there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(project); 
			Expect("project").WhenSerializing(path.Index);
			Expect("project").WhenSerializing(path.Type);
			Expect("hello-world").WhenSerializing(path.Id);

			DocumentPath<Project> p = project;
		}

		[U] public void UsingWithRequests()
		{
			var project = new Project { Name = "hello-world" };

			/** Here we can see and example how DocumentPath helps your describe your requests more tersely */
			var request = new IndexRequest<Project>(2) { Document = project };
			request = new IndexRequest<Project>(project) { };
			
			/** when comparing with the full blown constructor and passing document manually 
			* DocumentPath&lt;T&gt;'s benefits become apparent. 
			*/
			request = new IndexRequest<Project>(IndexName.From<Project>(), TypeName.From<Project>(), 2)
			{
				Document = project
			};
		}
	}
}
