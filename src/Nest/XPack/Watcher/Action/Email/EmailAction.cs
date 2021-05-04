// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IEmailAction : IAction
	{
		[DataMember(Name = "account")]
		string Account { get; set; }

		[DataMember(Name = "attachments")]
		IEmailAttachments Attachments { get; set; }

		[DataMember(Name = "bcc")]
		IEnumerable<string> Bcc { get; set; }

		[DataMember(Name = "body")]
		IEmailBody Body { get; set; }

		[DataMember(Name = "cc")]
		IEnumerable<string> Cc { get; set; }

		[DataMember(Name = "from")]
		string From { get; set; }

		[DataMember(Name = "priority")]
		EmailPriority? Priority { get; set; }

		[DataMember(Name = "reply_to")]
		IEnumerable<string> ReplyTo { get; set; }

		[DataMember(Name = "subject")]
		string Subject { get; set; }

		[DataMember(Name = "to")]
		IEnumerable<string> To { get; set; }
	}

	public class EmailAction : ActionBase, IEmailAction
	{
		public EmailAction(string name) : base(name) { }

		public string Account { get; set; }
		public override ActionType ActionType => ActionType.Email;

		public IEmailAttachments Attachments { get; set; }

		public IEnumerable<string> Bcc { get; set; }

		public IEmailBody Body { get; set; }

		public IEnumerable<string> Cc { get; set; }

		public string From { get; set; }

		public EmailPriority? Priority { get; set; }

		public IEnumerable<string> ReplyTo { get; set; }

		public string Subject { get; set; }

		public IEnumerable<string> To { get; set; }
	}

	public class EmailActionDescriptor : ActionsDescriptorBase<EmailActionDescriptor, IEmailAction>, IEmailAction
	{
		public EmailActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.Email;

		string IEmailAction.Account { get; set; }
		IEmailAttachments IEmailAction.Attachments { get; set; }
		IEnumerable<string> IEmailAction.Bcc { get; set; }
		IEmailBody IEmailAction.Body { get; set; }
		IEnumerable<string> IEmailAction.Cc { get; set; }
		string IEmailAction.From { get; set; }
		EmailPriority? IEmailAction.Priority { get; set; }
		IEnumerable<string> IEmailAction.ReplyTo { get; set; }
		string IEmailAction.Subject { get; set; }
		IEnumerable<string> IEmailAction.To { get; set; }

		public EmailActionDescriptor Account(string account) => Assign(account, (a, v) => a.Account = v);

		public EmailActionDescriptor From(string from) => Assign(from, (a, v) => a.From = v);

		public EmailActionDescriptor To(IEnumerable<string> to) => Assign(to, (a, v) => a.To = v);

		public EmailActionDescriptor To(params string[] to) => Assign(to, (a, v) => a.To = v);

		public EmailActionDescriptor Cc(IEnumerable<string> cc) => Assign(cc, (a, v) => a.Cc = v);

		public EmailActionDescriptor Cc(params string[] cc) => Assign(cc, (a, v) => a.Cc = v);

		public EmailActionDescriptor Bcc(IEnumerable<string> bcc) => Assign(bcc, (a, v) => a.Bcc = v);

		public EmailActionDescriptor Bcc(params string[] bcc) => Assign(bcc, (a, v) => a.Bcc = v);

		public EmailActionDescriptor ReplyTo(IEnumerable<string> replyTo) => Assign(replyTo, (a, v) => a.ReplyTo = v);

		public EmailActionDescriptor ReplyTo(params string[] replyTo) => Assign(replyTo, (a, v) => a.ReplyTo = v);

		public EmailActionDescriptor Subject(string subject) => Assign(subject, (a, v) => a.Subject = v);

		public EmailActionDescriptor Body(Func<EmailBodyDescriptor, IEmailBody> selector) =>
			Assign(selector.InvokeOrDefault(new EmailBodyDescriptor()), (a, v) => a.Body = v);

		public EmailActionDescriptor Priority(EmailPriority? priority) => Assign(priority, (a, v) => a.Priority = v);

		public EmailActionDescriptor Attachments(Func<EmailAttachmentsDescriptor, IPromise<IEmailAttachments>> selector) =>
			Assign(selector, (a, v) => a.Attachments = v?.Invoke(new EmailAttachmentsDescriptor())?.Value);
	}
}
