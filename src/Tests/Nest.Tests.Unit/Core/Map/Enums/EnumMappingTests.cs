using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Core.Map.Enums
{
	[TestFixture]
	public class EnumMappingTests : BaseJsonTests
	{
		private class MyClass
		{
			public MyEnum MyEnum { get; set; }
		}

		private enum MyEnum
		{
			Value1,
			Value2
		}

		[Test]
		public void EnumShouldMapToIntByDefault()
		{
			var result = this._client.Map<MyClass>(m => m.MapFromAttributes());
			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void EnumCanBeOverriddenAfterMapFromAttributes()
		{
			var result = this._client.Map<MyClass>(m => m
				.MapFromAttributes()
				.Properties(props=>props
					.String(s=>s
						.Name(p=>p.MyEnum)
						.Index(FieldIndexOption.NotAnalyzed)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod());
		}

	}

}
