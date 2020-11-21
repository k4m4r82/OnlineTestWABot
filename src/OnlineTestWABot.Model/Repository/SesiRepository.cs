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
    public interface ISesiRepository : IBaseRepository<Sesi>
    {
        Sesi GetLastSession(string userId);
    }

    public class SesiRepository : ISesiRepository
    {
        private readonly IDbContext _context;

        public SesiRepository(IDbContext context)
        {
            _context = context;
        }

        public Sesi GetLastSession(string userId)
        {
            Sesi obj = null;

            try
            {
                var sql = @"select * from sesi 
                            where user_id = @userId and is_timeout = 0
                            order by sesi_id desc 
                            limit 1";
                obj = _context.Db.QuerySingleOrDefault<Sesi>(sql, new { userId });
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return obj;
        }

        public int Save(Sesi obj)
        {
            var result = 0;

            try
            {
                _context.Db.Insert<Sesi>(obj);
                result = 1;
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public int Update(Sesi obj)
        {
            var result = 0;

            try
            {
                result = _context.Db.Update<Sesi>(obj) ? 1 : 0;
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public int Delete(Sesi obj)
        {
            throw new NotImplementedException();
        }

        public IList<Sesi> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
