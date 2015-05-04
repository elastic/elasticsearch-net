using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class InnerHitsTests : IntegrationTests
	{
		private readonly string _indexName;

		public InnerHitsTests()
		{
			this._indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var create = this.Client.CreateIndex(this._indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<King>(m => m.MapFromAttributes())
				.AddMapping<Prince>(m => m.MapFromAttributes().SetParent<King>())
				.AddMapping<Duke>(m => m.MapFromAttributes().SetParent<Prince>())
				.AddMapping<Earl>(m => m.MapFromAttributes().SetParent<Duke>())
				.AddMapping<Baron>(m => m.MapFromAttributes().SetParent<Earl>())
			);

			var bulk = new BulkDescriptor();
			IndexAll(bulk, () => NestTestData.Session.List<King>(2).Get(), indexChildren: king =>
				IndexAll(bulk, () => NestTestData.Session.List<Prince>(2).Get(), king.Name, prince =>
					IndexAll(bulk, () => NestTestData.Session.List<Duke>(3).Get(), prince.Name, duke =>
						IndexAll(bulk, () => NestTestData.Session.List<Earl>(5).Get(), duke.Name, earl =>
							IndexAll(bulk, () => NestTestData.Session.List<Baron>(1).Get(), earl.Name)
						)
					)
				)
			);
			var bulkResult = this.Client.Bulk(b => bulk);
			this.Client.Refresh(r => r.Index(this._indexName));
		}


		private void IndexAll<TRoyal>(BulkDescriptor bulk, Func<IList<TRoyal>> create, string parent = null, Action<TRoyal> indexChildren = null)
			where TRoyal : class, IRoyal
		{
			var current = create();
			//looping twice horrible but easy to debug :)
			foreach (var royal in current)
			{
				var royal1 = royal;
				bulk.Index<TRoyal>(i => i.Document(royal1).Index(this._indexName).Parent(parent));
			}
			if (indexChildren == null) return;
			foreach (var royal in current)
				indexChildren(royal);
		}

		[Test]
		[SkipVersion("0 - 1.4.9", "Inner hits introduced in 1.5")]
		public void HasParent()
		{
			var results = this.Client.Search<Prince>(s => s
				.Index(this._indexName)
				.Query(q => q
					.HasParent<King>(hp => hp
						.Query(qq => qq.MatchAll())
						.InnerHits()
					)
				)
			);

			results.IsValid.Should().BeTrue();
			results.Hits.Should().NotBeEmpty();
			foreach(var hit in results.Hits)
			{
				var kings = hit.InnerHits["king"].Documents<King>();
				kings.Should().NotBeEmpty();
			}
		}

		[Test]
		[SkipVersion("0 - 1.4.9", "Inner hits introduced in 1.5")]
		public void HasChild()
		{
			var results = this.Client.Search<King>(s => s
				.Index(this._indexName)
				.Query(q => q
					.Filtered(f => f
						.Filter(ff => ff
							.HasChild<Prince>(hc => hc
								.Query(qq => qq.MatchAll())
									.InnerHits(ih => ih
									.Name("princes")
								)
							)
						)
					)
				)
			);

			results.IsValid.Should().BeTrue();
			results.Hits.Should().NotBeEmpty();
			foreach (var hit in results.Hits)
			{
				var princes = hit.InnerHits["princes"].Documents<Prince>();
				princes.Should().NotBeEmpty();
			};
		}

		[Test]
		[SkipVersion("0 - 1.4.9", "Inner hits introduced in 1.5")]
		public void Search()
		{
			var results = this.Client.Search<Duke>(s => s
				.Index(this._indexName)
				.InnerHits(innerHits => innerHits
					.Add("earls", i => i
						.Type<Earl>(ii => ii
							.Size(5)
							.InnerHits(innerInnerHits => innerInnerHits
								.Add("barons", iii => iii.Type<Baron>())
							)
						)
					)
					.Add("princes", i => i.Type<Prince>())
				)
			);
			results.IsValid.Should().BeTrue();
			results.Hits.Should().NotBeEmpty();
			foreach (var hit in results.Hits)
			{
				hit.InnerHits.Should().NotBeEmpty();
				hit.InnerHits.Should().ContainKey("earls");
				var earlHits = hit.InnerHits["earls"].Hits;
				earlHits.Total.Should().BeGreaterThan(0);
				earlHits.Hits.Should().NotBeEmpty().And.HaveCount(5);
				var earls = earlHits.Documents<Earl>();
				earls.Should().NotBeEmpty().And.OnlyContain(earl => !earl.Name.IsNullOrEmpty());
				foreach (var earlHit in earlHits.Hits)
				{
					var baronHits = earlHit.InnerHits["barons"];
					baronHits.Should().NotBeNull();
					var baron = baronHits.Documents<Baron>().FirstOrDefault();
					baron.Should().NotBeNull();
					baron.Name.Should().StartWith("baron");
				}
			}

		}

	}
}