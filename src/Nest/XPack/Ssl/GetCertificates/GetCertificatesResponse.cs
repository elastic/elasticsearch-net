using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetCertificatesResponse : IResponse
	{
		[JsonIgnore]
		IReadOnlyCollection<ClusterCertificateInformation> Certificates { get; }

	}

	public class GetCertificatesResponse : ResponseBase, IGetCertificatesResponse
	{
		public IReadOnlyCollection<ClusterCertificateInformation> Certificates { get; internal set; } =
			EmptyReadOnly<ClusterCertificateInformation>.Collection;
	}

	public class ClusterCertificateInformation
	{

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("alias")]
		public string Alias { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }

		[JsonProperty("subject_dn")]
		public string SubjectDomainName { get; set; }

		[JsonProperty("serial_number")]
		public string SerialNumber { get; set; }

		[JsonProperty("has_private_key")]
		public bool HasPrivateKey { get; set; }

		[JsonProperty("expiry")]
		public DateTimeOffset Expiry { get; set; }
	}
}
