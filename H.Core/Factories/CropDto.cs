﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using H.Core.CustomAttributes;
using H.Core.Enumerations;
using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using Prism.Mvvm;

namespace H.Core.Factories;

/// <summary>
/// A class used to validate input as it relates to a <see cref="CropViewItem"/>. This class is used to valid input before any input
/// is transferred to the <see cref="CropViewItem"/>
/// </summary>
public class CropDto : DtoBase, ICropDto
{
    #region Fields

    private double _amountOfIrrigation;
    private int _year;
    private CropType _cropType;
    private ObservableCollection<CropType> _cropTypes;

    #endregion

    #region Constructors

    public CropDto()
    {
        this.ValidCropTypes = new ObservableCollection<CropType>() { CropType.NotSelected, CropType.Oats, CropType.Wheat, CropType.Barley };
        this.CropType = this.ValidCropTypes.ElementAt(0);
        this.Year = DateTime.Now.Year;

        this.PropertyChanged += OnPropertyChanged;
    }

    #endregion

    #region Properties

    public CropType CropType
    {
        get => _cropType;
        set => SetProperty(ref _cropType, value);
    }

    public ObservableCollection<CropType> ValidCropTypes
    {
        get => _cropTypes;
        set => SetProperty(ref _cropTypes, value);
    }

    public int Year
    {
        get => _year;
        set => SetProperty(ref _year, value);
    }


    [Units(MetricUnitsOfMeasurement.Millimeters)]
    public double AmountOfIrrigation
    {
        get => _amountOfIrrigation;
        set => SetProperty(ref _amountOfIrrigation, value);
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// The user must specify a crop type before proceeding
    /// </summary>
    private void ValidateCropType()
    {
        var key = nameof(CropType);
        if (this.CropType == CropType.NotSelected)
        {
            AddError(key, "A crop type must be selected");
        }
        else
        {
            RemoveError(key);
        }
    }

    private void ValidateAmountOfIrrigation()
    {
        var key = nameof(AmountOfIrrigation);
        if (this.AmountOfIrrigation < 0)
        {
            AddError(key, "Amount of irrigation cannot be negative");
        }
        else
        {
            RemoveError(key);
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals(nameof(CropType)))
        {
            // Ensure the crop type is valid
            this.ValidateCropType();
        }
        else if (e.PropertyName.Equals(nameof(AmountOfIrrigation)))
        {
            this.ValidateAmountOfIrrigation();
        }
    }

    #endregion
}