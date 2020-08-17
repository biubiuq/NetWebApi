
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreApi
{
  public class EntityModelBinder2 : IModelBinder
  {
    private readonly BodyModelBinder bodyModelBinder;

   

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      var request = bindingContext.ActionContext.HttpContext.Request;

      JObject data = request.HttpContext.Items["1"] as JObject;
      if (data == null)
      {
        using (var sr = new StreamReader(request.Body))
        {
          var json = sr.ReadToEndAsync().Result;
          data = JsonConvert.DeserializeObject<JObject>(json);
          request.HttpContext.Items['1'] = data;
        }
      }
      return Task.CompletedTask;
    }
  }
}
