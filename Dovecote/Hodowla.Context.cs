﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HodowlaEntities : DbContext
    {
        public HodowlaEntities()
            : base("name=HodowlaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Dovecote> Dovecote { get; set; }
        public virtual DbSet<EyeColor> EyeColor { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<Pigeon> Pigeon { get; set; }
        public virtual DbSet<Race> Race { get; set; }
    }
}
