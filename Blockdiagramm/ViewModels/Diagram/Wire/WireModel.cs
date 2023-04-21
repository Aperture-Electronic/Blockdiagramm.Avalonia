using Blockdiagramm.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram.Wire
{
    public class WireModel : ViewModelBase
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
            set => this.RaiseAndSetIfChanged(ref wireType, value);
        }

        public WireStatus WireStatus
        {
            get => wireStatus;
            set => this.RaiseAndSetIfChanged(ref wireStatus, value);
        }

        #endregion
    }
}
