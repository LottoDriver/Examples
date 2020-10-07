using System;
using System.Configuration;
using System.Windows.Forms;

using LottoDriver.Examples.CustomersApi.Common.DataAccess;

namespace LottoDriver.Examples.CustomersApi.DatabaseViewer
{
    public partial class FrmMain : Form
    {
        private IDatabase _database;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (_database == null)
                {
                    _database = new SQLiteDatabase(ConfigurationManager.AppSettings["DatabasePath"]);
                }

                _database.BeginTransaction();
                try
                {
                    Cursor = Cursors.WaitCursor;
                    bindingSource1.SuspendBinding();

                    _database.LottoDrawFindRecent(dataTable1);

                }
                finally
                {
                    _database.RollbackTransaction();

                    bindingSource1.ResumeBinding();
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
