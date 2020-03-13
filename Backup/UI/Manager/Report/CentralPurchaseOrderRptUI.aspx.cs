﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace UI.Manager.Report
{
    public partial class CentralPurchaseOrderRptUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string purorderid = Request.QueryString["purorderid"];
                if (purorderid != null)
                {
                    HFRptdtlId.Value = purorderid.ToString();
                }

                if (HFRptdtlId.Value != null)
                {
                    ReportViewer1.Visible = true;
                    ReportParameter p1 = new ReportParameter("purorderid");
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    }
}