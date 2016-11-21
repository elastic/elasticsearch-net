using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<EmailBody>))]
	public interface IEmailBody
	{
		[JsonProperty("text")]
		string Text { get; set; }

		[JsonProperty("html")]
		string Html { get; set; }
	}

	public class EmailBody : IEmailBody
	{
		public string Text { get; set; }

		public string Html { get; set; }
	}

	public class EmailBodyDescriptor : DescriptorBase<EmailBodyDescriptor, IEmailBody>, IEmailBody
	{
		string IEmailBody.Text { get; set; }
		string IEmailBody.Html { get; set; }

		public EmailBodyDescriptor Text(string text) => Assign(a => a.Text = text);

		public EmailBodyDescriptor Html(string html) => Assign(a => a.Html = html);
	}
}
