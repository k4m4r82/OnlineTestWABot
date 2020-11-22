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

                                        "Daftar keyword yang tersedia :\r\n\r\n" +
                                        "\U00002705 */about* - informasi program\r\n" +
                                        "\U00002705 */bantuan* - untuk menampilkan perintah yang tersedia\r\n" +
                                        "\U00002705 */mulai* - untuk memulai tes bahasa inggris\r\n" +
                                        "\U00002705 */soal* - untuk mendapatkan soal tes bahasa inggris\r\n" +
                                        "\U00002705 */soalterakhir* - informasi soal terakhir\r\n" +
                                        "\U00002705 */jawab jawaban* - untuk menjawab soal. contoh */jawab A*\r\n" +
                                        "\U00002705 */batal* - untuk mengabaikan soal terakhir/enggak sanggup jawab \U0001F61C\r\n" +
                                        "\U00002705 */selesai* - untuk mengakhiri tes bahasa Inggris";

        private const string PERINTAH_SALAH = "Maaf perintah *{0}* tidak dikenal, ketik */bantuan* untuk informasi lebih lanjut.";
        private const string AUTHOR = "Kamarudin";
        private const string EMAIL = "rudi.krsoftware@gmail.com";
        private const string URL = "http://coding4ever.net/";

        private string _currentVersion = string.Empty;
        private IWhatsAppNETAPI _whatsAppApi; // deklarasi objek WhatsApp NET Client

        public FrmMain()
        {
            InitializeComponent();

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
                    case "/about":
                        msgToReplay = string.Format(ABOUT, _currentVersion, AUTHOR, EMAIL, URL);
                        break;

                    case "/bantuan":
                        msgToReplay = string.Format(BANTUAN, _currentVersion, AUTHOR, EMAIL, URL);
                        break;

                    case "/mulai":
                        perintahBot.Mulai(user.user_id, ref msgToReplay);
                        break;

                    case "/soal":
                        perintahBot.Soal(user.user_id, ref msgToReplay);
                        break;

                    case "/soalterakhir":
                        perintahBot.SoalTerakhir(user.user_id, ref msgToReplay);
                        break;

                    case "/jawab":
                        perintahBot.Jawab(user.user_id, param, ref msgToReplay);                        
                        break;

                    case "/batal":
                        perintahBot.Batal(user.user_id, ref msgToReplay);
                        break;

                    case "/selesai":
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
            _whatsAppApi = new WhatsAppNETAPI.WhatsAppNETAPI();

            var url = "https://web.whatsapp.com";

            using (new StCursor(Cursors.WaitCursor, new TimeSpan(0, 0, 0, 0)))
            {
                // buka chrome web browser untuk menjalankan WhatsApp Web
                if (_whatsAppApi.Connect(url, chkHeadless.Checked))
                {
                    while (!_whatsAppApi.OnReady())
                    {
                        if (chkHeadless.Checked)
                        {
                            if (_whatsAppApi.IsScanMe())
                            {
                                var frmScanQRCode = new FrmScanQRCode(_whatsAppApi);
                                frmScanQRCode.ShowDialog();
                            }
                        }

                        Thread.Sleep(1000);
                    }

                    // subscribe event OnMessageRecieved 
                    _whatsAppApi.OnMessageRecieved += OnMessageRecievedEventHandler;
                    _whatsAppApi.MessageSubscribe();

                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                    _whatsAppApi.Disconnect();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            using (new StCursor(Cursors.WaitCursor, new TimeSpan(0, 0, 0, 0)))
            {
                // unsubscribe event OnMessageRecieved 
                _whatsAppApi.OnMessageRecieved -= OnMessageRecievedEventHandler;
                _whatsAppApi.MessageUnSubscribe();

                // tutup chrome web browser
                _whatsAppApi.Disconnect();

                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }      
        }

        private void OnMessageRecievedEventHandler(MsgArgs e)
        {            
            var user = new User
            {
                user_id = e.Sender
            };

            var chat = new Chat
            {
                user_id = e.Sender,
                text = e.Msg
            };

            var msgToReplay = string.Empty;
            AutoReplay(user, chat, ref msgToReplay);

            _whatsAppApi.SendMessage(new MsgArgs(e.Sender, msgToReplay));
        }
    }
}
