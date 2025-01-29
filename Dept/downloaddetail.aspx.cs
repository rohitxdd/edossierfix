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

using System.IO;
using System.Text;

public partial class downloaddetail : BasePage
{

    DataTable dt = new DataTable();
    bank_challan bc = new bank_challan();
    DropdownUtility objdrp = new DropdownUtility();

    MD5Util md5Util = new MD5Util();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    DataTable dtFinal = new DataTable();
    message msg = new message();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            int temp = bc.insert_fee(); 
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
    private void fillgrid()
    {

        dt = bc.getbankdetails(rbttype.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            grddetails.DataSource = dt;
            grddetails.DataBind();
        }
        else
        {
            message msg = new message();
            msg.Show("No Data Found");
        }
        for (int a = 0; a < dt.Rows.Count; a++)
        {

            HyperLink hytotal = (HyperLink)(grddetails.Rows[a].FindControl("hytotal"));
            //HyperLink hyfee = (HyperLink)(grddetails.Rows[a].FindControl("hyfee"));
            if (dt.Rows[a]["total_count"].ToString() == "0")
            {
                //lnk3.Enabled = false;
                hytotal.Enabled = false;
            }
            else
            {
                //lnk3.Enabled = true;
                hytotal.Enabled = true;
            }
            //if (dt.Rows[a]["fee_receive_count"].ToString() == "0")
            //{
            //    //lnk3.Enabled = false;
            //    hyfee.Enabled = false;
            //}
            //else
            //{
            //    //lnk3.Enabled = true;
            //    hyfee.Enabled = true;
            //}
        }
    }
    


    protected void grddetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string flag;
            int index = Convert.ToInt32(e.CommandArgument);
            string visible = grddetails.DataKeys[index].Values["dateofsending"].ToString();
            if (visible == "NotSentYet")
            {
                flag = "Y";
                DataTable dt = new bank_challan().get_csv_data1(flag, " ", " ");               
                string sb = ExportTableToCsvString(dt, false);
                StringWriter oStringWriter = new StringWriter();
                oStringWriter.WriteLine(sb);
                DataTable dtfee = bc.checkdatefeedetails();
                int val;
                string slot_p;
                if (dtfee.Rows.Count > 0)
                {
                    string date = dtfee.Rows[0]["dateofsending"].ToString();
                    DataTable dt4 = bc.getmaxslot(date);
                    int incree = Convert.ToInt32(dt4.Rows[0]["slot"]);
                    val = incree + 1;
                    slot_p = val.ToString();
                    int dtupdate = bc.updatedate(val.ToString());
                }
                else
                {
                    int slot = 1;
                    slot_p = slot.ToString();
                    int dtupdate = bc.updatedate(slot.ToString());
                }
                fillgrid();
                Response.ContentType = "text/plain";
                Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("To_Bank-{0}.txt", string.Format("{0:ddMMyyyy}", DateTime.Today) + "_File" + slot_p));
                Response.Clear();
                using (StreamWriter writer = new StreamWriter(Response.OutputStream))
                {
                    writer.Write(oStringWriter.ToString());
                }
                Response.End();
            }
            else
            {
                flag = "N";
                string dos = grddetails.DataKeys[index].Values["dateofsending"].ToString();
                string slot = grddetails.DataKeys[index].Values["slot"].ToString();
                DataTable dt = new bank_challan().get_csv_data1(flag, dos, slot);
                string sb = ExportTableToCsvString(dt, false);
                StringWriter oStringWriter = new StringWriter();
                oStringWriter.WriteLine(sb);
                Response.ContentType = "text/plain";
                Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("To_Bank-{0}.txt", string.Format("{0:ddMMyyyy}", DateTime.Today)+"_File"+slot));
                Response.Clear();
                //, Encoding.Default
                using (StreamWriter writer = new StreamWriter(Response.OutputStream))
                {
                    writer.Write(oStringWriter.ToString());
                }

                Response.End();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private string ExportTableToCsvString(DataTable table, bool printHeaders)
    {
        StringBuilder sb = new StringBuilder();

        if (printHeaders)
        {
            for (int colCount = 0;
                 colCount < table.Columns.Count; colCount++)
            {
                sb.Append(table.Columns[colCount].ColumnName);
                if (colCount != table.Columns.Count - 1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }
        }
        for (int rowCount = 0;
             rowCount < table.Rows.Count; rowCount++)
        {
            for (int colCount = 0;
                 colCount < table.Columns.Count; colCount++)
            {
                sb.Append(table.Rows[rowCount][colCount]);
                if (colCount != table.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            if (rowCount != table.Rows.Count - 1)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
    protected void grddetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string date = grddetails.DataKeys[e.Row.RowIndex].Values["dateofsending"].ToString();
            if (date != "NotSentYet")
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lnkdwnload");
                lb.Text = "ResendData";
                string[] str = date.Split('/');
                string yy = str[0].ToString();
                string mm = str[1].ToString();
                string dd = str[2].ToString();
                string date1 = dd + '/' + mm + '/' + yy;
                Label lbd = (Label)e.Row.FindControl("lbldate");
                lbd.Text = date1.ToString();
               
            }
            else
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lnkdwnload");
                lb.Text = "SendData";
                Label lbd = (Label)e.Row.FindControl("lbldate");
                lbd.Text = date.ToString();
            }

            string dateofsending = grddetails.DataKeys[e.Row.RowIndex].Values["dateofsending"].ToString();
            string slot = grddetails.DataKeys[e.Row.RowIndex].Values["slot"].ToString();
            HyperLink hytotal = (HyperLink)e.Row.FindControl("hytotal");
            string url1 = md5Util.CreateTamperProofURL("ChallanCandidateDetails.aspx", null, "dateofsending=" + MD5Util.Encrypt(dateofsending, true) + "&slot=" + MD5Util.Encrypt(slot, true) + "&flag=" + MD5Util.Encrypt("A", true));
            hytotal.NavigateUrl = url1;
            //HyperLink hyfee = (HyperLink)e.Row.FindControl("hyfee");
            //string url2 = md5Util.CreateTamperProofURL("ChallanCandidateDetails.aspx", null, "dateofsending=" + MD5Util.Encrypt(dateofsending, true) + "&slot=" + MD5Util.Encrypt(slot, true) + "&flag=" + MD5Util.Encrypt("Y", true));
            //hyfee.NavigateUrl = url2;
           
        }
       
    }
    protected void rbttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
 
//if (e.CommandName == "send")
//  {
//    int index = Convert.ToInt32(e.CommandArgument);
//    flag = "Y";              
//    DataTable dt = new bank_challan().get_csv_data1(flag," "," ");
//    string sb = ExportTableToCsvString(dt, false);
//    StringWriter oStringWriter = new StringWriter();
//    oStringWriter.WriteLine(sb);
//    DataTable dtfee=bc.checkdatefeedetails();
//    if (dtfee.Rows.Count > 0)
//    {
//        string date = dtfee.Rows[0]["dateofsending"].ToString();
//        DataTable dt1 = bc.getmaxslot(date);                   
//        int incree = Convert.ToInt32(dt1.Rows[0]["slot"]);
//        int val = incree + 1;
//        int dtupdate = bc.updatedate(val.ToString());
//    }
//    else
//    {
//        int slot = 1;
//        int dtupdate = bc.updatedate(slot.ToString());
//    }
//    fillgrid();
//    Response.ContentType = "text/plain";
//    Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("To_Bank-{0}.txt", string.Format("{0:ddMMyyyy}", DateTime.Today)));
//    Response.Clear();
//    using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
//    {
//        writer.Write(oStringWriter.ToString());
//    }


//    Response.End();
//}
//if (e.CommandName == "resend")
//{
//    int index = Convert.ToInt32(e.CommandArgument);
//    flag = "N";

//     string visible = grddetails.DataKeys[index].Values["dateofsending"].ToString();
//     string slot = grddetails.DataKeys[index].Values["slot"].ToString();
//     DataTable dt = new bank_challan().get_csv_data1(flag,visible,slot);
//    string sb = ExportTableToCsvString(dt, false);
//    StringWriter oStringWriter = new StringWriter();
//    oStringWriter.WriteLine(sb);
//    Response.ContentType = "text/plain";
//    Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("To_Bank-{0}.txt", string.Format("{0:ddMMyyyy}", DateTime.Today)));
//    Response.Clear();
//    using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
//    {
//        writer.Write(oStringWriter.ToString());
//    }

//    Response.End();
//}








