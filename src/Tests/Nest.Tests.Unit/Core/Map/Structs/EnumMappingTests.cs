using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Unit.Core.Map.Structs
{
	[TestFixture]
	public class StructMappingTests : BaseJsonTests
	{
		private class MyClass
		{
			public MyStruct Struct { get; set; }
		}

		private class MyClass2
		{
			[ElasticProperty(Analyzer = "default")]
			public MyStruct StructProperty { get; set; }
		}

		private struct MyStruct
		{
			public string Object { get; set; }
		}

		[Test]
		public void StructWithNoAttributeSetIsIgnored()
		{
			//unknow value types are not handled by default by MapFromAttributes()
			var result = this._client.Map<MyClass>(m => m.MapFromAttributes());
			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void StructWithAttributeButNoTypeInformationThrows()
		{
			//unknown value types with missing FieldType information in the attribute should throw an exception
			
			//example

			//Nest.DslException : Property Struct on type MyClass2 <continued>
			//has an ElasticProperty attribute but its FieldType (Type = ) can not be inferred <continued>
			//and is not set explicitly while calling MapFromAttributes

			var e = Assert.Throws<DslException>(() =>
			{
				var result = this._client.Map<MyClass2>(m => m.MapFromAttributes());
				this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod());
			});

			e.Message.Should().EndWith("while calling MapFromAttributes");
			e.Message.Should().Contain("StructProperty");
		}

	}

}
