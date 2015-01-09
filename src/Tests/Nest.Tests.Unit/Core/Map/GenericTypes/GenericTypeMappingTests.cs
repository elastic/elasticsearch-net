using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.GenericTypes
{
	[TestFixture]
	public class GenericTypeMappingTests : BaseJsonTests
	{
		public class GenericTypes
		{
			public IEnumerable<string> ListField { get; set; }
			public Tuple<string, int> TupleField { get; set; }
			public Nullable<int> NullableField { get; set; }
		}

		[Test]
		public void GenericTypeMapping()
		{
			var result = this._client.Map<GenericTypes>(m => m.MapFromAttributes());
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}

}
