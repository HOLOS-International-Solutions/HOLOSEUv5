using System;
using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Mappers;
using H.Core.Models;
using H.Core.Models.Animals;
using Prism.Ioc;

namespace H.Core.Factories;

public class ManagementPeriodFactory : IManagementPeriodFactory
{
    #region Fields

    private readonly IMapper _managementPeriodDtoToManagementPeriodDtoMapper;
    private readonly IMapper _managementPeriodViewItemToManagementPeriodDtoMapper;
    private readonly IMapper _managementPeriodDtoToManagementPeriodViewItemMapper;
    private readonly IMapper _managementPeriodToManagementPeriodDtoMapper;
    private readonly IMapper _managementPeriodDtoToManagementPeriodMapper;

    #endregion

    #region Constructors

    public ManagementPeriodFactory(IContainerProvider containerProvider)
    {
        if (containerProvider == null)
        {
            throw new ArgumentNullException(nameof(containerProvider));
        }

        _managementPeriodDtoToManagementPeriodDtoMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodDtoToManagementPeriodDtoMapper));
        _managementPeriodViewItemToManagementPeriodDtoMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodViewItemToManagementPeriodDtoMapper));
        _managementPeriodDtoToManagementPeriodViewItemMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodDtoToManagementPeriodViewItemMapper));
        _managementPeriodToManagementPeriodDtoMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodToManagementPeriodDtoMapper));
        _managementPeriodDtoToManagementPeriodMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodDtoToManagementPeriodMapper));
    }

    #endregion

    #region Public Methods

    public IManagementPeriodDto CreateManagementPeriodDto()
    {
        var dto = new ManagementPeriodDto();
        dto.Name = "New Management Period";
        dto.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
        dto.EndDate = new DateTime(DateTime.Now.Year, 12, 31);
        dto.NumberOfDays = (dto.EndDate - dto.StartDate).Days + 1;
        
        return dto;
    }

    public IManagementPeriodDto CreateManagementPeriodDto(IManagementPeriodDto template)
    {
        var dto = new ManagementPeriodDto();
        _managementPeriodDtoToManagementPeriodDtoMapper.Map(template, dto);
        return dto;
    }

    public IManagementPeriodDto CreateManagementPeriodDto(ManagementPeriodViewItem template)
    {
        var dto = new ManagementPeriodDto();
        _managementPeriodViewItemToManagementPeriodDtoMapper.Map(template, dto);
        return dto;
    }

    public IManagementPeriodDto CreateManagementPeriodDto(ManagementPeriod managementPeriod)
    {
        var dto = new ManagementPeriodDto();
        _managementPeriodToManagementPeriodDtoMapper.Map(managementPeriod, dto);
        return dto;
    }

    public ManagementPeriodViewItem CreateManagementPeriodViewItem(IManagementPeriodDto dto)
    {
        var viewItem = new ManagementPeriodViewItem();
        _managementPeriodDtoToManagementPeriodViewItemMapper.Map(dto, viewItem);
        return viewItem;
    }

    public ManagementPeriod CreateManagementPeriod(IManagementPeriodDto dto)
    {
        var managementPeriod = new ManagementPeriod();
        _managementPeriodDtoToManagementPeriodMapper.Map(dto, managementPeriod);
        return managementPeriod;
    }

    #endregion
}