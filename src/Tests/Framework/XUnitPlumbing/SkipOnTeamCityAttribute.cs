using System;

namespace Tests.Framework
{
	public class SkipOnTeamCityAttribute : Attribute
	{
		public SkipOnTeamCityAttribute(string reason)
		{
		}
	}
}
