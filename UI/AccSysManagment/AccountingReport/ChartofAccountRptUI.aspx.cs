﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABLL;
using Microsoft.Reporting.WebForms;

namespace UI.AccSysManagment.AccountingReport
{
    public partial class ChartofAccountRptUI : System.Web.UI.Page
    {

        MainHeadBLL MBLL = new MainHeadBLL();
        SubCode_1BLL S1BLL = new SubCode_1BLL();
        SubCode_2BLL S2BLL = new SubCode_2BLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Title = "Chart Of Account";
                LoadMainHead();
                LoadSubCodeId1();
                LoadSubCodeId2();
                string mainheadid = Request.QueryString["mainheadid"];
                string subcode1id = Request.QueryString["subcode1id"];
                string subcode2id = Request.QueryString["subcode2id"];

                if (mainheadid != null)
                {

                    ddlMainHead.SelectedValue = mainheadid.ToString();
                    if (subcode1id != null)
                    {
                        ddlSubCode1.SelectedValue = subcode1id.ToString();
                        if (subcode2id != null)
                        {
                            ddlSubCode2.SelectedValue = subcode2id.ToString();
                        }
                    }

                }

             
                Loaddata();
            }
        }
       
        public void Loaddata()
        {
            ReportViewer1.Visible = true;

            ReportParameter p1 = new ReportParameter("mainheadid");
            ReportParameter p2 = new ReportParameter("subcode1id");
            ReportParameter p3 = new ReportParameter("subcode2id");
        

            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3});
          //  this.ReportViewer1.LocalReport.Refresh();
        }
        private void LoadMainHead()
        {
            var query = MBLL.LoadMainHead(0);
            ddlMainHead.DataSource = query;
            ddlMainHead.DataTextField = "MainHeadName_Num";
            ddlMainHead.DataValueField = "MainHeadId";
            ddlMainHead.DataBind();
            ddlMainHead.Items.Insert(0, new ListItem("Select One", "0"));
        }
        private void LoadSubCodeId1()
        {
            var query = S1BLL.LoadSuvCode_1Data(0, "", "", Convert.ToInt32(ddlMainHead.SelectedValue),"","");
            ddlSubCode1.DataSource = query;
            ddlSubCode1.DataTextField = "SubCode1Name_Num";
            ddlSubCode1.DataValueField = "SubCode_1Id";
            ddlSubCode1.DataBind();
            ddlSubCode1.Items.Insert(0, new ListItem("Select One", "0"));
        }
        private void LoadSubCodeId2()
        {
            var query = S2BLL.LoadSuvCode_2Data(0,"", "", Convert.ToInt32(ddlSubCode1.SelectedValue),"","","","");
            ddlSubCode2.DataSource = query;
            ddlSubCode2.DataTextField = "SubCode2Name_Num";
            ddlSubCode2.DataValueField = "SubCode_2Id";
            ddlSubCode2.DataBind();
            ddlSubCode2.Items.Insert(0, new ListItem("Select One", "0"));
        }

        protected void ddlMainHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId1();
        }
        protected void ddlSubCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId2();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Loaddata();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            LoadMainHead();
            LoadSubCodeId1();
            LoadSubCodeId2();
            this.ReportViewer1.LocalReport.Refresh();
        }


    }
}