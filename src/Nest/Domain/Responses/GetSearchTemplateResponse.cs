using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IGetSearchTemplateResponse : IResponse
	{
	}

	public class GetSearchTemplateResponse : BaseResponse, IGetSearchTemplateResponse
	{
	}
}
