using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IQuery
	{
		/// <summary>
		/// The _name of the query. this allows you to retrieve for each document what part of the query it matched on
		/// </summary>
		string Name { get; set; }
		bool IsConditionless { get; }
	}
}
