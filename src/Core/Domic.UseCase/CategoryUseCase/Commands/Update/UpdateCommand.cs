﻿using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public string Id   { get; set; }
    public string Name { get; set; }
}