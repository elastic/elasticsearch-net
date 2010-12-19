using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Indexer.Domain
{
	public class PostMetaData
	{
		public string Username { get; set; }
		public int Points { get; set; }
		public int Type { get; set; }
		public DateTime Created { get; set; }
		public int CommentsCount { get; set; }
	}
}
