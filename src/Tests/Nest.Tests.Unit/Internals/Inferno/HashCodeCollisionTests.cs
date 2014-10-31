using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class HashCodeCollisionTests : BaseJsonTests
	{
		[Test]
		public void PropertyNameMarkerEqualsDoesNotUseHashCode()
		{
			var collision = GetHashCollison();

			var propertyMarker1 = new PropertyNameMarker { Name = collision.Item1 };
			var propertyMarker2 = new PropertyNameMarker { Name = collision.Item2 };
			this.TestAddingToDictionary(propertyMarker1, propertyMarker2);
		}

		[Test]
		public void PropertyPathMarkerEqualsDoesNotUseHashCode()
		{
			var collision = GetHashCollison();

			var propertyMarker1 = new PropertyPathMarker { Name = collision.Item1 };
			var propertyMarker2 = new PropertyPathMarker { Name = collision.Item2 };
			this.TestAddingToDictionary(propertyMarker1, propertyMarker2);
		}

		[Test]
		public void IndexNameMarkerEqualsDoesNotUseHashCode()
		{
			var collision = GetHashCollison();

			var indexMarker1 = new IndexNameMarker { Name = collision.Item1 };
			var indexMarker2 = new IndexNameMarker { Name = collision.Item2 };
			this.TestAddingToDictionary(indexMarker1, indexMarker2);
		}

		[Test]
		public void TypeNameMarkerEqualsDoesNotUseHashCode()
		{
			var collision = GetHashCollison();

			var typeNameMarker1 = new TypeNameMarker { Name = collision.Item1 };
			var typeNameMarker2 = new TypeNameMarker { Name = collision.Item2 };
			this.TestAddingToDictionary(typeNameMarker1, typeNameMarker2);
		}

		public void TestAddingToDictionary<T>(T first, T second)
		{
			first.GetHashCode().Should().Be(second.GetHashCode());

			var dict = new Dictionary<T, bool> {{first, true}};
			Assert.DoesNotThrow(() => dict.Add(second, true));
		
		}

		public Tuple<string, string> GetHashCollison()
		{
			var hashes = new Dictionary<int, string>();

			for (var x = 1; x < 10000; x++)
			{
				for (var i = 0; i < 1000000; i++)
				{
					var guid = Guid.NewGuid().ToString();
					var hashCode = guid.GetHashCode();

					if (hashes.ContainsKey(hashCode) && hashes[hashCode] != guid)
						return Tuple.Create(guid, hashes[hashCode]);

					hashes.Add(hashCode, guid);
				}
			}
			Assert.Fail("no collision found during test run");
			return null;
		}



	}
}
