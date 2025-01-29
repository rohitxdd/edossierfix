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
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class ChallangeAcknowledgement : BasePage
{
    challengeansheet objchallenge = new challengeansheet();
    CandidateData obcd = new CandidateData();
    eAdmitCard objeadmit = new eAdmitCard();
    string examid = "", rid = "", cpdid = "", post = "", exam = "", rbt = "", applid = "", batchid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["examid"] != null)
        {
            examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
        }
        if (Request.QueryString["rid"] != null)
        {
            rid = MD5Util.Decrypt(Request.QueryString["rid"].ToString(), true);
        }
        if (Request.QueryString["cpdid"] != null)
        {
            cpdid = MD5Util.Decrypt(Request.QueryString["cpdid"].ToString(), true);
        }
        if (Request.QueryString["post"] != null)
        {
            post = MD5Util.Decrypt(Request.QueryString["post"].ToString(), true);
        }
        if (Request.QueryString["exam"] != null)
        {
            exam = MD5Util.Decrypt(Request.QueryString["exam"].ToString(), true);
        }
        if (Request.QueryString["applid"] != null)
        {
            applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        }
        if (Request.QueryString["batchid"] != null)
        {
            batchid = MD5Util.Decrypt(Request.QueryString["batchid"].ToString(), true);
        }
        DataTable dtTier = objchallenge.getExamTier(examid);

        if (dtTier.Rows.Count > 0)
        {
            if (dtTier.Rows[0]["examtypeid"].ToString() == "1")
            {
                rbt = "1";
            }
            else if (dtTier.Rows[0]["examtypeid"].ToString() == "2")
            {
                rbt = "2";
            }
            else if (dtTier.Rows[0]["examtypeid"].ToString() == "3")
            {
                rbt = "3";
            }
            else if (dtTier.Rows[0]["examtypeid"].ToString() == "4")
            {
                rbt = "4";
            }
            else if (dtTier.Rows[0]["examtypeid"].ToString() == "5")
            {
                rbt = "5";
            }
        }

       // DataTable dtCandDetails = obcd.getAdmitcarddetails(applid, examid, "1", rbt);
        DataTable dtCandDetails = new DataTable();
        if (batchid != "")
        {
            dtCandDetails = objeadmit.getAdmitcarddetails_batchwise(applid, examid, "1", batchid,"");
        }
        else
        {
            dtCandDetails = objeadmit.getAdmitcarddetails(applid, examid, "1", rbt, "","");
        }
        DataTable dt = objchallenge.GetSuccessChallengemast_Details(examid, rid, cpdid,batchid);

        if (dt.Rows.Count > 0)
        {

            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;

                document.Open();

                //Header Table
                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 1.0f });

                phrase = new Phrase();
                phrase.Add(new Chunk("Printed on : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //cell.PaddingBottom = 10f;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Goverment of NCT of Delhi\n", FontFactory.GetFont("Arial", 12, Font.BOLD, Color.BLACK)));
                phrase.Add(new Chunk("DELHI SUBORDINATE SERVICES SELECTION BOARD\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                phrase.Add(new Chunk("FC-18,INSTITUTIONAL AREA,KARKARDOOMA,DELHI\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                phrase.Add(new Chunk("eChallenge Answers Acknowledgement Slip", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);
                document.Add(table);

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.20f, 0.40f, 0.23f, 0.17f });

                phrase = new Phrase();
                phrase.Add(new Chunk("Name of Candidate :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(dtCandDetails.Rows[0]["name"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Candidate Roll No :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(dtCandDetails.Rows[0]["rollno"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Question Booklet No", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //cell.PaddingTop = 10f;
                //table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk(dt.Rows[0]["QuestionNo"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //cell.PaddingTop = 10f;
                //table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Amount", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //cell.PaddingTop = 10f;
                //cell.PaddingBottom = 10f;
                //table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk(dt.Rows[0]["CAmt"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //cell.PaddingTop = 10f;
                //cell.PaddingBottom = 10f;
                ////cell.Colspan = 3;
                //table.AddCell(cell);



                phrase = new Phrase();
                phrase.Add(new Chunk("Date of Exam :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(exam, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Post Code :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(post, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Amount :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(dt.Rows[0]["CAmt"].ToString() + " /-", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Transaction ID :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(dt.Rows[0]["Orderno"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Bank Transaction Date :", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(dt.Rows[0]["banktrandate"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);
                table.SpacingAfter = 10f;
                document.Add(table);

                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.15f, 0.28f, 0.28f, 0.28f });


                phrase = new Phrase();
                phrase.Add(new Chunk("Challenges Submitted for Question Booklet No : " + dt.Rows[0]["bookletcode"].ToString(), FontFactory.GetFont("Arial", 9, Font.UNDERLINE, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.Colspan = 4;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("S.No.", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Question Booklet No", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Question No", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Answer as per Answer Key", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Answer as per Candidate", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Remarks", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //table.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("Final Response of DSSSB", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                //table.AddCell(cell);

                document.Add(table);


                table = new PdfPTable(4);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.15f, 0.28f, 0.28f, 0.28f });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    phrase = new Phrase();
                    phrase.Add(new Chunk((i + 1).ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk(dt.Rows[i]["QuestionNo"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    table.AddCell(cell);

                    //phrase = new Phrase();
                    //phrase.Add(new Chunk(dt.Rows[i]["QuestionNo"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    //table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk(dt.Rows[i]["ans"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk(dt.Rows[i]["sanswer"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    table.AddCell(cell);

                    //phrase = new Phrase();
                    //phrase.Add(new Chunk(dt.Rows[i]["Remarks"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    //table.AddCell(cell);

                    //phrase = new Phrase();
                    //phrase.Add(new Chunk("Under Process", FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                    //cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                    //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    //table.AddCell(cell);


                }

                table.SpacingAfter = 20f;
                document.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 1.0f });
                phrase = new Phrase();
                phrase.Add(new Chunk("Sd/-\n", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                phrase.Add(new Chunk("For Delhi Subordinate Services Selection Board", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                table.AddCell(cell);
                document.Add(table);



                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Acknowledgement.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }


    }

    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell PhraseCellWithBorder(Phrase phrase, int align)
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