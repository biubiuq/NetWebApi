
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreApi
{
  /// <summary>
  /// 针对post请求专门使用的modelBinder
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class EntityModelBinder2<T> : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var request = bindingContext.ActionContext.HttpContext.Request;
      var elementType = bindingContext.ModelType;
      var entity = Activator.CreateInstance(elementType);
   
      JObject data = request.HttpContext.Items["1"] as JObject;
      if (data == null)
      {
        using (var sr = new StreamReader(request.Body))
        {
          var json = sr.ReadToEndAsync().Result;
          entity = JsonConvert.DeserializeObject<T>(json);
          request.HttpContext.Items['1'] = data;
       
          foreach (PropertyInfo item in  elementType.GetProperties())
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
     



        }
        bindingContext.Model = entity;
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
      }
      return Task.CompletedTask;
    }
  }
}
