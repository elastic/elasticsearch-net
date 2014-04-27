using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class ReproduceConnectionStallsTests : IntegrationTests
	{
		private readonly string _index;

		public ReproduceConnectionStallsTests()
		{
			this._index = ElasticsearchConfiguration.NewUniqueIndexName();
			var people = NestTestData.Session.List<Person>(10000).Get().ToList();
			var lotsOfPeople = Partition(people, 1000).ToList();
			var createIndexResult =  this._client.CreateIndex(_index, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.Settings(s=>s.Add("index.refresh_interval", -1))
				.AddMapping<Person>(m => m.MapFromAttributes())
			);
			Parallel.ForEach(lotsOfPeople, (listOfPeople) =>
			{
				_client.IndexMany(listOfPeople, _index);
			});
			_client.UpdateSettings(u=>u.Index(_index).RefreshInterval("1s"));
			_client.Refresh(r=>r.Index(_index));

		}


		[Test]
		public void SearchShouldNotHaveBlips()
		{
			//Search once so that caches are sure to exist.
			var timing = this.Search();


			var timings = new List<Timings>();
			for (var i = 0; i < 1000; i++)
			{
				timing = this.Search();
				timings.Add(timing);
			}

			timings.Should().OnlyContain(t => t.IsValid);

			var minElasticsearch = timings.Min(t => t.ElasticsearchTook);
			var minNest = timings.Min(t => t.NestTook);
				
			//elasticsearch is fast! min should absolutely be below 2ms
			minElasticsearch.Should().BeLessThan(10);
			
			//nest has some overhead, it should be wthin reason though
			minNest.Should().BeLessThan(20);


			var maxElasticsearch = timings.Max(t => t.ElasticsearchTook);
			var maxNest = timings.Max(t => t.NestTook);

			maxElasticsearch.Should().BeLessThan(20);
			maxNest.Should().BeLessThan(50);

			var plotElasticsearch = timings.Select(p => p.ElasticsearchTook).ToArray();
			var plotNest = timings.Select(p => p.NestTook).ToArray();

			plotElasticsearch.Should().NotBeEmpty();
			plotNest.Should().NotBeEmpty();

			var csv = string.Join("\n", timings.Select((t,i) => "{0}\t{1}\t{2}\t{3}".F(i, t.ElasticsearchTook, t.NestTook, t.IsValid)));
			Assert.Pass(csv);

		}

		private Timings Search()
		{
			int take = 100;
			var stopwatch = Stopwatch.StartNew();
			var search = this._client.Search<Person>(s => s.MatchAll().Size(take).Index(_index));
			stopwatch.Stop();
			return new Timings()
			{
				ElasticsearchTook = search.ElapsedMilliseconds,
				NestTook = stopwatch.ElapsedMilliseconds,
				IsValid = search.IsValid && search.Documents.Count() == take
				
			};
		}

		public class Timings
		{
			public long ElasticsearchTook { get; set; }
			public long NestTook { get; set; }
			public bool IsValid { get; set; }
		}

		
		public IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> source, int size)
		{
			T[] array = null;
			int count = 0;
			foreach (T item in source)
			{
				if (array == null)
				{
					array = new T[size];
				}
				array[count] = item;
				count++;
				if (count == size)
				{
					yield return new ReadOnlyCollection<T>(array);
					array = null;
					count = 0;
				}
			}
			if (array != null)
			{
				Array.Resize(ref array, count);
				yield return new ReadOnlyCollection<T>(array);
			}
		}
	}

	
}
