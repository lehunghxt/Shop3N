﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Shop.DAL
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class ShopEntities : DbContext
{
    public ShopEntities(string connectionString) :
        base(new System.Data.Entity.Core.Objects.ObjectContext(new System.Data.Entity.Core.EntityClient.EntityConnection(connectionString), true),true)
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<tblProduct> tblProduct { get; set; }

    public virtual DbSet<tblUser> tblUser { get; set; }

}

}

