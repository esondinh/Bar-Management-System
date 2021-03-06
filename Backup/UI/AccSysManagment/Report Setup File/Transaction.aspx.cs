﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABLL;
using ADTO;


namespace UI.AccSysManagment.Report_Setup_File
{
    public partial class Transaction : System.Web.UI.Page
    {
        TransitemBLL tbll = new TransitemBLL();
        TransitemDTO tdto = new TransitemDTO();
        MainHeadDTO MDTO = new MainHeadDTO();
        MainHeadBLL MBLL = new MainHeadBLL();

        SubCode_1DTO S1DTO = new SubCode_1DTO();
        SubCode_1BLL S1BLL = new SubCode_1BLL();

        SubCode_2DTO S2DTO = new SubCode_2DTO();
        SubCode_2BLL S2BLL = new SubCode_2BLL();

        COAInfoDTO CDTO = new COAInfoDTO();
        COAInfoBLL CBLL = new COAInfoBLL();

        MainVoucherDTO MVDTO = new MainVoucherDTO();
        MainVoucherBLL MVBLL = new MainVoucherBLL();

        SubVoucherDTO SDTO = new SubVoucherDTO();
        SubVoucherBLL SBLL = new SubVoucherBLL();
        TransDetailsDTO TDDTO = new TransDetailsDTO();
        TransDetailsBLL TDBLL = new TransDetailsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadMainHead_DR();
                LoadSubCodeId1_DR();
                LoadSubCodeId2_DR();
                LoadCOA_DR();

                LoadMainHead_CR();
                LoadSubCodeId1_CR();
                LoadSubCodeId2_CR();
                LoadCOA_CR();

                LoadMainVocher_CR();
                LoadSubVocher_CR();
                LoadMainVocher_DR();
                LoadSubVocher_DR();
                /*
                LoadSubCodeId1();
                LoadSubCodeId2();
                LoadSubVocher();
                LoadCOA();
                 * */
                // LoadCOA_DR();
                LoadTrans();
                LoadTransItemGrid();
                LoadTransDetailsGrid();
                LoadMainVocher_CR();

                //  LoadSubVocher_CR();
                LoadMainVocher_DR();

                if (TranList.Rows.Count == 0)
                {
                    PnlTransection.Visible = false;
                }
                if (TransDetailsGrid.Rows.Count == 0)
                {
                    PnlgvTransectDtl.Visible = false;
                }

            }
        }
        /*
                private void LoadCOA()
                {
                    var query = CBLL.LoadCoAInfo(0, "", "", Convert.ToInt32(ddlSubcode2_DR.SelectedValue), "", "", "", "", "", "");
                    ddlCOA_DR.DataSource = query;
                    ddlCOA_DR.DataTextField = "COAName_Num";
                    ddlCOA_DR.DataValueField = "COAId";
                    ddlCOA_DR.DataBind();
                    ddlCOA_DR.Items.Insert(0, new ListItem("Select One", "0"));
                    ddlCOA_CR.DataSource = query;
                    ddlCOA_CR.DataTextField = "COAName_Num";
                    ddlCOA_CR.DataValueField = "COAId";
                    ddlCOA_CR.DataBind();
                    ddlCOA_CR.Items.Insert(0, new ListItem("Select One", "0"));
                }

                private void LoadSubCodeId2()
                {
                    var query2 = S2BLL.LoadSuvCode_2Data(0, "", "", Convert.ToInt32(ddlSubcode1_CR.SelectedValue), "", "", "", "");

                    ddlSubcode2_CR.DataSource = query2;
                    ddlSubcode2_CR.DataTextField = "SubCode2Name_Num";
                    ddlSubcode2_CR.DataValueField = "SubCode_2Id";
                    ddlSubcode2_CR.DataBind();
                    ddlSubcode2_CR.Items.Insert(0, new ListItem("Select One", "0"));
                    ddlSubcode2_DR.DataSource = query2;
                    ddlSubcode2_DR.DataTextField = "SubCode2Name_Num";
                    ddlSubcode2_DR.DataValueField = "SubCode_2Id";
                    ddlSubcode2_DR.DataBind();
                    ddlSubcode2_DR.Items.Insert(0, new ListItem("Select One", "0"));
                }

                private void LoadSubCodeId1()
                {
                    var query2 = S1BLL.LoadSuvCode_1Data(0, "", "", Convert.ToInt32(ddlMainHeadCodeId_CR.SelectedValue), "", "");

                    ddlSubcode1_CR.DataSource = query2;
                    ddlSubcode1_CR.DataTextField = "SubCode1Name_Num";
                    ddlSubcode1_CR.DataValueField = "SubCode_1Id";
                    ddlSubcode1_CR.DataBind();
                    ddlSubcode1_CR.Items.Insert(0, new ListItem("Select One", "0"));
                    ddlSubcode1_DR.DataSource = query2;
                    ddlSubcode1_DR.DataTextField = "SubCode1Name_Num";
                    ddlSubcode1_DR.DataValueField = "SubCode_1Id";
                    ddlSubcode1_DR.DataBind();
                    ddlSubcode1_DR.Items.Insert(0, new ListItem("Select One", "0"));
                }
                private void LoadSubVocher()
                {

                    var query2 = SBLL.LoadSubVoucherData(0, 0);
                    ddlSubVoucher_DR.DataSource = query2;
                    ddlSubVoucher_DR.DataTextField = "SubVoucherCodeName";
                    ddlSubVoucher_DR.DataValueField = "SubVoucherId";
                    ddlSubVoucher_DR.DataBind();
                    ddlSubVoucher_DR.Items.Insert(0, new ListItem("Select One", "0"));
                    ddlSubVoucher_CR.DataSource = query2;
                    ddlSubVoucher_CR.DataTextField = "SubVoucherCodeName";
                    ddlSubVoucher_CR.DataValueField = "SubVoucherId";
                    ddlSubVoucher_CR.DataBind();
                    ddlSubVoucher_CR.Items.Insert(0, new ListItem("Select One", "0"));
                }
                private void LoadMainVocher()
                {
                    var query = MVBLL.LoadMainVocher(0);
                    ddlMaintVoucher_CR.DataSource = query;
                    ddlMaintVoucher_CR.DataTextField = "MainVoucherCode_Name";
                    ddlMaintVoucher_CR.DataValueField = "MainVoucherId";

                    ddlMaintVoucher_CR.DataBind();
                    ddlMaintVoucher_CR.Items.Insert(0, new ListItem("Select One", "0"));

                    ddlMainVoucher_DR.DataSource = query;
                    ddlMainVoucher_DR.DataTextField = "MainVoucherCode_Name";
                    ddlMainVoucher_DR.DataValueField = "MainVoucherId";

                    ddlMainVoucher_DR.DataBind();
                    ddlMainVoucher_DR.Items.Insert(0, new ListItem("Select One", "0"));
                }*/
        private void LoadMainVocher_DR() // MainVoucherName
        {
            var query = MVBLL.LoadMainVocher(0);
            ddlMainVoucher_DR.DataSource = query;
            ddlMainVoucher_DR.DataTextField = "MainVoucherCode_Name";
            ddlMainVoucher_DR.DataValueField = "MainVoucherId";

            ddlMainVoucher_DR.DataBind();
            ddlMainVoucher_DR.Items.Insert(0, new ListItem("Select One", "0"));
        }


        private void LoadMainVocher_CR() // MainVoucherName
        {
            var query = MVBLL.LoadMainVocher(0);
            ddlMaintVoucher_CR.DataSource = query;
            ddlMaintVoucher_CR.DataTextField = "MainVoucherCode_Name";
            ddlMaintVoucher_CR.DataValueField = "MainVoucherId";

            ddlMaintVoucher_CR.DataBind();
            ddlMaintVoucher_CR.Items.Insert(0, new ListItem("Select One", "0"));
        }
        private void LoadSubVocher_DR() // MainVoucherName
        {

            var query2 = SBLL.LoadSubVoucherData(0, Convert.ToInt32(ddlMainVoucher_DR.SelectedValue));
            ddlSubVoucher_DR.DataSource = query2;
            ddlSubVoucher_DR.DataTextField = "SubVoucherCodeName";
            ddlSubVoucher_DR.DataValueField = "SubVoucherId";
            ddlSubVoucher_DR.DataBind();
            ddlSubVoucher_DR.Items.Insert(0, new ListItem("Select One", "0"));
        }


        private void LoadSubVocher_CR() // MainVoucherName
        {
            var query = SBLL.LoadSubVoucherData(0, Convert.ToInt32(ddlMaintVoucher_CR.SelectedValue));
            ddlSubVoucher_CR.DataSource = query;
            ddlSubVoucher_CR.DataTextField = "SubVoucherCodeName";
            ddlSubVoucher_CR.DataValueField = "SubVoucherId";
            ddlSubVoucher_CR.DataBind();
            ddlSubVoucher_CR.Items.Insert(0, new ListItem("Select One", "0"));

        }


        public void clearcontol()
        {

            TranList.SelectedIndex = -1;
            tranlistdropp.SelectedValue = "0";
            SaveBtn.Text = "Save";
            HiddenTransId.Value = "";
            LoadMainHead_DR();
            LoadMainHead_CR();
            LoadSubCodeId1_DR();
            LoadSubCodeId1_CR();
            LoadSubCodeId2_DR();
            LoadSubCodeId2_CR();
            LoadCOA_DR();
            LoadCOA_CR();

            LoadTransDetailsGrid();
            AddBtn.Text = "Save";
            TransDetailsGrid.SelectedIndex = -1;
            LoadMainVocher_DR();
            LoadSubVocher_DR();
            LoadMainVocher_CR();
            LoadSubVocher_CR();



        }

        private void LoadTransItemGrid()
        {
            var data = tbll.LoadTransData(0);
            TranList.DataSource = data;
            TranList.DataBind();
        }

        private void LoadTransDetailsGrid()
        {
            var data = TDBLL.LoadTransDetailsData(0);

            TransDetailsGrid.DataSource = data;

            TransDetailsGrid.DataBind();
        }
        private void LoadTrans()
        {
            var query = tbll.LoadTransData(0);

            tranlistdropp.DataSource = query;
            tranlistdropp.DataTextField = "TranName";
            tranlistdropp.DataValueField = "TranId";
            tranlistdropp.DataBind();
            tranlistdropp.Items.Insert(0, new ListItem("Select One", "0"));


        }
        private void LoadMainHead_CR()
        {
            var query = MBLL.LoadMainHead(0);

            ddlMainHeadCodeId_CR.DataSource = query;
            ddlMainHeadCodeId_CR.DataTextField = "MainHeadName_Num";
            ddlMainHeadCodeId_CR.DataValueField = "MainHeadId";
            ddlMainHeadCodeId_CR.DataBind();
            ddlMainHeadCodeId_CR.Items.Insert(0, new ListItem("Select One", "0"));

        }

        private void LoadMainHead_DR()
        {
            var query = MBLL.LoadMainHead(0);
            ddlMainHeadCodeId_DR.DataSource = query;
            ddlMainHeadCodeId_DR.DataTextField = "MainHeadName_Num";
            ddlMainHeadCodeId_DR.DataValueField = "MainHeadId";
            ddlMainHeadCodeId_DR.DataBind();
            ddlMainHeadCodeId_DR.Items.Insert(0, new ListItem("Select One", "0"));
        }
        private void LoadSubCodeId1_CR()
        {
            var query2 = S1BLL.LoadSuvCode_1Data(0, "", "", Convert.ToInt32(ddlMainHeadCodeId_CR.SelectedValue), "", "");

            ddlSubcode1_CR.DataSource = query2;
            ddlSubcode1_CR.DataTextField = "SubCode1Name_Num";
            ddlSubcode1_CR.DataValueField = "SubCode_1Id";
            ddlSubcode1_CR.DataBind();
            ddlSubcode1_CR.Items.Insert(0, new ListItem("Select One", "0"));

        }
        private void LoadSubCodeId1_DR()
        {
            var query = S1BLL.LoadSuvCode_1Data(0, "", "", Convert.ToInt32(ddlMainHeadCodeId_DR.SelectedValue), "", "");
            ddlSubcode1_DR.DataSource = query;
            ddlSubcode1_DR.DataTextField = "SubCode1Name_Num";
            ddlSubcode1_DR.DataValueField = "SubCode_1Id";
            ddlSubcode1_DR.DataBind();
            ddlSubcode1_DR.Items.Insert(0, new ListItem("Select One", "0"));
        }

        private void LoadSubCodeId2_CR()
        {

            var query2 = S2BLL.LoadSuvCode_2Data(0, "", "", Convert.ToInt32(ddlSubcode1_CR.SelectedValue), "", "", "", "");

            ddlSubcode2_CR.DataSource = query2;
            ddlSubcode2_CR.DataTextField = "SubCode2Name_Num";
            ddlSubcode2_CR.DataValueField = "SubCode_2Id";
            ddlSubcode2_CR.DataBind();
            ddlSubcode2_CR.Items.Insert(0, new ListItem("Select One", "0"));
        }

        private void LoadSubCodeId2_DR()
        {

            var query = S2BLL.LoadSuvCode_2Data(0, "", "", Convert.ToInt32(ddlSubcode1_DR.SelectedValue), "", "", "", "");
            ddlSubcode2_DR.DataSource = query;
            ddlSubcode2_DR.DataTextField = "SubCode2Name_Num";
            ddlSubcode2_DR.DataValueField = "SubCode_2Id";
            ddlSubcode2_DR.DataBind();
            ddlSubcode2_DR.Items.Insert(0, new ListItem("Select One", "0"));
        }
        private void LoadCOA_DR()
        {

            var query = CBLL.LoadCoAInfo(0, "", "", Convert.ToInt32(ddlSubcode2_DR.SelectedValue), "", "", "", "", "", "");
            ddlCOA_DR.DataSource = query;
            ddlCOA_DR.DataTextField = "COAName_Num";
            ddlCOA_DR.DataValueField = "COAId";
            ddlCOA_DR.DataBind();
            ddlCOA_DR.Items.Insert(0, new ListItem("Select One", "0"));

        }

        private void LoadCOA_CR()
        {
            var query2 = CBLL.LoadCoAInfo(0, "", "", Convert.ToInt32(ddlSubcode2_CR.SelectedValue), "", "", "", "", "", "");
            ddlCOA_CR.DataSource = query2;
            ddlCOA_CR.DataTextField = "COAName_Num";
            ddlCOA_CR.DataValueField = "COAId";
            ddlCOA_CR.DataBind();
            ddlCOA_CR.Items.Insert(0, new ListItem("Select One", "0"));
        }

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            Clear_TransItem();
        }

        public void Clear_TransItem()
        {
            LoadTrans();
            tranName.Text = "";
            LoadTransItemGrid();
            SaveBtn.Text = "Save";
            TranList.SelectedIndex = -1;

        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SaveBtn.Text == "Update")
            {
                tdto.TranId = Convert.ToInt32(HiddenTransId.Value);
                tdto.TranName = tranName.Text;
                tdto.UpdateBy = "MB";
                tdto.UpdateDate = DateTime.Now;
                tbll.Edit(tdto);

            }
            else
            {
                tdto.TranName = tranName.Text;

                tdto.CreateBy = "MB";
                tdto.CreateDate = DateTime.Now;
                tbll.Add(tdto);



            }

            Clear_TransItem();

        }

        protected void TranList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<TransitemDTO> bb = new List<TransitemDTO>();
            bb = tbll.LoadTransData(Convert.ToInt32(TranList.DataKeys[TranList.SelectedIndex].Values["TranId"].ToString()));
            HiddenTransId.Value = bb.First().TranId.ToString();

            tranName.Text = bb.First().TranName.ToString();
            SaveBtn.Text = "Update";
        }

        protected void TranList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TranList.PageIndex = e.NewPageIndex;
            LoadTransItemGrid();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            TDDTO.CRCOAId = int.Parse(ddlCOA_CR.SelectedValue);
            TDDTO.DRCOAId = int.Parse(ddlCOA_DR.SelectedValue);
            TDDTO.CreateBy = "MB";
            TDDTO.CreateDate = DateTime.Now;
            TDDTO.CRSubCoId2 = int.Parse(ddlSubcode1_CR.SelectedValue);
            TDDTO.DRSubCoId2 = int.Parse(ddlSubcode2_DR.SelectedValue);
            TDDTO.TranId = int.Parse(tranlistdropp.SelectedValue);
            TDDTO.DrSubVoucherId = int.Parse(ddlSubVoucher_DR.SelectedValue);
            TDDTO.CrSubVoucherId = int.Parse(ddlSubVoucher_CR.SelectedValue);
            if (AddBtn.Text == "Save")
            {
                TDBLL.Add(TDDTO);
            }
            else
            {
                TDDTO.AccTransDtlId = int.Parse(HFCOAID.Value);
                TDBLL.Edit(TDDTO);

            }

            clearcontol();
        }

        protected void ddlMaintVoucher_CR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubVocher_CR();
        }

        protected void ddlMainHeadCodeId_DR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId1_DR();
            LoadSubCodeId2_DR();
            LoadCOA_DR();
        }

        protected void ddlMainHeadCodeId_CR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId1_CR();
            LoadSubCodeId2_CR();
            LoadCOA_CR();
        }

        protected void ddlSubcode1_DR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId2_DR();
            LoadCOA_DR();
        }

        protected void ddlSubcode2_DR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCOA_DR();
        }

        protected void ddlSubcode1_CR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCodeId2_CR();
            LoadCOA_CR();
        }

        protected void ddlSubcode2_CR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCOA_CR();
        }

        protected void TransDetailsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<TransDetailsDTO> bb = new List<TransDetailsDTO>();
            List<SubVoucherDTO> cc = new List<SubVoucherDTO>();

            bb = TDBLL.LoadTransDetailsData(Convert.ToInt32(TransDetailsGrid.DataKeys[TransDetailsGrid.SelectedIndex].Values["AccTransDtlId"].ToString()));
            HFCOAID.Value = bb.First().AccTransDtlId.ToString();

            ddlSubcode2_DR.SelectedValue = bb.First().DRSubCoId2.ToString();
            ddlCOA_DR.SelectedValue = bb.First().DRCOAId.ToString();
            ddlCOA_CR.SelectedValue = bb.First().CRCOAId.ToString();
            ddlSubcode2_CR.SelectedValue = bb.First().CRSubCoId2.ToString();
            ddlSubVoucher_DR.SelectedValue = bb.First().DrSubVoucherId.ToString();
            ddlSubVoucher_CR.SelectedValue = bb.First().CrSubVoucherId.ToString();

            ddlMainVoucher_DR.SelectedValue = bb.First().DRMainVoucherId.ToString();
            ddlMaintVoucher_CR.SelectedValue = bb.First().CRMainVoucherId.ToString();
            ddlMainHeadCodeId_CR.SelectedValue = bb.First().CRMainHeadId.ToString();
            ddlMainHeadCodeId_DR.SelectedValue = bb.First().DRMainHeadId.ToString();
            ddlSubcode1_CR.SelectedValue = bb.First().CRSubCoId1.ToString();
            ddlSubcode1_DR.SelectedValue = bb.First().DRSubCoId1.ToString();
            tranlistdropp.SelectedValue = bb.First().TranId.ToString();
            tranlistdropp.SelectedValue = bb.First().TranId.ToString();
            AddBtn.Text = "Update";
        }

        protected void ddlMainVoucher_DR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubVocher_DR();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Clear_TransItem();
            clearcontol();
            AddBtn.Text = "Save";
        }

        protected void TransDetailsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TransDetailsGrid.PageIndex = e.NewPageIndex;
            LoadTransDetailsGrid();
        }



    }
}