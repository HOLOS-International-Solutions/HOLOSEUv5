﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExCSS;
using H.Core.Enumerations;
using H.Core.Services.StorageService;

namespace H.Avalonia.ViewModels.OptionsViews
{
    public class UserSettingsDisplayViewModel : ViewModelBase
    {
        #region Fields
        private bool _showCustomEquilibriumCarbonInput;
        private bool _showCustomRunInPeriodTillage;
        private bool _showCustomN2OEmissionFactor;
        private double _customN2OEmissionFactor;
        private double _emissionFactorForLeachingAndRunoff;
        private double _emissionFactorForVolatilization;
        private double _defaultSupplementalFeedingLossPercentage;
        private double _percentageOfStrawReturnedToSoilForRootCrops;
        private double _percentageOfProductReturnedToSoilForRootCrops;
        private double _percentageOfRootsReturnedToSoilForPerennials;
        private double _percentageOfProductReturnedToSoilForPerennials;
        private double _percentageOfRootsReturnedToSoilForFodderCorn;
        private double _percentageOfProductReturnedToSoilForFodderCorn;
        private double _percentageOfRootsReturnedToSoilForAnnuals;
        private int _defaultRunInPeriod;
        private double _percentageOfProductReturnedToSoilForAnnuals;
        private double _percentageOfStrawReturnedToSoilForAnnuals;
        private double _customEquilibriumCarbonValue;
        private double _carbonConcentration;
        private CarbonModellingStrategies _carbonModellingStrategy;
        private EquilibriumCalculationStrategies _equilibriumCalculationStrategy;
        private TillageType _runInPeriodTillageType;
        private PumpType _defaultPumpType;
        private ObservableCollection<CarbonModellingStrategies> _carbonModellingStrategiesList;
        private ObservableCollection<EquilibriumCalculationStrategies> _equilibriumCalculationStrategiesList;
        private ObservableCollection<TillageType> _runInPeriodTillageList;
        private ObservableCollection<PumpType> _pumpTypeList;

        #endregion

        #region Constructors
        public UserSettingsDisplayViewModel(IStorageService storageService) : base(storageService)
        {
            ManageData();
            System.Diagnostics.Debug.WriteLine(ActiveFarm.Defaults.DefaultPumpType);
        }
        #endregion

        #region Properties
        //Wrapper properties for validation before setting the values
        public double CustomN2OEmissionFactor
        {
            get => _customN2OEmissionFactor;
            set
            {
                if (SetProperty(ref _customN2OEmissionFactor, value))
                {
                    if (ValidateNonNegative(nameof(CustomN2OEmissionFactor), value))
                    {
                        ActiveFarm.Defaults.CustomN2OEmissionFactor = value;
                    }
                }
            }
        }

        public double EmissionFactorForLeachingAndRunoff
        {
            get => _emissionFactorForLeachingAndRunoff;
            set
            {
                if (SetProperty(ref _emissionFactorForLeachingAndRunoff, value))
                {
                    if (ValidateNonNegative(nameof(EmissionFactorForLeachingAndRunoff), value))
                    {
                        ActiveFarm.Defaults.EmissionFactorForLeachingAndRunoff = value;
                    }
                }
            }
        }

        public double EmissionFactorForVolatilization
        {
            get => _emissionFactorForVolatilization;
            set
            {
                if (SetProperty(ref _emissionFactorForVolatilization, value))
                {
                    if (ValidateNonNegative(nameof(EmissionFactorForVolatilization), value))
                    {
                        ActiveFarm.Defaults.EmissionFactorForVolatilization = value;
                    }
                }
            }
        }

        public double DefaultSupplementalFeedingLossPercentage
        {
            get => _defaultSupplementalFeedingLossPercentage;
            set
            {
                if (SetProperty(ref _defaultSupplementalFeedingLossPercentage, value))
                {
                    if (ValidateNonNegative(nameof(DefaultSupplementalFeedingLossPercentage), value))
                    {
                        ActiveFarm.Defaults.DefaultSupplementalFeedingLossPercentage = value;
                    }
                }
            }
        }

        public double PercentageOfStrawReturnedToSoilForRootCrops
        {
            get => _percentageOfStrawReturnedToSoilForRootCrops;
            set
            {
                if (SetProperty(ref _percentageOfStrawReturnedToSoilForRootCrops, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfStrawReturnedToSoilForRootCrops), value))
                    {
                        ActiveFarm.Defaults.PercentageOfStrawReturnedToSoilForRootCrops = value;
                    }
                }
            }
        }

        public double PercentageOfProductReturnedToSoilForRootCrops
        {
            get => _percentageOfProductReturnedToSoilForRootCrops;
            set
            {
                if (SetProperty(ref _percentageOfProductReturnedToSoilForRootCrops, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfProductReturnedToSoilForRootCrops), value))
                    {
                        ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForRootCrops = value;
                    }
                }
            }
        }

        public double PercentageOfRootsReturnedToSoilForPerennials
        {
            get => _percentageOfRootsReturnedToSoilForPerennials;
            set
            {
                if (SetProperty(ref _percentageOfRootsReturnedToSoilForPerennials, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfRootsReturnedToSoilForPerennials), value))
                    {
                        ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForPerennials = value;
                    }
                }
            }
        }

        public double PercentageOfProductReturnedToSoilForPerennials
        {
            get => _percentageOfProductReturnedToSoilForPerennials;
            set
            {
                if (SetProperty(ref _percentageOfProductReturnedToSoilForPerennials, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfProductReturnedToSoilForPerennials), value))
                    {
                        ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForPerennials = value;
                    }
                }
            }
        }

        public double PercentageOfRootsReturnedToSoilForFodderCorn
        {
            get => _percentageOfRootsReturnedToSoilForFodderCorn;
            set
            {
                if (SetProperty(ref _percentageOfRootsReturnedToSoilForFodderCorn, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfRootsReturnedToSoilForFodderCorn), value))
                    {
                        ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForFodderCorn = value;
                    }
                }
            }
        }

        public double PercentageOfProductReturnedToSoilForFodderCorn
        {
            get => _percentageOfProductReturnedToSoilForFodderCorn;
            set
            {
                if (SetProperty(ref _percentageOfProductReturnedToSoilForFodderCorn, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfProductReturnedToSoilForFodderCorn), value))
                    {
                        ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForFodderCorn = value;
                    }
                }
            }
        }

        public double PercentageOfRootsReturnedToSoilForAnnuals
        {
            get => _percentageOfRootsReturnedToSoilForAnnuals;
            set
            {
                if (SetProperty(ref _percentageOfRootsReturnedToSoilForAnnuals, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfRootsReturnedToSoilForAnnuals), value))
                    {
                        ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForAnnuals = value;
                    }
                }
            }
        }

        public int DefaultRunInPeriod
        {
            get => _defaultRunInPeriod;
            set
            {
                if (SetProperty(ref _defaultRunInPeriod, value))
                {
                    if (ValidateNonNegative(nameof(DefaultRunInPeriod), value))
                    {
                        ActiveFarm.Defaults.DefaultRunInPeriod = value;
                    }
                }
            }
        }

        public double PercentageOfProductReturnedToSoilForAnnuals
        {
            get => _percentageOfProductReturnedToSoilForAnnuals;
            set
            {
                if (SetProperty(ref _percentageOfProductReturnedToSoilForAnnuals, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfProductReturnedToSoilForAnnuals), value))
                    {
                        ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForAnnuals = value;
                    }
                }
            }
        }

        public double PercentageOfStrawReturnedToSoilForAnnuals
        {
            get => _percentageOfStrawReturnedToSoilForAnnuals;
            set
            {
                if (SetProperty(ref _percentageOfStrawReturnedToSoilForAnnuals, value))
                {
                    if (ValidatePercentage(nameof(PercentageOfStrawReturnedToSoilForAnnuals), value))
                    {
                        ActiveFarm.Defaults.PercentageOfStrawReturnedToSoilForAnnuals = value;
                    }
                }
            }
        }

        public double CustomEquilibriumCarbonValue
        {
            get => _customEquilibriumCarbonValue;
            set
            {
                if (SetProperty(ref _customEquilibriumCarbonValue, value))
                {
                    if (ValidateNonNegative(nameof(CustomEquilibriumCarbonValue), value))
                    {
                        ActiveFarm.StartingSoilOrganicCarbon = value;
                    }
                }
            }
        }

        public double CarbonConcentration
        {
            get => _carbonConcentration;
            set
            {
                if (SetProperty(ref _carbonConcentration, value))
                {
                    if (ValidateNonNegative(nameof(CarbonConcentration), value))
                    {
                        ActiveFarm.Defaults.CarbonConcentration = value;
                    }
                }
            }
        }

        public CarbonModellingStrategies CarbonModellingStrategy
        {
            get => _carbonModellingStrategy;
            set
            {
                if (SetProperty(ref _carbonModellingStrategy, value))
                {
                    ActiveFarm.Defaults.CarbonModellingStrategy = value;
                }
            } 
        }

        public EquilibriumCalculationStrategies EquilibriumCalculationStrategy
        {
            get => _equilibriumCalculationStrategy;
            set
            {
                if (SetProperty(ref _equilibriumCalculationStrategy, value))
                {
                    ActiveFarm.Defaults.EquilibriumCalculationStrategy = value;
                }
            }
        }

        public TillageType RunInPeriodTillageType
        {
            get => _runInPeriodTillageType;
            set
            {
                if (SetProperty(ref _runInPeriodTillageType, value))
                {
                    ActiveFarm.Defaults.RunInPeriodTillageType = value;
                }
            }
        }

        public PumpType DefaultPumpType
        {
            get => _defaultPumpType;
            set
            {
                if (SetProperty(ref _defaultPumpType, value))
                {
                    ActiveFarm.Defaults.DefaultPumpType = value;
                }
            }
        }

        //Display the custom equilibrium carbon input field if the user selects the custom option
        public bool ShowCustomEquilibriumCarbonInput
        {
            get => _showCustomEquilibriumCarbonInput;
            set => SetProperty(ref _showCustomEquilibriumCarbonInput, value);
        }

        //Display the custom N2O emission factor input field if the user selects the custom option
        public bool ShowCustomN2OEmissionFactor
        {
            get => _showCustomN2OEmissionFactor;
            set => SetProperty(ref _showCustomN2OEmissionFactor, value);
        }

        //Display the custom run-in period tillage input field if the user selects the custom option
        public bool ShowCustomRunInPeriodTillage
        {
            get => _showCustomRunInPeriodTillage;
            set => SetProperty(ref _showCustomRunInPeriodTillage, value);
        }

        //Collections for ComboBox
        public ObservableCollection<CarbonModellingStrategies> CarbonModellingStrategiesList
        {
            get => _carbonModellingStrategiesList;
            set => SetProperty(ref _carbonModellingStrategiesList, value);
        }

        public ObservableCollection<EquilibriumCalculationStrategies> EquilibriumCalculationStrategiesList
        {
            get => _equilibriumCalculationStrategiesList;
            set => SetProperty(ref _equilibriumCalculationStrategiesList, value);
        }

        public ObservableCollection<TillageType> RunInPeriodTillageList
        {
            get => _runInPeriodTillageList;
            set => SetProperty(ref _runInPeriodTillageList, value);
        }

        public ObservableCollection<PumpType> PumpTypeList
        {
            get => _pumpTypeList;
            set => SetProperty(ref _pumpTypeList, value);
        }

        #endregion

        #region Methods
        public void ManageData()
        {
            // Populate the CarbonModellingStrategiesList with all CarbonModellingStrategies values
            CarbonModellingStrategiesList = new ObservableCollection<CarbonModellingStrategies>();
            var carbonModellingStrategies = CarbonModellingStrategies.GetValues(typeof(CarbonModellingStrategies));
            foreach (CarbonModellingStrategies carbonModellingStrategy in carbonModellingStrategies)
            {
                if (!CarbonModellingStrategiesList.Contains(carbonModellingStrategy))
                {
                    CarbonModellingStrategiesList.Add(carbonModellingStrategy);
                }
            }

            // Populate the EquilibriumCalculationStrategiesList with all EquilibriumCalculationStrategies values
            EquilibriumCalculationStrategiesList = new ObservableCollection<EquilibriumCalculationStrategies>();
            var equilibriumCalculationStrategies = EquilibriumCalculationStrategies.GetValues(typeof(EquilibriumCalculationStrategies));
            foreach (EquilibriumCalculationStrategies equilibriumCalculationStrategy in equilibriumCalculationStrategies)
            {
                if (!EquilibriumCalculationStrategiesList.Contains(equilibriumCalculationStrategy))
                {
                    EquilibriumCalculationStrategiesList.Add(equilibriumCalculationStrategy);
                }
            }

            // Populate the RunInPeriodTillageList with all TillageType values
            RunInPeriodTillageList = new ObservableCollection<TillageType>();
            var tillageTypes = TillageType.GetValues(typeof(TillageType));
            foreach (TillageType tillageType in tillageTypes)
            {
                if (!RunInPeriodTillageList.Contains(tillageType))
                {
                    RunInPeriodTillageList.Add(tillageType);
                }
            }

            // Populate the PumpTypeList with all PumpType values
            PumpTypeList = new ObservableCollection<PumpType>();
            var pumpTypes = PumpType.GetValues(typeof(PumpType));
            foreach (PumpType pumpType in pumpTypes)
            {
                if (!PumpTypeList.Contains(pumpType))
                {
                    PumpTypeList.Add(pumpType);
                }
            }

            // Initialize properties to values from the ActiveFarm
            this.CustomN2OEmissionFactor = base.ActiveFarm.Defaults.CustomN2OEmissionFactor;
            this.EmissionFactorForLeachingAndRunoff = base.ActiveFarm.Defaults.EmissionFactorForLeachingAndRunoff;
            this.EmissionFactorForVolatilization = base.ActiveFarm.Defaults.EmissionFactorForVolatilization;
            this.DefaultSupplementalFeedingLossPercentage = base.ActiveFarm.Defaults.DefaultSupplementalFeedingLossPercentage;
            this.PercentageOfStrawReturnedToSoilForRootCrops = base.ActiveFarm.Defaults.PercentageOfStrawReturnedToSoilForRootCrops;
            this.PercentageOfProductReturnedToSoilForRootCrops = base.ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForRootCrops;
            this.PercentageOfRootsReturnedToSoilForPerennials = base.ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForPerennials;
            this.PercentageOfProductReturnedToSoilForPerennials = base.ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForPerennials;
            this.PercentageOfRootsReturnedToSoilForFodderCorn = base.ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForFodderCorn;
            this.PercentageOfProductReturnedToSoilForFodderCorn = base.ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForFodderCorn;
            this.PercentageOfRootsReturnedToSoilForAnnuals = base.ActiveFarm.Defaults.PercentageOfRootsReturnedToSoilForAnnuals;
            this.DefaultRunInPeriod = base.ActiveFarm.Defaults.DefaultRunInPeriod;
            this.PercentageOfProductReturnedToSoilForAnnuals = base.ActiveFarm.Defaults.PercentageOfProductReturnedToSoilForAnnuals;
            this.PercentageOfStrawReturnedToSoilForAnnuals = base.ActiveFarm.Defaults.PercentageOfStrawReturnedToSoilForAnnuals;
            this.CustomEquilibriumCarbonValue = base.ActiveFarm.StartingSoilOrganicCarbon;
            this.CarbonConcentration = base.ActiveFarm.Defaults.CarbonConcentration;
            this.CarbonModellingStrategy = base.ActiveFarm.Defaults.CarbonModellingStrategy;
            this.EquilibriumCalculationStrategy = base.ActiveFarm.Defaults.EquilibriumCalculationStrategy;
            this.RunInPeriodTillageType = base.ActiveFarm.Defaults.RunInPeriodTillageType;
            this.DefaultPumpType = base.ActiveFarm.Defaults.DefaultPumpType;
        }

        //Validates the value if it is negative
        public bool ValidateNonNegative(string propertyName, double value)
        {
            if (value < 0)
            {
                AddError(propertyName, H.Core.Properties.Resources.ErrorMustBeGreaterThan0);
                return false;
            }
            else
            {
                RemoveError(propertyName);
                return true;
            }
        }

        //Validates the value if it is not between 0 and 100
        public bool ValidatePercentage(string propertyName, double value)
        {
            if (value < 0 || value > 100)
            {
                AddError(propertyName, H.Core.Properties.Resources.ErrorMustBeBetween0And100);
                return false;
            }
            else
            {
                RemoveError(propertyName);
                return true;
            }
        }
        #endregion
    }
}
