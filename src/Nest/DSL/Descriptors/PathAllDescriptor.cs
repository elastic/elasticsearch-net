using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class PathAllDescriptor : PathDescriptor
	{
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public PathAllDescriptor AllIndices()
		{
			this._AllIndices = true;
			return this;
		}
		public PathAllDescriptor AllTypes()
		{
			this._AllTypes = true;
			return this;
		}
	}
}
