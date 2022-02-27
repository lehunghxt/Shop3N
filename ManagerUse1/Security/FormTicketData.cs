namespace Einvoince.Web.Security
{
    using System;

    /// <summary>
    /// The form ticket data.
    /// </summary>
    public class FormTicketData
    {
        #region Public Properties
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

        #endregion
    }
}