using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SlackAttachmentField))]
	public interface ISlackAttachmentField
	{
		[DataMember(Name ="short")]
		bool? Short { get; set; }

		[DataMember(Name ="title")]
		string Title { get; set; }

		[DataMember(Name ="value")]
		string Value { get; set; }
	}

	public class SlackAttachmentField : ISlackAttachmentField
	{
		public bool? Short { get; set; }
		public string Title { get; set; }

		public string Value { get; set; }
	}

	public class SlackAttachmentFieldsDescriptor : DescriptorPromiseBase<SlackAttachmentFieldsDescriptor, IList<ISlackAttachmentField>>
	{
		public SlackAttachmentFieldsDescriptor() : base(new List<ISlackAttachmentField>()) { }

		public SlackAttachmentFieldsDescriptor Field(Func<SlackAttachmentFieldDescriptor, ISlackAttachmentField> selector) =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new SlackAttachmentFieldDescriptor())));
	}

	public class SlackAttachmentFieldDescriptor : DescriptorBase<SlackAttachmentFieldDescriptor, ISlackAttachmentField>, ISlackAttachmentField
	{
		bool? ISlackAttachmentField.Short { get; set; }
		string ISlackAttachmentField.Title { get; set; }

		string ISlackAttachmentField.Value { get; set; }

		public SlackAttachmentFieldDescriptor Title(string title) => Assign(a => a.Title = title);

		public SlackAttachmentFieldDescriptor Value(string value) => Assign(a => a.Value = value);

		public SlackAttachmentFieldDescriptor Short(bool? @short = true) => Assign(a => a.Short = @short);
	}
}
