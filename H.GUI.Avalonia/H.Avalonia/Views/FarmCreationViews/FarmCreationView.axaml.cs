using Avalonia.Controls;
using H.Avalonia.ViewModels;
using System;
using H.Avalonia.ViewModels.FarmCreationViews;

namespace H.Avalonia.Views.FarmCreationViews
{
    public partial class FarmCreationView : UserControl
    {
        #region Fields

        private FarmCreationViewModel _farmCreationViewModel;

        #endregion

        #region Constructors

        public FarmCreationView()
        {
            InitializeComponent();
        }

        public FarmCreationView(FarmCreationViewModel farmCreationViewModel)
        {
            InitializeComponent();

            if (farmCreationViewModel != null)
            {
                _farmCreationViewModel = farmCreationViewModel;
            }
            else
            {
                throw new ArgumentNullException(nameof(farmCreationViewModel));
            }

        }

        #endregion
    }
}