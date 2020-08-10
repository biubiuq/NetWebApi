using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Entity
{
    public class UserInfo:IValidatableObject
    {
        
        public string Name { get; set; }
        /// <summary>
        /// 会自动调用validate方法进行check
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == "123")
            {
                yield return new ValidationResult("不能等于123"
                    , new[] {"Name错误"}
                    );
            }
        }
    }
}
