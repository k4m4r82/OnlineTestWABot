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

using OnlineTestWABot.Model.Entity;
using OnlineTestWABot.Controller;

namespace OnlineTestWABot.WinApp
{
    public class PerintahBot
    {
        private const string SESSION_TIMEOUT = @"Maaf sesi tes bahasa Inggris Anda sudah selesai/belum dibuat, ketik *mulai* untuk memulai tes.";
        private const string SESSION_NOT_TIMEOUT = @"Sesi Anda belum habis, silahkan melanjutkan menjawab soal atau *selesai* untuk melihat hasil tes.";
        private const string SOAL_SUDAH_DIJAWAB = @"Maaf soal terakhir tidak ditemukan/sudah dijawab, ketik *soal* untuk mendapatkan soal baru atau *selesai* untuk melihat hasil tes.";
        private const string SESSION_OK = @"Selamat !!! sesi tes bahasa Inggris Anda sudah dibuat, ketik *soal* untuk memulai tes.";
        private const string TES_SELESAI = @"Selamat !!! Anda sudah menyelesaikan semua soal, ketik *selesai* untuk mengakhiri tes.";

        private const string HASIL_TES = "\U0001F3C1 Hasil tes Anda \U0001F3C1\r\n\r\n" +
                                         "\U00002705 *Benar* = {0}\r\n" +
                                         "\U0000274C *Salah* = {1}\r\n" +
                                         "\U0001F4DC *Nilai* = {2}\r\n\r\n" +
                                          "ooO Terima kasih \U0001F64F Ooo";

        public void Mulai(string userId, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                lastSession = SaveSession(userId);
                msgToReplay = SESSION_OK;
            }
            else
            {
                var sekarang = DateTime.Now;
                var ts = sekarang.Subtract(lastSession.tanggal);

                if (ts.Minutes >= 30) // ession time out max 30 menit
                {
                    lastSession = SaveSession(userId);
                    msgToReplay = SESSION_OK;
                }
                else
                {
                    msgToReplay = SESSION_NOT_TIMEOUT;
                }
            }
        }

        public void Soal(string userId, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                msgToReplay = SESSION_TIMEOUT;
            }
            else
            {
                // cek soal terakhir sudah dijawab atau belum
                var lastSoal = GetLastSoal(lastSession.sesi_id);

                if (lastSoal != null && string.IsNullOrEmpty(lastSoal.jawaban))
                {
                    msgToReplay = @"Maaf soal terakhir belum dijawab, ketik *soalterakhir* untuk melihat soal terakhir.";
                }
                else
                {
                    var newSoal = GetNewSoal(lastSession.sesi_id);
                    if (newSoal == null)
                    {
                        msgToReplay = TES_SELESAI;
                    }
                    else
                    {
                        msgToReplay = newSoal.soal + "\r\n\r\n" +
                                  "Ketik: *jawab jawaban* untuk menjawab. Contoh: *jawab a*";

                        msgToReplay = msgToReplay.Replace("[A]", "*[A]*");
                        msgToReplay = msgToReplay.Replace("[B]", "*[B]*");
                        msgToReplay = msgToReplay.Replace("[C]", "*[C]*");
                        msgToReplay = msgToReplay.Replace("[D]", "*[D]*");
                        msgToReplay = msgToReplay.Replace("[E]", "*[E]*");
                        msgToReplay = msgToReplay.Replace("BACAAN", "*BACAAN*");
                        msgToReplay = msgToReplay.Replace("PERTANYAAN", "*PERTANYAAN*");

                        msgToReplay = string.Format("*Soal Nomor {0}*", newSoal.nomor_soal) + ":\r\n\r\n" +
                                      msgToReplay;

                        var histori = new HistoriTes
                        {
                            user_id = userId,
                            sesi_id = lastSession.sesi_id,
                            tanggal = DateTime.Now,
                            bank_soal_id = newSoal.bank_soal_id
                        };

                        SaveHistoriTes(histori);
                    }                    
                }
            }
        }

        public void SoalTerakhir(string userId, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                msgToReplay = SESSION_TIMEOUT;
            }
            else
            {
                var lastSoal = GetLastSoal(lastSession.sesi_id, false, 0);
                if (lastSoal != null)
                {
                    msgToReplay = lastSoal.BankSoal.soal + "\r\n\r\n" +
                                  "Ketik: *jawab jawaban* untuk menjawab. Contoh: *jawab a*";

                    msgToReplay = msgToReplay.Replace("[A]", "*[A]*");
                    msgToReplay = msgToReplay.Replace("[B]", "*[B]*");
                    msgToReplay = msgToReplay.Replace("[C]", "*[C]*");
                    msgToReplay = msgToReplay.Replace("[D]", "*[D]*");
                    msgToReplay = msgToReplay.Replace("[E]", "*[E]*");

                    msgToReplay = string.Format("*Soal Nomor {0}*", lastSoal.BankSoal.nomor_soal) + ":\r\n\r\n" +
                                  msgToReplay;
                }
                else
                {
                    msgToReplay = @"Maaf soal terakhir tidak ditemukan, ketik *soal* untuk mendapatkan soal baru atau *selesai* untuk melihat hasil tes.";
                }
            }
        }

        public void Jawab(string userId, string param, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                msgToReplay = SESSION_TIMEOUT;
            }
            else
            {
                if (string.IsNullOrEmpty(param))
                {
                    msgToReplay = @"Maaf jawaban kosong, ketik *jawab jawaban* untuk menjawab soal. Contoh: /jawab a";
                }
                else
                {
                    var lastSoal = GetLastSoal(lastSession.sesi_id);
                    if (lastSoal != null)
                    {
                        lastSoal.jawaban = param.ToUpper();
                        lastSoal.hasil_jawaban = param.ToUpper() == lastSoal.BankSoal.jawaban.ToUpper();

                        if (lastSoal.hasil_jawaban)
                        {
                            msgToReplay = "Jawaban Anda benar \U0001F44D";
                        }
                        else
                        {
                            msgToReplay = string.Format("Jawaban Anda salah \U0001F61C jawaban yang benar: *{0}*", lastSoal.BankSoal.jawaban);
                        }

                        UpdateHistoriTes(lastSoal);

                        msgToReplay = msgToReplay + string.Format("\r\n\r\nAnalisa/Keterangan:\r\n{0}\r\n\r\nKetik *soal* untuk mendapatkan soal baru atau *selesai* untuk melihat hasil tes.", lastSoal.BankSoal.analisa);
                    }
                    else
                    {
                        msgToReplay = SOAL_SUDAH_DIJAWAB;
                    }
                }
            }
        }

        public void Batal(string userId, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                msgToReplay = SESSION_TIMEOUT;
            }
            else
            {
                var lastSoal = GetLastSoal(lastSession.sesi_id);

                if (lastSoal != null)
                {
                    lastSoal.batal = true;
                    UpdateHistoriTes(lastSoal);

                    msgToReplay = "Soal terakhir sudah dibatalkan, ketik *soal* untuk mendapatkan soal baru atau *selesai* untuk melihat hasil tes.\r\n\r\n";
                }
                else
                {
                    msgToReplay = SOAL_SUDAH_DIJAWAB;
                }
            }
        }

        public void Selesai(string userId, ref string msgToReplay)
        {
            var lastSession = GetLastSession(userId);

            if (lastSession == null)
            {
                msgToReplay = SESSION_TIMEOUT;
            }
            else
            {
                var hasilTes = GetHasilTes(lastSession.sesi_id);
                var nilai = hasilTes.benar * 2.5;

                msgToReplay = string.Format(HASIL_TES, hasilTes.benar, hasilTes.salah, nilai);

                lastSession.is_timeout = true;
                SetTimeout(lastSession);
            }
        }        

        private void SaveHistoriTes(HistoriTes obj)
        {
            IHistoriTesController controller = new HistoriTesController();
            var result = controller.Save(obj);
        }

        private void UpdateHistoriTes(HistoriTes obj)
        {
            IHistoriTesController controller = new HistoriTesController();
            var result = controller.Update(obj);
        }

        private BankSoal GetNewSoal(int sesiId)
        {
            IBankSoalController controller = new BankSoalController();
            var obj = controller.GetNewSoal(sesiId);

            return obj;
        }

        private HasilTes GetHasilTes(int sesiId)
        {
            IHistoriTesController controller = new HistoriTesController();
            var obj = controller.GetHasilTes(sesiId);

            return obj;
        }

        private HistoriTes GetLastSoal(int sesiId, bool cekJawaban = true, int incrementSoalNumber = 1)
        {
            IBankSoalController controller = new BankSoalController();
            var obj = controller.GetLastSoal(sesiId, cekJawaban, incrementSoalNumber);

            return obj;
        }

        private void SetTimeout(Sesi obj)
        {
            ISesiController controller = new SesiController();
            var result = controller.Update(obj);
        }

        private Sesi GetLastSession(string userId)
        {
            ISesiController controller = new SesiController();
            var obj = controller.GetLastSession(userId);

            return obj;
        }

        private Sesi SaveSession(string userId)
        {
            var obj = new Sesi
            {
                user_id = userId
            };

            ISesiController controller = new SesiController();
            var result = controller.Save(obj);

            return obj;
        }
    }
}
