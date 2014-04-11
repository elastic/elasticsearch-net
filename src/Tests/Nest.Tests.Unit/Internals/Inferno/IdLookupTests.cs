using System;
using System.Globalization;
using NUnit.Framework;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class IdLookupTests
	{
		[ElasticType(IdProperty = "Guid")]
		internal class AlternateIdClass
		{
			public Guid Guid { get; set; }
		}
		internal class LongIdClass { public long Id { get; set; } }
		internal class DoubleIdClass { public double Id { get; set; } }
		internal class IntIdClass { public int Id { get; set; } }
		internal class DecimalIdClass { public decimal Id { get; set; } }
		internal class FloatIdClass { public float Id { get; set; } }
		internal class CustomObjectIdClass { public MyCustomClass Id { get; set; } }
		internal class MyCustomClass
		{
			public override string ToString()
			{
				return "static id ftw";
			}
		}

		internal class BaseIdClass { public int Id { get; set; } }

		internal class InheritedIdClass : BaseIdClass { public string Name { get; set; } }

		[Test]
		public void TestAlternateIdLookup()
		{
			var expectedGuid = Guid.NewGuid();
			var id = new IdResolver().GetIdFor(new AlternateIdClass { Guid = expectedGuid });
			StringAssert.AreEqualIgnoringCase(expectedGuid.ToString(), id);
		}

		[Test]
		public void TestIntLookup()
		{
			var expected = 12;
			var id = new IdResolver().GetIdFor(new IntIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(CultureInfo.InvariantCulture), id);
		}
		[Test]
		public void TestDecimalLookup()
		{
			var expected = 12m;
			var id = new IdResolver().GetIdFor(new DecimalIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(CultureInfo.InvariantCulture), id);
		}
		[Test]
		public void TestFloatLookup()
		{
			var expected = 12f;
			var id = new IdResolver().GetIdFor(new FloatIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(CultureInfo.InvariantCulture), id);
		}
		[Test]
		public void TestLongLookup()
		{
			var expected = long.MaxValue;
			var id = new IdResolver().GetIdFor(new LongIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(CultureInfo.InvariantCulture), id);
		}
		
		[Test]
		public void TestDoubleLookup()
		{
			var expected = 12d;
			var id = new IdResolver().GetIdFor(new DoubleIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(CultureInfo.InvariantCulture), id);
		}
		[Test]
		public void TestCustomLookup()
		{
			var expected = new MyCustomClass();
			var id = new IdResolver().GetIdFor(new CustomObjectIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(), id);
		}

		[Test]
		public void TestInheritedLookup()
		{
			var expected = new InheritedIdClass() { Id = 123 };
			var id = new IdResolver().GetIdFor(expected);
			id = new IdResolver().GetIdFor(expected);
			Assert.AreEqual(expected.Id.ToString(), id);
		}

		[Test]
		[Ignore]
		public void TestHitsCache()
		{
			var expected = 12m;
			var id = new IdResolver().GetIdFor(new DecimalIdClass { Id = expected });
			id = new IdResolver().GetIdFor(new DecimalIdClass { Id = expected });
			StringAssert.AreEqualIgnoringCase(expected.ToString(), id);
		}
	}
}
