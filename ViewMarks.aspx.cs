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
using System.Threading;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class ViewMarks : BasePage
{
    Marks objmarks = new Marks();
    message msg = new message();
    DataTable dt;
    MD5Util md5util = new MD5Util();
    Utility Utlity = new Utility();
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;
    Random randObjCode = new Random();
    challengeansheet objchallenge = new challengeansheet();
    static string postcode = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            UniqueRandomNumber = randObj.Next(1, 10000);
            Session["token"] = UniqueRandomNumber.ToString();
            this.csrftoken.Value = Session["token"].ToString();
            fillddlexam();
        }
        else
        {
            //TRgetCodeMob.Visible = true;
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
    private void fillddlexam()
    {
        try
        {
            string regno = Session["rid"].ToString();
            dt = objmarks.getexamtoViewMarks(regno);
            if (dt.Rows.Count > 0)
            {
                trddlexam.Visible = true;
                ddlexam.DataTextField = "examdtl";
                ddlexam.DataValueField = "examid";
                ddlexam.DataSource = dt;
                ddlexam.DataBind();
                ddlexam.Items.Insert(0, Utility.ddl_Select_Value());
            }
            else
            {
                trddlexam.Visible = false;
                msg.Show("No Result Available Yet");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }




    protected void ddlexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexam.SelectedValue != "")
            {
                clearlabel();
                string FlagCount_P_Applied = "";
                string regno = Session["rid"].ToString();
                bool wpresent = objmarks.checkexamattendance(ddlexam.SelectedValue, regno);
                if (wpresent)
                {
                    tratndnc.Visible = false;
                    tbl1.Visible = true;

                    DataTable dtTier = objchallenge.getExamTier(ddlexam.SelectedValue);
                    string tierval = "", isPETmarksreq = "" ;
                    if (dtTier.Rows.Count > 0)
                    {
                        tierval = dtTier.Rows[0]["tier"].ToString();
                        isPETmarksreq = dtTier.Rows[0]["isPETmarksreq"].ToString();
                    }
                    DataTable dt = objmarks.GetPostCode(ddlexam.SelectedValue, regno, tierval);
                    hdnapplid.Value = dt.Rows[0]["applid"].ToString();
                    string jid = dt.Rows[0]["jid"].ToString();
                    string posts = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0)
                        {
                            posts += ",";
                        }
                        posts += Utility.getstring(dt.Rows[i]["post"].ToString());
			postcode = dt.Rows[i]["post"].ToString();
                    }
                    //lblposts.Text = posts;
                    if (dt.Rows.Count == 1)
                    {
                        FlagCount_P_Applied = "S";
                    }
                    else
                    {
                        FlagCount_P_Applied = "M";
                    }


                    DataTable dtcand = objmarks.GetCandDetails(regno);
                    if (dtcand.Rows.Count > 0)
                    {
                        //lblname.Text = dtcand.Rows[0]["name"].ToString();
                        //lbldob.Text = dtcand.Rows[0]["dob"].ToString();

                        DataTable dt1 = objmarks.GetMarks(FlagCount_P_Applied, ddlexam.SelectedValue, regno, tierval);
                        if (dt1.Rows.Count > 0 || isPETmarksreq == "N")
                        {
                            TRUC.Visible = true;
                            lblHead.Visible = true;
                            lblname.Text = dtcand.Rows[0]["name"].ToString();
                            lbldob.Text = dtcand.Rows[0]["dob"].ToString();
                            if (dt1.Rows.Count > 0)
                            {
                                lblrollno.Text = dt1.Rows[0]["rollno"].ToString();
                                if (dt1.Rows[0]["finalS1Marks"].ToString() != "" && dt1.Rows[0]["finalS2Marks"].ToString() != "")
                                {
                                    if (ddlexam.SelectedValue == "703" && postcode == "18/19 : Fire Operator")
                                    {
                                        lbls1marks.Text = "Section-A Marks/ Marks of Theory(Out of 20 Marks) : " + dt1.Rows[0]["finalS1Marks"].ToString();
                                        lbls2marks.Text = "Section-B Marks/ Marks of Practical(Out of 80 Marks) : " + dt1.Rows[0]["finalS2Marks"].ToString();
                                    }
                                    else
                                    {
                                        lbls1marks.Text = "Section-A Marks : " + dt1.Rows[0]["finalS1Marks"].ToString();
                                        lbls2marks.Text = "Section-B Marks : " + dt1.Rows[0]["finalS2Marks"].ToString();
                                    }
                                    lblmrksobtd.Text = "Total : " + dt1.Rows[0]["finaltotalmarks"].ToString();
                                }
                                else
                                {
                                    lbls1marks.Text = "";
                                    lbls2marks.Text = "";
                                    lblmrksobtd.Text = dt1.Rows[0]["finaltotalmarks"].ToString();
                                }
                               
                            }
                            else
                            {
                                lblrollno.Text = objmarks.getattenrollno(ddlexam.SelectedValue, regno);
                                lblmrksobtd.Text = "--";
                            }
                            lblExamid.Text = ddlexam.SelectedItem.Text + "  (Exam ID-" + ddlexam.SelectedValue + ")";
                            lblpostcode.Text = posts;
                            DataTable dtisresultup = objmarks.isresultuploaded(jid, tierval);
                            if (dtisresultup.Rows.Count == 0)
                            {
                                lblremarks.Text = "--";
                                trmessage.Visible = false;
                                trmsgnextexam.Visible = false;
                            }
                            else
                            {
                                // DataTable dtcheckshortlist = objmarks.getrollno_notshortlisted(lblrollno.Text, tierval);
                                DataRow[] dr = dtisresultup.Select("rollno='" + lblrollno.Text + "' and flag='V'");
                                if (dr.Length == 0)
                                {
                                    if (isPETmarksreq == "N")
                                    {
                                        lblremarks.Text = "Disqualified";
                                    }
                                    else
                                    {
                                        lblremarks.Text = "You have not been Shortlisted";
                                    }
                                    trmessage.Visible = false;
                                    trmsgnextexam.Visible = false;
                                }
                                else
                                {
                                    if (isPETmarksreq == "N")
                                    {
                                        lblremarks.Text = "Qualified";
                                        trmessage.Visible = false;
                                        trmsgnextexam.Visible = false;
                                    }
                                    else
                                    {
                                        DataTable dtremarks = objmarks.getrollno(lblrollno.Text, tierval);
                                        if (dtremarks.Rows.Count != 0)
                                        {
                                            string appliedpost = "";
                                            for (int i = 0; i < dtremarks.Rows.Count; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    appliedpost += ",";
                                                }
                                                appliedpost += Utility.getstring(dtremarks.Rows[i]["postcode"].ToString());
                                            }

                                            trmessage.Visible = true;
                                            // lblremarks.Text = "You have been Shortlisted for Post code :" + dtremarks.Rows[0]["postcode"] + " ";
                                            lblremarks.Text = "You have been Shortlisted for Post code :" + appliedpost + " ";
                                            lblmessage.Text = "Upload eDossier as per Schedule";
                                        }
                                        else
                                        {
                                            lblremarks.Text = "--";
                                            trmessage.Visible = false;

                                        }
                                        DataTable dtremarks_nextexam = objmarks.getrollno_nextexam(lblrollno.Text, tierval);
                                        if (dtremarks_nextexam.Rows.Count != 0)
                                        {
                                            string appliedpost_ne = "";
                                            for (int i = 0; i < dtremarks_nextexam.Rows.Count; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    appliedpost_ne += ",";
                                                }
                                                appliedpost_ne += Utility.getstring(dtremarks_nextexam.Rows[i]["postcode"].ToString());
                                            }

                                            trmsgnextexam.Visible = true;
                                            // lblremarks.Text = "You have been Shortlisted for Post code :" + dtremarks.Rows[0]["postcode"] + " ";
                                            lblmsgnextexam.Text = "You have been Provisionally Shortlisted for Post code :" + appliedpost_ne + " for next Tier Exam";

                                        }
                                        else
                                        {
                                            lblmsgnextexam.Text = "";
                                            trmsgnextexam.Visible = false;

                                        }
                                    }
                                }

                            }


                        }

                        else
                        {
                            tratndnc.Visible = false;
                            trnotinmarks.Visible = true;
                            lblHead.Visible = false;
                            TRUC.Visible = false;
                        }

                    }
                    else
                    {
                        lblHead.Visible = false;
                        TRUC.Visible = false;
                    }

                    //TRUC.Visible = true;                  

                }
                else
                {
                    lblHead.Visible = false;
                    TRUC.Visible = false;
                    tratndnc.Visible = true;
                    tbl1.Visible = false;
                    trnotinmarks.Visible = false;
                }
            }
            else
            {
                lblHead.Visible = false;
                TRUC.Visible = false;
                tbl1.Visible = false;
                tratndnc.Visible = false;
                trnotinmarks.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("ErrorPage.aspx");
        }
    }

    public void clearlabel()
    {
        //lblappno.Text = "";
        lbldob.Text = "";
        lblExamid.Text = "";
        lblmrksobtd.Text = "";
        lblname.Text = "";
        lblpostcode.Text = "";
        lblremarks.Text = "";
        lblrollno.Text = "";
        lblmessage.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // //string url = md5util.CreateTamperProofURL("Application.aspx", null, "flag=" + MD5Util.Encrypt("print", true));
        // //ClientScript.RegisterStartupScript(this.GetType(), "CalValue", "<script>javascript:window.open('" + url + "','_blank')</script>");
        //// MasterPage.FindControl("master").Visible = false;
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);  


        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Result.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //StringWriter stringWriter = new StringWriter();
        //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        //div1.RenderControl(htmlTextWriter);

        //StringReader stringReader = new StringReader(stringWriter.ToString());
        //Document Doc = new Document(PageSize.A4, 10f, 10f, 20f, 0f);


        //HTMLWorker htmlparser = new HTMLWorker(Doc);
        //PdfWriter.GetInstance(Doc, Response.OutputStream);

        //Doc.Open();
        //htmlparser.Parse(stringReader);
        //Doc.Close();
        //Response.Write(Doc);
        //Response.End();

        Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            PdfPTable table = null;

            Phrase phrase = null;
            PdfPCell cell = null;
            document.Open();

            table = new PdfPTable(2);
            table.TotalWidth = 400f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.2f, 0.8f });

            phrase = new Phrase();
            phrase.Add(new Chunk("Result", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.Colspan = 2;
            table.AddCell(cell);

            table.AddCell(PhraseCell(new Phrase("Exam ID :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblExamid.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("Post Applied :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblpostcode.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("Roll Number :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblrollno.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("Name :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblname.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("DOB :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lbldob.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("Marks Obtained :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblmrksobtd.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("Remarks  :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblremarks.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblmessage.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            table.AddCell(PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
            table.AddCell(PhraseCell(new Phrase(lblmsgnextexam.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));

            //phrase = new Phrase();
            //phrase.Add(new Chunk(lblmessage.Text, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            //cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
            //cell.Colspan = 2;
            //table.AddCell(cell);

            document.Add(table);
            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }

    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.BLACK;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }

}