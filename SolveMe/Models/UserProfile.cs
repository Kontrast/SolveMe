//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolveMe.Models
{
    using System;
    using System.Collections.Generic;

    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Tasks = new HashSet<Tasks>();
            this.Tasks1 = new HashSet<Tasks>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Tasks> Tasks1 { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}