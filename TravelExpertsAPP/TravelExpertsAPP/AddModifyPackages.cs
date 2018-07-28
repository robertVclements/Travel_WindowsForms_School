﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsLibrary;

namespace TravelExpertsAPP
{
    public partial class AddModifyPackages : Form
    {
        public bool addPackage;
        public Package package;
        public AddModifyPackages()
        {
            InitializeComponent();
        }

        private void AddModifyPackages_Load(object sender, EventArgs e)
        {
            if (addPackage) //if addProduct is true then add button was picked
            {
                this.Text = "Add Package";//changed form title
            }
            else //if modify button was picked
            {
                this.Text = "Modify Package";
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addPackage)
            {
                package = new Package();
                this.PackageData(package);
                try
                {
                    package.PackageId = PackageDB.AddPackage(package);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Package newPackage = new Package();
                newPackage.PackageId = package.PackageId;
                this.PackageData(newPackage);
                try
                {
                    if (!PackageDB.UpdatePackages(package, newPackage))
                    {
                        MessageBox.Show("Another user has updated or deleted" +
                               package.PkgName, "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void PackageData(Package package)
        {
            package.PkgName = txtPkgName.Text;
            package.PkgStartDate = Convert.ToDateTime(dtpPkgStartDate.Text);
            package.PkgEndDate = Convert.ToDateTime(dtpPkgEndDate.Text);
            package.PkgDesc = txtPkgDescription.Text;
            package.PkgBasePrice = Convert.ToDouble(txtBasePrice.Text);
            package.PkgAgencyCommission = Convert.ToDouble(txtPkgAgencyCommission.Text);

        }
    }
}
