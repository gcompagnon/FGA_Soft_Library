/// ==========================================================
/// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT 
///  WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED,    
/// INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES   
/// OF MERCHANTABILITY AND/OR FITNESS FOR A  PARTICULAR   
/// PURPOSE.								
/// ==========================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Bloomberglp.Blpapi.Examples
{
    public partial class FormBulkData : Form
    {
        public FormBulkData(DataTable dataSource)
        {
            InitializeComponent();
            dataGridViewRDBulkData.DataSource = dataSource;
            dataGridViewRDBulkData.Columns["Id"].Visible = false;
        }

        /// <summary>
        /// Save data to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string delim = ",";
            DataTable data = null;
            SaveFileDialog save = new SaveFileDialog();

            save.Title = "Save Data";
            save.Filter = "Text|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (save.FileName.Trim().Length > 0)
                {
                    // check for existing file
                    if (File.Exists(save.FileName.Trim()) )
                    {
                        // delete file
                        File.Delete(save.FileName.Trim());
                    }
                    // get data table from grid
                    data = (DataTable)dataGridViewRDBulkData.DataSource;
                    StreamWriter writer = new StreamWriter(save.FileName);
                    int columnCount = data.Columns.Count;
                    string output = string.Empty;
                    // create header
                    foreach (DataColumn column in data.Columns)
                    {
                        output = string.Concat(output, column.ColumnName, delim);
                    }
                    writer.WriteLine(output.Substring(0, output.Length - 1));
                    // output data
                    foreach (DataRow row in data.Rows)
                    {
                        output = string.Empty;
                        for (int index = 0; index < columnCount; index++)
                        {
                            output = string.Concat(output, row[index], delim);
                        }
                        writer.WriteLine(output.Substring(0, output.Length - 1));
                    }
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Close bulk data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonBDClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}