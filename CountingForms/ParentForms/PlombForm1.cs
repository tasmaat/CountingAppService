using CountingDB;
using CountingForms.DictionaryForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.ParentForms
{
    public partial class PlombForm1 : Form
    {
        public DataSet Pechat4 = null;
        private int id_EncashPoint;
        private BindingSource bindingSource;
        private OpenFileDialog openFileDialog;  
        private Timer timer1 = new Timer();
        MSDataBase dataBase = new MSDataBase();
        public PlombForm1(int id_EncashPoint1, int i)
        {
            InitializeComponent();

            MSDataBase dataBase = new MSDataBase();
            dataBase.Connect();

            bindingSource = new BindingSource();           
           
            this.id_EncashPoint = id_EncashPoint1;
          
            
            //MessageBox.Show("id_EncashPoint=" + id_EncashPoint1.ToString());
            
            Pechat4 = dataBase.GetData9("SELECT TOP (1000) * FROM[CountingDB].[dbo].[t_g_pechatclien] where id_encashpoint = "+ id_EncashPoint);
           
           
            dgList.AutoGenerateColumns = false;
            dgList.Columns.Add("name_pechat", "Наименование пересчета");
            dgList.Columns["name_pechat"].Visible = true;
            dgList.Columns["name_pechat"].Width = 100;
            dgList.Columns["name_pechat"].DataPropertyName = "name_pechat";

            
            dgList.Columns.Add("id", "id");
            dgList.Columns["id"].Visible = false;
            dgList.Columns["id"].DataPropertyName = "id";

            bindingSource.DataSource = Pechat4.Tables[0];
            dgList.DataSource = bindingSource.DataSource;
            if(Pechat4!=null&& Pechat4.Tables[0].Rows.Count>0)
            textBox1.Text = Pechat4.Tables[0].AsEnumerable().Select(x => x.Field<string>("name_pechat")).First<string>();
            pokazimg();
            if(i==1)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = false;
            }
            else if(i==2)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = true; 
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            if (Pechat4.Tables[0].Rows.Count > 0)
                if (((DataRowView)bindingSource.Current)["id"].ToString() == "")
                {                    
                    if (!dataBase.Status())
                        dataBase.Connect();
                    DataSet dataSet = dataBase.GetData9("Select * from [t_g_pechatclien]");
                    DataRow row1 = dataSet.Tables[0].NewRow();
                    row1["id_encashpoint"] = ((DataRowView)bindingSource.Current)["id_encashpoint"];
                    row1["name_pechat"] = ((DataRowView)bindingSource.Current)["name_pechat"];
                    row1["img1"] = ((DataRowView)bindingSource.Current)["img1"];
                    row1["img2"] = ((DataRowView)bindingSource.Current)["img2"];
                    row1["img3"] = ((DataRowView)bindingSource.Current)["img3"];
                    row1["img4"] = ((DataRowView)bindingSource.Current)["img4"];

                    row1["creation"] = DateTime.Now;
                    row1["lastupdate"] = DateTime.Now;
                    row1["last_update_user"] = DataExchange.CurrentUser.CurrentUserId;

                    dataSet.Tables[0].Rows.Add(row1);

                    dataBase.UpdateData(dataSet, "t_g_pechatclien");


                    MessageBox.Show("Запись создана!");
                    Close();
                }
                else
                {                    
                    
                    if (!dataBase.Status())
                        dataBase.Connect();

                    DataSet dataSet = dataBase.GetData9("Select * from [t_g_pechatclien]");
                    DataRow row = dataSet.Tables[0].AsEnumerable().Where(x => x.Field<Int64>("id") ==Convert.ToInt64(((DataRowView)bindingSource.Current)["id"])).First<DataRow>();
                    
                    row["id_encashpoint"] = ((DataRowView)bindingSource.Current)["id_encashpoint"];
                    row["name_pechat"] = textBox1.Text;
                    row["img1"] = ((DataRowView)bindingSource.Current)["img1"];
                    row["img2"] = ((DataRowView)bindingSource.Current)["img2"];
                    row["img3"] = ((DataRowView)bindingSource.Current)["img3"];
                    row["img4"] = ((DataRowView)bindingSource.Current)["img4"];

                    row["lastupdate"] = DateTime.Now;
                    row["last_update_user"] = DataExchange.CurrentUser.CurrentUserId;

                   
                    dataBase.UpdateData(dataSet, "t_g_pechatclien");
                    MessageBox.Show("Запись обновлена!");
                    Close();
                }
        }
        private void pokazimg()
        {
            bindingSource.DataSource = Pechat4.Tables[0];
            dgList.DataSource = bindingSource.DataSource;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            if (dgList.CurrentCell != null)
            {
                if (Pechat4 != null && Pechat4.Tables[0] != null)
                {

                    if (dgList.CurrentRow.Index < Pechat4.Tables[0].Rows.Count)
                    {
                        if ((((DataRowView)bindingSource.Current)["img1"] != null) & (((DataRowView)bindingSource.Current)["img1"] != DBNull.Value))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img1"], 0, ((byte[])((DataRowView)bindingSource.Current)["img1"]).Length);

                            pictureBox1.Image = Image.FromStream(memoryStream);

                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                            memoryStream.Dispose();
                        }
                        else
                            pictureBox1.Image = null;

                        if ((((DataRowView)bindingSource.Current)["img2"] != null) & (((DataRowView)bindingSource.Current)["img2"] != DBNull.Value))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img2"], 0, ((byte[])((DataRowView)bindingSource.Current)["img2"]).Length);

                            pictureBox2.Image = Image.FromStream(memoryStream);

                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                            memoryStream.Dispose();
                        }
                        else
                            pictureBox2.Image = null;

                        if ((((DataRowView)bindingSource.Current)["img3"] != null) & (((DataRowView)bindingSource.Current)["img3"] != DBNull.Value))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img3"], 0, ((byte[])((DataRowView)bindingSource.Current)["img3"]).Length);

                            pictureBox3.Image = Image.FromStream(memoryStream);

                            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

                            memoryStream.Dispose();
                        }
                        else
                            pictureBox3.Image = null;

                        if ((((DataRowView)bindingSource.Current)["img4"] != null) & (((DataRowView)bindingSource.Current)["img4"] != DBNull.Value))
                        {
                            MemoryStream memoryStream = new MemoryStream();

                            memoryStream.Write((byte[])((DataRowView)bindingSource.Current)["img4"], 0, ((byte[])((DataRowView)bindingSource.Current)["img4"]).Length);

                            pictureBox4.Image = Image.FromStream(memoryStream);

                            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

                            memoryStream.Dispose();
                        }
                        else
                            pictureBox4.Image = null;
                    }
                }
            }
        }

        private void dgList_SelectionChanged(object sender, EventArgs e)
        {
           pokazimg();
        }

        //private void dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    pokazimg();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            
            openFileDialog = new OpenFileDialog();
         
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox1.ImageLocation = openFileDialog.FileName;
            this.Text = openFileDialog.FileName;
            System.Drawing.Image img = System.Drawing.Image.FromFile(Text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);


            if (dgList.CurrentCell != null)

            {
                ((DataRowView)bindingSource.Current)["img1"] = ms.ToArray();
                ((DataRowView)bindingSource.Current)["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                ((DataRowView)bindingSource.Current)["lastupdate"] = DateTime.Now.ToString();
            }
            else
            {                
                DataRow row = Pechat4.Tables[0].NewRow();
                row["img1"] = ms.ToArray();
                row["id_encashpoint"] = id_EncashPoint.ToString();
                row["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                row["lastupdate"] = DateTime.Now.ToString();
                row["name_pechat"] = textBox1.Text.ToString().Trim();
                row["creation"] = DateTime.Now.ToString();
                Pechat4.Tables[0].Rows.Add(row);
            }

            
            timer1.Interval = 100;

            timer1.Tick += obrabfile1;
            timer1.Enabled = true;

         

            MessageBox.Show("Операция выполнена!");

            
        }
        private void obrabfile1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            pokazimg();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox2.ImageLocation = openFileDialog.FileName;
            this.Text = openFileDialog.FileName;
            System.Drawing.Image img = System.Drawing.Image.FromFile(Text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);




            if (dgList.CurrentCell != null)
              //  if(Pechat4.Tables[0].Rows.Count>0)
            {



                ((DataRowView)bindingSource.Current)["img2"] = ms.ToArray();
                ((DataRowView)bindingSource.Current)["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                ((DataRowView)bindingSource.Current)["lastupdate"] = DateTime.Now.ToString();
            }
            else
            {
              //  Pechat4 = null;
             //   Pechat4 = dataBase.GetData("t_g_pechatclien");
                DataRow countDenomRow = Pechat4.Tables[0].NewRow();
                countDenomRow["img2"] = ms.ToArray();
                countDenomRow["id_encashpoint"] = id_EncashPoint.ToString();
                countDenomRow["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                countDenomRow["lastupdate"] = DateTime.Now.ToString();               
                countDenomRow["name_pechat"] = textBox1.Text.ToString().Trim();
                countDenomRow["creation"] = DateTime.Now.ToString();
                Pechat4.Tables[0].Rows.Add(countDenomRow);
            }

            timer1 = new Timer();
            timer1.Interval = 100;

            timer1.Tick += obrabfile1;
            timer1.Enabled = true;



            MessageBox.Show("Операция выполнена!");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox3.ImageLocation = openFileDialog.FileName;
            this.Text = openFileDialog.FileName;
            System.Drawing.Image img = System.Drawing.Image.FromFile(Text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            if (dgList.CurrentCell != null)
            {
                ((DataRowView)bindingSource.Current)["img3"] = ms.ToArray();
                ((DataRowView)bindingSource.Current)["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                ((DataRowView)bindingSource.Current)["lastupdate"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow row = Pechat4.Tables[0].NewRow();
                row["img3"] = ms.ToArray();
                row["id_encashpoint"] = id_EncashPoint.ToString();
                row["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                row["lastupdate"] = DateTime.Now.ToString();
                row["name_pechat"] = textBox1.Text.ToString().Trim();
                row["creation"] = DateTime.Now.ToString();
                Pechat4.Tables[0].Rows.Add(row);
            }
            timer1.Interval = 100;

            timer1.Tick += obrabfile1;
            timer1.Enabled = true;



            MessageBox.Show("Операция выполнена!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            pictureBox4.ImageLocation = openFileDialog.FileName;
            this.Text = openFileDialog.FileName;
            System.Drawing.Image img = System.Drawing.Image.FromFile(Text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            if (dgList.CurrentCell != null)
            {
                ((DataRowView)bindingSource.Current)["img4"] = ms.ToArray();
                ((DataRowView)bindingSource.Current)["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                ((DataRowView)bindingSource.Current)["lastupdate"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow row = Pechat4.Tables[0].NewRow();
                row["img4"] = ms.ToArray();
                row["id_encashpoint"] = id_EncashPoint.ToString();
                row["last_update_user"] = DataExchange.CurrentUser.CurrentUserId.ToString();
                row["lastupdate"] = DateTime.Now.ToString();
                row["name_pechat"] = textBox1.Text.ToString().Trim();
                row["creation"] = DateTime.Now.ToString();
                Pechat4.Tables[0].Rows.Add(row);
            }
            timer1.Interval = 100;

            timer1.Tick += obrabfile1;
            timer1.Enabled = true;

            MessageBox.Show("Операция выполнена!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;

                ((DataRowView)bindingSource.Current)["img1"] = null;

                MessageBox.Show("Операция выполнена!");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image = null;

                ((DataRowView)bindingSource.Current)["img2"] = null;

                MessageBox.Show("Операция выполнена!");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image = null;

                ((DataRowView)bindingSource.Current)["img3"] = null;

                MessageBox.Show("Операция выполнена!");

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Image != null)
            {
                pictureBox4.Image = null;

                ((DataRowView)bindingSource.Current)["img4"] = null;

                MessageBox.Show("Операция выполнена!");

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image!=null)
            {
                ImageForm imageForm = new ImageForm(pictureBox1);                
                
                DialogResult dialogResult = imageForm.ShowDialog();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                ImageForm imageForm = new ImageForm(pictureBox2);

                DialogResult dialogResult = imageForm.ShowDialog();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Image != null)
            {
                ImageForm imageForm = new ImageForm(pictureBox4);

                DialogResult dialogResult = imageForm.ShowDialog();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                ImageForm imageForm = new ImageForm(pictureBox3);

                DialogResult dialogResult = imageForm.ShowDialog();
            }
        }
    }
}
