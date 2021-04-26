/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class DocumentPaths
	{
		/**[[document-paths]]
		 * === Document paths
		 *
		 * Many APIs in Elasticsearch describe a path to a document. In NEST, besides generating a constructor that takes
		 * an Index and Id separately, we also generate a constructor that allows you to describe the path
		 * to your document more succinctly using a an instance of the `DocumentPath<T>` type.
		 */

		/**
		 * ==== Creating new instances */
		[U] public void FromId()
		{
			/** here we create a new document path based on Project with the id 1 */
			IDocumentPath path = new DocumentPath<Project>(1);
			Expect("project").WhenSerializing(path.Index);
			Expect(1).WhenSerializing(path.Id);

			/** You can still override the inferred index name*/
			path = new DocumentPath<Project>(1).Index("project1");
			Expect("project1").WhenSerializing(path.Index);

			/** and there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(1);
			Expect("project").WhenSerializing(path.Index);
			Expect(1).WhenSerializing(path.Id);
		}

		/**
		 * ==== Creating from a document type instance
		 * if you have an instance of your document you can use it as well generate document paths
		 */
		[U] public void FromObject()
		{
			var project = new Project { Name = "hello-world" };

			/** here we create a new document path based on the instance of `Project`, project */
			IDocumentPath path = new DocumentPath<Project>(project);
			Expect("project").WhenSerializing(path.Index);
			Expect("hello-world").WhenSerializing(path.Id);

			/** You can still override the inferred index name*/
			path = new DocumentPath<Project>(project).Index("project1");
			Expect("project1").WhenSerializing(path.Index);

			/** and again, there is also a static way to describe such paths */
			path = DocumentPath<Project>.Id(project);
			Expect("project").WhenSerializing(path.Index);

			DocumentPath<Project> p = project;
		}

		/**
		 * ==== An example with requests */
		[U] public void UsingWithRequests()
		{
			/* Given the following CLR type that describes a document */
			var project = new Project { Name = "hello-world" };

			/** we can see an example of how `DocumentPath` helps your describe your requests more tersely */
			var request = new IndexRequest<Project>(2) { Document = project };
			request = new IndexRequest<Project>(project);

			/** when comparing with the full blown constructor and passing document manually,
			* `DocumentPath<T>`'s benefits become apparent. Compare the following request that doesn't
			* use `DocumentPath<T>` with the former examples
			*/
			request = new IndexRequest<Project>(IndexName.From<Project>(), 2)
			{
				Document = project
			};
		}
		/**
		 * Much more verbose, wouldn't you agree?
		 */
	}
}
