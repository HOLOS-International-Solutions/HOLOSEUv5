using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Mappers;
using Prism.Ioc;

namespace H.Core.Factories;

public class ManagementPeriodFactory : IManagementPeriodFactory
{
    #region Fields

    private readonly IMapper _managementPeriodDtoToManagementPeriodDtoMapper;

    #endregion

    #region Constructors

    public ManagementPeriodFactory(IContainerProvider containerProvider)
    {
        if (containerProvider != null)
        {
            
            _managementPeriodDtoToManagementPeriodDtoMapper = containerProvider.Resolve<IMapper>(nameof(ManagementPeriodDtoToManagementPeriodDtoMapper));
        }
        else
        {
            throw new ArgumentNullException(nameof(containerProvider));
        }
    }

    #endregion

    #region Public Methods

    public IManagementPeriodDto CreateManagementPeriodDto()
    {
        return new ManagementPeriodDto();
    }

    #endregion
}