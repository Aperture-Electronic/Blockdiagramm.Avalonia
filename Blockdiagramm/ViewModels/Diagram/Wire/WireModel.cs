using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram.Wire
{
    public class WireModel : INotifyPropertyChanged
    {
        #region Internal fields
        private WireType wireType;
        private WireStatus wireStatus;
        #endregion

        #region Readonly properties

        #endregion

        #region Notify properties
        public WireType WireType
        {
            get => wireType;
            set => NotifyProperty.ChangeProperty(ref wireType, value, nameof(WireType), OnPropertyChanged);
        }

        public WireStatus WireStatus
        {
            get => wireStatus;
            set => NotifyProperty.ChangeProperty(ref wireStatus, value, nameof(WireStatus), OnPropertyChanged);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
