using System;
using System.Net.Mail;
using log4net;
using System.IO;

namespace Library
{
    public class MailManager
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MailManager));

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public MailManager()
        { }

        public MailManager(string host = "", int port = 25, bool ssl = false, string from = "", string password = "", string account = "", bool useDefaultCredentials = false, string displayName = "")
        {
            this.EnableSSL = ssl;
            this.Port = port;
            this.UseDefaultCredentials = useDefaultCredentials;
            if (!string.IsNullOrEmpty(host)) this.Host = host;
            if (!string.IsNullOrEmpty(from)) this.From = from;
            if (!string.IsNullOrEmpty(password)) this.Password = password;
            if (!string.IsNullOrEmpty(account)) this.Account = account;
            if (!string.IsNullOrEmpty(displayName)) this.DisplayName = displayName;
        }

        public void SendEmail(string to = "", string title = "", string content = "", string cc = "", string bcc = "")
        {
            if (!string.IsNullOrEmpty(to)) this.To = to;
            if (!string.IsNullOrEmpty(title)) this.Title = title;
            if (!string.IsNullOrEmpty(content)) this.Content = content;
            if (!string.IsNullOrEmpty(cc)) this.CC = cc;
            if (!string.IsNullOrEmpty(bcc)) this.BCC = bcc;
            this.SendEmail();
        }
        public void SendEmail()
        {
            string send = string.Empty;
            string[] arr = To.Replace(',', ';').Replace('|', ';').Split(';');

            MailMessage msg = new MailMessage();

            From = From.Replace('<', '|').Replace(">", string.Empty);
            if (From.Contains("|"))
            {
                var arrForm = From.Split('|');
                From = arrForm[1];
                DisplayName = arrForm[0];
            }

            if (!string.IsNullOrEmpty(this.DisplayName)) msg.From = new MailAddress(From, this.DisplayName);
            else msg.From = new MailAddress(From);
            msg.Subject = Title;
            msg.Body = Content;
            msg.IsBodyHtml = true;

            if (FileStream != null && !string.IsNullOrEmpty(FileName))
            {
                Attachment att = new Attachment(FileStream, FileName);
                msg.Attachments.Add(att);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                bool result = regex.IsMatch(arr[i]);
                if (result == false) throw new Exception("Địa chỉ email không hợp lệ.");
                else msg.To.Add(arr[i]);
            }

            if (!string.IsNullOrEmpty(CC))
            {
                var ccs = CC.Split(';');
                foreach (var cc in ccs)
                {
                    msg.CC.Add(new MailAddress(cc));
                }
            }

            if (!string.IsNullOrEmpty(BCC))
            {
                var bccs = BCC.Split(';');
                foreach (var bcc in bccs)
                {
                    msg.Bcc.Add(new MailAddress(bcc));
                }
            }

            this.Send(msg);
        }

        private void Send(MailMessage msg)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host; //Sử dụng SMTP của gmail
                smtp.Port = Port;
                smtp.EnableSsl = EnableSSL;
                smtp.UseDefaultCredentials = UseDefaultCredentials;

                if (string.IsNullOrEmpty(Account)) Account = From;
                smtp.Credentials = new System.Net.NetworkCredential(Account, Password);
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
