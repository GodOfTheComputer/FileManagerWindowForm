﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerWindowForm
{
    public partial class MainMenuForm : XtraForm
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void AddFileSimpleButton_Click(object sender, EventArgs e)
        {
            (new AddFileForm()).ShowDialog();
        }

        private void ExitSimpleButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShareFileSimpleButton_Click(object sender, EventArgs e)
        {
            (new ShareFileForm()).ShowDialog();
        }

        private void DownloadFileimpleButton_Click(object sender, EventArgs e)
        {
            (new DownloadFileForm()).ShowDialog();
        }

        private void DeleteSimpleButton_Click(object sender, EventArgs e)
        {
            (new DeleteFileForm()).ShowDialog();
        }
    }
}
