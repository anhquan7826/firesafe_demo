using Firesafe.Domain.Entities;

namespace Infrastructure.Authorization;

public static class FiresafePolicy
{
    public const string FirebaseAuthenticated = "FirebaseAuthenticated";
    public const string MustBeRegistered = "MustBeRegistered";
    public const string IsASupplier = "IsASupplier";

    public static RequiredRoles Requirement(string policy)
    {
        return policy switch
        {
            MustBeRegistered => new RequiredRoles([]),
            IsASupplier => new RequiredRoles([Roles.Supplier]),
            _ => new RequiredRoles()
        };
    }
}