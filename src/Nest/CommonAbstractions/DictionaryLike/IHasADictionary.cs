using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IHasADictionary
	{
		[JsonIgnore]
		IDictionary Dictionary { get; }
	}
}