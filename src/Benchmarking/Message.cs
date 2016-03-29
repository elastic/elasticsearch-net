using System;

namespace Benchmarking
{
	public class Message
	{
		public string Body { get; set; }

		public Guid Id { get; set; }

		public DateTime Timestamp { get; set; }
	}
}