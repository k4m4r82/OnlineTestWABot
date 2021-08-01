/**
 * Copyright (C) 2020 Kamarudin (http://coding4ever.net/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * The latest version of this file can be found at https://github.com/k4m4r82/OnlineTestWABot
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WhatsAppNETAPI;
using ConceptCave.WaitCursor;

using OnlineTestWABot.Model.Entity;
using OnlineTestWABot.Controller;
using System.Threading;
using System.Diagnostics;

namespace OnlineTestWABot.WinApp
{
    public partial class FrmMain : Form
    {
        private const string ABOUT = "*WhatsApp Bot - Online Test {0}*\r\n" +
                                     "By *{1}*\r\n" +
                                     "Email : {2}\r\n" +
                                     "Blog : {3}";

        private const string BANTUAN = "*WhatsApp Bot - Online Test {0}*\r\n" +
                                       "By *{1}*\r\n" +
                                       "Email : {2}\r\n" +
                                       "Blog : {3}\r\n\r\n" +

                                        "Anda akan mendapatkan *40 SOAL* dengan model pilihan ganda.\r\n\r\n" +

                                        "Daftar keyword yang tersedia :\r\n\r\n" +
                                        "\U00002705 *about* - informasi program\r\n" +
                                        "\U00002705 *bantuan* - untuk menampilkan perintah yang tersedia\r\n" +
                                        "\U00002705 *mulai* - untuk memulai tes bahasa inggris\r\n" +
                                        "\U00002705 *soal* - untuk mendapatkan soal tes bahasa inggris\r\n" +
                                        "\U00002705 *soalterakhir* - informasi soal terakhir\r\n" +
                                        "\U00002705 *jawab jawaban* - untuk menjawab soal. contoh *jawab A*\r\n" +
                                        "\U00002705 *batal* - untuk mengabaikan soal terakhir/enggak sanggup jawab \U0001F61C\r\n" +
                                        "\U00002705 *selesai* - untuk mengakhiri tes bahasa Inggris";

        private const string PERINTAH_SALAH = "Maaf perintah *{0}* tidak dikenal, ketik *bantuan* untuk informasi lebih lanjut.";
        private const string AUTHOR = "Kamarudin";
        private const string EMAIL = "rudi.krsoftware@gmail.com";
        private const string URL = "http://coding4ever.net/";

        private string _currentVersion = string.Empty;
        private IWhatsAppNETAPI _wa; // deklarasi objek WhatsApp NET Client

        public FrmMain()
        {
            InitializeComponent();
            _wa = new WhatsAppNETAPI.WhatsAppNETAPI();

            _currentVersion = string.Format("v{0}", GetCurrentVersion());
            this.Text = string.Format("WhatsApp Bot - Online Test {0}", _currentVersion);
        }

        private string GetCurrentVersion()
        {
            var fvi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            var version = fvi.ProductMajorPart + "." + fvi.ProductMinorPart + "." + fvi.ProductBuildPart;

            return version;
        }

        private void AutoReplay(User user, Chat chat, ref string msgToReplay)
        {
            if (chat.text.Length > 0)
            {
                var splitPesan = chat.text.Split(' ');
                var keyword = splitPesan[0];
                var param = string.Empty;

                if (splitPesan.Count() > 1) param = splitPesan[1];

                SaveUser(user);
                SaveChat(chat);
                
                var perintahBot = new PerintahBot();

                switch (keyword.ToLower())
                {
                    case "about":
                        msgToReplay = string.Format(ABOUT, _currentVersion, AUTHOR, EMAIL, URL);
                        break;

                    case "bantuan":
                        msgToReplay = string.Format(BANTUAN, _currentVersion, AUTHOR, EMAIL, URL);
                        break;

                    case "mulai":
                        perintahBot.Mulai(user.user_id, ref msgToReplay);
                        break;

                    case "soal":
                        perintahBot.Soal(user.user_id, ref msgToReplay);
                        break;

                    case "soalterakhir":
                        perintahBot.SoalTerakhir(user.user_id, ref msgToReplay);
                        break;

                    case "jawab":
                        perintahBot.Jawab(user.user_id, param, ref msgToReplay);                        
                        break;

                    case "batal":
                        perintahBot.Batal(user.user_id, ref msgToReplay);
                        break;

                    case "selesai":
                        perintahBot.Selesai(user.user_id, ref msgToReplay);                        
                        break;

                    default:
                        msgToReplay = string.Format(PERINTAH_SALAH, keyword.ToLower());
                        break;
                }

                Console.WriteLine("msgToReplay: {0}", msgToReplay);
            }
        }

        private void SaveChat(Chat obj)
        {
            IChatController controller = new ChatController();
            var result = controller.Save(obj);
        }

        private void SaveUser(User obj)
        {
            IUserController controller = new UserController();
            var result = controller.Save(obj);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLokasiWhatsAppNETAPINodeJs.Text))
            {
                MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs'  belum di set", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtLokasiWhatsAppNETAPINodeJs.Focus();
                return;
            }

            _wa.WaNetApiNodeJsPath = txtLokasiWhatsAppNETAPINodeJs.Text;

            if (!_wa.IsWaNetApiNodeJsPathExists)
            {
                MessageBox.Show("Maaf, lokasi folder 'WhatsApp NET API NodeJs' tidak ditemukan !!!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtLokasiWhatsAppNETAPINodeJs.Focus();
                return;
            }

            Connect();
        }

        private void Connect()
        {
            this.UseWaitCursor = true;

            // subscribe event
            _wa.OnStartup += OnStartupHandler;
            _wa.OnReceiveMessage += OnReceiveMessageHandler;
            _wa.OnClientConnected += OnClientConnectedHandler;

            _wa.Connect();

            using (var frm = new FrmStartUp())
            {
                // subscribe event
                _wa.OnStartup += frm.OnStartupHandler;
                _wa.OnScanMe += frm.OnScanMeHandler;

                frm.UseWaitCursor = true;
                frm.ShowDialog();

                // unsubscribe event
                _wa.OnStartup -= frm.OnStartupHandler;
                _wa.OnScanMe -= frm.OnScanMeHandler;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            using (new StCursor(Cursors.WaitCursor, new TimeSpan(0, 0, 0, 0)))
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;

                _wa.Disconnect();
            }
        }

        private void btnLokasiWAAutomateNodejs_Click(object sender, EventArgs e)
        {
            var folderName = ShowDialogOpenFolder();

            if (!string.IsNullOrEmpty(folderName)) txtLokasiWhatsAppNETAPINodeJs.Text = folderName;
        }

        private string ShowDialogOpenFolder()
        {
            var folderName = string.Empty;

            using (var dlgOpen = new FolderBrowserDialog())
            {
                var result = dlgOpen.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlgOpen.SelectedPath))
                {
                    folderName = dlgOpen.SelectedPath;
                }
            }

            return folderName;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        # region event handler

        private void OnStartupHandler(string message)
        {
            // koneksi ke WA berhasil
            if (message.IndexOf("Ready") >= 0)
            {
                btnStart.Invoke(new MethodInvoker(() => btnStart.Enabled = false));
                btnStop.Invoke(new MethodInvoker(() => btnStop.Enabled = true));

                this.UseWaitCursor = false;
            }

            // koneksi ke WA GAGAL, bisa dicoba lagi
            if (message.IndexOf("Failure") >= 0 || message.IndexOf("Timeout") >= 0
                || message.IndexOf("ERR_NAME") >= 0)
            {
                // unsubscribe event
                _wa.OnStartup -= OnStartupHandler;
                _wa.OnReceiveMessage -= OnReceiveMessageHandler;
                _wa.OnClientConnected -= OnClientConnectedHandler;

                _wa.Disconnect();

                this.UseWaitCursor = false;

                var msg = string.Format("{0}\n\nKoneksi ke WA gagal, silahkan cek koneksi internet Anda", message);
                MessageBox.Show(msg, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnReceiveMessageHandler(WhatsAppNETAPI.Message message)
        {
            var user = new User
            {
                user_id = message.from
            };

            var chat = new Chat
            {
                user_id = message.from,
                text = message.content
            };

            var msgToReplay = string.Empty;
            AutoReplay(user, chat, ref msgToReplay);

            _wa.ReplyMessage(new ReplyMsgArgs(message.from, msgToReplay, message.id));
        }

        private void OnClientConnectedHandler()
        {
            System.Diagnostics.Debug.Print("ClientConnected on {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }

        #endregion                
    }
}
