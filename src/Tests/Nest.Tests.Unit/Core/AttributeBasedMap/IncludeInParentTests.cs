using NUnit.Framework;

namespace Nest.Tests.Unit.Core.AttributeBasedMap
{
	[TestFixture]
	public class IncludeInParentTests : BaseAttributeMappingTests
	{
		private class SimpleNestedMapParent
		{
			public string Name { get; set; }
			[ElasticProperty(Type = FieldType.Nested, IncludeInParent = true)]
			public SimpleNestedMapChild SimpleNestedMapChild { get; set; }
			[ElasticProperty(Type = FieldType.Nested)]
			public SimpleNestedMapChild SimpleNestedMapChildDontIncludeInParent { get; set; }
		}

		private class SimpleNestedMapChild
		{
			public string Name { get; set; }
		}

		[Test]
		public void TestIncludeInParent()
		{
			var json = this.CreateMapFor<SimpleNestedMapParent>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}