using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiFoTestApplication.DataObjects;

namespace WiFoTestApplication
{
    internal static class Extensions
    {
        public static List<Type> SupportedControls = new List<Type>
        {
            typeof(TextBox),
            typeof(NumericUpDown),
            typeof(DateTimePicker)
        };

        public static bool IsNumericType(this object type)
        {
            return Type.GetTypeCode(type.GetType()) switch
            {
                TypeCode.Byte => true,
                TypeCode.SByte => true,
                TypeCode.UInt16 => true,
                TypeCode.UInt32 => true,
                TypeCode.UInt64 => true,
                TypeCode.Int16 => true,
                TypeCode.Int32 => true,
                TypeCode.Int64 => true,
                TypeCode.Decimal => true,
                TypeCode.Double => true,
                TypeCode.Single => true,
                _ => false
            };
        }

        public static void SetMetadata(this Control control, ControlMetadata metadata)
        {
            if (control == null)
                return;

            control.Tag = metadata;
        }

        public static void SetMetadata(this Control control, bool required)
        {
            control.SetMetadata(new ControlMetadata
            {
                Required = required
            });
        }

        public static void SetMetadata(this Control control, object value)
        {
            control.SetMetadata(new ControlMetadata
            {
                OriginalValue = value
            });
        }

        public static ControlMetadata GetMetadata(this Control control)
        {
            if (control == null)
                return null;

            if (!(control.Tag is ControlMetadata metadata))
                return null;

            metadata.Name = control.Name;
            return metadata;
        }

        public static List<Control> GetRequiredControl(this Control rootControl)
        {
            var result = new List<Control>();
            GetRequiredControl(rootControl, result);
            return result;
        }

        private static void GetRequiredControl(Control rootControl, List<Control> controlList)
        {
            foreach (Control control in rootControl.Controls)
            {
                var metadata = control.GetMetadata();
                if (metadata == null)
                    continue;

                if (metadata.Required)
                    controlList.Add(control);

                if (control.Controls.Count > 0)
                    GetRequiredControl(control, controlList);
            }
        }

        public static object GetValue(this Control control)
        {
            return control switch
            {
                TextBox txtBox => txtBox.Text,
                DateTimePicker dtPicker => dtPicker.Value,
                NumericUpDown numUpDown => numUpDown.Value,
                _ => default
            };
        }

        public static bool IsEmpty(this Control control)
        {
            var value = control.GetValue();

            switch (control)
            {
                case TextBox txtBox when string.IsNullOrEmpty(txtBox.Text):
                case DateTimePicker dtPicker when dtPicker.Value == DateTime.MinValue:
                case NumericUpDown numUpDown when numUpDown.Value == 0:
                    return true;
                default:
                    return false;
            }
        }

        public static object ChangeType(object value, Type type)
        {
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
        }
    }
}
