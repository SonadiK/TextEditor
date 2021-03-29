// Author: Sonadi Kannnagara
// Date: 28th March, 2021
// Type: Lab 5
// Description: This program allows some functionalities like create, open, save, cut, copy, paste text files.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class formTextEditor : Form
    {
        // The file path of the active file.
        string filePath = String.Empty;       

        public formTextEditor()
        {
            InitializeComponent();
        }

        #region"Event handlers"
        /// <summary>
        /// Clear the textbox input, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileNewClick(object sender, EventArgs e)
        {
            // ask the user to confirm.
            // if the user confirm.
            if (MessageBox.Show("This will delete the current file. Would you like to proceed?", "Confirm New File", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Clear the textbox.
                textBoxInput.Clear();
                // Clear the file path.
                filePath = String.Empty;
                // Update the file name.
                UpdateTitle();
            }
        }

        /// <summary>
        /// Open a text file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenClick(object sender, EventArgs e)
        {
            // Create a new open dialog.
            OpenFileDialog openDialog = new OpenFileDialog();
            // Set a filetr.
            openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openDialog.Title = "Open file.";
            // If the user select a file and click ok.
            if (openDialog.ShowDialog() == DialogResult.OK)
            {                
                filePath = openDialog.FileName;
                FileStream fileToAccess = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader fileReader = new StreamReader(fileToAccess);
                textBoxInput.Text = fileReader.ReadToEnd();
                fileReader.Close();
                // Update the file name.
                UpdateTitle();
            }
        }

        /// <summary>
        /// Save the current content of the text file if the path is known.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveClick(object sender, EventArgs e)
        {
            // if there is not already a file path.
            if (filePath == String.Empty)
            {
                // Call save as event handler.
                FileSaveAsClick(sender, e);
            }
            //if there is a file path.
            else
            {
                // Save the file.
                SaveTextFile(filePath);
            }
        }

        /// <summary>
        /// Save the text file to a specified location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveAsClick(object sender, EventArgs e)
        {
            // Create a new save dialog.
            SaveFileDialog saveDialog = new SaveFileDialog();
            // Set the filter.
            saveDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            // if the user selects a file and click ok.
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {   
                // Set the new file path.
                filePath = saveDialog.FileName;
                // Save the file.
                SaveTextFile(filePath);
                // Update the title.
                UpdateTitle();
                 
            }
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileExitClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will close the current file. Would you like to proceed?", "Confirm New File", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }                
        }

        /// <summary>
        /// Copy the selected content of the text file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCopyClick(object sender, EventArgs e)
        {
            // if the textbox is not null.
            if (textBoxInput.Text.Length != 0)
            {
                Clipboard.SetText(textBoxInput.SelectedText);
            }
        }

        /// <summary>
        /// Cut the selected content from the text file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCutClick(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (textBoxInput.Text.Length != 0)
            {
                Clipboard.SetText(textBoxInput.SelectedText);
                textBoxInput.SelectedText = "";
            }
        }

        /// <summary>
        /// Paste the copied content on the text file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditPasteClick(object sender, EventArgs e)
        {
            // if the clipboard contains text.
            if (Clipboard.ContainsText())
            {   
                textBoxInput.Text = textBoxInput.Text.Insert(textBoxInput.SelectionStart, Clipboard.GetText());
                //textBoxInput.SelectedText = Clipboard.GetText();
            }
        }

        /// <summary>
        /// Display some information about the text editor application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Text Editor \n By Sonadi Kannangara\n\n 25th March, 2021. \n\n For NETD 2202\n", "About This Application.");
        }
#endregion
        #region "Functions"
        /// <summary>
        /// This function will save the content of the text file.
        /// </summary>
        /// <param name="path"></param>
        private void SaveTextFile(string path)
        {
            FileStream myFile = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(myFile);
            writer.Write(textBoxInput.Text);
            writer.Close();
        }

        /// <summary>
        /// Updates the title of the form to include the path name of the file if one exists.
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = "Text Editor";
            if(filePath != String.Empty)
            {
                this.Text += " - " + filePath;
            }
        }

        #endregion

        private void TextModified(object sender, EventArgs e)
        {
           
        }
    }
}
