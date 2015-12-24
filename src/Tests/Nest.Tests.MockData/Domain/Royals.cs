using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Tests.MockData.Domain
{
	public interface IRoyal { }

	[ElasticType(IdProperty = "Name")]
	public class King : IRoyal
	{
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Name { get; set; }
	}

	[ElasticType(IdProperty = "Name")]
	public class Prince : IRoyal
	{
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Name { get; set; }
	}

	[ElasticType(IdProperty = "Name")]
	public class Duke : IRoyal
	{
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Name { get; set; }
	}

	[ElasticType(IdProperty = "Name")]
	public class Earl : IRoyal
	{
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Name { get; set; }
	}

	[ElasticType(IdProperty = "Name")]
	public class Baron : IRoyal
	{
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Name { get; set; }
	}
}
