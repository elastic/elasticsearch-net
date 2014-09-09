using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ITemplateSection
	{
		object Instance { get; }
		string Variable { get; }
	}
}
