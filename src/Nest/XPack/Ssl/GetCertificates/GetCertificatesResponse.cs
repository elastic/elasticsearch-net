using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetCertificatesResponse : IResponse
	{
		[IgnoreDataMember]
		IReadOnlyCollection<ClusterCertificateInformation> Certificates { get; }

	}

	public class GetCertificatesResponse : ResponseBase, IGetCertificatesResponse
	{
		public IReadOnlyCollection<ClusterCertificateInformation> Certificates { get; internal set; } =
			EmptyReadOnly<ClusterCertificateInformation>.Collection;
	}

	public class ClusterCertificateInformation
	{

		[DataMember(Name = "path")]
		public string Path { get; internal set; }

		[DataMember(Name = "alias")]
		public string Alias { get; internal set; }

		[DataMember(Name = "format")]
		public string Format { get; internal set; }

		[DataMember(Name = "subject_dn")]
		public string SubjectDomainName { get; internal set; }

		[DataMember(Name = "serial_number")]
		public string SerialNumber { get; internal set; }

		[DataMember(Name = "has_private_key")]
		public bool HasPrivateKey { get; internal set; }

		[DataMember(Name = "expiry")]
		public DateTimeOffset Expiry { get; internal set; }
	}
}
