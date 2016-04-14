namespace Nest
{
	/// <summary>
	/// If the license is valid but is older or has less capabilities this will list out the reasons why a resubmission with acknowledge=true is required
	/// </summary>
	public class LicenseAcknowledgement
	{
		public string Message { get; set; }
		public string[] License { get; set; }
	}
}
