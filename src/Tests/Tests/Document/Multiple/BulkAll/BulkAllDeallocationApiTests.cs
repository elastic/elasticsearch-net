using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.BulkAll
{
	public class BulkAllDeallocationApiTests : BulkAllApiTestsBase
	{
		public BulkAllDeallocationApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		[I] public void ForEachAsyncReleasesProcessedItemsInMemory()
		{
			WeakReference<SmallObject> deallocReference = null;
			SmallObject obj = null;

			var lazyCollection = this.GetLazyCollection(
				weakRef => deallocReference = weakRef,
				delegate { },//...
				delegate { },//Making sure that all of the objects have gone through pipeline
				delegate { },//so that the first one can be deallocated
				delegate { },//Various GC roots prevent several of previous (2 or 3)
				delegate { },//items in the lazy Enumerable from deallocation during forced GC
				delegate { },//...
				delegate {
					GC.Collect(2, GCCollectionMode.Forced, true);
					deallocReference.TryGetTarget(out obj);
				}
			);

			var index = CreateIndexName();
			var observableBulk = this.Client.BulkAll(lazyCollection, f => f
				.MaxDegreeOfParallelism(1)
				.Size(1)
				.Index(index)
				.BufferToBulk((r, buffer) => r.IndexMany(buffer)));

			observableBulk.Wait(TimeSpan.FromSeconds(30), delegate { });

			deallocReference.Should().NotBeNull();
			obj.Should().BeNull();
		}

		private IEnumerable<SmallObject> GetLazyCollection(params Action<WeakReference<SmallObject>>[] getFirstObjectCallBack)
		{
			var counter = 0;
			foreach (var callback in getFirstObjectCallBack)
			{
				var obj = new SmallObject { Id = ++counter };
				callback(new WeakReference<SmallObject>(obj));
				yield return obj;
			}
		}
	}
}