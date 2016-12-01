using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A watch action that sends email notifications.
	/// To use the email action, you must configure at least one email account.
	/// </summary>
	[JsonObject]
	public interface IEmailAction : IAction
	{
		[JsonProperty("account")]
		string Account { get; set; }

		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("to")]
		IEnumerable<string> To { get; set; }

		[JsonProperty("cc")]
		IEnumerable<string> Cc { get; set; }

		[JsonProperty("bcc")]
		IEnumerable<string> Bcc { get; set; }

		[JsonProperty("reply_to")]
		IEnumerable<string> ReplyTo { get; set; }

		[JsonProperty("subject")]
		string Subject { get; set; }

		[JsonProperty("body")]
		IEmailBody Body { get; set; }

		[JsonProperty("priority")]
		EmailPriority? Priority { get; set; }

		[JsonProperty("attachments")]
		IEmailAttachments Attachments { get; set; }

		[JsonProperty("attach_data")]
		[Obsolete("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
		Union<bool?, AttachData> AttachData { get; set; }
	}

	/// <summary>
	/// A watch action that sends email notifications.
	/// To use the email action, you must configure at least one email account.
	/// </summary>
	public class EmailAction : ActionBase, IEmailAction
	{
		public override ActionType ActionType => ActionType.Email;

		public EmailAction(string name) : base(name) {}

		public string Account { get; set; }

		public string From { get; set; }

		public IEnumerable<string> To { get; set; }

		public IEnumerable<string> Cc { get; set; }

		public IEnumerable<string> Bcc { get; set; }

		public IEnumerable<string> ReplyTo { get; set; }

		public string Subject { get; set; }

		public IEmailBody Body { get; set; }

		public EmailPriority? Priority { get; set; }

		/// <summary>
		/// Attaches an attachment to the email
		/// </summary>
		/// <remarks>
		/// Only available in Watcher 2.3 and up
		/// </remarks>
		public IEmailAttachments Attachments { get; set; }

		/// <summary>
		/// Indicates whether the watch execution data should be attached to the email.
		/// If set to <c>true</c>, the data is attached as a YAML file called data.yml.
		/// If it’s set to <c>false</c>, no data is attached.
		/// To attach data and control the format of the attached data, assign <see cref="AttachData"/>
		/// </summary>
		[Obsolete("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
		public Union<bool?, AttachData> AttachData { get; set; }
	}

	/// <summary>
	/// A watch action that sends email notifications.
	/// To use the email action, you must configure at least one email account.
	/// </summary>
	public class EmailActionDescriptor : ActionsDescriptorBase<EmailActionDescriptor, IEmailAction>, IEmailAction
	{
		public EmailActionDescriptor(string name) : base(name) {}

		protected override ActionType ActionType => ActionType.Email;

		string IEmailAction.Account { get; set; }
		string IEmailAction.From { get; set; }
		IEnumerable<string> IEmailAction.To { get; set; }
		IEnumerable<string> IEmailAction.Cc { get; set; }
		IEnumerable<string> IEmailAction.Bcc { get; set; }
		IEnumerable<string> IEmailAction.ReplyTo { get; set; }
		string IEmailAction.Subject { get; set; }
		IEmailBody IEmailAction.Body { get; set; }
		EmailPriority? IEmailAction.Priority { get; set; }
		IEmailAttachments IEmailAction.Attachments { get; set; }
		Union<bool?, AttachData> IEmailAction.AttachData { get; set; }

		public EmailActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public EmailActionDescriptor From(string from) => Assign(a => a.From = from);

		public EmailActionDescriptor To(IEnumerable<string> to) => Assign(a => a.To = to);

		public EmailActionDescriptor To(params string[] to) => Assign(a => a.To = to);

		public EmailActionDescriptor Cc(IEnumerable<string> cc) => Assign(a => a.Cc = cc);

		public EmailActionDescriptor Cc(params string[] cc) => Assign(a => a.Cc = cc);

		public EmailActionDescriptor Bcc(IEnumerable<string> bcc) => Assign(a => a.Bcc = bcc);

		public EmailActionDescriptor Bcc(params string[] bcc) => Assign(a => a.Bcc = bcc);

		public EmailActionDescriptor ReplyTo(IEnumerable<string> replyTo) => Assign(a => a.ReplyTo = replyTo);

		public EmailActionDescriptor ReplyTo(params string[] replyTo) => Assign(a => a.ReplyTo = replyTo);

		public EmailActionDescriptor Subject(string subject) => Assign(a => a.Subject = subject);

		public EmailActionDescriptor Body(Func<EmailBodyDescriptor, IEmailBody> selector) =>
			Assign(a => a.Body = selector.InvokeOrDefault(new EmailBodyDescriptor()));

		public EmailActionDescriptor Priority(EmailPriority priority) => Assign(a => a.Priority = priority);

		/// <summary>
		/// Attach an attachment to the email, only available since watcher 2.3 and up.
		/// </summary>
		public EmailActionDescriptor Attachments(Func<EmailAttachmentsDescriptor, IPromise<IEmailAttachments>> selector) =>
			Assign(a => a.Attachments = selector?.Invoke(new EmailAttachmentsDescriptor())?.Value);

		/// <summary>
		/// Indicates whether the watch execution data should be attached to the email.
		/// If set to <c>true</c>, the data is attached as a YAML file called data.yml.
		/// If it’s set to <c>false</c>, no data is attached.
		/// To attach data and control the format of the attached data, use <see cref="AttachData(DataAttachmentFormat)"/>
		/// </summary>
		[Obsolete("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
		public EmailActionDescriptor AttachData(bool attach = true) =>
			Assign(a => a.AttachData = attach);

		/// <summary>
		/// Indicates to attach the watch execution data attached to the email and the format.
		/// </summary>
		[Obsolete("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
		public EmailActionDescriptor AttachData(DataAttachmentFormat format) =>
			Assign(a => a.AttachData = new AttachData { Format = format });
	}
}
