using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlackAttachmentField>))]
	public interface ISlackAttachmentField
	{
		[JsonProperty("short")]
		bool? Short { get; set; }

		[JsonProperty("title")]
		string Title { get; set; }

		[JsonProperty("value")]
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

		public SlackAttachmentFieldDescriptor Short(bool? @short = true) => Assign(a => a.Short = @short);

		public SlackAttachmentFieldDescriptor Title(string title) => Assign(a => a.Title = title);

		public SlackAttachmentFieldDescriptor Value(string value) => Assign(a => a.Value = value);
	}
}
