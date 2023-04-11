using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class NotifyProperty
    {
        /// <summary>
        /// Change the property and notify the change
        /// When the new value is same as the old value, the change will not be assigned
        /// </summary>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="field">Reference to internal field</param>
        /// <param name="value">New value to set</param>
        /// <param name="propertyName">Changing property name to notify</param>
        /// <param name="onPropertyChanged">Property change notifier</param>
        public static void ChangeProperty<T>(ref T field, T value, string propertyName, Action<string> onPropertyChanged)
        {
            if ((field != null) && (value != null))
            {
                if (field.Equals(value))
                {
                    return;
                }
            }

            field = value;
            onPropertyChanged(propertyName);
        }
    }
}
