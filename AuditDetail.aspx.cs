using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class AuditDetail : BasePage
{
    DataTable dt = new DataTable();
    //Master ms = new Master();
    ClsAudit ms = new ClsAudit();
    message msg = new message();
    message msg1 = new message();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    //DropdownUtility objDropdownUtility = new DropdownUtility();   
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            //fill_User_Id();
            fillgrid();
        }
        else
        {
            if (((Request.Form["ctl00$body$csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["ctl00$body$csrftoken"].ToString().Equals(Session["token"].ToString())))
            {
                //valid Page
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
    //private void fill_User_Id()
    //{

    //    string usertype = Session["usertype"].ToString();
    //    string userid = Session["userid"].ToString();

    //    try
    //    {
    //        dt = objDropdownUtility.FillUserId(userid,usertype);
    //        ddlUserId.DataSource = dt;
    //        ddlUserId.DataTextField = "UserId";
    //        ddlUserId.DataValueField = "UserId";
    //        ddlUserId.DataBind();

    //        if (usertype == "1")
    //        {
    //           ddlUserId.Items.Insert(0, Utility.ddl_Zero_Value());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("ErrorPage.aspx");
    //    }
    //}
    private void fillgrid()
    {

        //if (!ValidateDropdown.validate(ddlUserId.SelectedValue, "UserMaster", "UserId"))
        //{
        //    msg1.Show("Invalid Inputs");
        //}
        //else
        //{
            try
            {
                //dt = ms.bindauditgrid(ddlUserId.SelectedValue.ToString(), ddlActive.SelectedValue.ToString(), Session["usertype"].ToString());
                dt = ms.bindauditgrid("", "", "");
                if (dt.Rows.Count > 0)
                {
                    grdaudit.DataSource = dt;
                    grdaudit.DataBind();
                    grdaudit.Visible = true;                  
                }
                else
                {
                    msg.Show("Record Not found");
                    grdaudit.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                Response.Redirect("../ErrorPage.aspx");
            }
        //}
    }
    protected void grdaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdaudit.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    //protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnsearch_Click(sender, e);
    //}
    //protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnsearch_Click(sender, e);
    //}
}
