﻿using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public string Token { get; set; }
    public string Name  { get; set; }
}