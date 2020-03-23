using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
    public enum Role
    {
        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2,
        [Description("Colaborator")]
        Colaborator = 3
    }
}
