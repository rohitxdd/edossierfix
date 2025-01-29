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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Threading;

public partial class AdmitCardPdf : Page
{
    DataTable dt = new DataTable();
    eAdmitCard objeadmit = new eAdmitCard();
    MD5Util md5util = new MD5Util();
    string applid = "";
    string examid = "";
    string rbtvalue = "";
    string flagfromintra = "";
    string post = "";
    string postcode = "";
    string flagfromboard = "";
    Random randObj = new Random();
    Int32 UniqueRandomNumber = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["applid"] != null)
            {
                //if (MD5Util.Decrypt(Request.QueryString["admiit"].ToString(), true) == "1")
                //{
                applid = MD5Util.Decrypt(Request.QueryString["applid"].ToString(), true);
                examid = MD5Util.Decrypt(Request.QueryString["examid"].ToString(), true);
                rbtvalue = objeadmit.getexamtype(examid);
                //if (Request.QueryString["rbtvalue"] != null)
                //{
                //    rbtvalue = MD5Util.Decrypt(Request.QueryString["rbtvalue"].ToString(), true);
                //}
                if (Request.QueryString["flagfromintradssb"] != null)
                {
                    flagfromintra = MD5Util.Decrypt(Request.QueryString["flagfromintradssb"].ToString(), true);
                }
                if (Request.QueryString["flagfromboard"] != null)
                {
                    flagfromboard = MD5Util.Decrypt(Request.QueryString["flagfromboard"].ToString(), true);
                }
                string ReExam = "";
                if (Request.QueryString["ReExam"] != null)
                {
                    ReExam = MD5Util.Decrypt(Request.QueryString["ReExam"].ToString(), true);
                }
                string batchid = "";
                if (Request.QueryString["batchid"] != null)
                {
                    batchid = MD5Util.Decrypt(Request.QueryString["batchid"].ToString(), true);
                }
                string isprov = "";
                if (Request.QueryString["isprov"] != null)
                {
                    isprov = MD5Util.Decrypt(Request.QueryString["isprov"].ToString(), true);
                }
                if (ReExam == "Y")
                {
                    dt = objeadmit.getAdmitcarddetailsforReExam(applid, examid, flagfromintra, isprov);
                }
                else
                {
                    if (batchid != "")
                    {
                        dt = objeadmit.getAdmitcarddetails_batchwise(applid, examid, flagfromintra, batchid, isprov);
                    }
                    else
                    {
                        dt = objeadmit.getAdmitcarddetails(applid, examid, flagfromintra, rbtvalue, flagfromboard, isprov);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["jid"].ToString()) <= 598 && Convert.ToInt32(dt.Rows[0]["jid"].ToString()) != 591)
                    {
                        Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
                        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                            Phrase phrase = null;
                            PdfPCell cell = null;
                            PdfPTable table = null;
                            PdfPTable mainTable = null;
                            PdfPTable mainTable2 = null;
                            PdfPTable mainTable3 = null;
                            PdfPTable covidTable = null;
                            PdfPTable covidDeclaration = null;
                            Color color = null;
                            PdfPTable table3 = null;
                            document.Open();

                            //Header Table
                            mainTable = new PdfPTable(1);
                            mainTable.TotalWidth = 500f;
                            mainTable.LockedWidth = true;
                            mainTable.SetWidths(new float[] { 1.0f });
                            mainTable.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            phrase.Add(new Chunk("CANDIDATE'S COPY", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.BorderWidthBottom = 1f;
                            cell.BorderWidthLeft = 1f;
                            cell.BorderWidthTop = 1f;
                            cell.BorderWidthRight = 1f;
                            mainTable.AddCell(cell);


                            table = new PdfPTable(2);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.SetWidths(new float[] { 0.15f, 0.55f });

                            cell = ImageCell("~/images/dsssblogo.jpg", 50f, PdfPCell.ALIGN_CENTER);
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Goverment of NCT of Delhi\n", FontFactory.GetFont("Arial", 12, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk("DELHI SUBORDINATE SERVICES SELECTION BOARD\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk("FC-18,INSTITUTIONAL AREA,KARKARDOOMA,DELHI\n\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                            if (flagfromintra == "1")
                            {
                                //lbl_sample.Text = "Sample ";
                                phrase.Add(new Chunk("Sample e - Admit Card", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));

                            }
                            else if (flagfromintra == "2")
                            {
                                //lbl_sample.Text = "Reprinted ";
                                phrase.Add(new Chunk("Reprinted e - Admit Card", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));


                            }
                            else if (flagfromintra == "5")
                            {
                                //lbl_sample.Text = "Reprinted ";
                                phrase.Add(new Chunk("Reprinted e - Admit Card", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));


                            }
                            if (rbtvalue == "2")
                            {
                                //lblexamtype.Text = "(Tier-II Exam)";
                                //lblexamtype.Visible = true;
                                phrase.Add(new Chunk("(Tier-II Exam)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));

                            }
                            else if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                //lblexamtype.Text = "(Tier-III Exam)";
                                //lblexamtype.Visible = true;
                                //phrase.Add(new Chunk("(Physical Endurance Test)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("(Computer Based Exam)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("(PET/ Skill Test )", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                }

                            }
                            else if (rbtvalue == "4")
                            {
                                //lblexamtype.Text = "(Tier-III Exam)";
                                //lblexamtype.Visible = true;
                                phrase.Add(new Chunk("(Tier-III Exam)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));

                            }
                            //else if (rbtvalue == "5")
                            //{
                            //    //lblexamtype.Text = "(Tier-III Exam)";
                            //    //lblexamtype.Visible = true;
                            //    //phrase.Add(new Chunk("(Skill Test)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                            //    phrase.Add(new Chunk("(PET/ Skill Test )", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));

                            //}
                            else
                            {
                                //lblexamtype.Text = "";
                                //lblexamtype.Visible = false;

                            }

                            if (dt.Rows[0]["acstatid"].ToString() == "2" || dt.Rows[0]["acstatid"].ToString() == "4" || dt.Rows[0]["acstatid"].ToString() == "5")
                            {
                                phrase.Add(new Chunk(" (Provisional) \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));

                                DataTable dtoano = objeadmit.get_OAnumber(applid);
                                if (dtoano.Rows[0]["OAnumber"].ToString().ToUpper() == "NIL")
                                {
                                    phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("O.A.Number : " + dtoano.Rows[0]["OAnumber"].ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                    //lbloanunber.Text = "O.A.Number : " + dtoano.Rows[0]["OAnumber"].ToString();
                                    //lbloanunber.Visible = true;
                                }
                                //lbl_prov.Visible = true;

                            }
                            else
                            {
                                phrase.Add(new Chunk("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                            }

                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            //cell.PaddingBottom = 10f;
                            table.AddCell(cell);

                            Byte[] BarCode1bytes = Utility.CreateBarcode(dt.Rows[0]["rollno"].ToString());
                            cell = ImageCellFromBytes(BarCode1bytes, 45f, 100f, PdfPCell.ALIGN_LEFT);
                            cell.Colspan = 2;
                            table.AddCell(cell);
                            mainTable.AddCell(table);
                            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            if (flagfromboard == "Y")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    post = post + dt.Rows[i]["jobtitle"].ToString() + ",";
                                    //post = "Multiple Posts..";
                                    postcode = postcode + " " + dt.Rows[i]["postcode"].ToString();
                                    if (dt.Rows[i]["acstatid"].ToString() == "2" || dt.Rows[i]["acstatid"].ToString() == "4" || dt.Rows[i]["acstatid"].ToString() == "5")
                                    {
                                        postcode += " (Provisional) ";
                                    }
                                    postcode += ",";
                                    //refr = refr + dt.Rows[i]["dummy_no"].ToString() + ",";
                                }
                                if (dt.Rows.Count > 1)
                                {
                                    post = "Multiple Posts as per Post Codes..";
                                }
                                post = post.Substring(0, post.Length - 1);
                                postcode = postcode.Substring(0, postcode.Length - 1);
                            }
                            else
                            {
                                DataTable dt_post = new DataTable();
                                if (ReExam == "Y")
                                {
                                    dt_post = objeadmit.getAdmitcarddetails_post_ReExam(applid, examid, flagfromintra);
                                }
                                else
                                {
                                    if (batchid != "")
                                    {
                                        dt_post = objeadmit.getAdmitcarddetails_post_batchwise(applid, examid, flagfromintra, batchid, isprov);
                                    }
                                    else
                                    {
                                        dt_post = objeadmit.getAdmitcarddetails_post(applid, examid, flagfromintra, rbtvalue);
                                    }
                                }

                                for (int i = 0; i < dt_post.Rows.Count; i++)
                                {
                                    post = post + dt_post.Rows[i]["jobtitle"].ToString() + ",";
                                    //post = "Multiple Posts..";
                                    postcode = postcode + " " + dt_post.Rows[i]["postcode"].ToString();
                                    if (dt_post.Rows[i]["acstatid"].ToString() == "2" || dt_post.Rows[i]["acstatid"].ToString() == "4" || dt_post.Rows[i]["acstatid"].ToString() == "5")
                                    {
                                        postcode += " (Provisional) ";
                                    }
                                    postcode += ",";
                                    //refr = refr + dt.Rows[i]["dummy_no"].ToString() + ",";
                                }
                                if (dt_post.Rows.Count > 1)
                                {
                                    post = "Multiple Posts as per Post Codes..";
                                }
                               //dated:- 18/05/2023
                                if(examid != "523")
                               {
                                post = post.Substring(0, post.Length - 1);
                                postcode = postcode.Substring(0, postcode.Length - 1);
                               }
                            }

                            //refr = refr.Substring(0, refr.Length - 1);
                            //lblpost.Text = Utility.getstring(post).ToUpper();
                            //lblpstcode.Text = postcode;
                            //lblpostcode.Text = lblpstcode.Text;


                            PdfPTable table1 = new PdfPTable(2);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            table1.SetWidths(new float[] { 0.80f, 0.20f });
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            PdfPTable tableInner1 = new PdfPTable(2);
                            tableInner1.SetWidths(new float[] { 0.50f, 0.50f });

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Post Name : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(Utility.getstring(post).ToUpper(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Post Code :", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(postcode, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Exam Date & Time: ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["dateofexam"].ToString() + "" + dt.Rows[0]["timeofexam"].ToString(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 20f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Roll No : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["rollno"].ToString(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 20f;
                            tableInner1.AddCell(cell);
                            table1.AddCell(tableInner1);
                            cell = ImageCell("~/Images/affix_photo.gif", 40f, PdfPCell.ALIGN_CENTER);
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);
                            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            table1 = new PdfPTable(2);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;

                            table1.SetWidths(new float[] { 0.80f, 0.20f });
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            tableInner1 = new PdfPTable(2);
                            tableInner1.SetWidths(new float[] { 0.50f, 0.50f });

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Candidate Name : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["name"].ToString().ToUpper(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Category : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["category"].ToString() + " \n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            if (dt.Rows[0]["SubCategory"].ToString() != "")
                            {
                                phrase.Add(new Chunk("SubCategory : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk(dt.Rows[0]["SubCategory"].ToString() + " \n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));

                                //if (dt.Rows[0]["phsubcat"].ToString() != "")
                                //{
                                //    phrase.Add(new Chunk("(" + dt.Rows[0]["phsubcat"].ToString() + ")", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));

                                //}

                            }
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Father's/Husband's Name : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(Utility.getstring(dt.Rows[0]["fname"].ToString().ToUpper()), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Reporting Time : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["reportingtime"].ToString(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            tableInner1.AddCell(cell);
                            table1.AddCell(tableInner1);

                            string datatransfer = "";
                            DataTable dtckwdatatra = objeadmit.checkwdatatransfer(applid);
                            if (dtckwdatatra.Rows.Count == 0)
                            {
                                datatransfer = "Y";
                            }
                            DataTable dtcheckphoto = objeadmit.getcandidatephoto(applid, flagfromboard, datatransfer);
                            if (dtcheckphoto.Rows.Count > 0)
                            {

                                if (flagfromboard == "Y")
                                {
                                    cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://10.128.65.106/eadmitcard/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 80f, 75f, PdfPCell.ALIGN_CENTER);
                                }
                                else
                                {
                                    //cell = ImageCellFileHandler("ImgHandler2.ashx", 80f, 75f, PdfPCell.ALIGN_CENTER);
                                    cell = ImageCellFileHandler(md5util.CreateTamperProofURL("https://dsssbonline.nic.in/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 80f, 75f, PdfPCell.ALIGN_CENTER);
                                    //cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:7431/DSSBONLINE/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 80f, 75f, PdfPCell.ALIGN_CENTER);
                                }
                                //cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:7431/DSSBONLINE/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("p", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 80f, 75f, PdfPCell.ALIGN_CENTER);

                            }
                            else
                            {
                                phrase = new Phrase();
                                phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            }
                            cell.MinimumHeight = 100f;
                            cell.BorderColor = Color.BLACK;
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            table1 = new PdfPTable(2);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            table1.SetWidths(new float[] { 0.80f, 0.20f });
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Examination Center : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["center_code"].ToString() + "\n" + dt.Rows[0]["centername"].ToString(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.MinimumHeight = 50f;
                            table1.AddCell(cell);
                            if (dtcheckphoto.Rows.Count > 0)
                            {
                                if (flagfromboard == "Y")
                                {
                                    cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://10.128.65.106/eadmitcard/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 80f, 75f, PdfPCell.ALIGN_CENTER);
                                }
                                else
                                {
                                    cell = ImageCellFileHandler(md5util.CreateTamperProofURL("https://dsssbonline.nic.in/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 35f, 87f, PdfPCell.ALIGN_CENTER);
                                    //cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:7431/DSSBONLINE/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 35f, 87f, PdfPCell.ALIGN_CENTER);
                                }
                                // cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:49901/DSSBONLINE/ImgHandler.ashx", null, "id=" + MD5Util.Encrypt(applid.ToString(), true) + "&type=" + MD5Util.Encrypt("s", true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 35f, 87f, PdfPCell.ALIGN_CENTER);


                            }
                            else
                            {
                                phrase = new Phrase();
                                phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            }
                            cell.MinimumHeight = 50f;
                            cell.BorderColor = Color.BLACK;
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);
                            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            table1 = new PdfPTable(2);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            table1.SetWidths(new float[] { 0.80f, 0.20f });
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            tableInner1 = new PdfPTable(3);
                            tableInner1.SetWidths(new float[] { 0.33f, 0.33f, 0.34f });

                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase = new Phrase();
                                    phrase.Add(new Chunk("Signature of Invigilator ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                                    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                                    cell.FixedHeight = 100f;
                                    cell.Colspan = 2;
                                }
                                else
                                {
                                    phrase = new Phrase();
                                    phrase.Add(new Chunk("Signature of Asstt. Observer ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                    cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                                    cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                                    cell.FixedHeight = 100f;
                                    cell.Colspan = 2;
                                }

                            }
                            else
                            {
                                phrase = new Phrase();
                                phrase.Add(new Chunk("Signature of Invigilator - I ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                                cell.FixedHeight = 100f;
                                tableInner1.AddCell(cell);

                                phrase = new Phrase();
                                phrase.Add(new Chunk("Signature of Invigilator - II ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                                cell.FixedHeight = 100f;
                            }
                            tableInner1.AddCell(cell);
                            PdfPTable tableInner2 = new PdfPTable(1);
                            tableInner2.DefaultCell.Border = Rectangle.NO_BORDER;


                            if (dt.Columns.Contains("SigId"))
                            {

                                string sigId = dt.Rows[0]["SigId"].ToString();
                                if (sigId != "")
                                {
                                    //string signtrurl = md5util.CreateTamperProofURL("SigntrHandler.ashx", null, "sigid=" + MD5Util.Encrypt(sigId, true));
                                    if (flagfromboard == "Y")
                                    {
                                        cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://10.128.65.106/eadmitcard/SigntrHandler.ashx", null, "sigid=" + MD5Util.Encrypt(sigId, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 35f, 90f, PdfPCell.ALIGN_CENTER);
                                    }
                                    else
                                    {
                                        cell = ImageCellFileHandler(md5util.CreateTamperProofURL("https://dsssbonline.nic.in/SigntrHandler.ashx", null, "sigid=" + MD5Util.Encrypt(sigId, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 35f, 90f, PdfPCell.ALIGN_CENTER);
                                        //cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:7431/DSSBONLINE/SigntrHandler.ashx", null, "sigid=" + MD5Util.Encrypt(sigId, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true) + "&datatransfer=" + MD5Util.Encrypt(datatransfer, true)), 35f, 90f, PdfPCell.ALIGN_CENTER);
                                    }
                                    //cell = ImageCellFileHandler(md5util.CreateTamperProofURL("http://localhost:49901/DSSBONLINE/SigntrHandler.ashx", null, "sigid=" + MD5Util.Encrypt(sigId, true) + "&flagfromboard=" + MD5Util.Encrypt(flagfromboard, true)), 35f, 90f, PdfPCell.ALIGN_CENTER);
                                }
                                else
                                {
                                    phrase = new Phrase();
                                    phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                                }

                            }
                            else
                            {
                                phrase = new Phrase();
                                phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            }
                            cell.MinimumHeight = 50f;
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            tableInner2.AddCell(cell);
                            tableInner1.AddCell(tableInner2);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Authorized Signatory  \n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(Utility.getstring(dt.Rows[0]["CDesig"].ToString()), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            tableInner2.AddCell(cell);
                            tableInner1.AddCell(tableInner2);
                            table1.AddCell(tableInner1);

                            tableInner1 = new PdfPTable(1);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Candidate's Signature", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            cell.FixedHeight = 50f;
                            tableInner1.AddCell(cell);
                            table1.AddCell(tableInner1);

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Candidate's Left thumb impression", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Candidate's thumb impression", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            }
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            cell.FixedHeight = 50f;
                            tableInner1.AddCell(cell);
                            table1.AddCell(tableInner1);


                            mainTable.AddCell(table1);
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            table1 = new PdfPTable(1);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Candidates are advised to bring pages 1,2 and 3 of this admit card in the examination hall along with one original photo id proof like Voter ID,Adhar card,Driving license etc. ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Candidates are advised to bring pages 1 and 2 of this admit card in the examination hall along with one original photo id proof like Voter ID,Adhar card,Driving license etc. ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            }
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);
                            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            table1 = new PdfPTable(1);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {

                                //phrase = new Phrase();
                                //phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    cell = ImageCell("~/Images/onlineins.jpg", 65f, PdfPCell.ALIGN_CENTER);
                                }
                                else
                                {
                                    cell = ImageCell("~/Images/appins.jpg", 65f, PdfPCell.ALIGN_CENTER);
                                }
                            }
                            else
                            {
                                //INVIGILATOR'S COPY
                                cell = ImageCell("~/Images/ins.jpg", 65f, PdfPCell.ALIGN_CENTER);
                            }
                            cell.FixedHeight = 300f;
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);



                            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            table1 = new PdfPTable(1);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Page 1 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Page 1 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            table1.AddCell(cell);
                            mainTable.AddCell(table1);

                            //document.NewPage();
                            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                        
                            // Second page starts from here

                            mainTable2 = new PdfPTable(1);
                            mainTable2.TotalWidth = 500f;
                            mainTable2.LockedWidth = true;
                            mainTable2.SetWidths(new float[] { 1.0f });
                            mainTable2.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("INVIGILATOR'S COPY", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("ASSTT. OBSERVER'S COPY", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                                }
                            }
                            else
                            {
                                phrase.Add(new Chunk("INVIGILATOR'S COPY", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                            cell.BorderWidthBottom = 1f;
                            cell.BorderWidthLeft = 1f;
                            cell.BorderWidthTop = 1f;
                            cell.BorderWidthRight = 1f;
                            cell.FixedHeight = 10f;
                            mainTable2.AddCell(cell);
                            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            table = new PdfPTable(2);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.SetWidths(new float[] { 0.50f, 0.50f });
                            cell = ImageCellFromBytes(BarCode1bytes, 45f, 100f, PdfPCell.ALIGN_RIGHT);
                            cell.Colspan = 2;
                            table.AddCell(cell);
                            mainTable2.AddCell(table);
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            PdfPTable tablePostCardPhoto = new PdfPTable(1);
                            tablePostCardPhoto.TotalWidth = 350f;
                            tablePostCardPhoto.LockedWidth = true;
                            tablePostCardPhoto.SetWidths(new float[] { 1.0f });
                            phrase = new Phrase();
                            //    if (dt.Rows[0]["isonline"].ToString() == "Y")
                            //  {
                            //    phrase.Add(new Chunk("Please paste a recent coloured Postard Size 4\" x 6\" photograph of upper half of body only clearly showing face, both ears and both shoulders. You will not be allowed to appear in the examination if you fail to follow these instructions. \n", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                            //  }
                            //  else
                            //  {
                            phrase.Add(new Chunk("Please paste a Postcard size (4\" x 6\") latest colored photograph here. \n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            // }
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("(The Candidate and the Invigilator to sign across the photograph as indicated in the instructions below.)", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("(The Candidate and the Asstt. Observer to sign across the photograph as indicated in the instructions below.)", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                }
                            }
                            else
                            {
                                phrase.Add(new Chunk("(The Candidate and the Invigilator to sign across the photograph as indicated in the instructions below.)", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            }
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            cell.FixedHeight = 440f;
                            tablePostCardPhoto.AddCell(cell);
                            mainTable2.AddCell(tablePostCardPhoto);
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            table = new PdfPTable(3);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.SetWidths(new float[] { 0.35f, 0.30f, 0.35f });
                            table.SpacingBefore = 10f;


                            phrase = new Phrase();
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("Candidate to put his/her Left hand Thumb Impression here in presence of Invigilator", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("Candidate to put his/her Left hand Thumb Impression here in presence of Asstt. Observer", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                }
                            }
                            else
                            {
                                phrase.Add(new Chunk("Candidate to put his/her Left hand Thumb Impression here in presence of Invigilator", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));

                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            table.AddCell(cell);
                            cell = ImageCell("~/Images/arrow.jpg", 55f, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                            cell = PhraseCellWithBorder(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
                            cell.FixedHeight = 50f;
                            table.AddCell(cell);

                            mainTable2.AddCell(table);

                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            table = new PdfPTable(2);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.SetWidths(new float[] { 0.50f, 0.50f });
                            table.SpacingBefore = 10f;

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Roll No. : ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["rollno"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.Colspan = 2;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Name : ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["name"].ToString().ToUpper(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.Colspan = 2;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Post Code : ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(postcode, FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            table.AddCell(cell);


                            phrase = new Phrase();
                            phrase.Add(new Chunk("_______________________ \n", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk("Signature of the Candidate", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Date of Exam : ", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            phrase.Add(new Chunk(dt.Rows[0]["dateofexam"].ToString() + dt.Rows[0]["timeofexam"].ToString(), FontFactory.GetFont("Arial", 9, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.PaddingTop = 10f;
                            table.AddCell(cell);


                            phrase = new Phrase();
                            phrase.Add(new Chunk("Name : __________________________________", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            //cell.PaddingTop = 50f;
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Roll No. : __________________________________", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            //cell.PaddingTop = 50f;
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.PaddingTop = 10f;
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            cell.Colspan = 2;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("(In Candidate's handwriting in presence of Invigilator)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("(In Candidate's handwriting in presence of Asstt. Observer)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                }
                            }
                            else
                            {
                                phrase.Add(new Chunk("(In Candidate's handwriting in presence of Invigilator)", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            cell.Colspan = 2;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("\n", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("I have verified signature and photo printed above with the face & signature of the appearing candidate", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));


                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                                cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                                cell.Colspan = 2;
                                table.AddCell(cell);
                            }
                            phrase = new Phrase();
                            phrase.Add(new Chunk("_____________________________ \n", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    phrase.Add(new Chunk("Signature of the Invigilator", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                }
                                else
                                {
                                    phrase.Add(new Chunk("Signature of the Asstt. Observer", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                                }
                            }
                            else
                            {
                                phrase.Add(new Chunk("Signature of the Invigilator", FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            cell.Colspan = 2;
                            table.AddCell(cell);

                            string lbltoshow = "";
                            if (rbtvalue == "3" || rbtvalue == "5" || rbtvalue == "6")
                            {
                                if (dt.Rows[0]["isonline"].ToString() == "Y")
                                {
                                    lbltoshow = "Invigilator";
                                }
                                else
                                {
                                    lbltoshow = "Asstt. Observer";
                                }
                            }
                            else
                            {
                                lbltoshow = "Invigilator";
                            }

                            if (dt.Rows[0]["isonline"].ToString() == "")
                            {
                                phrase = new Phrase();
                                phrase.Add(new Chunk("INSTRUCTIONS FOR CANDIDATES:\n\n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("a) The Candidate to paste a latest colored postcard size (4\" x 6\") photograph of his/her own in the designated space. \n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("b) " + lbltoshow + " shall ensure that photograph and signature on this page matches with photograph and signature of Candidate on Page 1 of Admit Card.\n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("c) The Candidate to sign across the photograph on left side and put his/her Left  hand Thumb Impression in the designated space,", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("in the presence of the " + lbltoshow + ". \n", FontFactory.GetFont("Arial", 8, Font.UNDERLINE, Color.BLACK)));
                                phrase.Add(new Chunk("d) The " + lbltoshow + " should sign across the photograph of the candidate on the right side.  \n", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("e)  It is mandatory for the candidate to bring this page of the Admit Card with pasted photograph.", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));
                                phrase.Add(new Chunk("If he/she doesn't bring this , then he/she will not be allowed to enter the examination centre. \n", FontFactory.GetFont("Arial", 8, Font.UNDERLINE, Color.BLACK)));
                                phrase.Add(new Chunk("f) It is mandatory to handover this page to the " + lbltoshow + ".", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)));

                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                                cell.Colspan = 2;
                                cell.PaddingTop = 25f;
                                table.AddCell(cell);
                            }

                            mainTable2.AddCell(table);
                            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                       
                            table = new PdfPTable(1);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Page 2 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Page 2 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table.AddCell(cell);
                            mainTable2.AddCell(table);

                            // ------covid instruction start--------------//

                            covidTable = new PdfPTable(1);
                            covidTable.TotalWidth = 500f;
                            covidTable.LockedWidth = true;
                            covidTable.SetWidths(new float[] { 1.0f });
                            covidTable.DefaultCell.Border = Rectangle.NO_BORDER;

                            table1 = new PdfPTable(1);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;

                            cell = ImageCell("~/Images/CovidInstruction.jpg", 80f, PdfPCell.ALIGN_CENTER);

                            cell.FixedHeight = 800f;
                            cell.PaddingTop = 40f;
                            table1.AddCell(cell);
                            covidTable.AddCell(table1);


                            table = new PdfPTable(1);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Page 4 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Page 4 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table.AddCell(cell);
                            covidTable.AddCell(table);

                            //------ instruction end---------------//
                            //------ Covid declaratione start---------------//

                            covidDeclaration = new PdfPTable(1);
                            covidDeclaration.TotalWidth = 500f;
                            covidDeclaration.LockedWidth = true;
                            covidDeclaration.SetWidths(new float[] { 1.0f });
                            covidDeclaration.DefaultCell.Border = Rectangle.NO_BORDER;

                            table1 = new PdfPTable(1);
                            table1.TotalWidth = 500f;
                            table1.LockedWidth = true;

                            cell = ImageCell("~/Images/CovidDeclaration.jpg", 80f, PdfPCell.ALIGN_CENTER);

                            cell.FixedHeight = 800f;
                            cell.PaddingTop = 40f;
                            table1.AddCell(cell);
                            covidDeclaration.AddCell(table1);


                            table = new PdfPTable(1);
                            table.TotalWidth = 500f;
                            table.LockedWidth = true;
                            table.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                phrase.Add(new Chunk("Page 3 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            else
                            {
                                phrase.Add(new Chunk("Page 3 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            }
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table.AddCell(cell);
                            covidDeclaration.AddCell(table);
                            //------ Covid declaratione end---------------//
                            //Page 3
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            mainTable3 = new PdfPTable(1);
                            mainTable3.TotalWidth = 500f;
                            mainTable3.LockedWidth = true;
                            mainTable3.SetWidths(new float[] { 1.0f });
                            mainTable3.DefaultCell.Border = Rectangle.NO_BORDER;



                            table3 = new PdfPTable(1);
                            table3.TotalWidth = 500f;
                            table3.LockedWidth = true;

                            cell = ImageCell("~/Images/onlineacpage3.jpg", 150f, PdfPCell.ALIGN_CENTER);


                            cell.FixedHeight = 500f;
                            table3.AddCell(cell);
                            mainTable3.AddCell(table3);

                            table3 = new PdfPTable(1);
                            table3.TotalWidth = 500f;
                            table3.LockedWidth = true;
                            table3.DefaultCell.Border = Rectangle.NO_BORDER;

                            phrase = new Phrase();
                            phrase.Add(new Chunk("Page 3 of 4", FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            cell.VerticalAlignment = PdfCell.ALIGN_BOTTOM;
                            table3.AddCell(cell);
                            mainTable3.AddCell(table3);
                            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                            document.Add(mainTable);
                            document.Add(mainTable2);
                            if (dt.Rows[0]["isonline"].ToString() == "Y")
                            {
                                document.Add(mainTable3);
                            }
                            document.Add(covidDeclaration);
                            document.Add(covidTable);
                            document.Close();
                            byte[] bytes = memoryStream.ToArray();

                            string watermarkImagePath = "~/Images/dsssblogo_Watermark.jpg";
                            var img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(watermarkImagePath));
                            img.SetAbsolutePosition(50, 150);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                PdfReader reader = new PdfReader(bytes);

                                PdfStamper stamper = new PdfStamper(reader, stream);
                                // {
                                for (var i = 0; i < reader.NumberOfPages; i++)
                                {
                                    var content = stamper.GetUnderContent(i + 1);
                                    content.AddImage(img);
                                }
                                stamper.Close();

                                bytes = stream.ToArray();
                                stream.Close();
                            }

                            memoryStream.Close();
                            string emailflg = "";
                            if (Request.QueryString["emailflg"] != null)
                            {
                                emailflg = MD5Util.Decrypt(Request.QueryString["emailflg"].ToString(), true);
                            }
                            if (emailflg == "Y")
                            {
                                string emailid = dt.Rows[0]["email"].ToString();
                                string path = Server.MapPath("~/TEMP/") + "AdmitCard_" + dt.Rows[0]["rollno"].ToString() + ".pdf";
                                File.WriteAllBytes(path, bytes);

                                //email
                                Email objemail = new Email();
                                string body = "Please find attached Admit Card for exam to be held on " + dt.Rows[0]["dateofexam"].ToString();
                                bool emailsent = objemail.sendMail(emailid, "", "", "sadsssb.delhi@nic.in", "Admit Card", body, path);
                                if (emailsent)
                                {
                                    int temp = objeadmit.UpdateAdmitcarddownload_emailsent(applid);
                                }
                                File.Delete(path);

                            }

                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["rollno"].ToString() + "_AdmitCard.pdf");
                            Response.ContentType = "application/pdf";
                            Response.Buffer = true;
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(bytes);

                            Response.End();

                            Response.Close();

                        }
                    }
                    else
                    {
                       string url = md5util.CreateTamperProofURL("AdmitCard_AsPerEdcil.aspx", null, "applid=" + MD5Util.Encrypt(applid, true) + "&examid=" + MD5Util.Encrypt(examid, true));
                       Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                    }
                }
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
        cell.PaddingBottom = 2f;
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