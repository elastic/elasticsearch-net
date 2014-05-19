using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce579Tests : BaseJsonTests
	{
		public class ParentRecord
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string LocationId { get; set; }

			public List<Guid> AssignedUsers { get; set;  }
			public List<Guid> OverrideUsers { get; set;  }
			public bool Unassigned { get; set;  }
			public bool Global { get; set;  }
		}

		public class ChildRecord
		{

		}

		private static BaseQuery CreatePermissionsFilter(
			Guid CurrentUserId, 
			List<Guid> usersToSearch, 
			bool searchUnassigned, 
			QueryDescriptor<ParentRecord> filter
		)
		{
			return filter.Bool(c => c
				.Should(
					s => s.Terms(p => p.AssignedUsers.First(), usersToSearch),
					s => s.Term(p => p.OverrideUsers, CurrentUserId),	
					s => s.Term(p => p.Unassigned, "true"),
					s => s.Term(p => p.Global, "true")
				)
				.MinimumNumberShouldMatch(1)
			);
		}

		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/579
		/// </summary>
		[Test]
		public void Issue579()
		{
			var currentUserId = Guid.Parse("8f836113-3392-4dad-9971-02ac915fd64a");
			var usersToSearch = new[]
			{
				Guid.Parse("6625c0bf-5414-4985-880c-3b34152e9453"), 
				Guid.Parse("7ff7bc06-c75e-4ed1-9732-f3c6c9743670"), 
				Guid.Parse("e4c48ebf-7602-43a3-b82f-1a83e2ae1c0b")
			}.ToList();

			var searchUnassigned = false;
			var search = new SearchDescriptor<ParentRecord>()
				.Query(qd => qd
					.Bool(b => b
						.Must(
							q => q.Term(p => p.LocationId, "my-location")
							&& CreatePermissionsFilter(currentUserId, usersToSearch, searchUnassigned, q)
					)
				)
			);
			this.JsonEquals(search, MethodInfo.GetCurrentMethod());
		}

	}
}
