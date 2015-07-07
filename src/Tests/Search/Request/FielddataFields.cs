using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class FielddataFields
	{
		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

			protected override object ExpectJson => new
			{
				fielddata_fields = new [] { "name", "leadDeveloper", "startedOn" }
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.FielddataFields(
					f => f.Name,
					f => f.LeadDeveloper,
					f => f.StartedOn
				);

			protected override SearchRequest<Project> Initializer => 
				new SearchRequest<Project>
				{
					FielddataFields = new PropertyPathMarker[] { "name", "leadDeveloper", "startedOn" }
				};
		}
	}
}
