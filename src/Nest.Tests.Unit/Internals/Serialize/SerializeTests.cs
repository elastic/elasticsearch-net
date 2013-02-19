using System.Reflection;
using NUnit.Framework;

namespace Nest.Tests.Unit.Internals.Serialize
{
	[TestFixture]
	public class SerializeTests : BaseJsonTests
	{
		public class SimpleClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		[Test]
		public void SimpleClassUsesCamelCase()
		{
			var simpleClass = new SimpleClass { Id = 2, Name = "X" };
			var json = this._client.SerializeCamelCase(simpleClass);
			this.JsonEquals(json, MethodInfo.GetCurrentMethod());
		}
	}
}
