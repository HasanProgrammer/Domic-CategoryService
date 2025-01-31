using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Category.Events;
using Domic.Domain.Category.ValueObjects;

#pragma warning disable CS0649

namespace Domic.Domain.Category.Entities;

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
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="name"></param>
    public Category(IDateTime dateTime, IGlobalUniqueIdGenerator globalUniqueIdGenerator, IIdentityUser identityUser,
        ISerializer serializer, string name
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id          = globalUniqueIdGenerator.GetRandom(6);
        CreatedBy   = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        Name        = new Name(name);
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryCreated {
                Id          = Id          ,
                CreatedBy   = CreatedBy   ,
                CreatedRole = CreatedRole ,
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
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="name"></param>
    public void Change(IDateTime dateTime, IIdentityUser identityUser,
        ISerializer serializer, string name
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        Name        = new Name(name);
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryUpdated {
                Id          = Id          ,
                UpdatedBy   = UpdatedBy   ,
                UpdatedRole = UpdatedRole ,
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
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="id"></param>
    public void Delete(IDateTime dateTime, IIdentityUser identityUser,
        ISerializer serializer, string id
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsDeleted   = IsDeleted.Delete;
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new CategoryDeleted {
                Id          = id          ,
                UpdatedBy   = UpdatedBy   ,
                UpdatedRole = UpdatedRole ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}