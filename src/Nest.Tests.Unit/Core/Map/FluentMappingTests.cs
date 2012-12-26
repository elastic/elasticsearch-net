using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class FluentMappingTests : BaseJsonTests
	{
		[Test]
		public void GetSimple()
		{
			var mapping = new TypeMappingDescriptor<ElasticSearchProject>()
				.MapFromAttributes()
				.TypeName("elasticsearchprojects2")
				.SourceEnabled(false);

		}
	}
}
