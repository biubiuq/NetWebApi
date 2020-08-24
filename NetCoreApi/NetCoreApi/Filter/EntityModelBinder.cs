using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreApi
{
    public class EntityModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider;
            var elementType = bindingContext.ModelType.GetTypeInfo();

            var entity =  Activator.CreateInstance(elementType);
            return Task.CompletedTask;
        }
    }
}
