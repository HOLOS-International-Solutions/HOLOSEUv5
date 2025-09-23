﻿using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Services;

public interface IComponentService
{
    string GetUniqueComponentName(Farm farm, ComponentBase component);
}