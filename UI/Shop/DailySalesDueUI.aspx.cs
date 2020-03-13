﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ABLL;
using DTO;
using PBLL.Page_ObjectBLL;
using System.Web.Security;

namespace UI.Shop
{
    public partial class DailySalesDueUI : System.Web.UI.Page
    {
        InvenSalesBLL salBLL = new InvenSalesBLL();
        BLL.ReportBLL reportBLL = new BLL.ReportBLL();
        LoginBLL LBLL = new LoginBLL();
        PageObjectRoleBLL PObjRoleBLL = new PageObjectRoleBLL();
        InvenSalesInputBLL InBLL = new InvenSalesInputBLL();
        InvenSalesInputDTO InDTO = new InvenSalesInputDTO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SearchDate();
                RoleName();
            }
        }
        public void RoleName()
        {

            string empusername = HttpContext.Current.User.Identity.Name;

            var role = LBLL.GetRoleName_By_User(empusername);
            int roleid = role.First().RoleId;

            var loadPage = PObjRoleBLL.Page_ObjectRole(0, roleid, "", "");
            int count = loadPage.Count;

            int matcheddata = 0;
            for (int i = 0; i < count; i++)
            {
                if (HttpContext.Current.Request.Url.AbsolutePath == loadPage[i].PagePath.ToString())
                {
                    matcheddata = matcheddata + 1;
                }
            }
            if (matcheddata == 1)
            {
            }
            else
            {
                FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket("", true, 0);
                string hash1 = FormsAuthentication.Encrypt(ticket1);
                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, hash1);
                cookie1.Expires = DateTime.Now.AddMinutes(0);

                if (ticket1.IsPersistent)
                    cookie1.Expires = ticket1.Expiration;

                Response.Cookies.Add(cookie1);
                Response.Redirect(" LoginUI.aspx");
            }
        }
        void SearchDate()
        {

            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = salBLL.LoadSalesDateDue(txtDateFrom.Text, txtDateTo.Text);
            objPds.AllowPaging = true;
            objPds.PageSize = 8;
            int CurPage;
            if (Request.QueryString["Page"] != null)
                CurPage = Convert.ToInt32(Request.QueryString["Page"]);
            else
                CurPage = 1;


            objPds.CurrentPageIndex = CurPage - 1;
            lblCurrentPage.Text = "Page: " + CurPage.ToString();

            if (!objPds.IsFirstPage)
                lnkPrev.NavigateUrl = Request.CurrentExecutionFilePath
                + "?Page=" + Convert.ToString(CurPage - 1);

            if (!objPds.IsLastPage)
                lnkNext.NavigateUrl = Request.CurrentExecutionFilePath
                + "?Page=" + Convert.ToString(CurPage + 1);

            RptMainHead.DataSource = objPds;
            RptMainHead.DataBind();
            // ddlMember.SelectedValue=
        }
        protected void LinkButton_Sale(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Shop/Report/DailySalesReportRpt.aspx?date=" + e.CommandArgument.ToString() + "&TotalSale=0 &GuestCharge=0&card=0&ExtraBill=0&GouestChargePlus=0&TotalAmount=0&BarSale=0&CateringSale=0&BekarySale=0&RestuarentSale=0&GuestSale=0&quan=0');", true);
        }
        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            // var data = reportBLL.LoadDailySale(e.CommandArgument.ToString());
            txtSaleDate.Text = Convert.ToDateTime(e.CommandArgument.ToString()).ToShortDateString();
            double? sale1, sale2;
            var data = salBLL.LoadDailySaleDue(txtSaleDate.Text);
            var dataCock = salBLL.LoadDailySaleCocktailDue(txtSaleDate.Text);
            var dataWCock = salBLL.LoadDailySaleWihoutCocktailDue(txtSaleDate.Text);
            if (dataCock.Count > 0)
                sale1 = dataCock.Sum(o => o.Quantity);
            else
                sale1 = 0;
            if (dataWCock.Count > 0)
                sale2 = dataWCock.Sum(o => o.Quantity);
            else
                sale2 = 0;

            HFQuantity.Value = (sale1 + sale2).ToString();
            txtBarBill.Text = data.Sum(o => o.PaidAmount).ToString();
            var dataRes = salBLL.LoadDailyResturentDue(txtSaleDate.Text);
            if (dataRes.Count > 0)
                txtRestuarentBill.Text = dataRes.First().PaidAmount.ToString();
            var dataCate = salBLL.LoadDailyCateringDue(txtSaleDate.Text);
            if (dataCate.Count > 0)
                txtCateringBill.Text = dataCate.First().PaidAmount.ToString();
            var dataBekar = salBLL.LoadDailyBekaryDue(txtSaleDate.Text);
            if (dataBekar.Count > 0)
                txtBekaryBill.Text = dataBekar.First().PaidAmount.ToString();
            var dataGuest = salBLL.LoadDailyGuestChargeDue(txtSaleDate.Text);
            if (dataGuest.Count > 0)
                txtGuestChargeBill.Text = dataGuest.Sum(o => o.PaidAmount).ToString();
            txtTotalSale.Text = (double.Parse(txtBarBill.Text) + double.Parse(txtRestuarentBill.Text) + double.Parse(txtCateringBill.Text) + double.Parse(txtBekaryBill.Text) + double.Parse(txtGuestChargeBill.Text)).ToString();


            //  txtTotalSale.Text=
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDate();
        }

        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            // Response.Redirect("/Shop/Report/RatingOfGoods_BranchWiseRptUI.aspx?brid=" + brid.ToString());
            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Shop/Report/DailySalesDueReportRpt.aspx?date=" + txtSaleDate.Text + "&TotalSale="
                + txtTotalSale.Text + "&GuestCharge=" + txtGuestChargeBill.Text + "&card=" + "0" + "&ExtraBill=" + "0"
                + "&GouestChargePlus=" + "0" + "&TotalAmount=" + "0" + "&BarSale=" + txtBarBill.Text + "&CateringSale=" + txtCateringBill.Text + "&BekarySale=" + txtBekaryBill.Text + "&RestuarentSale=" + txtRestuarentBill.Text + "&GuestSale=" + "0" + "&quan=" + HFQuantity.Value + "');", true);
        }
    }
}