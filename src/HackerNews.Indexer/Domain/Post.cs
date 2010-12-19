using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Indexer.Domain
{
	public class Post
	{
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		//Totally unnecessary but need to have nesting going on. 
		public PostMetaData Meta { get; set;}
	}
}
