using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nest
{
	///<inheritdoc/>
	public class GatewaySettings 
	{
		///<inheritdoc/>
		public int? ExpectedNodes { get; internal set; }

		///<inheritdoc/>
		public int? ExpectedMasterNodes { get; internal set; }

		///<inheritdoc/>
		public int? ExpectedDataNodes { get; internal set; }

		///<inheritdoc/>
		public TimeUnitExpression RecoveryAfterTime { get; internal set; }

	}
}