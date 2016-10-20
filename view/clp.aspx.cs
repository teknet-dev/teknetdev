﻿using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tessnet2;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using Image = System.Drawing.Image;

namespace PR3.view
{
    public partial class pr3 : System.Web.UI.Page
    {
        private static int nbC;
        private static string mot;
        private const int maxWidth = 750;
        private const int maxHeight = 750;

        public DataTable tableAH = null;
        private int [] TableCorrespondance = {   
                                                0,
                                               15,
                                               35,
                                               60,
                                              100,
                                              150,
                                              240
        } ;

        protected void Page_Load(object sender, EventArgs e)
        {
            imagController imagC = new imagController();
            imagC.idimg = "jo";
            imagC.longueur = 3;
            imagC.hautueur = 2;
            load();
            // Création de l'objet Bdd pour l'intéraction avec la base de donnée MySQL
            nbLettreTxt.Visible = false;
            ImagModel bdd = new ImagModel();
            bdd.AddContact(imagC);
        }

        //Fonction qui permet de detecter les nombre de caractère
        public int getNbOfCharacter(Image imageToSplit)
        {
            string s = "";
            var image = new Bitmap(imageToSplit);
            var ocr = new Tesseract();
            ocr.SetVariable("load_system_dawg", false);
            ocr.SetVariable("load_freq_dawg", false);
            ocr.Init(Server.MapPath(@"\tessdata\"), "eng", false);
            var result = ocr.DoOCR(image, Rectangle.Empty);
            int nbLettre = 0;
            foreach (tessnet2.Word word in result)
            {
                s += word.Text;
                nbLettre += word.Text.Length;
            }

            mot = s;
            return (nbC = nbLettre) ;
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        protected void btnCrop_Click1(object sender, EventArgs e)
        {
            string confilename, confilepath;
            string fileName = Path.GetFileName(imgUpload.ImageUrl);
            string filePath = Path.Combine(Server.MapPath("~/img"), fileName);

            if (File.Exists(filePath))
            {
                System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                Rectangle CropArea = new Rectangle
                (
                    Convert.ToInt32(X.Value),
                    Convert.ToInt32(Y.Value),
                    Convert.ToInt32(W.Value),
                    Convert.ToInt32(H.Value)
                );

                try
                {
                    Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                    using (Graphics g = Graphics.FromImage(bitMap))
                    {
                        g.DrawImage(cropImage(orgImg, CropArea), new Point(0, 0));
                    }
                    int v = CropArea.Width;
                    string r = v.ToString();
                    test.Text = r;

                    int s = CropArea.Height;
                    string h = s.ToString();
                    haute.Text = h;
                    confilename = "Crp_" + fileName;
                    confilepath = Path.Combine(Server.MapPath("~/crpmg"), confilename);
                    bitMap.Save(confilepath);
                    cropimg.Visible = true;
                    cropimg.Src = "~/crpmg/" + confilename;
                    int nbCrtr = this.getNbOfCharacter(bitMap);
                    panCrop.Visible = false;
                    string prefixe = "caractère";
                    if(nbCrtr > 1) prefixe += "s";
                }
                catch
                {
                    throw;
                }
            }
        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {
            string uploadFileName = "";

            if (FU1.HasFile)
            {
                string ext = Path.GetExtension(FU1.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                {
                    HttpPostedFile pf = FU1.PostedFile;
                    uploadFileName = Guid.NewGuid().ToString() + ext;
                    System.Drawing.Image imageToUplolad = System.Drawing.Image.FromStream(pf.InputStream);
                    int imageWidth = imageToUplolad.Width;
                    int imageHeigth = imageToUplolad.Height;

                    double rapport = (double)imageWidth / (double)imageHeigth;

                    if (imageWidth > maxWidth)
                    {
                        imageWidth = maxWidth;
                        imageHeigth = (int)(imageWidth / rapport) ;
                    }

                    if (imageHeigth > maxHeight)
                    {
                        imageHeigth = maxHeight;
                        imageWidth = (int)(imageHeigth * rapport);
                    }
                    imageToUplolad = ResizeBitmap((Bitmap)imageToUplolad, imageWidth, imageHeigth); ///new width, height
                    string ssssssssssss ="" ;
                    try
                    {
                         ssssssssssss = Path.Combine(Server.MapPath("~/img/"), uploadFileName);
                        imageToUplolad.Save(Path.Combine(Server.MapPath("~/img/"), uploadFileName));
                    }
                    catch (Exception newE) {
                        lblMsg.Text = newE.Message + "   " +ssssssssssss;
                    }                              
                    
                    try
                    {
                        imgUpload.ImageUrl = "~/img/" + uploadFileName;
                        panCrop.Visible = true;
                        btnCrop.Visible = true;
                        cropimg.Visible = false;
                    }
                    catch
                    {
                        lblMsg.Text = "Error! Please try again";
                    }
                }
                else
                {
                    lblMsg.Text = "Select file type not allowed!";
                }

            }
            else
            {
                lblMsg.Text = "Please select file first!";
            }
        }

        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        public void load() {
                TxtSearch.Attributes.Add("onkeyUp", "return doSearch();");
                TxtSearch.Attributes.Add("onfocus", String.Format("SetCursorToTextEnd({0})",TxtSearch.ID));
                CreationTableAH();
                TxtSearch_TextChanged(null,null);
        }

        private void CreationTableAH()
        {
            tableAH = new DataTable("TableLED");
            tableAH.Columns.Add(new DataColumn("Type", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Designation", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Puissance", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Voltage", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Espacement", typeof(String)));


            tableAH.Columns.Add(new DataColumn("Tail", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Couleur", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Module chaine", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Longueur câble", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Module ml", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Profondeur", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Prix catalogue", typeof(String)));
            tableAH.Columns.Add(new DataColumn("Luminosité", typeof(String)));
        }
        private DataTable tableBind;
        private void remplirTable(string conditions = "")
        {
            try
            {
                //if (tableAH.Rows.Count == 0)
                //{
                    MySqlConnection conn = null;
                    string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
                    conn = new MySqlConnection(_strConn);
                    conn.Open();
                    string query = "SELECT type, designation, puissance, voltage, espacement, tail, couleur, modules_chaine, longueur_cable, modules_ml, profondeur, prix_catalogue, nblumen FROM led " + conditions;
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    tableAH.Clear();
                    while (reader.Read())
                    {
                        DataRow row = tableAH.NewRow();
                        row[0] = reader["type"].ToString();
                        row[1] = reader["designation"].ToString();
                        row[2] = reader["puissance"].ToString();
                        row[3] = reader["voltage"].ToString();
                        row[4] = reader["espacement"].ToString();


                        row[5] = reader["tail"].ToString();
                        row[6] = reader["couleur"].ToString();
                        row[7] = reader["modules_chaine"].ToString();
                        row[8] = reader["longueur_cable"].ToString();
                        row[9] = reader["modules_ml"].ToString();
                        row[10] = reader["profondeur"].ToString();
                        row[11] = reader["prix_catalogue"].ToString();
                        row[12] = reader["nblumen"].ToString();
                        tableAH.Rows.Add(row);
                    }
                //}
                //tableBind = filter(conditions) ;
                tableBind = tableAH;

                LEDList.DataSource = tableBind ;
                LEDList.DataBind();
//              LEDList.SelectedIndex = -1; // Problème quand on va calculer....
                List_SelectedIndexChanged(null, null);
            }
            catch { };
        }

        public void insertImag() {
            try
            {
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;

                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                // Ouverture de la connexion SQL
                   string im=txtespace.Text;
                   string lo = test.Text;
                   string larg = test.Text;
                   cn.Open();

                // Requête SQL
                string query = "INSERT INTO image (idimg, longueur, hauteur) VALUES ('"+im+"','"+lo+"','"+larg+"')";

                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
               }
            }
            catch {}
        }

        public void insertResult()
        {
            try
            {
                System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                provider.NumberGroupSeparator = ".";
                decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
                decimal ht0 = Convert.ToDecimal(haute.Text, provider);
                decimal ll0 = Convert.ToDecimal(test.Text, provider);
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;

                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                    decimal sulta = (ht0 * ll0);
                    string resulta = sulta.ToString();
                    //puissance         
                    decimal reltp = sulta * carb;
                    string resultpw = reltp.ToString();

                    string lt = TxtLed.Text;
                    cn.Open();
                    // Requête SQL
                    string query = "INSERT INTO resultat (type_Led, Puissance_Total,Nombre_Led,Alimantation) VALUES ('" + lt + "','" + resultpw + "','" + resulta + "','" + nbV.Text + "')";

                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch{}
        }

        public void insertResult2()
        {
            try
            {
                System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                provider.NumberGroupSeparator = ".";
                //puissance
                decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
                //long
                decimal numl = Convert.ToDecimal(test.Text, provider);
                //hauteur
                decimal numh = Convert.ToDecimal(haute.Text, provider);
                //espacement
                decimal nume = Convert.ToDecimal(txtespace.Text, provider);
                string _strConn = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;

                using (MySqlConnection cn = new MySqlConnection(_strConn))
                {
                      //nombre led    
                    decimal resu = (numh * numl)/nume;
                    string nnbled = resu.ToString();
                
                    //convert       
                    string lt = TxtLed.Text;
                    //puissance
                    decimal r=resu*carb;
                    string resultp = r.ToString();

                    cn.Open();
                    // Requête SQL
                    string query = "INSERT INTO resultat (type_Led, Puissance_Total,Nombre_Led,Alimantation) VALUES ('" + lt + "','" + resultp + "','" + nnbled + "','" + nbV.Text + "')";

                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();

                }
            }
            catch {}
        }
        
		private int chercheCorrespondance(double d)
        {
            int i = 0;
            while ((i < TableCorrespondance.Length) && (d > TableCorrespondance[i])) i++ ;
            if (i >= TableCorrespondance.Length) return Convert.ToInt32(d);
            else return TableCorrespondance[i];
        }
		
        protected void Button1_Click(object sender, EventArgs e)
        {
		   // dao();
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ",";
            provider.NumberGroupSeparator = ".";
            decimal carb = Convert.ToDecimal(txtpuis.Text, provider);
            decimal ht = Convert.ToDecimal(haute.Text, provider);
            decimal ll = Convert.ToDecimal(test.Text, provider);
          //  decimal res = Convert.ToDecimal(txtpuis.Text, provider);
            if (txtespace.Text.Equals(""))
            {
                decimal result = (ht * ll);
                //puissance         
                string afresult = result.ToString();
                nbLed.Text = afresult;
                decimal res = Convert.ToDecimal(result, provider);
                decimal pw1 = chercheCorrespondance((double)res * (double)carb) ;
                string resultpw = pw1.ToString();
                nbP.Text = resultpw;

                insertImag();
                insertResult();
            }
            else
            {
                int k;
                int echelle = 1 ;
                switch (DropDownList2.SelectedIndex)
                {
                    case 0: k = 3; break;
                    case 1: k = 5; break;
                    case 2: k = 7; break;
                    default: k = 9; break;
                }
                decimal carb2 = Convert.ToDecimal(txtpuis.Text, provider);

                decimal ht2 = Convert.ToDecimal(haute.Text, provider); // Height

                //nb led.
                decimal result = Convert.ToInt32((echelle * ht2 * k * nbC) * Convert.ToInt32(LEDList.SelectedRow.Cells[9].Text) / (1000)) ;
          
                string afresult = result.ToString(); // Entier
                nbLed.Text = afresult;

                //puissance
                decimal pw1 = chercheCorrespondance((double)result * (double)carb2) ;
                string resultpw = pw1.ToString();
                nbP.Text = resultpw;
                insertResult2();
                insertImag();

                drawImageTebk() ;

            }
        }

        protected void drawImageTebk() 
        {
            string extension = Path.GetExtension(cropimg.Src) ;
            string filePath = Path.Combine(Server.MapPath("~/crpmg"),"temp" + extension) ;
            string nomFont;
            switch (DropDownList2.SelectedIndex)
            {
                case 0: nomFont = "enhanced_dot_digital-7"; break;
                case 1: nomFont = "advanced_dot_digital-7"; break;
                case 2: nomFont = "triple_dot_digital-7"; break;
                default: nomFont = ""; break;
            }

            Bitmap bitMap ;
            
            using (Image org = (Image)Bitmap.FromFile(Server.MapPath(cropimg.Src)))
                bitMap = new Bitmap(org.Width, org.Height);
            using (Graphics graph = Graphics.FromImage(bitMap))
            {
                String text = mot ;
                Rectangle ImageSize = new Rectangle(0,0,bitMap.Width,bitMap.Height);
                graph.FillRectangle(Brushes.White, ImageSize);
                StringFormat strFormat = new StringFormat();

                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;

                String temp = Server.MapPath("/library/font/" + nomFont + ".ttf") ;
                

                graph.DrawString(text,scalling(graph,temp,text,bitMap.Width,bitMap.Height), Brushes.Black,
                new Rectangle(0, 0, bitMap.Width, bitMap.Height), strFormat);
            } 

            bitMap.Save(filePath) ;
            cropimg.Src = "~/crpmg/" + "temp" + extension ;
        }
        private Font scalling(Graphics g, String family,string text, int width, int height)
        {
            PrivateFontCollection p = new PrivateFontCollection();
            p.AddFontFile(family);
            Font fontFamily = new Font(p.Families[0],10);
            SizeF RealSize = g.MeasureString(text, fontFamily);
            float ratio = (float)RealSize.Height / (float)RealSize.Width;
            float boxRatio = (float)height / (float)width;
            float ScaleRatio ;
            if (ratio > boxRatio) // (height / Width = r)
            {
                //resize  height
                ScaleRatio = (float)height / (float)RealSize.Height;
            }
            else
            {
                //resize width
                ScaleRatio = (float)width / (float)RealSize.Width;
            }
            float ScaleFontSize = fontFamily.Size*ScaleRatio*0.9f ;
            return new Font(fontFamily.FontFamily, ScaleFontSize);
        }

        protected void List_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LEDList, "Select$" + e.Row.RowIndex); // Activates click capability
            }
        }

        protected void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LEDList.SelectedIndex > -1)
            {
                TxtLed.Text = LEDList.SelectedRow.Cells[1].Text;
                txtpuis.Text = LEDList.SelectedRow.Cells[2].Text;
                txtespace.Text = LEDList.SelectedRow.Cells[4].Text;
                nbV.Text = LEDList.SelectedRow.Cells[3].Text;
                nbV.ReadOnly = true;
                nbV.BackColor = Color.WhiteSmoke;
            }
            else{}
        }
        
        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string req = TxtSearch.Text;
            if (req == "")
            {
                remplirTable();
                return;
            }

            // Condition dans la base : type, designation, puissance, voltage, espacement, tail, couleur, modules_chaine, longueur_cable, modules_ml, profondeur, prix_catalogue, nblumen
            req = " WHERE type LIKE '%" + req + "%' OR " +
                        " designation LIKE '%" + req + "%' OR " +
                        " puissance LIKE '%" + req + "%' OR " +
                        " voltage LIKE '%" + req + "%' OR " +
                        " espacement LIKE '%" + req + "%' OR " +

                        " tail LIKE '%" + req + "%' OR " +
                        " couleur LIKE '%" + req + "%' OR " +
                        " modules_chaine LIKE '%" + req + "%' OR " +
                        " longueur_cable LIKE '%" + req + "%' OR " +
                        " modules_ml LIKE '%" + req + "%' OR " +
                        " profondeur LIKE '%" + req + "%' OR " +
                        " prix_catalogue LIKE '%" + req + "%' OR " +
                        " nblumen LIKE '%" + req + "%'";
            remplirTable(req);

//          TxtSearch.Focus();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}

