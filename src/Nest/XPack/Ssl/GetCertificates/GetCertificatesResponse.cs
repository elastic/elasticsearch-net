// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Nest.Utf8Json;
namespace Nest
{
	[JsonFormatter(typeof(GetCertificatesResponseFormatter))]
	public class GetCertificatesResponse : ResponseBase
	{
		[IgnoreDataMember]
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

	internal class GetCertificatesResponseFormatter : IJsonFormatter<GetCertificatesResponse>
	{
		private static readonly ReadOnlyCollectionFormatter<ClusterCertificateInformation> Formatter =
			new ReadOnlyCollectionFormatter<ClusterCertificateInformation>();

		public void Serialize(ref JsonWriter writer, GetCertificatesResponse value, IJsonFormatterResolver formatterResolver) =>
			throw new NotImplementedException();

		public GetCertificatesResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var response = new GetCertificatesResponse();

			if (reader.ReadIsNull())
				return response;

			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.BeginArray:
					response.Certificates = Formatter.Deserialize(ref reader, formatterResolver);
					break;
				case JsonToken.BeginObject:
					var count = 0;
					while (reader.ReadIsInObject(ref count))
					{
						var property = reader.ReadPropertyNameSegmentRaw();
						if (ResponseFormatterHelpers.ServerErrorFields.TryGetValue(property, out var errorValue))
						{
							switch (errorValue)
							{
								case 0:
									if (reader.GetCurrentJsonToken() == JsonToken.String)
										response.Error = new Error { Reason = reader.ReadString() };
									else
									{
										var formatter = formatterResolver.GetFormatter<Error>();
										response.Error = formatter.Deserialize(ref reader, formatterResolver);
									}
									break;
								case 1:
									if (reader.GetCurrentJsonToken() == JsonToken.Number)
										response.StatusCode = reader.ReadInt32();
									else
										reader.ReadNextBlock();
									break;
							}
						}
						else
							reader.ReadNextBlock();
					}
					break;
			}

			return response;
		}
	}
}
