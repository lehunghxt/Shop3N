using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service
{
   public class DrawTable
    {
        private String headerTable;
        private String footerTable;
        private String nameOfAjaxFuction;
        private String nullData;
        private Boolean collapsable = false;
        private string html = "";

        public Boolean Collapsable
        {
            get { return collapsable; }
            set { collapsable = value; }
        }

        public String NullData
        {
            get { return nullData; }
            set { nullData = value; }
        }


        public String FooterTable
        {
            get { return footerTable; }
            set { footerTable = value; }
        }


        public string NameOfAjaxFuction
        {
            get { return nameOfAjaxFuction; }
            set { nameOfAjaxFuction = value; }
        }

        public String HeaderTable
        {
            get { return headerTable; }
            set { headerTable = value; }
        }

        public DrawTable(string titleTable, List<string> cols, int width, string ajax)
        {
            nameOfAjaxFuction = ajax;
            if (collapsable == true)
            {
                html = @" <div class='box-tools pull-right'>
							    <button class='btn btn-box-tool' data-widget='collapse'><i class='fa fa-minus'></i></button>
						        </div><!-- /.box-tools -->";
            }
            headerTable = @" 
                                <div class='box box-warning'> <!-- .box - info -->
                                    <div class='box-body'>
                                     <div id='example2_wrapper' > <!-- /.example3423_wrapper -->
                                            <div class='row'><div class='col-sm-12'>   <!-- .row va  col-sm-12 -->
                                                <table id='example2' class='table table-bordered table-hover'>
                                                    <thead>
                                                      <tr>";
            foreach (string col in cols)
            {
                headerTable += @"<th>" + col.ToString() + @"</th>";
            }

            headerTable += @"</tr>
                                                    </thead>
                                                    <tbody>";
            footerTable = @" 
                                                    </tbody>
                                                 </table> 
                                        </div></div> <!-- .row va  col-sm-12 -->";

            nullData = headerTable + @"<tr><td colspan='" + cols.Count() + @"'><div class='padding_content'>Không tìm thấy dòng nào phù hợp</div> </td></tr>" + footerTable;
        }


        public string drawFooter(int vitri, int vitri1, int tong, string strPagePre, string strPage, string strPageNext)
        {
            string str = "";
            str += @"
                                        <div class='row'> <!--  row paging-->
                                              <div class='col-sm-5'> <!--  col-sm-5-->
                                                <div class='dataTables_info' id='example223_info' role='status' aria-live='polite' style='padding-top: 8px;'>
                                                                 Trang
                                        " + CGlobal.ToStr(vitri) + @"
                                        /
                                        " + CGlobal.ToStr(vitri1) + @"
                                        (" + CGlobal.ToStr(tong) + @"
                                        Dòng)
                                                </div>
                                              </div><!--  /col-sm-5-->

                                              <div class='col-sm-7'> <!--  col-sm-7-->
                                                        
                                                  <div class='dataTables_paginate paging_simple_numbers' id='example2_paginate'>
                                                      <ul class='pagination' style='margin-bottom: 2px;margin-top: 2px;'>            
                                                          " + CGlobal.ToStr(strPagePre) + @"
                                                          " + CGlobal.ToStr(strPage) + @"            
                                                          " + CGlobal.ToStr(strPageNext) + @"
                       
                                                      </ul>               
                                                 </div>  
                                              </div> <!--  /col-sm-7-->
                                        </div><!-- / .row paging-->
                                    </div>  <!-- /.example3423_wrapper -->
                               </div><!-- /.box-body -->
                            </div><!-- /..box - info  -->"
                ;
            return str;
        }

        public string pagingFooterForGroupAdviser(int count, int page, int perpage, int Pagesize, string IdGroup)
        {
            int tong = count;
            int vitri = 0;
            int vitri1 = 0;
            string strPage = "";
            string strPagePre = "";
            string strPageNext = "";

            if (page < 1)
                page = 1;

            if (tong <= perpage)
            {
                return "";
            }
            int i = 1;

            int totalPage = count / perpage;
            if (count % perpage != 0)
            {
                totalPage = (count / perpage) + 1;
            }
            for (int j = 0; j <= totalPage / Pagesize; j++)
            {
                vitri1 = totalPage;
                vitri = page;
                tong = count;
                if (vitri1 == 0) vitri1 = 1;
                int k = 0;
                k = j * Pagesize;
                for (int a = i; a <= k + Pagesize && a <= totalPage; a++)
                {
                    i = 1;
                    if (j == 0) i = k + Pagesize;
                    else i = k + Pagesize;
                    if (i > page && k <= page)
                    {
                        if (page == a)
                        {
                            strPage += @"<li class='paginate_button active'><a href='#' aria-controls='example" + a.ToString() + @"' data-dt-idx='1' tabindex='0'>"
                                             + a.ToString() + @" </a></li>";

                        }
                        else
                        {
                            strPage += String.Format(@"<li class='paginate_button'><a href='#' aria-controls='example" + a.ToString() + @"' data-dt-idx='1' tabindex='0'
                                            onclick='return " + NameOfAjaxFuction + @"(2,{0},{1})' >"
                                 + a.ToString() + @"</a></li>", (a), IdGroup);
                        }
                    }
                }
                if (page >= 2)
                {

                    strPagePre = String.Format(@"<li class= 'paginate_button previous active' id='example2_previous'>
                                   <a href='#' aria-controls='example2' data-dt-idx=1 tabindex=0 onclick='return " + nameOfAjaxFuction + @"(2,{0},{1})'>" + "&nbsp;"
                                   + @"Previous</a></li>", (page - 1), IdGroup);
                }
                else
                {
                    strPagePre = @"<li class='paginate_button previous disabled' id='example2_previous'> <a href='#' aria-controls='example2' data-dt-idx='0' tabindex='0'>Previous</a></li>";
                }
                if (page < totalPage)
                {
                    strPageNext = String.Format(@"<li class='paginate_button next'>
                                   <a href='#' aria-controls=example34 data-dt-idx=1 tabindex=0 onclick='return " + nameOfAjaxFuction + @"(2,{0},{1})' >" + "&nbsp;"
                                  + @"Next</a></li>", (page + 1), IdGroup);
                }
                else
                {
                    strPageNext = @"<li class='paginate_button next disabled'><a href='#' aria-controls='example2' data-dt-idx='0' tabindex='0'>Next</a></li>";
                }
            }

            return drawFooter(vitri, vitri1, tong, strPagePre, strPage, strPageNext);
        }


        public string pagingFooter(int count, int page, int perpage, int Pagesize, int Id)
        {

            int tong = count;
            int vitri = 0;
            int vitri1 = 0;
            string strPage = "";
            string strPagePre = "";
            string strPageNext = "";

            if (page < 1)
                page = 1;
            if (tong <= perpage)
            {
                return "";
            }

            int i = 1;

            int totalPage = count / perpage;
            if (count % perpage != 0)
            {
                totalPage = (count / perpage) + 1;
            }
            for (int j = 0; j <= totalPage / Pagesize; j++)
            {
                vitri1 = totalPage;
                vitri = page;
                tong = count;
                if (vitri1 == 0) vitri1 = 1;
                int k = 0;
                k = j * Pagesize;
                for (int a = i; a <= k + Pagesize && a <= totalPage; a++)
                {
                    i = 1;
                    if (j == 0) i = k + Pagesize;
                    else i = k + Pagesize;
                    if (i > page && k <= page)
                    {
                        if (page == a)
                        {
                            strPage += @"<li class='paginate_button active'><a href='#' aria-controls='example" + a.ToString() + @"' data-dt-idx='1' tabindex='0'>"
                                             + a.ToString() + @" </a></li>";

                        }
                        else
                        {
                            strPage += String.Format(@"<li class='paginate_button'><a href='#' aria-controls='example" + a.ToString() + @"' data-dt-idx='1' tabindex='0'
                                            onclick='return " + nameOfAjaxFuction + @"({0},{1},1)' >"
                                 + a.ToString() + @"</a></li>", (a), (Id));
                        }
                    }
                }
                if (page >= 2)
                {

                    strPagePre = String.Format(@"<li class= 'paginate_button previous' id='example2_previous'>
                                   <a href='#' aria-controls='example2' data-dt-idx=1 tabindex=0 onclick='return " + nameOfAjaxFuction + @"({0},{1},1)'>" + "&nbsp;"
                                   + @"Previous</a></li>", (page - 1), (Id));
                }
                else
                {
                    strPagePre = @"<li class='paginate_button previous disabled' id='example2_previous'> <a href='#' aria-controls='example2' data-dt-idx='0' tabindex='0'>Previous</a></li>";
                }
                if (page < totalPage)
                {
                    strPageNext = String.Format(@"<li class='paginate_button next'>
                                   <a href='#' aria-controls=example34 data-dt-idx=1 tabindex=0 onclick='return " + nameOfAjaxFuction + @"({0},{1},1)' >" + "&nbsp;"
                                  + @"Next</a></li>", (page + 1), (Id));
                }
                else
                {
                    strPageNext = @"<li class='paginate_button next disabled'><a href='#' aria-controls='example2' data-dt-idx='0' tabindex='0'>Next</a></li>";
                }
            }

            return drawFooter(vitri, vitri1, tong, strPagePre, strPage, strPageNext);

        }
    }
}
