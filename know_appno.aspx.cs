using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class know_appno : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populate_year();
        }
    }

    private void populate_year()
    {
        ListItem li;

        int year = DateTime.Now.Year;

        for (int i = year; i >= year - 60; i--)
        {
            li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            DropDownList_year.Items.Add(li);
        }
        li = new ListItem();
        li.Text = "-Select-";
        li.Value = "-1";
        DropDownList_year.Items.Insert(0, li);
    }

    protected void txt_submit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    private void fillgrid()
    {
        try
        {
            string regno = txt_dd.Text + txt_mm.Text + txt_yyyy.Text + txt_regno.Text + DropDownList_year.SelectedItem.ToString();
            CandidateData objcd = new CandidateData();
            DataTable dt = objcd.getapplctnnos(regno);
            grdappno.DataSource = dt;
            grdappno.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}
