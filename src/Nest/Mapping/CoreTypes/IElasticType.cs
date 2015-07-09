using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IElasticType : IFieldMapping
	{
		FieldName Name { get; set; }
		TypeName Type { get; }
	}
}
