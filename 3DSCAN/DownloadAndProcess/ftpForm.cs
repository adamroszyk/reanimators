using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Needed
using System.IO;
using System.Net;
using System.Security;
using System.Diagnostics;

namespace DownloadDataFTP
{
    public partial class ftpForm : Form
    {
        List<string> files = new List<string>();
        private Timer timer1;
        bool isDownloading = false;

        public ftpForm()
        {
            InitializeComponent();

            refreshCredentials();
        }

        private void refreshCredentials()
        {
            txtFTPAddress.Text = "ftp://vrowser.e-kei.pl/public_html/HACKATON/";
            txtUsername.Text = "adam";
            txtPassword.Text = "###Admin123";

            startRefreshing();
           // DoAll();
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; 
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isDownloading) {
                DoAll();
            }
        }

        private void DoAll() {
            isDownloading = true;
            files.Clear();
            getFileList(txtFTPAddress.Text , txtUsername.Text, txtPassword.Text);
        }

        private void startRefreshing()
        {
            InitTimer();
        }

        private byte[] downloadedData;

        //Connects to the FTP server and downloads the file
        private void downloadFile(string FTPAddress, string filename, string username, string password)
        {
            downloadedData = new byte[0];

            try
            {
                //Optional
                this.Text = "Connecting...";
                Application.DoEvents();

                //Create FTP request
                //Note: format is ftp://server.com/file.ext
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress + "/" + filename) as FtpWebRequest;

                //Optional
                this.Text = "Retrieving Information...";
                Application.DoEvents();

                //Get the file size first (for progress bar)
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true; //don't close the connection

                int dataLength = (int)request.GetResponse().ContentLength;

                //Optional
                this.Text = "Downloading File...";
                Application.DoEvents();

                //Now get the actual data
                request = FtpWebRequest.Create(FTPAddress + "/" + filename) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false; //close the connection when done

                //Set up progress bar
                progressBar1.Value = 0;
                progressBar1.Maximum = dataLength;
                lbProgress.Text = "0/" + dataLength.ToString();

                //Streams
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks

                while (true)
                {
                    Application.DoEvents(); //prevent application from crashing

                    //Try to read the data
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Nothing was read, finished downloading
                        progressBar1.Value = progressBar1.Maximum;
                        lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

                        Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                        //Update the progress bar
                        if (progressBar1.Value + bytesRead <= progressBar1.Maximum)
                        {
                            progressBar1.Value += bytesRead;
                            lbProgress.Text = progressBar1.Value.ToString() + "/" + dataLength.ToString();

                            progressBar1.Refresh();
                            Application.DoEvents();
                        }
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                reader.Close();
                memStream.Close();
                response.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error connecting to the FTP Server."+e.Message);
            }

            this.Text = "Download Data through FTP";

            username = string.Empty;
            password = string.Empty;
        }

        //Connects to the FTP server and request the list of available files
        private void getFileList(string FTPAddress, string username, string password)
        {
            try
            {
                //Create FTP request
                FtpWebRequest request = FtpWebRequest.Create(FTPAddress) as FtpWebRequest;

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream)
                {
                    Application.DoEvents();
                    files.Add(reader.ReadLine());
                }
                reader.Close();
                responseStream.Close(); 
                response.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error connecting to the FTP Server");
            }

            if (files.Count != 0)
            {
                downloadEachFile();
            }
        }

        private void downloadEachFile()
        {
            foreach (String file in files) {
                Console.WriteLine(file);

                if (!File.Exists(file))
                {
                    downloadFile(txtFTPAddress.Text, file, txtUsername.Text, txtPassword.Text);


                    FileStream newFile = new FileStream(file, FileMode.Create);
                    newFile.Write(downloadedData, 0, downloadedData.Length);
                    newFile.Close();
                }
            }
            isDownloading = false;

            checkIfCanProcess();
        }

        private void checkIfCanProcess()
        {
            String searchFolder = Path.GetDirectoryName(Application.ExecutablePath);
            var filters = new String[] { "jpg", "jpeg"};
            var files = GetFilesFrom(searchFolder, filters, false);


            if (files.Length < 8) return;

                int cnt = 0;
            foreach (string file in files)
            {
                cnt++;
                Console.WriteLine(file);
                Console.WriteLine(Path.GetDirectoryName(Application.ExecutablePath)+"\\PHO");
                File.Move(file, Path.GetDirectoryName(Application.ExecutablePath) + "\\PHO\\"+ cnt+".jpg");
                Console.WriteLine("Done transfering files");
            }

            System.Threading.Thread.Sleep(2000);

            string strCmdText;
            strCmdText = "automatic_reconstructor --workspace_path WRK --image_path PHO";
            Process.Start("C:/Users/HPZVRBackpack/Desktop/REPO/reanimators/3DSCAN/DownloadAndProcess/bin/Debug/COLMAP", strCmdText);
            Console.WriteLine("Procedure invoked");


            Application.Exit();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
    }
}