using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMatchAllFilter : IFilterBase
	{
	}

	public class MatchAllFilter : FilterBase, IMatchAllFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return false;
			}

		}
	}
}
