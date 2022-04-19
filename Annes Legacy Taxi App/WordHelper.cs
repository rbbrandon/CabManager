using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Robs_Taxi_Manager
{
    public static class WordHelper
    {
        public static bool MergeDriverInvoice(string invoiceFileName, string invoiceTemplateFile)
        {
            if (File.Exists(Constants.DATA_TEMP_FILE) && File.Exists(invoiceTemplateFile))
            {
                //Start Word app (hidden).
                try
                {
                    Word.Application wordApp = new Word.Application();

                    try
                    {
                        wordApp.Visible = false;

                        // Open the template.
                        Word._Document wordTemplate = wordApp.Documents.Open(invoiceTemplateFile);

                        //wordTemplate.MailMerge.OpenDataSource(Constants.DATA_TEMP_FILE);

                        // MailMerge the document with the data file.
                        Word.MailMerge tempDocument = wordTemplate.MailMerge;
                        tempDocument.OpenDataSource(Constants.DATA_TEMP_FILE);
                        if (tempDocument.State == Word.WdMailMergeState.wdMainAndDataSource)
                        {
                            tempDocument.Destination = Word.WdMailMergeDestination.wdSendToNewDocument;
                            tempDocument.Execute();
                        }

                        // Close the template (do not save).
                        wordTemplate.Close(Word.WdSaveOptions.wdDoNotSaveChanges);

                        // Delete the data file
                        File.Delete(Constants.DATA_TEMP_FILE);

                        // Create the Invoices directory if not existing.
                        if (!Directory.Exists(Constants.INVOICE_PATH))
                            Directory.CreateDirectory(Constants.INVOICE_PATH);

                        try
                        {
                            // Save the merged document as a PDF.
                            wordApp.ActiveDocument.SaveAs2(invoiceFileName, Word.WdSaveFormat.wdFormatPDF);
                        }
                        catch
                        {

                        }

                        // Close the merged document (do not save, as already saved).
                        ((Word._Document)wordApp.ActiveDocument).Close(Word.WdSaveOptions.wdDoNotSaveChanges);

                        // Quit Word
                        ((Word._Application)wordApp.Application).Quit(Word.WdSaveOptions.wdDoNotSaveChanges);

                        // Open invoice file in default reader.
                        if (File.Exists(invoiceFileName))
                            Process.Start(invoiceFileName);

                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Word installed, but something didnt work");
                        // Quit Word
                        ((Word._Application)wordApp.Application).Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                    }
                }
                catch
                {
                    MessageBox.Show("Word not installed");
                }
            }

            return false;
        }
    }
}
