//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace clientFactory
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attachments
    {
        public int Id { get; set; }
        public string FileLocation { get; set; }
        public System.DateTime uploadTime { get; set; }
    
        public virtual Report Report { get; set; }
    }
}
