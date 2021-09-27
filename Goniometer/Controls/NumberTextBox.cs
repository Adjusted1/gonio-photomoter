using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Goniometer.Controls
{
    [DefaultBindingProperty("Value")]
    public partial class NumberTextBox : TextBox
    {

        public double? Value
        {
            get
            {
                double d;
                if (Double.TryParse(Text, out d))
                    return d;
                else
                    return null;
            }
            set
            {
                Text = String.Format("{0:0.0}", value);
            }
        }

        public NumberTextBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NumberTextBox
            // 
            this.TextChanged += new System.EventHandler(this.NumberTextBox_TextChanged);
            this.ResumeLayout(false);
        }

        protected EventHandler OnValueChanged;
        private void NumberTextBox_TextChanged(object sender, EventArgs e)
        {
            var temp = OnValueChanged;
            if (temp != null)
                temp(sender, e);
        }
    }
}
