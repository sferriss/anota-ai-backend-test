﻿namespace Catalog.Domain.Abstractions;

public interface IEntity<T>
{
    T Id { get; set; }
}