using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WiFoTestApplication.DataObjects;
using ZimLabs.CoreLib;
using ZimLabs.CoreLib.Extensions;

namespace WiFoTestApplication
{
    /*
     * Note: This project is a test project for the interaction with reflection and controls.
     */
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tries to show the given data
        /// </summary>
        /// <param name="data">The data which should be shown</param>
        private void ShowData(object data)
        {
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                var control = GetControl(this, property.Name);
                if (control == null)
                    continue;

                SetValue(control, Core.GetPropertyValue(data, property.Name));
            }
        }

        /// <summary>
        /// Tries to get the data of the controls and add them to the given data object
        /// </summary>
        /// <param name="data">The data object which should be used</param>
        private void GetData(object data)
        {
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                var control = GetControl(this, property.Name);

                var value = control?.GetValue();
                if (value == null)
                    continue;

                property.SetValue(data, Extensions.ChangeType(value, property.PropertyType));
            }
        }

        /// <summary>
        /// Search for a control which contains the given name
        /// </summary>
        /// <param name="rootControl">The root control with the child controls</param>
        /// <param name="name">The name which should be search for</param>
        /// <returns>The control which was found, default = null</returns>
        private Control GetControl(Control rootControl, string name)
        {
            var control = rootControl.Controls.Cast<Control>().Where(w => !(w is Label))
                .FirstOrDefault(f => f.Name.ContainsIgnoreCase(name));

            if (control == null)
                return null;

            if (Extensions.SupportedControls.Contains(control.GetType()))
                return control;

            return control.Controls.Count > 0 ? GetControl(control, name) : null;
        }

        /// <summary>
        /// Sets the value of the given control
        /// </summary>
        /// <param name="ctrl">The control which should be set</param>
        /// <param name="value">The value which should be set</param>
        private void SetValue(Control ctrl, object value)
        {
            switch (ctrl)
            {
                case TextBox txtBox:
                    txtBox.Text = value.ToString();
                    return;
                case DateTimePicker dtPicker when value is DateTime dtValue:
                    dtPicker.Value = dtValue;
                    return;
                case NumericUpDown numUpDown when value.IsNumericType():
                    numUpDown.Value = Convert.ToDecimal(value);
                    break;
            }
        }

        /// <summary>
        /// Occurs when the user hits the show button
        /// </summary>
        private void buttonShowData_Click(object sender, EventArgs e)
        {
            try
            {
                var person = new Person
                {
                    Id = 1,
                    FirstName = "Philipp J.",
                    LastName = "Fry",
                    Birthday = new DateTime(1985, 7, 12),
                    AccountNumber = 42
                };

                ShowData(person);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// Occurs when the user hits the get button
        /// </summary>
        private void buttonGet_Click(object sender, EventArgs e)
        {
            try
            {
                var requiredControls = this.GetRequiredControl();

                var message = "";
                foreach (var control in requiredControls)
                {
                    if (control.IsEmpty())
                        message += $"Control: {control.Name}\r\n";
                }

                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var person = new Person();
                GetData(person);

                MessageBox.Show($"Id: {person.Id}\r\n" +
                                $"First name: {person.FirstName}\r\n" +
                                $"Last name: {person.LastName}\r\n" +
                                $"Birthday: {person.Birthday:dd.MM.yyyy}\r\n" +
                                $"Account number: {person.AccountNumber}",
                    "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            textBoxFirstName.SetMetadata(true);
            textBoxLastname.SetMetadata(true);
        }
    }
}
