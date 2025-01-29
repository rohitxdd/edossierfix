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
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;
public partial class updatefeedetail : BasePage
{
    message msg = new message();
    DataAccess da = new DataAccess();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (check_proc_status())
        {
            ArrayList list = new ArrayList();
            if (FileUpload.HasFile)
            {
                string strFileName = FileUpload.FileName;
                string strExtension = Path.GetExtension(strFileName);
                if (strExtension != ".xls" && strExtension != ".xlsx" && strExtension != ".csv" && strExtension != ".csv")
                {
                    Response.Write("<script>alert('Failed to import bank challan. Cause: Invalid csv file.');</script>");
                    return;
                }
                string header = "No";
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                string strUploadFileName = physicalPath + @"\temp\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                FileUpload.SaveAs(strUploadFileName);
                string pathOnly = Path.GetDirectoryName(strUploadFileName);
                string fileName = Path.GetFileName(strUploadFileName);
                string sql = @"SELECT * FROM [" + fileName + "]";
                using (OleDbConnection connection = new OleDbConnection(
                          @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                          ";Extended Properties=\"Text;HDR=" + header + "\""))
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    Dictionary<int, dict> challan = new Dictionary<int, dict>();
                    dict temp_challan;
                    //Dictionary<string,SqlParameter[]> temp_challan;

                    DataTable dataTable = new DataTable();
                    dataTable.Locale = CultureInfo.CurrentCulture;
                    adapter.Fill(dataTable);
                    DataTable dt1 = new DataTable();
                    dt1 = dataTable;
                    bank_challan bc = new bank_challan();
                    int k = 0;
                    int s = 0;
                    string notexist = "";
                    try
                    {

                        DataTable dtaapid = bc.get_appid();
                        //lbl_msg.Text = "File Uploading in DB started.";

                        DataTable dt_create = this.create_Table();

                        for (int i = 2; i < dt1.Rows.Count - 1; i++)
                        {
                            //get aplid

                            if (dt1.Rows[i][1] != null && dt1.Rows[i][1].ToString() != "" && !(dt1.Rows[i][1].ToString().Contains("ACCOUNT NO")))
                            {
                                // || dt1.Rows[i][1].ToString().Contains("32749540709")

                                string appid = dt1.Rows[i][5].ToString();
                                DataRow[] row = dtaapid.Select("applid='" + appid + "'");
                                if (i == 971)
                                {

                                }
                                if (row.Length > 0)
                                {
                                    list.Add(appid);
                                    string amount = dt1.Rows[i][11].ToString();
                                    amount = amount.Substring(0, amount.Length - 4);

                                    string dob = Utility.adddate(dt1.Rows[i][6].ToString());
                                    string transdate = Utility.adddate(dt1.Rows[i][7].ToString());

                                    DataRow dr = dt_create.NewRow();
                                    dr["jrnlno"] = dt1.Rows[i][0].ToString();
                                    dr["trandate"] = transdate;
                                    dr["tranbra"] = dt1.Rows[i][8].ToString();
                                    dr["feerecd"] = "Y";
                                    dr["transmode"] = dt1.Rows[i][1].ToString();
                                    dr["fromaccsys"] = dt1.Rows[i][2].ToString();
                                    dr["fromacc"] = dt1.Rows[i][3].ToString();
                                    dr["DOB"] = dob;
                                    dr["transtime"] = dt1.Rows[i][9].ToString();
                                    dr["feetype"] = dt1.Rows[i][4].ToString();
                                    dr["accountno"] = dt1.Rows[i][10].ToString();
                                    dr["amount"] = amount;
                                    dr["can_name"] = dt1.Rows[i][12].ToString();
                                    dr["app_id"] = dt1.Rows[i][5].ToString();

                                    dt_create.Rows.Add(dr);


                                    //k = bc.updatefeedetails(dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dob, transdate, dt1.Rows[i][8].ToString(), dt1.Rows[i][9].ToString(), dt1.Rows[i][10].ToString(), amount, dt1.Rows[i][12].ToString());
                                    //k++;    

                                }
                                else
                                {
                                    notexist = notexist + "," + appid;
                                    s++;
                                }
                            }
                        }


                        k = da.call_challan_proc(dt_create);

                        if (s > 0)
                        {
                            string id = notexist.Remove(0, 1);
                            msg.Show("application id " + id + " does not exist. please check");
                        }
                        if (k > 0)
                        {
                            msg.Show("CSV file has been uploaded successfully.Record Inserted = "+k);
                            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "alert('CSV file has been uploaded successfully.');", true);
                            //lbl_msg.Text = "CSV file has been uploaded successfully.";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.Show(ex.Message.ToString());
                        //msg.Show("CSV fils is distorted.");
                    }
                }
                //           ScriptManager.RegisterStartupScript(this,GetType(),"showalert","alert('Bank Fee Data Updated and Click OK to send SMS to the Candidates.');",true);
                //lbl_msg.Text = "SMS Sending started."; 

               // sendSMS(list);

                //            msg.Show("Message sent successfully.");
                //lbl_msg.Text = "SMS Sending finished.";
            }
            else
            {
                msg.Show("Please select a file first.");
            }
        }
        else
        {
            msg.Show("Bank Fee File Uploading is already in process. Try after some time.");
        }
    }
    public DataTable create_Table()
    {
        DataTable tb = new DataTable("Challan");      
            
        tb.Columns.Add(new DataColumn("jrnlno",typeof(Int64)));
        tb.Columns.Add(new DataColumn("trandate", typeof(string)));
        tb.Columns.Add(new DataColumn("tranbra", typeof(Int64)));
        tb.Columns.Add(new DataColumn("feerecd", typeof(string)));
        tb.Columns.Add(new DataColumn("transmode", typeof(string)));
        tb.Columns.Add(new DataColumn("fromaccsys", typeof(string)));
        tb.Columns.Add(new DataColumn("fromacc", typeof(string)));
        tb.Columns.Add(new DataColumn("DOB", typeof(string)));
        tb.Columns.Add(new DataColumn("transtime", typeof(string)));
        tb.Columns.Add(new DataColumn("feetype", typeof(string)));
        tb.Columns.Add(new DataColumn("accountno", typeof(string)));
        tb.Columns.Add(new DataColumn("amount", typeof(string)));
        tb.Columns.Add(new DataColumn("can_name", typeof(string)));
        tb.Columns.Add(new DataColumn("app_id", typeof(int)));

        return tb;
    }
    public void sendSMS(ArrayList list)
    {
        
        for (int i = 0; i < list.Count; i++)
        {
            
            string appid = list[i].ToString();
            //DataTable dtmobile = new DataTable();            
            //dtmobile = bc.get_job_application(appid);  

            bank_challan bc = new bank_challan();

            DataTable dtdetail = bc.getjobdeails(appid);
            if (dtdetail.Rows.Count > 0)
            {
                Sms objsms = new Sms();
                string msg1 = "Receipt of your fee for the post of " + dtdetail.Rows[0]["JobTitle"].ToString() + dtdetail.Rows[0]["postcode"].ToString() + " is confirmed at DSSSB.";
                //objsms.sendmsg(dtdetail.Rows[0]["mobileno"].ToString(), msg1);
                objsms.sendmsgNew(dtdetail.Rows[0]["mobileno"].ToString(), msg1, "1007161770394197227");
            }
        }        
    }
    public bool check_proc_status()
    {
        bank_challan bank_c=new bank_challan();
        string lock_status = bank_c.get_proc_status();

        if (lock_status == "Y")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
