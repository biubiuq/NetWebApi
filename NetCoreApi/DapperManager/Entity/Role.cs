using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DapperManager
{
   public class Role
    {
     
        public string Role_Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        [NotMapped]///忽略字段专用
        public virtual ICollection<Permisson> Permissons { get; set; }
    }
}
