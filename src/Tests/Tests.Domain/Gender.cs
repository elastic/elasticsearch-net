using Nest;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Tests.Domain
{
	[StringEnum]
	public enum Gender
	{
		Male,
		Female,
		NoneOfYourBeeswax
	}
}
