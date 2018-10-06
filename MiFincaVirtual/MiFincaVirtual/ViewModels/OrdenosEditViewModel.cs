using MiFincaVirtual.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiFincaVirtual.ViewModels
{
    public class OrdenosEditViewModel: BaseViewModel
    {
        #region Attributes
        private Ordenos ordeno;
        #endregion

        #region Properties
        public Ordenos Ordeno
        {
            get { return this.ordeno; }
            set { this.SetValue(ref this.ordeno, value); }
        }
        #endregion

        #region Constructors
        public OrdenosEditViewModel(Ordenos ordenoP)
        {
            this.ordeno = ordenoP;
        } 
        #endregion
    }
}
