// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DataAttachment))]
	public interface IDataAttachment : IEmailAttachment
	{
		[DataMember(Name = "format")]
		DataAttachmentFormat? Format { get; set; }
	}

	public class DataAttachment : IDataAttachment
	{
		public DataAttachmentFormat? Format { get; set; }
	}

	public class DataAttachmentDescriptor : DescriptorBase<DataAttachmentDescriptor, IDataAttachment>, IDataAttachment
	{
		DataAttachmentFormat? IDataAttachment.Format { get; set; }

		public DataAttachmentDescriptor Format(DataAttachmentFormat? format) => Assign(format, (a, v) => a.Format = v);
	}

	[StringEnum]
	public enum DataAttachmentFormat
	{
		[EnumMember(Value = "json")]
		Json,

		[EnumMember(Value = "yaml")]
		Yaml
	}
}
