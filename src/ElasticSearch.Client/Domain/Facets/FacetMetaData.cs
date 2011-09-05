using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	/// <summary>
	/// Single facet is an abstract for facets that only return a single result (such as statistical)
	/// </summary>
	[JsonObject]
	public abstract class SingleFacet : Facet
	{

	}

}
