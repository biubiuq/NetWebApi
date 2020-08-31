using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreApi
{
  public class EntityModelBinderForGet<T> : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            var elementType = bindingContext.ModelType;
            var entity = Activator.CreateInstance(elementType);
                entity = JsonConvert.DeserializeObject<T>(value);
       foreach (PropertyInfo item in elementType.GetProperties())
      {
        if (item.PropertyType == typeof(string))
        {
          var values = item.GetValue(entity);
          if (values == null)
          {
            item.SetValue(entity, "", null);
            continue;
          }
        }
        if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(DateTime?))
        {
          item.SetValue(entity, DateTime.Now, null);
        }
      }
      bindingContext.Model = entity;
      bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
      return Task.CompletedTask;
    }
  }
}
