// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SlackAttachmentFieldDescriptor())));
	}

	public class SlackAttachmentFieldDescriptor : DescriptorBase<SlackAttachmentFieldDescriptor, ISlackAttachmentField>, ISlackAttachmentField
	{
		bool? ISlackAttachmentField.Short { get; set; }
		string ISlackAttachmentField.Title { get; set; }

		string ISlackAttachmentField.Value { get; set; }

		public SlackAttachmentFieldDescriptor Title(string title) => Assign(title, (a, v) => a.Title = v);

		public SlackAttachmentFieldDescriptor Value(string value) => Assign(value, (a, v) => a.Value = v);

		public SlackAttachmentFieldDescriptor Short(bool? @short = true) => Assign(@short, (a, v) => a.Short = v);
	}
}
