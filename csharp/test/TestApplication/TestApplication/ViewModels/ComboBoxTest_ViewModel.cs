using TestApplication.Models;
using TestApplication.ViewModels.Common;
using TestApplication.ViewModels.Common.Base;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TestApplication.ViewModels
{
    public class ComboBoxTest_ViewModel : ViewModelBase
    {
        #region Member

        private ComboBoxTest_Model _model;

        #endregion

        #region Property

        public List<ComboBox_Dto> CmbItemsSource
        {
            get { return _cmbItemsSource; }
            set
            {
                if (_cmbItemsSource != value)
                {
                    _cmbItemsSource = value;
                    RaisePropertyChanged(() => this.CmbItemsSource);
                }
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<ComboBox_Dto> _cmbItemsSource;

        public ComboBox_Dto CmbSelectedItem
        {
            get { return _cmbSelectedItem; }
            set
            {
                if (_cmbSelectedItem != value)
                {
                    _cmbSelectedItem = value;
                    RaisePropertyChanged(() => this.CmbSelectedItem);

                    //((DelegateCommand)CmbSelectedItemChanged).RaiseCanExecuteChanged();
                }
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ComboBox_Dto _cmbSelectedItem = null;

        #endregion

        #region Command

        public ICommand Ok_ClickCommand
        {
            get
            {
                if (_ok_ClickCommand == null)
                {
                    _ok_ClickCommand = new DelegateCommand<object>(o =>
                    {
                        MessageBox.Show("OK!");
                    });
                }
                return _ok_ClickCommand;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ICommand _ok_ClickCommand = null;

        public ICommand Cmb_SelectionChanged
        {
            get
            {
                if (_cmb_SelectionChangedCommand == null)
                {
                    _cmb_SelectionChangedCommand = new DelegateCommand<object>(o =>
                    {
                        
                    });
                }
                return _cmb_SelectionChangedCommand;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ICommand _cmb_SelectionChangedCommand = null;

        #endregion

        #region Constructor

        public ComboBoxTest_ViewModel()
        {
            this._model = new ComboBoxTest_Model();

            this.CmbItemsSource = this._model.CreateItemsSource();
            this.CmbSelectedItem = this.CmbItemsSource.FirstOrDefault();
        }

        #endregion

        #region Method

        #endregion
    }
}
