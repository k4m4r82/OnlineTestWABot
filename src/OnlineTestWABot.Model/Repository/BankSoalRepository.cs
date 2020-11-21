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
using System.Linq;
using System.Text;

using Dapper;
using Dapper.Contrib.Extensions;
using OnlineTestWABot.Model.Entity;
using OnlineTestWABot.Model.Context;

namespace OnlineTestWABot.Model.Repository
{
    public interface IBankSoalRepository
    {
        BankSoal GetById(int soalId);
        BankSoal GetNewSoal(int sesiId);
        HistoriTes GetLastSoal(int sesiId, bool cekJawaban = true, int incrementSoalNumber = 1);
    }

    public class BankSoalRepository : IBankSoalRepository
    {
        private readonly IDbContext _context;

        public BankSoalRepository(IDbContext context)
        {
            _context = context;
        }

        public BankSoal GetById(int soalId)
        {
            BankSoal obj = null;

            try
            {
                obj = _context.Db.Get<BankSoal>(soalId);
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return obj;
        }

        public BankSoal GetNewSoal(int sesiId)
        {
            BankSoal obj = null;

            try
            {
                var random = new Random();

                var sql = @"select bank_soal_id from bank_soal 
                            where bank_soal_id not in (select bank_soal_id from histori_tes where sesi_id = @sesiId)";
                var soalId = _context.Db.Query<int>(sql, new { sesiId })
                                     .OrderBy(f => random.Next()).Take(1)
                                     .FirstOrDefault();

                obj = GetById(soalId);

                if (obj != null)
                {
                    IHistoriTesRepository repo = new HistoriTesRepository(_context);
                    obj.nomor_soal = repo.GetSoalNumber(sesiId);
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return obj;
        }

        public HistoriTes GetLastSoal(int sesiId, bool cekJawaban = true, int incrementSoalNumber = 1)
        {
            HistoriTes obj = null;

            try
            {
                var sql = @"select histori_tes.histori_tes_id, histori_tes.user_id, histori_tes.sesi_id, histori_tes.tanggal, histori_tes.jawaban, 
                            histori_tes.hasil_jawaban, histori_tes.batal, 
                            bank_soal.bank_soal_id, bank_soal.soal, bank_soal.jawaban, bank_soal.analisa
                            from histori_tes inner join bank_soal on bank_soal.bank_soal_id = histori_tes.bank_soal_id
                            {WHERE}
                            order by histori_tes.histori_tes_id desc 
                            limit 1";

                if (cekJawaban)
                    sql = sql.Replace("{WHERE}", "where histori_tes.sesi_id = @sesiId and histori_tes.batal = 0 and histori_tes.jawaban is null");
                else
                    sql = sql.Replace("{WHERE}", "where histori_tes.sesi_id = @sesiId and histori_tes.batal = 0");

                obj = _context.Db.Query<HistoriTes, BankSoal, HistoriTes>(sql, (histori, soal) =>
                {
                    histori.bank_soal_id = soal.bank_soal_id;
                    histori.BankSoal = soal;

                    return histori;
                }, new { sesiId }, splitOn: "bank_soal_id").SingleOrDefault();

                if (obj != null)
                {
                    IHistoriTesRepository repo = new HistoriTesRepository(_context);
                    obj.BankSoal.nomor_soal = repo.GetSoalNumber(sesiId, incrementSoalNumber);
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return obj;
        }
    }
}
