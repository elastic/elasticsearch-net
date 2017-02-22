using System.Collections.Generic;

namespace Nest_5_2_0
{
	/// <summary>
	/// If the license is valid but is older or has less capabilities this will list out the reasons why a resubmission with acknowledge=true is required
	/// </summary>
	public class LicenseAcknowledgement
	{
		public string Message { get; set; }
		public IReadOnlyCollection<string> License { get; set; }
	}
}
