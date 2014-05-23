using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Allow404
{
	public static class ApiEndpointsThatAllow404 
	{
		public static IEnumerable<string> Endpoints = new List<string>
		{
			"DocumentExists",
			"Delete",
			"IndexExists"
		};
	}
}
