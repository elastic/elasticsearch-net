using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Allow404
{
	public static class ApiEndpointsThatAllow404 
	{
		public static IEnumerable<string> Endpoints = new List<string>
		{
			"DocumentExists",
			"Delete",
			"IndexExists",
			"SearchExists",
			"AliasExists",
			"TemplateExists",
			"TypeExists",
			"Exists",
			"Get"
		};
	}
}
