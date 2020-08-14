using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Filter
{
  public class MyActionFilterAttribute: ActionFilterAttribute
  {

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        
      
    }
  }
}
