/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
