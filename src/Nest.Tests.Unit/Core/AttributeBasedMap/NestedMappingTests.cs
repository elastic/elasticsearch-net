using System;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.AttributeBasedMap
{
	[TestFixture]
	public class NestedMappingTests : BaseAttributeMappingTests
	{
		private class NestedMapParent
		{
			public string Name { get; set; }
			
            [ElasticProperty(Type=FieldType.nested)]
			public NestedMapChild Child { get; set; }
		}

		private class NestedMapChild
		{
			public string Name { get; set; }
		}

        private class NestedParentManyChildren
        {
            public string Name { get; set; }

            [ElasticProperty(Type = FieldType.nested)]
            public NestedMapChildren[] Children { get; set; }
        }

	    private class NestedMapChildren
        {
            [ElasticProperty(Index = FieldIndexOption.not_analyzed)]
            public string Name { get; set; }
        }

		[Test]
		public void TestNestedWriter()
		{ 
			var json = this.CreateMapFor<NestedMapParent>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}

        [Test]
        public void TestNestedWriter_ChildrenCollection()
        {
            var json = this.CreateMapFor<NestedParentManyChildren>();
            Console.WriteLine(json);
            this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
        }
	}
}
