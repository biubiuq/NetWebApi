using DapperManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperManager
{
  public class UniqueCheckIdValidate: ValidationAttribute
  {
    //public SqlRepository<UserInfo> _manger = new DapperManager.SqlRepository<UserInfo>();


    //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //{
    //  //当自定义验证 Attribute 作用于属性时 value 是属性值，作用于类时 value 是类对象
    //  //而 validationContext 始终是类对象
    //  //var employeeAddDto = (EmployeeAddDto)value;
    //  var info = (UserInfo)validationContext.ObjectInstance;
    //  if (_manger.Get(info.Id) != null)
    //  {
    //    return new ValidationResult(ErrorMessage, new[] { nameof(UserInfo) });
    //  }
   

    //  return ValidationResult.Success;
    
  }
}
