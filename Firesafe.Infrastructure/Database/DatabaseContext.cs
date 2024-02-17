using Firesafe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public required DbSet<User> Users { get; init; }
    public required DbSet<Role> Roles { get; init; }
    public required DbSet<Supplier> Suppliers { get; init; }
    public required DbSet<Cart> Carts { get; init; }
    public required DbSet<Category> Categories { get; init; }
    public required DbSet<Contact> Contacts { get; init; }
    public required DbSet<Product> Products { get; init; }
    public required DbSet<ProductCategory> ProductCategories { get; init; }
    public required DbSet<ProductImage> ProductImages { get; init; }
    public required DbSet<ProductReview> ProductReviews { get; init; }
    public required DbSet<ProductCertificate> ProductCertifications { get; init; }
    public required DbSet<SupplierContact> SupplierContacts { get; init; }
    public required DbSet<SupplierReview> SupplierReviews { get; init; }
    public required DbSet<Province> Provinces { get; init; }
    public required DbSet<UserRole> UserRoles { get; init; }
    public required DbSet<ProductFavorite> ProductFavorites { get; init; }
    public required DbSet<ProductViewHistory> ProductViewHistories { get; init; }
    public required DbSet<Newspaper> Newspapers { get; init; }
    public required DbSet<NewspaperImage> NewspaperImages { get; init; }
    public required DbSet<NewspaperCategory> NewspaperCategories { get; init; }
    public required DbSet<NewspaperCategoryJunction> NewspaperCategoryJunctions { get; init; }
    public required DbSet<UserDevice> UserDevices { get; init; }
    public required DbSet<Origin> Origins { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        User.ConfigureRelationship(modelBuilder.Entity<User>());
        Role.ConfigureRelationship(modelBuilder.Entity<Role>());
        Supplier.ConfigureRelationship(modelBuilder.Entity<Supplier>());
        Cart.ConfigureRelationship(modelBuilder.Entity<Cart>());
        Category.ConfigureRelationship(modelBuilder.Entity<Category>());
        Contact.ConfigureRelationship(modelBuilder.Entity<Contact>());
        Origin.ConfigureRelationship(modelBuilder.Entity<Origin>());
        Product.ConfigureRelationship(modelBuilder.Entity<Product>());
        ProductCategory.ConfigureRelationship(modelBuilder.Entity<ProductCategory>());
        ProductImage.ConfigureRelationship(modelBuilder.Entity<ProductImage>());
        ProductReview.ConfigureRelationship(modelBuilder.Entity<ProductReview>());
        SupplierContact.ConfigureRelationship(modelBuilder.Entity<SupplierContact>());
        SupplierReview.ConfigureRelationship(modelBuilder.Entity<SupplierReview>());
        Province.ConfigureRelationship(modelBuilder.Entity<Province>());
        UserRole.ConfigureRelationship(modelBuilder.Entity<UserRole>());
        ProductFavorite.ConfigureRelationship(modelBuilder.Entity<ProductFavorite>());
        ProductViewHistory.ConfigureRelationship(modelBuilder.Entity<ProductViewHistory>());
        Newspaper.ConfigureRelationship(modelBuilder.Entity<Newspaper>());
        NewspaperImage.ConfigureRelationship(modelBuilder.Entity<NewspaperImage>());
        NewspaperCategory.ConfigureRelationship(modelBuilder.Entity<NewspaperCategory>());
        NewspaperCategoryJunction.ConfigureRelationship(modelBuilder.Entity<NewspaperCategoryJunction>());
        UserDevice.ConfigureRelationship(modelBuilder.Entity<UserDevice>());
        ProductCertificate.ConfigureRelationship(modelBuilder.Entity<ProductCertificate>());

        Role.ConfigureInitialData(modelBuilder.Entity<Role>());
        Category.ConfigureInitialData(modelBuilder.Entity<Category>());
        Origin.ConfigureInitialData(modelBuilder.Entity<Origin>());
        Province.ConfigureInitialData(modelBuilder.Entity<Province>());
        NewspaperCategory.ConfigureInitialData(modelBuilder.Entity<NewspaperCategory>());

        base.OnModelCreating(modelBuilder);
    }
}