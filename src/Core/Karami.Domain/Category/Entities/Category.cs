using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Enumerations;
using Karami.Core.Domain.ValueObjects;
using Karami.Domain.Category.Events;
using Karami.Domain.Category.ValueObjects;

#pragma warning disable CS0649

namespace Karami.Domain.Category.Entities;

public class Category : Entity<string>
{
    //Value Objects
    
    public Name Name { get; private set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private Category() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="id"></param>
    /// <param name="createdBy"></param>
    /// <param name="createdRole"></param>
    /// <param name="name"></param>
    public Category(IDateTime dateTime, string id, string createdBy, string createdRole, string name)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id          = id;
        CreatedBy   = createdBy;
        CreatedRole = createdRole;
        Name        = new Name(name);
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryCreated {
                Id          = id          ,
                CreatedBy   = createdBy   ,
                CreatedRole = createdRole ,
                Name        = name        ,
                CreatedAt_EnglishDate = nowDateTime ,
                CreatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /*---------------------------------------------------------------*/
    
    //Behaviors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="name"></param>
    public void Change(IDateTime dateTime, string updatedBy, string updatedRole, string name)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        Name        = new Name(name);
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryUpdated {
                Id          = Id          ,
                UpdatedBy   = updatedBy   ,
                UpdatedRole = updatedRole ,
                Name        = name        ,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="id"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    public void Delete(IDateTime dateTime, string id, string updatedBy, string updatedRole)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsDeleted   = IsDeleted.Delete;
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryDeleted {
                Id          = id          ,
                UpdatedBy   = updatedBy   ,
                UpdatedRole = updatedRole ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}