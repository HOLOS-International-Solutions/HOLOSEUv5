﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Models;
using H.Core.Services.StorageService;
using Prism.Events;
using Prism.Regions;

namespace H.Avalonia.ViewModels.OptionsViews
{
    public class DefaultBeddingCompositionViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<DefaultBeddingCompositionDTO> _beddingCompositionDTOs;
        private IUnitsOfMeasurementCalculator _unitsCalculator;
        private string _nitrogenConcentrationHeader;
        private string _phosphorusConcentrationHeader;
        private string _carbonConcentrationHeader;

        #endregion

        #region Constructors

        public DefaultBeddingCompositionViewModel(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IStorageService storageService, 
            IUnitsOfMeasurementCalculator unitsCalculator) : base(regionManager, eventAggregator, storageService)
        {
            _unitsCalculator = unitsCalculator;
            _unitsCalculator.PropertyChanged -= UnitsOfMeasurementChangeListener;
            _unitsCalculator.PropertyChanged += UnitsOfMeasurementChangeListener;

            BeddingCompositionDTOs = new ObservableCollection<DefaultBeddingCompositionDTO>();
            this.Initialize();
            base.IsInitialized = true;
        }

        #endregion

        #region Properties

        public ObservableCollection<DefaultBeddingCompositionDTO> BeddingCompositionDTOs
        {
            get => _beddingCompositionDTOs;
            set => SetProperty(ref _beddingCompositionDTOs, value);
        }

        public string NitrogenConcentrationHeader
        {
            get => _nitrogenConcentrationHeader;
            set => SetProperty(ref _nitrogenConcentrationHeader, value);
        }

        public string PhosphorusConcentrationHeader
        {
            get => _phosphorusConcentrationHeader;
            set => SetProperty(ref _phosphorusConcentrationHeader, value);
        }

        public string CarbonConcentrationHeader
        {
            get => _carbonConcentrationHeader;
            set => SetProperty(ref _carbonConcentrationHeader, value);
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            foreach (var dataClassInstance in base.ActiveFarm.DefaultsCompositionOfBeddingMaterials)
            {
                var dto = new DefaultBeddingCompositionDTO(dataClassInstance, _unitsCalculator);
                dto.SetInitializationFlag(true);
                dto.TotalNitrogenKilogramsDryMatter = dataClassInstance.TotalNitrogenKilogramsDryMatter;
                dto.TotalPhosphorusKilogramsDryMatter = dataClassInstance.TotalPhosphorusKilogramsDryMatter;
                dto.TotalCarbonKilogramsDryMatter = dataClassInstance.TotalCarbonKilogramsDryMatter;
                dto.CarbonToNitrogenRatio = dataClassInstance.CarbonToNitrogenRatio;
                dto.SetInitializationFlag(false);
                this.BeddingCompositionDTOs.Add(dto);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            SetStrings();

            if (!IsInitialized)
            {
                this.BeddingCompositionDTOs.Clear();
                this.Initialize();
                base.IsInitialized = true;
            }

        }

        #endregion

        #region Private Methods

        private void SetStrings()
        {
            var displayUnits = StorageService.Storage.ApplicationData.DisplayUnitStrings;
            this.NitrogenConcentrationHeader = H.Core.Properties.Resources.LabelTotalNitrogen + " " + displayUnits.KilogramsNitrogenPerKilogramDryMatter;
            this.PhosphorusConcentrationHeader = H.Core.Properties.Resources.LabelTotalPhosphorus + " " + displayUnits.KilogramsPhosphorusPerKilogramDryMatter;
            this.CarbonConcentrationHeader = H.Core.Properties.Resources.LabelTotalCarbon + " " + displayUnits.KilogramsCarbonPerKilogramDryMatter;
        }

        #endregion

        #region Event Handlers

        private void UnitsOfMeasurementChangeListener(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IUnitsOfMeasurementCalculator.IsMetric))
            {
                foreach (var dto in BeddingCompositionDTOs)
                {
                    dto.UpdateUnitsOfMeasurementDependentProperties();
                }
            }
        }

        #endregion
    }
}
