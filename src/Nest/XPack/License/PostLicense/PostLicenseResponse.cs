using System.Runtime.Serialization;

namespace Nest
{
	public class PostLicenseResponse : ResponseBase
	{
		/// <summary>
		/// If the license is valid but is older or has less capabilities this will list out the reasons why a resubmission with acknowledge=true is
		/// required.
		/// null if no acknowledge resubmission is needed
		/// </summary>
		[DataMember(Name ="acknowledge")]
		public LicenseAcknowledgement Acknowledge { get; internal set; }

		[DataMember(Name ="acknowledged")]
		public bool Acknowledged { get; internal set; }

		[DataMember(Name ="license_status")]
		public LicenseStatus LicenseStatus { get; internal set; }
	}
}
