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

public partial class AuditDetails : BasePage
{
    message msg = new message();
    DataTable dt = new DataTable();
    ClsAudit ms = new ClsAudit();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        fillgrid();
    }

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
}
