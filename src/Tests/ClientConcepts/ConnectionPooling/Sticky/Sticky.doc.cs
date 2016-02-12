using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.Sticky
{
    public class Sticky
    {
        protected int NumberOfNodes = 10;

        [U] public void EachViewStartsAtNexPositionAndWrapsOver()
        {
            var uris = Enumerable.Range(9200, NumberOfNodes).Select(p => new Uri("http://localhost:" + p));
            var staticPool = new StickyConnectionPool(uris);

            this.AssertCreateView(staticPool);
        }

        public void AssertCreateView(StickyConnectionPool pool)
        {
            var startingPositions = Enumerable.Range(0, NumberOfNodes)
                .Select(i => pool.CreateView().First())
                .Select(n => n.Uri.Port)
                .ToList();

            var expectedOrder = Enumerable.Range(9200, NumberOfNodes);
            startingPositions.Should().ContainInOrder(expectedOrder);
        }
    }
}
