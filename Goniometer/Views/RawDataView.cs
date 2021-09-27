using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Goniometer_Controller.Functions;
using Goniometer_Controller.Models;

namespace Goniometer.Reports
{
    public partial class RawDataView : UserControl
    {
        private BindingSource _bindingSource;

        public RawDataView()
        {
            InitializeComponent();
        }

        public void BindDataSource(BindingList<MeasurementBase> datasource)
        {
            AssertDatasourceIsValid(datasource);

            _bindingSource = new BindingSource();
            _bindingSource.DataSource = datasource;
            measurementGridView.DataSource = _bindingSource;
        }

        private double GetLumenCount()
        {
            if (measurementGridView.DataSource == null)
                return 0;

            var measurements = measurementGridView.DataSource as BindingList<MeasurementBase>;
            if (measurements == null)
                return 0;

            return LightMath.CalculateLumens(measurements);
        }

        private void AssertDatasourceIsValid(IEnumerable<MeasurementBase> datasource)
        {
            if (datasource == null)
                throw new ArgumentNullException("datasource cannot be null");
        }
    }
}
