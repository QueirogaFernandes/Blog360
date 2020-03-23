using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
    public enum Status
    {
        [Description("Approved")]
        Approved = 1,
        [Description("Waiting for Approval")]
        WaitingApproval = 2,
        [Description("Not Approved")]
        NotApproved = 3
    }
}