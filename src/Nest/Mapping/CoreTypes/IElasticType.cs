using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticType : IFieldMapping
	{
		PropertyName Name { get; set; }
		TypeName Type { get; }
	}
}
