using System;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox4.Visible = false;
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox3.Visible = true;
                groupBox4.Visible = false;
                txtPassE.Text = string.Empty;
                txtSendE.Text = string.Empty;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox4.Visible = true;
                groupBox3.Visible = false;
                txtPassD.Text = string.Empty;
                txtSendD.Text = string.Empty;
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "MP4 files (*.MP4)|*.MP4|All files (*.*)|*.*";
            DialogResult result = openFileDialog1.ShowDialog();

            //Если нажата клавиша ОК
            if (result == DialogResult.OK)
            {
                string openFileName = openFileDialog1.FileName;
                txtFolder.Text = openFileName;
                refreshBox();
                Invalidate();
            }

            // Если нажата клавиша Отмена
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            var tfile = TagLib.File.Create(txtFolder.Text);
            if (txtSendE.Text != string.Empty && txtPassE.Text != string.Empty)
            {
                if (tfile.Tag.Album == null && tfile.Tag.Conductor == null)
                {
                    tfile.Tag.Album = txtSendE.Text;
                    tfile.Tag.Conductor = txtPassE.Text;
                    tfile.Save();
                    txtSendE.Text = "Admin> Успех!";
                    txtPassE.Text = "Admin> Операция выполнена";
                    refreshBox();
                }
                else if (tfile.Tag.AlbumSort == null && tfile.Tag.AmazonId == null)
                {
                    tfile.Tag.AlbumSort = txtSendE.Text;
                    tfile.Tag.AmazonId = txtPassE.Text;
                    tfile.Save();
                    txtSendE.Text = "Admin> Успех!";
                    txtPassE.Text = "Admin> Операция выполнена";
                    refreshBox();
                }
                else
                {
                    txtSendE.Text = "Admin> Ошибка!";
                    txtPassE.Text = "Admin> Нет места для записи";
                    refreshBox();
                }
            }
            else
            {
                txtSendE.Text = "Admin> Ошибка!";
                txtPassE.Text = "Admin > Незаполненные поля";
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var tfile = TagLib.File.Create(txtFolder.Text);

            if (txtPassD.Text == tfile.Tag.Conductor)
            {
                txtSendD.Text = tfile.Tag.Album;
                tfile.Tag.Conductor = string.Empty;
                tfile.Tag.Album = string.Empty;
                tfile.Save();
                refreshBox();
            }
            else if (txtPassD.Text == tfile.Tag.AmazonId)
            {
                txtSendD.Text = tfile.Tag.AlbumSort;
                tfile.Tag.AlbumSort = string.Empty;
                tfile.Tag.AmazonId = string.Empty;
                tfile.Save();
                refreshBox();
            }
            else
                txtSendD.Text = "Admin> Неверный пароль!";
        }
        public void refreshBox()
        {
            var tfile = TagLib.File.Create(txtFolder.Text);

            string send2 = tfile.Tag.AlbumSort;
            string pass2 = tfile.Tag.AmazonId;

            string send = tfile.Tag.Album;
            string pass = tfile.Tag.Conductor;

            string title = tfile.Tag.Title;
            TimeSpan duration = tfile.Properties.Duration;
            string description = tfile.Properties.Description;
            int bitRate = tfile.Properties.AudioBitrate;
            int vidHeight = tfile.Properties.VideoHeight;
            int vidWidth = tfile.Properties.VideoWidth;

            string checky = string.Empty;
            if (send != null && pass != null)
                checky = " ✓ ";
            string checky2 = string.Empty;
            if (send2 != null && pass2 != null)
                checky2 = " ✓ ";

            richTextBox1.Text = "Название: " + checky + title + " " + checky2 +
                " \nПродолжительность: " + duration + " \nОписание: " + description +
                "\nБитрейт: " + bitRate + " \nРазрешение: " + vidWidth + " х " + vidHeight;
           // + "\n\nLetter: " + send + "\n\nPassword: " + pass + "\n\nLetter2: " + send2 + "\n\nPassword2: " + pass2;
        }

        private void btnCleanD_Click(object sender, EventArgs e)
        {
            txtPassD.Text = string.Empty;
            txtSendD.Text = string.Empty;
        }

        private void btnCleanE_Click(object sender, EventArgs e)
        {
            txtPassE.Text = string.Empty;
            txtSendE.Text = string.Empty;
        }
    }
}
