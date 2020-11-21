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
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppNETAPI;

namespace OnlineTestWABot.WinApp
{
    public partial class FrmScanQRCode : Form
    {
        private IWhatsAppNETAPI _whatsAppApi;

        public FrmScanQRCode(IWhatsAppNETAPI whatsAppApi)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(0, 189, 164);

            _whatsAppApi = whatsAppApi;
            picQRCode.Image = _whatsAppApi.GetQRCode();
        }

        private void tmrRefreshQRCode_Tick(object sender, EventArgs e)
        {
            var image = _whatsAppApi.GetQRCode();
            if (image != null)
                picQRCode.Image = image;
            else
                this.Close();
        }
    }
}
