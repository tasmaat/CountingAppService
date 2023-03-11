using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountingForms.DictionaryForms
{
    public partial class ImageForm : Form
    {
       // public PictureBox PictureBox;
        public ImageForm( PictureBox picture)
        {
            InitializeComponent();
            pictureBox1.Image = picture.Image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
