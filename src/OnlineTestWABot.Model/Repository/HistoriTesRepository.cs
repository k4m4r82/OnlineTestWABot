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
    public interface IHistoriTesRepository : IBaseRepository<HistoriTes>
    {
        int GetSoalNumber(int sesiId, int incrementSoalNumber = 1);
        HasilTes GetHasilTes(int sesiId);
    }

    public class HistoriTesRepository : IHistoriTesRepository
    {
        private readonly IDbContext _context;

        public HistoriTesRepository(IDbContext context)
        {
            _context = context;
        }

        public int GetSoalNumber(int sesiId, int incrementSoalNumber = 1)
        {
            var result = 0;

            try
            {
                var sql = @"select count(*) + @increment 
                            from histori_tes 
                            where sesi_id = @sesiId";

                result = _context.Db.QuerySingleOrDefault<int>(sql, new { sesiId, increment = incrementSoalNumber });
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public HasilTes GetHasilTes(int sesiId)
        {
            HasilTes obj = null;

            try
            {
                var sql = @"select 
                                count(case hasil_jawaban when 1 then hasil_jawaban end) as benar,
                                count(case hasil_jawaban when 0 then hasil_jawaban end) as salah
                            from histori_tes 
                            where sesi_id = @sesiId and batal = 0 and jawaban is not null";

                obj = _context.Db.QuerySingleOrDefault<HasilTes>(sql, new { sesiId });

            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return obj;
        }

        public int Save(HistoriTes obj)
        {
            var result = 0;

            try
            {
                _context.Db.Insert<HistoriTes>(obj);
                result = 1;
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public int Update(HistoriTes obj)
        {
            var result = 0;

            try
            {
                result = _context.Db.Update<HistoriTes>(obj) ? 1 : 0;
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public int Delete(HistoriTes obj)
        {
            throw new NotImplementedException();
        }

        public IList<HistoriTes> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
