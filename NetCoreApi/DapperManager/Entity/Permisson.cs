using System;
using System.Collections.Generic;
using System.Text;

namespace DapperManager
{
    public class Permisson
    {
        public string Permisson_Id { get; set; }
        public string Name { get; set; }
        public string Parent_Id { get; set; }
        public string Icon { get; set; }
        public string KeyId { get; set; }

        public string Type { get; set; }
        public virtual ICollection<Permisson> Permissons { get; set; }
    }
}
