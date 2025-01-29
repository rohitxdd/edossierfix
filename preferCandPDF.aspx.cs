using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using System.Data;
using iTextSharp.text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;


public partial class preferCandPDF : BasePage
{
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    DataTable dt3 = new DataTable();
    CandidateData objCandD = new CandidateData();
    CandCombdData Candprefer = new CandCombdData();
    message msg = new message();
    MD5Util md5util = new MD5Util();
    string jid;
    string applid;
    string rollno;
    protected void Page_Load(object sender, EventArgs e)
    {
        jid = MD5Util.Decrypt(Request.QueryString["jid"].ToString(), true);
        applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
        rollno = MD5Util.Decrypt(Request.QueryString["rollno"].ToString(), true);
        if (!IsPostBack)
        {
            generatepdf();
        }
    }

    private void generatepdf()
    {
            try
            {
            dt = Candprefer.getdeptdata(jid);
            dt1 = Candprefer.getcandidatedata(jid, applid);
            dt2 = Candprefer.postdata(jid);
            dt3 = Candprefer.canddata(applid);

            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx");
            }
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
                Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    Phrase phrase = null;
                    PdfPCell cell = null;
                    
                    document.Open();

                    PdfPTable table1 = new PdfPTable(3);
                    table1.TotalWidth = 500f;
                    table1.LockedWidth = true;
                    table1.SetWidths(new float[] { 0.2f, 0.3f, 0.5f });
                    table1.DefaultCell.Border = Rectangle.NO_BORDER;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\n\nDELHI SUBORDINATE SERVICES SELECTION BOARD\n", FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 3;
                    table1.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("COMBINED EXAMINATION, 2022 FOR "+ dt2.Rows[0]["jobtitle"].ToString() + "\n\n", FontFactory.GetFont("Arial", 12, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 3;
                    table1.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\n\n Post Code : "+ dt2.Rows[0]["postcode"].ToString()+"                           Post Name : "+ dt2.Rows[0]["jobtitle"].ToString()+"\n\n", FontFactory.GetFont("Arial", 12, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 3;

                    table1.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("S.No.", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    table1.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("Dept Code", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    table1.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("Dept Name", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    table1.AddCell(cell);

                    
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        phrase = new Phrase();
                        phrase.Add(new Chunk((i + 1).ToString(), FontFactory.GetFont("Arial", 10, Color.BLACK)));
                        cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                        cell.Padding = 3f;
                        table1.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk(dt.Rows[i]["deptcode"].ToString(), FontFactory.GetFont("Arial", 10, Color.BLACK)));
                        cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                        cell.Padding = 3f;
                        table1.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk(dt.Rows[i]["deptname"].ToString(), FontFactory.GetFont("Arial", 10, Color.BLACK)));
                        cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                        cell.Padding = 3f;
                        table1.AddCell(cell);                     
                    }
                   
                    document.Add(table1);

                    PdfPTable table2 = new PdfPTable(2);
                    table2.TotalWidth = 500f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] {0.40f, 0.60f});
                    table2.DefaultCell.Border = Rectangle.NO_BORDER;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\n\n PREFERENCE FORM \n\n", FontFactory.GetFont("Arial", 12, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 2;
                    table2.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\n\n NAME OF APPLICANT : " + dt3.Rows[0]["name"].ToString() + "                         ROLL NO. : " + dt3.Rows[0]["rollno"].ToString() + "\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 2;

                    table2.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\n\n I have indicated preferences in the order of Priority as per my eligibility as follows: \n\n", FontFactory.GetFont("Arial", 9, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    cell.Colspan = 2;
                    table2.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("Preference", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    table2.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("Dept Code", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                    cell.Padding = 3f;
                    table2.AddCell(cell);



                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        phrase = new Phrase();
                        phrase.Add(new Chunk(dt1.Rows[i]["preference"].ToString(), FontFactory.GetFont("Arial", 10, Color.BLACK)));
                        cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                        cell.Padding = 3f;
                        table2.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk(dt1.Rows[i]["deptcode"].ToString(), FontFactory.GetFont("Arial", 10, Color.BLACK)));
                        cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                        cell.Padding = 3f;
                        table2.AddCell(cell);
                    }

                phrase = new Phrase();
                phrase.Add(new Chunk("\n\n Declaration:    1. 	The Preferences have been filled by me and not by any other person on my behalf..  \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                cell.Padding = 3f;
                cell.Colspan = 2;
                table2.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("              2.	I have indicated preference(s) for those Departments for which I am eligible as per the RR as on the last date of submission of the application form. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                cell.Padding = 3f;
                cell.Colspan = 2;
                table2.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("               3. The preferences have been indicated by me in order of priority. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                cell.Padding = 3f;
                cell.Colspan = 2;
                table2.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("           4. I understand that the order of preference once exercised by me is final and will not be changed subsequently under any circumstances. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                cell.Padding = 3f;
                cell.Colspan = 2;
                table2.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("          5. I have read the eligibility conditions / Recruitment rules as given in the Advertisement Notice and being eligible for the post have filled the above preferences. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                cell.Padding = 3f;
                cell.Colspan = 2;
                table2.AddCell(cell);

                //    phrase = new Phrase();
                //    phrase.Add(new Chunk("\n\n Note:    1. 	Candidates should apply only for those department posts for which they are eligible.  \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //    cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //    cell.Padding = 3f;
                //    cell.Colspan = 2;
                //    table2.AddCell(cell);

                //    phrase = new Phrase();
                //    phrase.Add(new Chunk("              2.	All the three erstwhile corporations of Delhi have been unified w.e.f. 22.05.2022 as Municipal Corporation of Delhi. Hence, all the results which are to be declared by DSSSB for the requisition of either NDMC, SDMC or EDMC will be issued/declared in the name of Municipal Corporation of Delhi (MCD) as requested by MCD vide Letter No. AO (Estt.)/S.O-V/CED/MCD/2022/1889 dated 06.09.2022. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //    cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //    cell.Padding = 3f;
                //    cell.Colspan = 2;
                //    table2.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("               3. Order of preference can not be changed subsequently after final submission of the form. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("           4. Once the candidates has been given his first available as per his merit, he will not be considered for any other option. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("          5. While giving prefernce of posts, the candidate must ensure that they fullfill all the requirements of the posts before giving there preferences/options for such posts. Options once shall be treated as final and will not be changed susequently under any circumstances. Therefore, Candidates must be careful in exercise of such options. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("         6. The DSSSB shall make final allotment of posts on the basis of merit-cum prefernce of posts given by the candidates and once a post alloted, no change of posts will be made by the DSSSB due to non-fulfillment of any post specific requirements of physical/medical/educational standards, etc. In other words, for example if a candidate has given a preference for a post and is selected for that post; in that case, if he(hereinafter may be read as 'he/she')fails to meet the medical/physical/educational standards, his(hereinafter may be read as 'he/she') candidature will be rejected and he will not be considered for other preferences. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);


                //phrase = new Phrase();
                //phrase.Add(new Chunk("       7. Final selection of candidates, will be made on the basis of 'overall performance in Tier-II Examination' and 'preference of posts' exercised by them. Once the candidate has been given his first available preference, as per his merit, he will not be considered for any other option. Candidates are, therefore, required to exercise preference very carefully. The option/preference once exercised by the candidates will be treated as FINAL and IRREVERSIBLE subsequent request for change of allocation/service by candidates will not be entertained under any circumstances/reasons.  \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);

                //phrase = new Phrase();
                //phrase.Add(new Chunk("                   Candidate are, therefore required to exercise preference of departments very carefully. The option/Preference once submitted by the candidates will be treated as final. Subsequent request for any change of allocation of department by candidate will not be entertained under any circumstances/reason. \n\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);


                //phrase = new Phrase();
                //phrase.Add(new Chunk("Undertaking by the Candidate: ", FontFactory.GetFont("Arial", 10,Font.BOLD, Color.BLACK)));
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //cell.Padding = 3f;
                //cell.Colspan = 2;
                //table2.AddCell(cell);

                //    phrase = new Phrase();
                //    phrase.Add(new Chunk(" I have read the eligibility conditions of the departments given in the advertisement notice and accordingly filled the preferences as above, as per my eligibility and the same may be treated as final. \n\n\n\n", FontFactory.GetFont("Arial", 10, Color.BLACK)));
                //    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                //    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                //    cell.Padding = 3f;
                //    cell.Colspan = 2;
                //    table2.AddCell(cell);

                phrase = new Phrase();
                    phrase.Add(new Chunk("Signature.............................. \n\n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                    cell.Padding = 3f;
                    cell.Colspan = 2;
                    table2.AddCell(cell);
                    phrase = new Phrase();
                    phrase.Add(new Chunk("Name of Candidate...................... \n\n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                    cell.Padding = 3f;
                    cell.Colspan = 2;
                    table2.AddCell(cell);
                    phrase = new Phrase();
                    phrase.Add(new Chunk("Roll No................................ \n\n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                    cell.Padding = 3f;
                    cell.Colspan = 2;
                    table2.AddCell(cell);

                    document.Add(table2);



                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CandidatePreferences.pdf");
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
        cell.Border = 0;
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
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCellFileHandler(string url, float height, float width, int align)
    {
        message msg = new message();
        msg.Show(url);
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Uri(url));
        //image.ScalePercent(scale);
        image.ScaleAbsolute(width, height);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;


    }

    private static PdfPCell ImageCellFromBytes(byte[] bytes, float height, float width, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bytes);
        //image.ScalePercent(scale);
        image.ScaleAbsolute(width, height);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
}