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

		[JsonProperty("color")]
		HipChatMessageColor? Color { get; set; }

		[JsonProperty("format")]
		HipChatMessageFormat? Format { get; set; }

		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("notify")]
		bool? Notify { get; set; }

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

		public HipChatMessageColor? Color { get; set; }

		public HipChatMessageFormat? Format { get; set; }

		public string From { get; set; }

		public bool? Notify { get; set; }

		public IEnumerable<string> Room { get; set; }

		public IEnumerable<string> User { get; set; }
	}

	public class HipChatMessageDescriptor : DescriptorBase<HipChatMessageDescriptor, IHipChatMessage>, IHipChatMessage
	{
		string IHipChatMessage.Body { get; set; }
		HipChatMessageColor? IHipChatMessage.Color { get; set; }
		HipChatMessageFormat? IHipChatMessage.Format { get; set; }
		string IHipChatMessage.From { get; set; }
		bool? IHipChatMessage.Notify { get; set; }
		IEnumerable<string> IHipChatMessage.Room { get; set; }
		IEnumerable<string> IHipChatMessage.User { get; set; }

		public HipChatMessageDescriptor Body(string body) => Assign(body, (a, v) => a.Body = v);

		public HipChatMessageDescriptor Format(HipChatMessageFormat? format) => Assign(format, (a, v) => a.Format = v);

		public HipChatMessageDescriptor Color(HipChatMessageColor? color) => Assign(color, (a, v) => a.Color = v);

		public HipChatMessageDescriptor Notify(bool? notify = true) => Assign(notify, (a, v) => a.Notify = v);

		public HipChatMessageDescriptor From(string from) => Assign(from, (a, v) => a.From = v);

		public HipChatMessageDescriptor Room(IEnumerable<string> room) => Assign(room, (a, v) => a.Room = v);

		public HipChatMessageDescriptor Room(params string[] room) => Assign(room, (a, v) => a.Room = v);

		public HipChatMessageDescriptor User(IEnumerable<string> user) => Assign(user, (a, v) => a.User = v);

		public HipChatMessageDescriptor User(params string[] user) => Assign(user, (a, v) => a.User = v);
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
