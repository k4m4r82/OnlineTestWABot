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

namespace OnlineTestWABot.Controller
{
    public interface IBankSoalController
    {
        BankSoal GetById(int soalId);
        BankSoal GetNewSoal(int sesiId);
        HistoriTes GetLastSoal(int sesiId, bool cekJawaban = true, int incrementSoalNumber = 1);
    }

    public class BankSoalController : IBankSoalController
    {
        private IBankSoalRepository _repository;

        public BankSoal GetById(int soalId)
        {
            BankSoal obj = null;

            using (IDbContext context = new DbContext())
            {
                _repository = new BankSoalRepository(context);
                obj = _repository.GetById(soalId);
            }

            return obj;
        }

        public BankSoal GetNewSoal(int sesiId)
        {
            BankSoal obj = null;

            using (IDbContext context = new DbContext())
            {
                _repository = new BankSoalRepository(context);
                obj = _repository.GetNewSoal(sesiId);
            }

            return obj;
        }

        public HistoriTes GetLastSoal(int sesiId, bool cekJawaban = true, int incrementSoalNumber = 1)
        {
            HistoriTes obj = null;

            using (IDbContext context = new DbContext())
            {
                _repository = new BankSoalRepository(context);
                obj = _repository.GetLastSoal(sesiId, cekJawaban, incrementSoalNumber);
            }

            return obj;
        }
    }
}
