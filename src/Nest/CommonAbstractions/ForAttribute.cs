using System;

namespace Nest
{
	/// <summary>
	/// Makes it explicit which API this request interface maps, the name of the interface informs
	/// The generator how to name related types
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface)]
	internal class MapsApiAttribute : Attribute
	{
		// ReSharper disable once UnusedParameter.Local
		public MapsApiAttribute(string restSpecName) { }
	}
}
