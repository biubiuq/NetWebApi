using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Filter
{
  public class EntityModelBinderForGet : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      throw new NotImplementedException();
    }
  }
}
