using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IQuery
	{
		bool IsConditionless { get; }
	}
}
