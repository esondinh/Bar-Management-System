﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.CompProfile;
using System.Web.Security;
using ABLL;
using PBLL.Page_ObjectBLL;

namespace UI.Manager
{
    public partial class CentralAllChallenViewUI : System.Web.UI.Page
    {
        InvenCentralChallenBLL ChBLL = new InvenCentralChallenBLL();
        BranchProfileBLL BrBLL = new BranchProfileBLL();
        LoginBLL LBLL = new LoginBLL();
        PageObjectRoleBLL PObjRoleBLL = new PageObjectRoleBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RoleName();
                Page.Title = "Challen View";
                LaodBranch();
                LoadNeedToReturn();


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
        private void LaodBranch()
        {
            var query = BrBLL.LoadBrProfileInfo(0, "", "", "");
            ddlBranch.DataSource = query.OrderBy(Items => Items.BrProName);
            ddlBranch.DataTextField = "BrProName";
            ddlBranch.DataValueField = "BrProId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
        }
        public static void Show(string message)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                message = message.Replace("'", "\'");
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');", true);
            }
        }
        public void Search()
        {

            if (ddlBranch.SelectedValue == "0" && txtChallanNo.Text == "" && txtDateTo.Text == "" && txtDateFrom.Text == "")
            {
                Show("Search By Valid Data");
            }
            else
            {
                int chno = 0;
                if (txtChallanNo.Text.ToString() != "")
                {
                    chno = Convert.ToInt32(txtChallanNo.Text);
                }
                else chno = 0;
                var collection = ChBLL.ChallenView_RptUI(chno, Convert.ToInt32(ddlBranch.SelectedValue.ToString()), txtDateFrom.Text.ToString(), txtDateTo.Text.ToString());

                if (collection.Count == 0)
                {
                    Show("No Data Found !");
                    return;
                }
                GVCOA.DataSource = collection;
                GVCOA.DataBind();





            }
        }
        public void LoadNeedToReturn()
        {

            var data = ChBLL.ChallenView_RptUI(0, 0, "", "");
            GVCOA.DataSource = data;
            GVCOA.DataBind();

        }

        protected void GVCOA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GVCOA.PageIndex = e.NewPageIndex;
            LoadNeedToReturn();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {
            GVCOA.SelectedIndex = -1;
            txtDateTo.Text = "";
            txtDateFrom.Text = "";
            txtChallanNo.Text = "";
            ddlBranch.SelectedValue = "0";
            LoadNeedToReturn();
        }

        protected void GVCOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ChallanId = Convert.ToInt32(GVCOA.DataKeys[GVCOA.SelectedIndex].Values["ChallanId"].ToString());
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Manager/Report/ChallanBranchRptUI.aspx?ChallanId=" + ChallanId.ToString() + "');", true);
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Shop/Report/BranchReturnProductRptUI.aspx?retunno=" + ReturnNo.ToString() + "');", true);

        }
    }
}