using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using H.Avalonia.ViewModels;
using H.Avalonia.ViewModels.FarmCreationViews;

namespace H.Avalonia.Views.FarmCreationViews
{
    public partial class FarmOptionsView : UserControl
    {
        #region Fields

        private FarmOptionsViewModel _farmOptionsViewModel;

        #endregion

        #region Constructors

        public FarmOptionsView(FarmOptionsViewModel farmOptionsViewModel)
        {
            InitializeComponent();

            if (farmOptionsViewModel != null)
            {
                _farmOptionsViewModel = farmOptionsViewModel;
            }
            else
            {
                throw new ArgumentNullException(nameof(farmOptionsViewModel));
            }
        }
        public FarmOptionsView()
        {
            InitializeComponent();
        }

        #endregion

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}