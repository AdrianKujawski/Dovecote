//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dovecote
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pigeon
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RingNO { get; set; }
        public Nullable<System.DateTime> Hatched { get; set; }
        public Nullable<long> Yearbook { get; set; }
        public byte[] Image { get; set; }
        public string Comment { get; set; }
        public long Dovecote_Id { get; set; }
        public long EyeColor_Id { get; set; }
        public long Color_Id { get; set; }
        public long Race_Id { get; set; }
        public long Line_Id { get; set; }
        public long Gender_Id { get; set; }
        public Nullable<long> Father { get; set; }
        public Nullable<long> Mother { get; set; }
    }
}