using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Linq;
using Shop.Domain;
using System.Net.Mail;
using System.Collections.Generic;
using log4net;
using System.Net;
using Library.Extensions;
using System.Threading;

namespace Shop.Service
{
    public class CGlobal
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CGlobal));
        public static string ConnectionString = ConfigurationSettings.AppSettings["ShopConn"];
        public static string AcceptedImage = ConfigurationSettings.AppSettings["AcceptedImage"];
        public static int PageSize = ToInt(ConfigurationSettings.AppSettings["PageSize"]);
        public static int Perpage = ToInt(ConfigurationSettings.AppSettings["Perpage"]);
        public static int Filesize = ToInt(ConfigurationSettings.AppSettings["Filesize"]);
        public static string recapcha = ConfigurationSettings.AppSettings["recapcha"];
        public static string recapchaSecret = ConfigurationSettings.AppSettings["recapchaSecret"];
        public static string urlwebsiteupfile = ConfigurationSettings.AppSettings["urlwebsiteupfile"];
        public static string ServerReport = ConfigurationSettings.AppSettings["ServerReport"];
        public static string ToUn_Unicode(string str)
        {

            str = str.Replace(".", "");
            str = str.Replace(",", "");
            str = str.Replace("?", "");
            str = str.Replace("!", "");
            str = str.Replace("@", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("%", "");
            str = str.Replace("$", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("=", "");
            str = str.Replace("+", "");
            str = str.Replace("/", "");
            str = str.Replace(" ", "-");
            //---------------------------------a^
            str = str.Replace("ấ", "a");
            str = str.Replace("ầ", "a");
            str = str.Replace("ẩ", "a");
            str = str.Replace("ẫ", "a");
            str = str.Replace("ậ", "a");
            //---------------------------------A^
            str = str.Replace("Ấ", "A");
            str = str.Replace("Ầ", "A");
            str = str.Replace("Ẩ", "A");
            str = str.Replace("Ẫ", "A");
            str = str.Replace("Ậ", "A");
            //---------------------------------a(
            str = str.Replace("ắ", "a");
            str = str.Replace("ằ", "a");
            str = str.Replace("ẳ", "a");
            str = str.Replace("ẵ", "a");
            str = str.Replace("ặ", "a");
            //---------------------------------A(
            str = str.Replace("Ắ", "A");
            str = str.Replace("Ằ", "A");
            str = str.Replace("Ẳ", "A");
            str = str.Replace("Ẵ", "A");
            str = str.Replace("Ặ", "A");
            //---------------------------------a
            str = str.Replace("á", "a");
            str = str.Replace("à", "a");
            str = str.Replace("ả", "a");
            str = str.Replace("ã", "a");
            str = str.Replace("ạ", "a");
            str = str.Replace("â", "a");
            str = str.Replace("ă", "a");
            //---------------------------------A
            str = str.Replace("Á", "A");
            str = str.Replace("À", "A");
            str = str.Replace("Ả", "A");
            str = str.Replace("Ã", "A");
            str = str.Replace("Ạ", "A");
            str = str.Replace("Â", "A");
            str = str.Replace("Ă", "A");
            //---------------------------------e^
            str = str.Replace("ế", "e");
            str = str.Replace("ề", "e");
            str = str.Replace("ể", "e");
            str = str.Replace("ễ", "e");
            str = str.Replace("ệ", "e");
            //---------------------------------E^
            str = str.Replace("Ế", "E");
            str = str.Replace("Ề", "E");
            str = str.Replace("Ể", "E");
            str = str.Replace("Ễ", "E");
            str = str.Replace("Ệ", "E");
            //---------------------------------e
            str = str.Replace("é", "e");
            str = str.Replace("è", "e");
            str = str.Replace("ẻ", "e");
            str = str.Replace("ẽ", "e");
            str = str.Replace("ẹ", "e");
            str = str.Replace("ê", "e");
            //---------------------------------E
            str = str.Replace("É", "E");
            str = str.Replace("È", "E");
            str = str.Replace("Ẻ", "E");
            str = str.Replace("Ẽ", "E");
            str = str.Replace("Ẹ", "E");
            str = str.Replace("Ê", "E");
            //---------------------------------i
            str = str.Replace("í", "i");
            str = str.Replace("ì", "i");
            str = str.Replace("ỉ", "i");
            str = str.Replace("ĩ", "i");
            str = str.Replace("ị", "i");
            //---------------------------------I
            str = str.Replace("Í", "I");
            str = str.Replace("Ì", "I");
            str = str.Replace("Ỉ", "I");
            str = str.Replace("Ĩ", "I");
            str = str.Replace("Ị", "I");
            //---------------------------------o^
            str = str.Replace("ố", "o");
            str = str.Replace("ồ", "o");
            str = str.Replace("ổ", "o");
            str = str.Replace("ỗ", "o");
            str = str.Replace("ộ", "o");
            //---------------------------------O^
            str = str.Replace("Ố", "O");
            str = str.Replace("Ồ", "O");
            str = str.Replace("Ổ", "O");
            str = str.Replace("Ô", "O");
            str = str.Replace("Ộ", "O");
            //---------------------------------o*
            str = str.Replace("ớ", "o");
            str = str.Replace("ờ", "o");
            str = str.Replace("ở", "o");
            str = str.Replace("ỡ", "o");
            str = str.Replace("ợ", "o");
            //---------------------------------O*
            str = str.Replace("Ớ", "O");
            str = str.Replace("Ờ", "O");
            str = str.Replace("Ở", "O");
            str = str.Replace("Ỡ", "O");
            str = str.Replace("Ợ", "O");
            //---------------------------------u*
            str = str.Replace("ứ", "u");
            str = str.Replace("ừ", "u");
            str = str.Replace("ử", "u");
            str = str.Replace("ữ", "u");
            str = str.Replace("ự", "u");
            //---------------------------------U*
            str = str.Replace("Ứ", "U");
            str = str.Replace("Ừ", "U");
            str = str.Replace("Ử", "U");
            str = str.Replace("Ữ", "U");
            str = str.Replace("Ự", "U");
            //---------------------------------y
            str = str.Replace("ý", "y");
            str = str.Replace("ỳ", "y");
            str = str.Replace("ỷ", "y");
            str = str.Replace("ỹ", "y");
            str = str.Replace("ỵ", "y");
            //---------------------------------Y
            str = str.Replace("Ý", "Y");
            str = str.Replace("Ỳ", "Y");
            str = str.Replace("Ỷ", "Y");
            str = str.Replace("Ỹ", "Y");
            str = str.Replace("Ỵ", "Y");
            //---------------------------------DD
            str = str.Replace("Đ", "D");
            str = str.Replace("Đ", "D");
            str = str.Replace("đ", "d");
            //---------------------------------o
            str = str.Replace("ó", "o");
            str = str.Replace("ò", "o");
            str = str.Replace("ỏ", "o");
            str = str.Replace("õ", "o");
            str = str.Replace("ọ", "o");
            str = str.Replace("ô", "o");
            str = str.Replace("ơ", "o");
            //---------------------------------O
            str = str.Replace("Ó", "O");
            str = str.Replace("Ò", "O");
            str = str.Replace("Ỏ", "O");
            str = str.Replace("Õ", "O");
            str = str.Replace("Ọ", "O");
            str = str.Replace("Ô", "O");
            str = str.Replace("Ơ", "O");
            //---------------------------------u
            str = str.Replace("ú", "u");
            str = str.Replace("ù", "u");
            str = str.Replace("ủ", "u");
            str = str.Replace("ũ", "u");
            str = str.Replace("ụ", "u");
            str = str.Replace("ư", "u");
            //---------------------------------U
            str = str.Replace("Ú", "U");
            str = str.Replace("Ù", "U");
            str = str.Replace("Ủ", "U");
            str = str.Replace("Ũ", "U");
            str = str.Replace("Ụ", "U");
            str = str.Replace("Ư", "U");

            str = str.Replace(".", "");

            return str;
        }

        public static string ClearBreak(string strIn)
        {
            strIn = Regex.Replace(strIn, "<DIV(.*?)>(.*?)</DIV>", "$2", RegexOptions.IgnoreCase);
            strIn = Regex.Replace(strIn, "<P(.*?)>(.*?)</P>", "$2", RegexOptions.IgnoreCase);
            return strIn;
        }
        public static string ClearHtml(string strIn)
        {
            strIn = Regex.Replace(strIn, @"&nbsp;", "  ", RegexOptions.IgnoreCase);
            return Regex.Replace(strIn, "<[^>]*>", "", RegexOptions.IgnoreCase);
        }

        public static string EnCodeUrl(string Url)
        {
            Url = Url.Replace(" ", "_");

            Url = Url.Replace(@"""", "quot");
            Url = Url.Replace("&", "amp");
            Url = Url.Replace("=", "Aacute14;");
            Url = Url.Replace("*", "Aacute2;");
            Url = Url.Replace("+", "Aacute3;");
            Url = Url.Replace(",", "Aacute4;");
            Url = Url.Replace(".", "Aacute5;");
            Url = Url.Replace("/", "Aacute6;");
            Url = Url.Replace(":", "Aacute7;");
            Url = Url.Replace("#", "Aacute8;");
            Url = Url.Replace("(", "Aacute9;");
            Url = Url.Replace(")", "Aacute10;");
            Url = Url.Replace("@", "Aacute11;");
            Url = Url.Replace("$", "Aacute12;");
            Url = Url.Replace("^", "Aacute13;");
            Url = Url.Replace(@"'", "Aacute1;");
            //Url = Url.Replace(";", "&­#59;");
            return Url;
        }
        public static string DeCodeUrlHTML(string Url)
        {
            try
            {
                Url = Url.Replace("ssvar|17;", " ");

                Url = Url.Replace("ssvar|16;", @"""");
                Url = Url.Replace("ssvar|15;", "&");
                Url = Url.Replace("ssvar|14;", "=");
                Url = Url.Replace("ssvar|2;", "*");
                Url = Url.Replace("ssvar|3;", "+");
                Url = Url.Replace("ssvar|4;", ",");
                Url = Url.Replace("ssvar|5;", ".");
                Url = Url.Replace("ssvar|6;", "/");
                Url = Url.Replace("ssvar|7;", ":");
                Url = Url.Replace("ssvar|8;", "#");
                Url = Url.Replace("ssvar|9;", "(");
                Url = Url.Replace("ssvar|10;", ")");
                Url = Url.Replace("ssvar|11;", "@");
                Url = Url.Replace("ssvar|12;", "$");
                Url = Url.Replace("ssvar|13;", "^");
                Url = Url.Replace("ssvar|1;", @"'");

                //Url = Url.Replace("&­#59;",";");

                return Url;
            }
            catch { return ""; }
        }
        public static string DeCodeUrl(string Url)
        {
            try
            {
                Url = Url.Replace("_", " ");

                Url = Url.Replace("quot", @"""");
                Url = Url.Replace("amp", "&");
                Url = Url.Replace("Aacute14;", "=");
                Url = Url.Replace("Aacute2;", "*");
                Url = Url.Replace("Aacute3;", "+");
                Url = Url.Replace("Aacute4;", ",");
                Url = Url.Replace("Aacute5;", ".");
                Url = Url.Replace("Aacute6;", "/");
                Url = Url.Replace("Aacute7;", ":");
                Url = Url.Replace("Aacute8;", "#");
                Url = Url.Replace("Aacute9;", "(");
                Url = Url.Replace("Aacute10;", ")");
                Url = Url.Replace("Aacute11;", "@");
                Url = Url.Replace("Aacute12;", "$");
                Url = Url.Replace("Aacute13;", "^");
                Url = Url.Replace("Aacute1;", @"'");

                //Url = Url.Replace("&­#59;",";");

                return Url;
            }
            catch { return ""; }
        }
        public static string ToFileName(object obj)
        {
            if (obj == null) return "";
            string tmp = obj.ToString();
            tmp = tmp.Replace(" ", "_");
            tmp = tmp.Replace(",", "");
            tmp = tmp.Replace("'", "");
            tmp = tmp.Replace("’", "");
            tmp = tmp.Replace("!", "");
            tmp = tmp.Replace("~", "");
            tmp = tmp.Replace("&", "");
            tmp = tmp.Replace("*", "");
            tmp = tmp.Replace("#", "");
            tmp = tmp.Replace("\"", "");
            tmp = tmp.Replace("/", "");
            tmp = tmp.Replace("\\", "");
            tmp = tmp.Replace("?", "");
            tmp = tmp.Replace(".", "");
            tmp = tmp.Replace(":", "");
            tmp = tmp.Replace("|", "");
            tmp = tmp.ToLower();
            return tmp;
        }
        public static string EncodeId(string Base64Encode, int UserId)
        {
            try
            {
                string ReEncode = Base64Encode + tosha(ToStr(UserId));
                return EnCodeUrl(ReEncode);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string EncodeId(string Base64Encode, string UserId)
        {
            try
            {
                string ReEncode = Base64Encode + tosha(ToStr(UserId));
                return EnCodeUrl(ReEncode);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string DecodeId(string EncodeId, string UserId)
        {
            try
            {
                EncodeId = DeCodeUrl(EncodeId);
                string ReId = Base64Decode(EncodeId.Substring(0, EncodeId.IndexOf(tosha(UserId.ToString()))));
                return ReId;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string tosha(string str)
        {
            string restr = "";
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                restr = sb.ToString().ToLower();
            }
            return restr;
        }
        public static string DecodeId(string EncodeId, int UserId)
        {
            try
            {
                EncodeId = DeCodeUrl(EncodeId);
                string ReId = Base64Decode(EncodeId.Substring(0, EncodeId.IndexOf(tosha(UserId.ToString()))));
                return ReId;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }
        private static string Donvi(string so)
        {
            string Kdonvi = "";

            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";
            return Kdonvi;
        }
        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToDouble(ch) > 1 && Convert.ToDouble(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim().Replace("một", "mốt") + " ";
                if (tr.Equals("0") && Convert.ToDouble(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToDouble(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToDouble(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToDouble(tr) > 0 && Convert.ToDouble(ch) > 1 && Convert.ToDouble(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim().Replace("một", "mốt") + " ";
                if (Convert.ToDouble(tr) > 0 && Convert.ToDouble(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToDouble(tr) > 0 && Convert.ToDouble(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToDouble(tr) > 0 && ch.Equals("1") && Convert.ToDouble(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                if (Convert.ToDouble(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToDouble(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }
            return Ktach;

        }
        public static void writelog(string PathFile, string ex)
        {
            if (ex is ThreadAbortException) return;
            string HostName = Dns.GetHostName();
            string hostAddress = Dns.GetHostByName(HostName).AddressList[0].ToString() + "/";
            PathFile = PathFile + hostAddress;
            if (!Directory.Exists(PathFile + hostAddress)) Directory.CreateDirectory(PathFile);
            StreamWriter log;
            string date = DateTime.Now.ToString("MMddyyyy");
            if (!File.Exists(PathFile + "logfile" + date + ".txt"))
            {
                log = new StreamWriter(PathFile + "logfile" + date + ".txt");
            }
            else
            {
                log = File.AppendText(PathFile + "logfile" + date + ".txt");
            }

            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(ex.ToString());
            log.WriteLine();

            // Close the stream:
            log.Close();
        }

        public static void writelog(string PathFile, Exception ex)
        {
            if (ex is ThreadAbortException) return;
            string HostName = Dns.GetHostName();
            string hostAddress = Dns.GetHostByName(HostName).AddressList[0].ToString() + "/";
            StreamWriter log;
            string date = DateTime.Now.ToString("MMddyyyy");
            if (!Directory.Exists(PathFile + hostAddress)) Directory.CreateDirectory(PathFile + hostAddress);
            if (!File.Exists(PathFile + hostAddress + "logfile" + date + ".txt"))
            {
                log = new StreamWriter(PathFile + hostAddress + "logfile" + date + ".txt");
            }
            else
            {
                log = File.AppendText(PathFile + hostAddress + "logfile" + date + ".txt");
            }

            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(ex.Message);
            log.WriteLine(ex.InnerException);
            log.WriteLine(ex.StackTrace);
            log.WriteLine();

            // Close the stream:
            log.Close();
        }
        public static string So_chu(double gNum, string Donvichu = "đồng")
        {
            try
            {
                if (gNum == 0)
                    return "Không đồng";
                string am = "";
                if (gNum < 0)
                {
                    gNum = CGlobal.ToDouble(CGlobal.ToStr(gNum).Replace("-", ""));
                    //   am = "Trừ ";
                    // Num
                }
                string lso_chu = "";
                string tach_mod = "";
                string tach_conlai = "";
                var Num = gNum;
                if (Donvichu == "đồng")
                    Num = Math.Round(gNum, 0);
                else if (gNum.ToString().Contains('.'))
                    Num = Convert.ToDouble(gNum.ToString().Split('.')[0]);
                string gN = Convert.ToString(Num);
                int m = Convert.ToInt32(gN.Length / 3);
                int mod = gN.Length - m * 3;
                string dau = "[+]";

                // Dau [+ , - ]
                if (gNum < 0)
                    dau = "[-]";
                dau = "";

                // Tach hang lon nhat
                if (mod.Equals(1))
                    tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
                if (mod.Equals(2))
                    tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
                if (mod.Equals(0))
                    tach_mod = "000";
                // Tach hang con lai sau mod :
                if (Num.ToString().Length > 2)
                    tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

                ///don vi hang mod
                int im = m + 1;
                if (mod > 0)
                    lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
                /// Tach 3 trong tach_conlai

                int i = m;
                int _m = m;
                int j = 1;
                string tach3 = "";
                string tach3_ = "";

                while (i > 0)
                {
                    tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                    tach3_ = tach3;
                    lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                    m = _m + 1 - j;
                    if (!tach3_.Equals("000"))
                        lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                    tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                    i = i - 1;
                    j = j + 1;
                }
                if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                    lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
                if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                    lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
                if (lso_chu.Trim().Length > 0)
                    lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " " + Donvichu;
                if (gNum < 0)
                {
                    lso_chu += "Trừ " + lso_chu;
                }

                if (Donvichu != "đồng")
                {
                    var str = gNum.ToString().Replace(',', '.');
                    var strs = str.Split('.');
                    if (strs.Length > 1)
                    {
                        var le = strs[strs.Length - 1];
                        if (le.Length == 1) le = le + "0";
                        lso_chu = lso_chu + " và " + So_chu(Convert.ToDouble(le), "").ToLower() + " cent.";
                    }
                    return am + lso_chu.ToString().Trim();
                }
                return am + lso_chu.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string NumberToWords(double number)
        {
            var str = number.ToString();

            /* Remove spaces and commas */
            str = str.Replace(",", "");
            str = str.Replace(" ", "");

            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[Convert.ToInt32(number)];
                else
                {
                    words += tensMap[Convert.ToInt32(number) / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[Convert.ToInt32(number) % 10];
                }
            }

            return words;
        }

        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }
        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        public static String ConvertToWords(String numb, String endStr = "Only")
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents    
                        endStr = "Paisa " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }
        private static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }

        public static string ToComa(object obj)
        {
            if (obj.ToString().Length == 0) return "";

            Double strvalue = Convert.ToDouble(obj);
            string revalue = strvalue.ToString("#,##0.####");

            if (revalue == "0") return "";
            return revalue;
        }
        public static string ToComaCoverByCus(object obj, string valueConfig, string format = "")
        {
            try
            {
                if (obj.ToString().Length == 0) return "";

                if (string.IsNullOrEmpty(format))
                {
                    Double strvalue = Convert.ToDouble(obj);
                    if (strvalue < 0)
                    {
                        strvalue = -strvalue;
                    }
                    string revalue = strvalue.ToString("#,##0.#####");
                    string[] buffChb = revalue.Split('.');
                    if (buffChb.Length > 0)
                    {
                        revalue = buffChb[0].Replace(",", ".");

                    }
                    if (buffChb.Length > 1)
                    {
                        revalue += "," + buffChb[1];
                    }
                    if (revalue == "0") return valueConfig;
                    return revalue;
                }
                else
                {
                    var de = Convert.ToDecimal(obj);
                    if (de == 0) return valueConfig;
                    if (de < 0) de = -de;
                    return de.Format(format);
                }
            }
            catch (Exception ex)
            { return ""; }
        }
        public static string ToComaCover(object obj, string format = "")
        {
            try
            {
                if (obj.ToString().Length == 0) return "";

                if (string.IsNullOrEmpty(format))
                {
                    Double strvalue = Convert.ToDouble(obj);
                    if (strvalue < 0)
                    {
                        strvalue = -strvalue;
                    }
                    string revalue = strvalue.ToString("#,###0.#####");
                    string[] buffChb = revalue.Split('.');
                    if (buffChb.Length > 0)
                    {
                        revalue = buffChb[0].Replace(",", ".");

                    }
                    if (buffChb.Length > 1)
                    {
                        revalue += "," + buffChb[1];
                    }
                    if (revalue == "0") return "";
                    return revalue;
                }
                else
                {
                    var de = Convert.ToDecimal(obj);
                    if (de == 0) return "";
                    if (de < 0) de = -de;
                    return de.Format(format);
                }
            }
            catch (Exception ex)
            { return ""; }
        }
        public static string ToComaCoverType2(object obj)
        {
            try
            {
                if (obj.ToString().Length == 0) return "_";
                Double strvalue = Convert.ToDouble(obj);
                if (strvalue < 0)
                {
                    strvalue = -strvalue;
                }
                if (strvalue == 0) return "_";
                string revalue = strvalue.ToString("#,###0.#####");
                string[] buffChb = revalue.Split('.');
                if (buffChb.Length > 0)
                {
                    revalue = buffChb[0].Replace(",", ".");

                }
                if (buffChb.Length > 1)
                {
                    revalue += "," + buffChb[1];
                }
                if (revalue == "0") return "_";
                return revalue;
            }
            catch (Exception ex)
            { return ""; }
        }
        public static string ToComaDV(object obj)
        {
            try
            {
                if (obj.ToString().Length == 0) return "";

                Double strvalue = Convert.ToDouble(obj);
                string revalue = strvalue.ToString("#,###0.#####");

                return revalue;
            }
            catch (Exception ex)
            { return ""; }
        }
        public static string ToComaRP(object obj)
        {
            try
            {
                if (obj.ToString().Length == 0) return "0";

                Double strvalue = Convert.ToDouble(obj);
                string revalue = strvalue.ToString("#,###0.#####");

                return revalue;
            }
            catch (Exception ex)
            { return ""; }
        }
        public static string ToComaHD(object obj)
        {
            try
            {
                if (obj.ToString().Length == 0) return "0";

                Double strvalue = Convert.ToDouble(obj);
                string revalue = strvalue.ToString("#,####0");
                revalue = revalue.Replace(",", ".");
                return revalue;
            }
            catch (Exception ex)
            { return ""; }
        }
        public static int ToInt(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            int val = 0;
            try
            {
                val = Convert.ToInt32(obj);

            }
            catch
            {

            }
            return val;
        }

        public static int ToInt(object obj, int value2)
        {
            if (obj == null || obj.ToString().Length == 0) return value2;
            return Convert.ToInt32(obj);
        }
        public static Decimal ToDecimalNoS(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            Decimal val = 0;
            try
            {
                val = Convert.ToDecimal(obj);
                if (val < 0)
                {
                    val = -val;
                }
            }
            catch
            {

            }
            return val;
        }
        public static Decimal ToDecimal(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            Decimal val = 0;
            try
            {
                val = Convert.ToDecimal(obj);

            }
            catch
            {

            }
            return val;
        }
        public static string ToDecimal00(object obj)
        {
            if (obj.ToString().Length == 0) return "";

            Double strvalue = Convert.ToDouble(obj);
            string revalue = strvalue.ToString("#0.00");

            return revalue;
        }


        public static double ToDouble(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            double val = 0;
            try
            {
                val = Convert.ToDouble(obj);
            }
            catch
            {

            }

            return val;
        }
        public static string ToStr(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return "";
            if (Convert.ToString(obj) == "True")
                return "1";
            if (Convert.ToString(obj) == "False")
                return "0";
            return Convert.ToString(obj);
        }
        public static string ToFeature(string obj)
        {
            if (obj != "")
            {
                string[] arrdes = obj.Split('?');
                string vvalue = arrdes[0];
                return vvalue.Replace("|", " : ");
            }
            else
                return "";
        }
        public static string ToStr(object obj, string value2)
        {
            if (obj == null || obj.ToString().Length == 0) return value2;
            return Convert.ToString(obj);
        }
        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return DateTime.Now;
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " : " + obj.ToString());
            }

            return DateTime.Now;
        }
        public static DateTime? ToDateTimenull(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return null;
            return DateTime.Parse(obj.ToString());
        }

        public static Boolean ToBoolean(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return false;
            if (ToStr(obj).Equals("on"))
                return true;
            else if (ToStr(obj).Equals("off"))
                return false;
            else if (ToStr(obj).Equals("1"))
                return true;
            else if(ToStr(obj).Equals("0"))
                return false;
            return Convert.ToBoolean(obj);
        }

        public static string ToYYYYMMDD(object ob)
        {
            DateTime dt;
            dt = ToDateTime(ob);
            return dt.ToString("yyyyMMdd");
        }
        public static string ToYYYY_MM_DD(object ob)
        {
            DateTime dt;
            dt = ToDateTime(ob);
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ToMM_DD_YYYY(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("MM/dd/yyyy");
            }
            else
                return "";

        }

        public static string ToDD_MM_YYYY(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("dd/MM/yyyy");
            }
            else
                return "";

        }
        public static string ToDD_MM_YYYY_HH_mm_SS(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("dd/MM/yyyy hh:mm:ss");
            }
            else
                return "";

        }

        public static string ToStringMM_DD_YYYY(string ob)
        {

            if (ToStr(ob) != "")
            {

                double dt;
                dt = ToDouble(ob);
                return dt.ToString("0#/##/####");
            }
            else
                return "";

        }

        public static string ToMMDDYYYY(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("MMddyyyy");
            }
            else
                return "";

        }
        public static string ToYYYYMMDDNew(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("yyyy-MM-dd");
            }
            else
                return "";

        }
        public static string ToMMDD(object ob)
        {
            if (ToStr(ob) != "")
            {
                DateTime dt;
                dt = ToDateTime(ob);
                return dt.ToString("MMdd");
            }
            else
                return "";

        }

        public static short ToShort(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            short val = 0;
            try
            {
                val = Convert.ToInt16(obj);

            }
            catch
            {
                val = 0;
            }
            return val;
        }
        public static long ToLong(object obj)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            long val = 0;
            try
            {
                val = Convert.ToInt64(obj);

            }
            catch
            {
                val = 0;
            }
            return val;
        }
        public static string ToPrice(double price)
        {
            if (price > 0)
            {
                return price.ToString("#,#0,00");
            }
            else
                return "Call for price";

        }

        public static string Topaymethol(string strname)
        {
            if (strname != "")
            {
                string re = "";
                strname = strname.ToLower();
                switch (strname)
                {
                    case "check"://Company Check or Order Money
                        re = "Check";
                        break;
                    case "money":
                        re = "Money Order";
                        break;
                    case "offline":
                        re = "Offline Order";
                        break;
                    case "google":
                        re = "Google Order";
                        break;
                    case "paypal":
                        re = "Palpal Order";
                        break;
                    case "authorize":
                        re = "Authorize Card";
                        break;
                    case "credit card":
                        re = "Credit Card Card";
                        break;
                    case "purchase":
                        re = "Purchase Order";
                        break;
                }
                return re;
            }
            else
                return "";

        }

        public static string DateToStr(object obj, int type)
        {
            DateTime dt;
            dt = ToDateTime(obj);
            switch (type)
            {
                case 1: return dt.ToString("dd-MM-yyyy");
                case 2: return dt.ToString("dd-MMM-yyyy");
                case 3: return dt.ToString("MM-dd-yyyy");
                case 4: return dt.ToString("MM/dd/yyyy");
            }
            return "";
        }

        public static string ClearSQLInjection(string strIn)
        {
            strIn = strIn.Replace("'", "");
            strIn = strIn.Replace(";", "");
            strIn = strIn.Replace("--", "");
            strIn = strIn.Replace("UNION", "");
            return strIn;

        }
        /// <summary>
        /// Trích 1 so luong tu cua chuoi ban dau 
        /// </summary>
        /// <param name="strIn">Chuoi ban dau</param>
        /// <param name="len">so luong to trích ra</param>
        public static string Encrypt(string strData)
        {
            UnicodeEncoding objEncode = new UnicodeEncoding();
            byte[] btArrData = objEncode.GetBytes(strData);
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] btResult = sha.ComputeHash(btArrData);
            return Convert.ToBase64String(btResult);
        }
        public static int ToIntMonth(string month)
        {
            int m = 0;
            switch (month)
            {
                case "January":
                    m = 1;
                    break;
                case "Frebuary":
                    m = 2;
                    break;
                case "March":
                    m = 3;
                    break;
                case "Appril":
                    m = 4;
                    break;
                case "May":
                    m = 5;
                    break;
                case "June":
                    m = 6;
                    break;
                case "July":
                    m = 7;
                    break;
                case "August":
                    m = 8;
                    break;
                case "September":
                    m = 9;
                    break;
                case "October":
                    m = 10;
                    break;
                case "November":
                    m = 11;
                    break;
                case "December":
                    m = 12;
                    break;

            }
            return m;
        }

        public static string[] SplitByString(string testString, string split)
        {
            int offset = 0;
            int index = 0;
            int[] offsets = new int[testString.Length + 1];

            while (index < testString.Length)
            {
                int indexOf = testString.IndexOf(split, index);
                if (indexOf != -1)
                {
                    offsets[offset++] = indexOf;
                    index = (indexOf + split.Length);
                }
                else
                {
                    index = testString.Length;
                }
            }

            string[] final = new string[offset + 1];
            if (offset == 0)
            {
                final[0] = testString;
            }
            else
            {
                offset--;
                final[0] = testString.Substring(0, offsets[0]);
                for (int i = 0; i < offset; i++)
                {
                    final[i + 1] = testString.Substring(offsets[i] + split.Length, offsets[i + 1] - offsets[i] - split.Length);
                }
                final[offset + 1] = testString.Substring(offsets[offset] + split.Length);
            }
            return final;
        }

        public static string Encrypttest(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypttest(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string GetUrlForgotpass(string url, string id, string newpass)
        {

            return url + "/" + id + "/" + newpass + "/";
        }
        public static string EncryptData(string sid)
        {
            return DecToHex(sid) + "0A1XB";
        }
        public static int DecryptData(string str)
        {
            string res = str.Substring(0, str.Length - 5);
            return Convert.ToInt32(res, 16);
        }

        public static string DecToHex(string number)
        {
            if (ToInt(number) == 0)
                return "0";
            int dec = Convert.ToInt32(number);
            string result = "";
            while (dec > 0)
            {
                int sodu = dec % 16;
                switch (sodu)
                {
                    case 10:
                        result = 'A' + result;
                        break;
                    case 11:
                        result = 'B' + result;
                        break;
                    case 12:
                        result = 'C' + result;
                        break;
                    case 13:
                        result = 'D' + result;
                        break;
                    case 14:
                        result = 'E' + result;
                        break;
                    case 15:
                        result = 'F' + result;
                        break;
                    default:
                        result = sodu.ToString() + result;
                        break;
                }
                dec = dec / 16;
            }
            return result;
        }
        public string EncryptBase(string input)
        {

            byte[] source;
            int length, length2;
            int blockCount;
            int paddingCount;

            if (input == null || input.Length == 0)
            {
                return String.Empty;
            }

            source = System.Text.Encoding.ASCII.GetBytes(input); ;
            length = input.Length;

            if ((length % 3) == 0)
            {
                paddingCount = 0;
                blockCount = length / 3;
            }
            else
            {
                paddingCount = 3 - (length % 3);//need to add padding
                blockCount = (length + paddingCount) / 3;
            }
            length2 = length + paddingCount;//or blockCount *3
            byte[] source2;
            source2 = new byte[length2];
            //copy data over insert padding
            for (int x = 0; x < length2; x++)
            {
                if (x < length)
                {
                    source2[x] = source[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte b1, b2, b3;
            byte temp, temp1, temp2, temp3, temp4;
            byte[] buffer = new byte[blockCount * 4];
            char[] result = new char[blockCount * 4];
            for (int x = 0; x < blockCount; x++)
            {
                b1 = source2[x * 3];
                b2 = source2[x * 3 + 1];
                b3 = source2[x * 3 + 2];

                temp1 = (byte)((b1 & 252) >> 2);//first

                temp = (byte)((b1 & 3) << 4);
                temp2 = (byte)((b2 & 240) >> 4);
                temp2 += temp; //second

                temp = (byte)((b2 & 15) << 2);
                temp3 = (byte)((b3 & 192) >> 6);
                temp3 += temp; //third

                temp4 = (byte)(b3 & 63); //fourth

                buffer[x * 4] = temp1;
                buffer[x * 4 + 1] = temp2;
                buffer[x * 4 + 2] = temp3;
                buffer[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < blockCount * 4; x++)
            {
                result[x] = sixbit2char(buffer[x]);
            }

            //covert last "A"s to "=", based on paddingCount
            switch (paddingCount)
            {
                case 0: break;
                case 1: result[blockCount * 4 - 1] = '='; break;
                case 2:
                    result[blockCount * 4 - 1] = '=';
                    result[blockCount * 4 - 2] = '=';
                    break;
                default: break;
            }
            return new String(result);
        }

        private char sixbit2char(byte b)
        {
            char[] lookupTable = new char[64]
                    {
                        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                        '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return lookupTable[(int)b];
            }
            else
            {
                //should not happen;
                return ' ';
            }
        }

        public string DecryptBase(string input)
        {
            char[] source;
            int length, length2, length3;
            int blockCount;
            int paddingCount;
            int temp = 0;

            if (input == null || input.Length == 0)
            {
                return String.Empty;
            }

            source = input.ToCharArray();
            length = input.Length;

            //find how many padding are there
            for (int x = 0; x < 2; x++)
            {
                if (input[length - x - 1] == '=')
                    temp++;
            }
            paddingCount = temp;
            //calculate the blockCount;
            //assuming all whitespace and carriage returns/newline were removed.
            blockCount = length / 4;
            length2 = blockCount * 3;

            byte[] buffer = new byte[length];//first conversion result
            byte[] buffer2 = new byte[length2];//decoded array with padding

            for (int x = 0; x < length; x++)
            {
                buffer[x] = char2sixbit(source[x]);
            }

            byte b, b1, b2, b3;
            byte temp1, temp2, temp3, temp4;

            for (int x = 0; x < blockCount; x++)
            {
                temp1 = buffer[x * 4];
                temp2 = buffer[x * 4 + 1];
                temp3 = buffer[x * 4 + 2];
                temp4 = buffer[x * 4 + 3];

                b = (byte)(temp1 << 2);
                b1 = (byte)((temp2 & 48) >> 4);
                b1 += b;

                b = (byte)((temp2 & 15) << 4);
                b2 = (byte)((temp3 & 60) >> 2);
                b2 += b;

                b = (byte)((temp3 & 3) << 6);
                b3 = temp4;
                b3 += b;

                buffer2[x * 3] = b1;
                buffer2[x * 3 + 1] = b2;
                buffer2[x * 3 + 2] = b3;
            }
            //remove paddings
            length3 = length2 - paddingCount;
            byte[] result = new byte[length3];

            for (int x = 0; x < length3; x++)
            {
                result[x] = buffer2[x];
            }

            return System.Text.Encoding.UTF8.GetString(result);
        }

        private byte char2sixbit(char c)
        {
            char[] lookupTable = new char[64]
            {
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                '0','1','2','3','4','5','6','7','8','9','+','/'};
            if (c == '=')
                return 0;
            else
            {
                for (int x = 0; x < 64; x++)
                {
                    if (lookupTable[x] == c)
                        return (byte)x;
                }
                //should not reach here
                return 0;
            }
        }
        public static Decimal ToRoundNum(object obj, bool isroundnum)
        {
            if (obj == null || obj.ToString().Length == 0) return 0;
            Decimal val = 0;
            try
            {
                if (isroundnum)
                {
                    obj = ToLong(obj);
                }
                val = Convert.ToDecimal(obj);

            }
            catch
            {

            }
            return val;
        }
        public static string ToRoundNumComa(object obj, bool isroundnum)
        {
            if (obj == null || obj.ToString().Length == 0) return "0";
            string val = "0";
            try
            {
                if (isroundnum)
                {
                    obj = ToLong(obj);
                }
                val = ToComa(obj);

            }
            catch
            {

            }
            return val;
        }
    }
}
