﻿using MultiMiner.Engine;
using MultiMiner.Utility.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MultiMiner.Win.Extensions;
using System.Reflection;
using MultiMiner.Engine.Data;
using MultiMiner.Engine.Installers;

namespace MultiMiner.Win.Forms
{
    public partial class AboutForm : MessageBoxFontForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            PopulateCopyright();
            PopulateAppVersions();
        }

        private void PopulateCopyright()
        {
            DateTime compileDate = Assembly.GetExecutingAssembly().GetCompileDate();
            const string source = "(C) 2013";
            licenseTextBox.Text = licenseTextBox.Text.Replace(source, String.Format("{0} - {1}", source, compileDate.Year));
        }

        private void PopulateAppVersions()
        {
            multiMinerLabel.Text = "MultiMiner " + MultiMinerInstaller.GetInstalledMinerVersion();
            revisionLabel.Text = String.Format("(rev {0})", MultiMinerInstaller.GetInstalledMinerRevision());
            revisionLabel.Left = multiMinerLabel.Left + multiMinerLabel.Width;

            PopulateXgminerVersion(bfgminerLabel);
        }

        private static void PopulateXgminerVersion(Label targetLabel)
        {
            MinerDescriptor miner = MinerFactory.Instance.GetDefaultMiner();
            string xgminerName = miner.Name;
            string xgminerPath = MinerPath.GetPathToInstalledMiner(miner);
            string xgminerVersion = String.Empty;

            if (File.Exists(xgminerPath))
                xgminerVersion = MinerInstaller.GetInstalledMinerVersion(miner, xgminerPath, miner.LegacyApi);

            if (string.IsNullOrEmpty(xgminerVersion))
                targetLabel.Text = String.Format("{0} not installed", xgminerName);
            else
                targetLabel.Text = String.Format("{0} {1} installed", xgminerName, xgminerVersion);
        }

        private void multiMinerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://multiminerapp.com/");
        }
    }
}
