using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.IndexTemplateExists
{
	public class DeleteTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await HEAD($"/_template/{name}")
				.Fluent(c => c.IndexTemplateExists(name))
				.Request(c => c.IndexTemplateExists(new IndexTemplateExistsRequest(name)))
				.FluentAsync(c => c.IndexTemplateExistsAsync(name))
				.RequestAsync(c => c.IndexTemplateExistsAsync(new IndexTemplateExistsRequest(name)))
				;
		}
	}
}
