using System;
using System.Data;

namespace Library.Web
{
    public class MenuTree
    {
        private string _Tree = "";
        private string _Srcipt = "";

        private string _IcoCha = "+ &nbsp;&nbsp;";
        private string _IcoCon = "- &nbsp;";

        private int _GioiHan = 1000;
        private string _StyleCha = "";
        private string _StyleXemTiep = "";
        private string _StyleCon = "";
        private string _StyleTroLai = "";

        private string _XemTiep = "Xem tiếp &nbsp; ->";
        private string _TroLai = "<- &nbsp; Trở lại";

        //even
        private string _TreeClick = "<script type='text/javascript'>function TreeClick(id){alert(id);}</script>";

        private DataTable _Table=null;

        public MenuTree(DataTable dt)
        {
            dt.Select("", "ID_Group");
            InitSrcipt();
        }

        public string Tree
        {
            get { return PtTree(); }
        }
        public string Script
        {
            get { return this._Srcipt; }
        }

        public string IcoCha
        {
            set { this._IcoCha = value; }
        }
        public string IcoCon
        {
            set { this._IcoCon = value; }
        }

        public int GioiHan
        {
            set { this._GioiHan = value; }
        }
        public string StyleCha
        {
            set { this._StyleCha = value; }
        }
        public string StyleXemTiep
        {
            set { this._StyleXemTiep = value; }
        }
        public string StyleCon
        {
            set { this._StyleCon = value; }
        }
        public string StyleTroLai
        {
            set { this._StyleTroLai = value; }
        }

        public string XemTiep
        {
            set { this._XemTiep = value; }
        }
        public string TroLai
        {
            set { this._TroLai = value; }
        }

        public string TreeClick
        {
            set { this._TreeClick = value; }
        }
        

        protected void InitSrcipt()
        {
            _Srcipt += "<script type='text/javascript'>";
                _Srcipt += "function clickmnu(id)";
                _Srcipt += "{";
                    _Srcipt += "if(document.getElementById(id).style.display == 'none') ";
                    _Srcipt += "document.getElementById(id).style.display = 'block'; ";
                    _Srcipt += "else document.getElementById(id).style.display = 'none'; ";
                _Srcipt += "}";

                _Srcipt += "function clicktiep(idm)";
                _Srcipt += "{";
                    _Srcipt += "if(document.getElementById('mnucontiep'+idm).style.display == 'none')";
                    _Srcipt += "{";
                        _Srcipt += "document.getElementById('mnucontiep'+idm).style.display = 'block';";
                        _Srcipt += "document.getElementById('tiep'+idm).style.display = 'none';";
                        _Srcipt += "document.getElementById('lui'+idm).style.display = 'block';";
                        _Srcipt += "document.getElementById('mnucondau'+idm.split('_',1)).style.display = 'none';";
                    _Srcipt += "}";
                    _Srcipt += "else ";
                    _Srcipt += "{";
                        _Srcipt += "document.getElementById('mnucontiep'+idm).style.display = 'none';";
                        _Srcipt += "document.getElementById('tiep'+idm).style.display = 'block';";
                        _Srcipt += "document.getElementById('lui'+idm).style.display = 'none';";
                        _Srcipt += "document.getElementById('mnucondau'+idm.split('_',1)).style.display = 'block';";
                    _Srcipt += "}";
                _Srcipt += "}";
            _Srcipt += "</script>";
            _Srcipt += _TreeClick;
        }

        protected string PtTree()
        {
            _Tree = "<div>";
            string vitri = "";
            int an;
            bool antrue = false;

            int n = _Table.Rows.Count - 1;
            int i = 0;
            for (i = 0; i < n; i++)
            {
                if (_Table.Rows[i + 1]["ID_Group"].ToString().Length > 3) //co nhom con
                {
                    an = 0;
                    vitri = i.ToString();
                    _Tree += "<div><a href='#" + _Table.Rows[i]["ID_Group"] + "' onclick=\"clickmnu('mnucon" + i + "');\" style='"+ _StyleCha +"'>"+ _IcoCha + _Table.Rows[i]["tennhom"].ToString() + "</a></div>";  // cha
                    _Tree += "<div id='mnucon" + i + "' style='display:none;padding-left:20px'>";   // mo div an chua nhom con
                    _Tree += "<div id='mnucondau" + vitri + "' style='display:block'>";
                    do
                    {
                        an++;
                        if (an > _GioiHan)
                        {
                            if (antrue == false)
                            {
                                i++;
                                vitri += "_" + i;
                                _Tree += "<div id='tiep" + vitri + "' onclick='clicktiep(\"" + vitri + "\")' style='"+ _StyleXemTiep +"'><a href='#mnu' style='font-size:13px'>"+ _XemTiep +"</a></div>";
                                antrue = true;
                                _Tree += "</div>";  // dong div con dau                               
                                _Tree += "<div id='mnucontiep" + vitri + "' style='display:none;margin-top:6px;'>"; // mo div mo rong them nhom con
                            }
                            _Tree += "<div><a href='#" + _Table.Rows[i]["ID_Group"] + "' onclick=\"TreeClick(" + _Table.Rows[i]["ID"] + ");\" style='" + _StyleCon + "' >" + _IcoCon + _Table.Rows[i++]["tennhom"].ToString() + "</a></div>";
                        }
                        else _Tree += "<div><a href='#" + _Table.Rows[++i]["ID_Group"] + "' onclick=\"TreeClick(" + _Table.Rows[i]["ID"] + ");\" style='" + _StyleCon + "' >" + _IcoCon + _Table.Rows[i]["tennhom"].ToString() + "<a></div>";
                    }
                    while (i < _Table.Rows.Count - 1 && _Table.Rows[i + 1]["ID_Group"].ToString().Length > 3);   //het nhom con
                    antrue = false;
                    if (an > _GioiHan)
                    {
                        _Tree += "<div id='lui" + vitri + "' onclick='clicktiep(\"" + vitri + "\")' style='display:none;"+ _StyleTroLai +"'><a href='#mnu' style='font-size:13px'>"+ _TroLai +"</a></div>";
                    }
                    _Tree += "</div>";  // dong div mo rong hoac dau
                    _Tree += "</div>";  // dong div nhom con
                }
                else _Tree += "<div><a href='#" + _Table.Rows[i]["ID_Group"] + "' onclick=\"TreeClick(" + _Table.Rows[i]["ID"] + ");\" style='" + _StyleCha + "'>" + _IcoCha + _Table.Rows[i]["tennhom"].ToString() + "</a></div>";
            }
            if (_Table.Rows[i]["ID_Group"].ToString().Length == 3) _Tree += "<div><a href='#" + _Table.Rows[i]["ID_Group"] + "' onclick=\"TreeClick(" + _Table.Rows[i]["ID"] + ");\" style='" + _StyleCha + "'>" + _IcoCha + _Table.Rows[i]["tennhom"].ToString() + "</a></div>";
            _Tree += "</div>";
            return this._Tree;
        }
    }
}
