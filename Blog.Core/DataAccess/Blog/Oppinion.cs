//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blog.Core.DataAccess.Blog
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oppinion
    {
        public int ID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
