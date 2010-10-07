using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.ConsolePlayground
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Blog
	{
		[JsonProperty]
		public int Id { get; set; }
	
		[JsonProperty]
		public string Title { get; set; }

		[JsonProperty]
		public string Body { get; set; }

		[JsonProperty]
		public Person Author { get; set; }

		[JsonProperty]
		public DateTime CreatedOn { get; set; }	
		
		
		public List<Comment> Comments { get; set; }

	}
}
