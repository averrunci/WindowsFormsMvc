﻿// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    public partial class MainForm : Form
    {
        public MainForm(ApplicationHost applicationHost)
        {
            InitializeComponent();

            mainContentControl.Content = applicationHost;
        }
    }
}
