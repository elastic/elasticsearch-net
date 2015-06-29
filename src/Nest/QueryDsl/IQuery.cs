using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IQuery
	{
		/// <summary>
		/// The _name of the query. this allows you to retrieve for each document what part of the query it matched on
		/// </summary>
		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }
		bool Conditionless { get; }
	}
}
