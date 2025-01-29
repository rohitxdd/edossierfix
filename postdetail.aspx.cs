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

public partial class postdetail : Page
{
    MD5Util md5util = new MD5Util();
    CandidateData objcd = new CandidateData();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["jobid"] != null)
            {
                //UniqueRandomNumber = randObj.Next(1, 10000);
                //Session["token"] = UniqueRandomNumber.ToString();
                //this.csrftoken.Value = Session["token"].ToString();
                string jid = MD5Util.Decrypt(Request.QueryString["jobid"].ToString(), true);
                filldetail(jid);
                DataTable dt4 = new DataTable();
                dt4 = objcd.GetJobName(jid);
                lblJobName.Text =  dt4.Rows[0]["JobTitle"].ToString();
                DataTable dtfee = objcd.getfeedetail(jid);
                lblfee.Text = "Rs " + dtfee.Rows[0]["feeamount"].ToString() + " /-";
                FillGrid(jid);
            }
        }
        else
        {
            //if (((Request.Form["csrftoken"] != null) && (Session["token"] != null)) && (Request.Form["csrftoken"].ToString().Equals(Session["token"].ToString())))
            //{
            //    //valid Page
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}
        }
    }
    private void filldetail(string jid)
    {

        DataTable dt = objcd.fillJobAdvtMaster(jid);
        if (dt.Rows.Count > 0)
        {
            lblclas.Text =dt.Rows[0]["JobDescription"].ToString();
            lblpay.Text = dt.Rows[0]["payscale"].ToString();
            lblessq.Text = dt.Rows[0]["essential_qual"].ToString();
            lbldesiredq.Text = dt.Rows[0]["desire_qual"].ToString();
            lblminage.Text = dt.Rows[0]["MinAge"].ToString();
            lblmaxage.Text = dt.Rows[0]["MaxAge"].ToString();
            lblprobper.Text=dt.Rows[0]["probation_year"].ToString();
            lblessnexperience.Text = dt.Rows[0]["essential_exp"].ToString();
            lbldesexpr.Text = dt.Rows[0]["desire_exp"].ToString();
            lblexpr.Text = dt.Rows[0]["exp_noofyears"].ToString();  
        }
           
    }
    private void FillGrid(string jid)
    {
      DataTable dt = objcd.GetAge_Relax(jid);

        for (int a = 0; a < dt.Rows.Count; a++)
        {

            if (dt.Rows[a]["CatIndCS"].ToString() == "C")
            {
                dt.Rows[a]["CatIndCS"] = "Category";
                dt.Rows[a]["CatCode"] = dt.Rows[a]["category"];
            }
            else
            {
                dt.Rows[a]["CatIndCS"] = "Special Category";
                dt.Rows[a]["CatCode"] = dt.Rows[a]["SubCat_name"];
            }
            if (dt.Rows[a]["CM"].ToString() == "C")
            {
                dt.Rows[a]["CM"] = dt.Rows[a]["D_Year"].ToString()+" Years " + "(In addition to any other relaxtion)";//"To Be Added";
            }
            else
            {
                dt.Rows[a]["CM"] = dt.Rows[a]["D_Year"].ToString() + " Years ";//"Maximum";
            }
            if (dt.Rows[a]["Fee_exmp"].ToString() == "Y")
            {
                dt.Rows[a]["Fee_exmp"] = "Yes";
            }
            else
            {
                dt.Rows[a]["Fee_exmp"] = "No";
            }
        }

        grdAgeRelax.DataSource = dt;
        grdAgeRelax.DataBind();


    }
}
