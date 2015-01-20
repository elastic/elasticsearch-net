using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Nest.Tests.Unit.Search.Aggregations
{
    [TestFixture]
    public class SingleBucketAggregationTests
    {
        [Test]
        public void CanSetInnerAggregationsInConstructor()
        {
            var bucket = new SingleBucket(new Dictionary<string, IAggregation> { 
                {"some_agg", new KeyItem { Key = "agg_key", DocCount = 123 }}
            });

            bucket.Aggregations.Should().ContainKey("some_agg");

            var item = (KeyItem) bucket.Aggregations["some_agg"];
            item.DocCount.Should().Be(123);
            item.Key.Should().Be("agg_key");

        }
    }
}
