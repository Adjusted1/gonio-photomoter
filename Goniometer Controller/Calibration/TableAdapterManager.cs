using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller.Calibration.CalibrationDataSetTableAdapters
{
    public partial class TableAdapterManager
    {
        /* The Calibration Dataset is small enough 
         * that we can just load the entire thing in memory.
         * Hence why we just create all the adapter and offer a FillAll Method
         * */

        public TableAdapterManager()
        {
            //setup new adapters
            this._certification_DataTableAdapter = new Certification_DataTableAdapter();
            this._certificationsTableAdapter = new CertificationsTableAdapter();
            this._lampsTableAdapter = new LampsTableAdapter();
            this._usage_LogTableAdapter = new Usage_LogTableAdapter();
        }

        public void FillDataSet(CalibrationDataSet dataset)
        {
            //fill in top-down order
            this._lampsTableAdapter.Fill(dataset.Lamps);
            this._certificationsTableAdapter.Fill(dataset.Certifications);
            this._certification_DataTableAdapter.Fill(dataset.Certification_Data);
            this._usage_LogTableAdapter.Fill(dataset.Usage_Log);
        }
    }
}
