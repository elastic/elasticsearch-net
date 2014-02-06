using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	internal partial class RawDispatch 
	{
		protected IRawElasticClient Raw { get; set; }

		public RawDispatch(IRawElasticClient rawElasticClient)
		{
			this.Raw = rawElasticClient;
		}
	}
}
