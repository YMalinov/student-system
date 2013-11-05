using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    public class AdminUserFullModel : UserFullModel
    {
        public bool IsActive { get; set; }
    }
}