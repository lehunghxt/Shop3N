
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
    using System.Collections.Generic;
    
public partial class tblUser
{

    public long Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Address { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public Nullable<long> CreateBy { get; set; }

    public Nullable<int> Status { get; set; }

    public Nullable<int> UserType { get; set; }

    public string Roles { get; set; }

    public string Email { get; set; }

}

}
