using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using NUnit.Framework.Constraints;

namespace Nest.Tests.Unit.Core.Update
{
	[TestFixture]
	public class UpdateAPIConsistencyTests : BaseJsonTests
	{
		public class MyDocument
		{
			public int Id { get; set; }
		
			public string Name { get; set; }

			public string Country { get; set; }
		}

		public class MyUpdate
		{
			public string Name { get; set; }
		}

		private MyDocument _document = new MyDocument
		{
			Id = 1,
			Name = "My document",
			Country = "Netherlands"
		};

		private MyUpdate _updateDocument = new MyUpdate { Name = "Martijn" };

		[Test]
		public void UpdateAPI_IdFromDocument()
		{
			this._client.Update<MyDocument, MyUpdate>(u => u
				.Id(_document)
				.PartialDocument(_updateDocument)
				.PartialDocumentAsUpsert()
			);
		}

		[Test]
		public void BulkUpdateAPI_IdFromDocument()
		{
			this._client.Bulk(u => u
				.Update<MyDocument, MyUpdate>(o=>o
					.Id(_document)
					.PartialDocument(_updateDocument)
					.PartialDocumentAsUpsert()
				)			
			);
		}
		
		[Test]
		public void UpdateAPI_IdFromDocument_PassToUpsert()
		{
			this._client.Update<MyDocument, MyUpdate>(u => u
				.Id(_document, useAsUpsert: true)
				.PartialDocument(_updateDocument)
			);
		}

		[Test]
		public void BulkUpdateAPI_IdFromDocument_PassToUpsert()
		{
			this._client.Bulk(u => u
				.Update<MyDocument, MyUpdate>(o=>o
					.Id(_document, useAsUpsert: true)
					.PartialDocument(_updateDocument)
				)			
			);
		}
		
		[Test]
		public void UpdateAPI_ManualId_WithUpsert()
		{
			this._client.Update<MyDocument, MyUpdate>(u => u
				.Id(_document.Id)
				.PartialDocument(_updateDocument)
				.Upsert(_document)
			);
		}

		[Test]
		public void BulkUpdateAPI_ManualId_WithUpsert()
		{
			this._client.Bulk(u => u
				.Update<MyDocument, MyUpdate>(o=>o
					.Id(_document.Id)
					.PartialDocument(_updateDocument)
					.Upsert(_document)
				)
			);
		}
	}
}
