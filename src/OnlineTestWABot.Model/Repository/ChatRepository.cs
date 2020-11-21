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
    public interface IChatRepository : IBaseRepository<Chat>
    {

    }

    public class ChatRepository : IChatRepository
    {
        private readonly IDbContext _context;

        public ChatRepository(IDbContext context)
        {
            _context = context;
        }

        public int Save(Chat obj)
        {
            var result = 0;

            try
            {
                _context.Db.Insert<Chat>(obj);
                result = 1;
            }
            catch (Exception ex)
            {
                //_log.Error("Error:", ex);
            }

            return result;
        }

        public int Update(Chat obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(Chat obj)
        {
            throw new NotImplementedException();
        }

        public IList<Chat> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
