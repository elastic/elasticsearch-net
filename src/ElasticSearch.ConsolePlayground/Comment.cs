using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.ConsolePlayground
{
	public class Comment
	{
		public string Body { get; set; }


		public DateTime CreatedOn { get; set; }


		public DateTime? ModifiedOn { get; set; }

		public int Id { get; set; }


		public Person Author { get; set; }

	}
}
