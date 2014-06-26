using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce346Tests : IntegrationTests
	{
		public class MediaStreamEntry
		{
			public int Id { get; set; }
			public Guid StreamId { get; set; }
			public ApprovalSettings ApprovalSettings { get; set; }
		}
		public class ApprovalSettings
		{
			public bool Approved { get; set; }
		}


		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/346
		/// </summary>
		[Test]
		public void NoSearchResults()
		{
			//test teardown will delete defaultindex_* indices
			//process id makes it so we can run these tests concurrently using NCrunch
			var index = ElasticsearchConfiguration.DefaultIndex + "_posts_" + Process.GetCurrentProcess().Id.ToString();

			var client =  new ElasticClient(this.Settings, new InMemoryConnection(this.Settings));

			var streamId = new Guid("8d00cf65-bf84-4035-9adb-695b1366304c");
			var approved = true;

			var response = client.Count<MediaStreamEntry>(c=>c
				.Index("StreamEntry")
				.Query(x => x.Bool(
					b => b.Must
						(
							x.Term(f => f.StreamId, streamId.ToString())
							, x.Term(f => f.ApprovalSettings.Approved, approved)
						)
					)
				)
			);

			//Approval settings should not appear twice just because we are spawing the nested queries of wrong lambda parameter (x)
			var request = response.ConnectionStatus.Request.Utf8String();
			Assert.AreEqual(1, Regex.Matches(request, @"approvalSettings\.approved").Count, request);
			
			//either use the lambda overload
			response = client.Count<MediaStreamEntry>(c=>c
				.Index("StreamEntry")
				.Query(x => x.Bool(
					b => b.Must
						(
							m=> m.Term(f => f.StreamId, streamId.ToString())
							, m => m.Term(f => f.ApprovalSettings.Approved, approved)
						)
					)
				)
			);

			request = response.ConnectionStatus.Request.Utf8String();
			//now we only see the query once
			Assert.AreEqual(1, Regex.Matches(request, @"approvalSettings\.approved").Count);


			//or use the static Query<MediaStreamEntry>
			response = client.Count<MediaStreamEntry>(c=>c
				.Index("StreamEntry")
				.Query(x => x.Bool(
					b => b.Must
						(
							Query<MediaStreamEntry>.Term(f => f.StreamId, streamId)
							, Query<MediaStreamEntry>.Term(f => f.ApprovalSettings.Approved, approved)
						)
					)
				)
			);

			request = response.ConnectionStatus.Request.Utf8String();
			//now we still only see the query once
			Assert.AreEqual(1, Regex.Matches(request, @"approvalSettings\.approved").Count);
		}

	}
}
