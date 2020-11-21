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

using OnlineTestWABot.Model.Entity;
using OnlineTestWABot.Model.Repository;
using OnlineTestWABot.Model.Context;

namespace OnlineTestWABot.WinApp.Controller
{
    public interface ISesiController : IBaseController<Sesi>
    {
        Sesi GetLastSession(string userId);
    }

    public class SesiController : ISesiController
    {
        private ISesiRepository _repository;

        public Sesi GetLastSession(string userId)
        {
            Sesi obj = null;

            using (IDbContext context = new DbContext())
            {
                _repository = new SesiRepository(context);
                obj = _repository.GetLastSession(userId);
            }

            return obj;
        }

        public int Save(Sesi obj)
        {
            var result = 0;

            using (IDbContext context = new DbContext())
            {
                _repository = new SesiRepository(context);
                result = _repository.Save(obj);
            }

            return result;
        }

        public int Update(Sesi obj)
        {
            var result = 0;

            using (IDbContext context = new DbContext())
            {
                _repository = new SesiRepository(context);
                result = _repository.Update(obj);
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
