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
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            var elementType = bindingContext.ModelType;
            var entity = Activator.CreateInstance(elementType);    


            throw new NotImplementedException();
    }
  }
}
