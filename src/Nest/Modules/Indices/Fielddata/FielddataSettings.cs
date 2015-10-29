using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nest
{
	///<inheritdoc/>
	public class FielddataSettings 
	{
		///<inheritdoc/>
		public string CacheSize { get; internal set; }

		///<inheritdoc/>
		public TimeUnitExpression CacheExpire { get; internal set; }
	}
}