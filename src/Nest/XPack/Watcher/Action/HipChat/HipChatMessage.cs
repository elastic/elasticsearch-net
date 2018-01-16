using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HipChatMessage>))]
	public interface IHipChatMessage
	{
		[JsonProperty("body")]
		string Body { get; set; }

		[JsonProperty("format")]
		HipChatMessageFormat? Format { get; set; }

		[JsonProperty("color")]
		HipChatMessageColor? Color { get; set; }

		[JsonProperty("notify")]
		bool? Notify { get; set; }

		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("room")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> Room { get; set; }

		[JsonProperty("user")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> User { get; set; }
	}

	public class HipChatMessage : IHipChatMessage
	{
		public string Body { get; set; }

		public HipChatMessageFormat? Format { get; set; }

		public HipChatMessageColor? Color { get; set; }

		public bool? Notify { get; set; }

		public string From { get; set; }

		public IEnumerable<string> Room { get; set; }

		public IEnumerable<string> User { get; set; }
	}

	public class HipChatMessageDescriptor : DescriptorBase<HipChatMessageDescriptor, IHipChatMessage>, IHipChatMessage
	{
		string IHipChatMessage.Body { get; set; }
		HipChatMessageFormat? IHipChatMessage.Format { get; set; }
		HipChatMessageColor? IHipChatMessage.Color { get; set; }
		bool? IHipChatMessage.Notify { get; set; }
		string IHipChatMessage.From { get; set; }
		IEnumerable<string> IHipChatMessage.Room { get; set; }
		IEnumerable<string> IHipChatMessage.User { get; set; }

		public HipChatMessageDescriptor Body(string body) => Assign(a => a.Body = body);

		public HipChatMessageDescriptor Format(HipChatMessageFormat? format) => Assign(a => a.Format = format);

		public HipChatMessageDescriptor Color(HipChatMessageColor? color) => Assign(a => a.Color = color);

		public HipChatMessageDescriptor Notify(bool? notify = true) => Assign(a => a.Notify = notify);

		public HipChatMessageDescriptor From(string from) => Assign(a => a.From = from);

		public HipChatMessageDescriptor Room(IEnumerable<string> room) => Assign(a => a.Room = room);

		public HipChatMessageDescriptor Room(params string[] room) => Assign(a => a.Room = room);

		public HipChatMessageDescriptor User(IEnumerable<string> user) => Assign(a => a.User = user);

		public HipChatMessageDescriptor User(params string[] user) => Assign(a => a.User = user);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum HipChatMessageFormat
	{
		[EnumMember(Value = "html")]
		Html,

		[EnumMember(Value = "text")]
		Text
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum HipChatMessageColor
	{
		[EnumMember(Value = "gray")]
		Gray,

		[EnumMember(Value = "green")]
		Green,

		[EnumMember(Value = "purple")]
		Purple,

		[EnumMember(Value = "red")]
		Red,

		[EnumMember(Value = "yellow")]
		Yellow,
	}
}
