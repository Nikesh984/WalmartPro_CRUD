using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WalmartPro.Models;

public partial class WalmartProContext : DbContext
{
    public WalmartProContext()
    {
    }

    public WalmartProContext(DbContextOptions<WalmartProContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CustomerSupport> CustomerSupports { get; set; }

    public virtual DbSet<DeliveryLogistic> DeliveryLogistics { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProductLine> OrderProductLines { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPromotion> ProductPromotions { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<SupplyLogistic> SupplyLogistics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwActiveProductPromotion> VwActiveProductPromotions { get; set; }

    public virtual DbSet<VwAggregatedProductReviewsExtended> VwAggregatedProductReviewsExtendeds { get; set; }

    public virtual DbSet<VwEnhancedCustomerOrderSummary> VwEnhancedCustomerOrderSummaries { get; set; }

    public virtual DbSet<VwEnhancedSupportTicketOverview> VwEnhancedSupportTicketOverviews { get; set; }

    public virtual DbSet<VwOrderPricingSummary> VwOrderPricingSummaries { get; set; }

    public virtual DbSet<VwProductInventoryStatus> VwProductInventoryStatuses { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    public virtual DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=WalmartPro;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__6DB38D4E09443C2E");

            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<CustomerSupport>(entity =>
        {
            entity.HasKey(e => e.SupportId).HasName("PK__Customer__9285D928EAE38BCB");

            entity.ToTable("CustomerSupport");

            entity.Property(e => e.SupportId).HasColumnName("Support_ID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IssueDescription)
                .HasMaxLength(1000)
                .HasColumnName("Issue_Description");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.CustomerSupports)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__CustomerS__Order__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerSupports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerS__User___73BA3083");
        });

        modelBuilder.Entity<DeliveryLogistic>(entity =>
        {
            entity.HasKey(e => e.LogisticsId).HasName("PK__Delivery__14FC4C79BE46DBF3");

            entity.ToTable("Delivery_Logistics");

            entity.Property(e => e.LogisticsId).HasColumnName("Logistics_ID");
            entity.Property(e => e.ActualDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("Actual_Delivery_Date");
            entity.Property(e => e.ExpectedDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("Expected_Delivery_Date");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TrackingId)
                .HasMaxLength(50)
                .HasColumnName("Tracking_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.DeliveryLogistics)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Delivery___Order__6A30C649");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1E4639B10337BA5");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AdjustProductStock");
                    tb.HasTrigger("trg_AutoPopulateDeliveryLogistics");
                });

            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasColumnName("Order_Status");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(50)
                .HasColumnName("Shipping_Method");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("Total_Price");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__User_ID__47DBAE45");
        });

        modelBuilder.Entity<OrderProductLine>(entity =>
        {
            entity.HasKey(e => e.OrderProductId).HasName("PK__Order_Pr__65BE90238178349D");

            entity.ToTable("Order_Product_Line");

            entity.Property(e => e.OrderProductId).HasColumnName("Order_Product_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProductLines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Pro__Order__7F2BE32F");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProductLines)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Order_Pro__Produ__00200768");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__DA6C7FE109991A6B");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Payment_Date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.TransactionDetails)
                .HasMaxLength(1000)
                .HasColumnName("Transaction_Details");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Payments_Order");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__9834FB9A396AEFD8");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.ActualPrice)
                .HasColumnType("money")
                .HasColumnName("Actual_Price");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(1000)
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("Product_Name");
            entity.Property(e => e.StockQuantity).HasColumnName("Stock_Quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Products_Category");
        });

        modelBuilder.Entity<ProductPromotion>(entity =>
        {
            entity.HasKey(e => e.PromotionProductId).HasName("PK__Product___C5324B9244887D1C");

            entity.ToTable("Product_Promotions");

            entity.Property(e => e.PromotionProductId).HasColumnName("Promotion_Product_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.PromotionId).HasColumnName("Promotion_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPromotions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Product_Promotions_Product");

            entity.HasOne(d => d.Promotion).WithMany(p => p.ProductPromotions)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_Product_Promotions_Promotion");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__DAF79AFBFFF073EE");

            entity.Property(e => e.PromotionId).HasColumnName("Promotion_ID");
            entity.Property(e => e.DiscountPercentage).HasColumnName("Discount_Percentage");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__F85DA7EBDE10AD2D");

            entity.Property(e => e.ReviewId).HasColumnName("Review_ID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Review_Date");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Reviews_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Reviews_User");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__Sellers__009FC2495F0F0CDE");

            entity.HasIndex(e => e.SellerEmail, "UQ__Sellers__3726DD505F34C51E").IsUnique();

            entity.HasIndex(e => e.SellerUsername, "UQ__Sellers__A66055F00BA2A640").IsUnique();

            entity.Property(e => e.SellerId).HasColumnName("Seller_ID");
            entity.Property(e => e.LastLoginDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Login_Date");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Registration_Date");
            entity.Property(e => e.SellerEmail)
                .HasMaxLength(256)
                .HasColumnName("Seller_Email");
            entity.Property(e => e.SellerFirstName)
                .HasMaxLength(50)
                .HasColumnName("Seller_First_Name");
            entity.Property(e => e.SellerLastName)
                .HasMaxLength(50)
                .HasColumnName("Seller_Last_Name");
            entity.Property(e => e.SellerMobileNumber)
                .HasMaxLength(15)
                .HasColumnName("Seller_Mobile_Number");
            entity.Property(e => e.SellerPassword)
                .HasMaxLength(256)
                .HasColumnName("Seller_Password");
            entity.Property(e => e.SellerUsername)
                .HasMaxLength(50)
                .HasColumnName("Seller_Username");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.ShippingAddressId).HasName("PK__Shipping__1F023AA037EE1A83");

            entity.Property(e => e.ShippingAddressId).HasColumnName("Shipping_Address_ID");
            entity.Property(e => e.AppartmentName)
                .HasMaxLength(100)
                .HasColumnName("Appartment_Name");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Region).HasMaxLength(25);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ShippingA__User___6E01572D");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Shopping__D6AB58B9C02E9B22");

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.CartId).HasColumnName("Cart_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ShoppingCart_User");
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__Shopping__7B65152148199DAF");

            entity.Property(e => e.CartItemId).HasColumnName("CartItem_ID");
            entity.Property(e => e.CartId).HasColumnName("Cart_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Cart).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_ShoppingCartItems_Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ShoppingCartItems_Product");
        });

        modelBuilder.Entity<SupplyLogistic>(entity =>
        {
            entity.HasKey(e => e.SupplyLogisticsId).HasName("PK__SupplyLo__C9C62AF6AB9EC360");

            entity.Property(e => e.SupplyLogisticsId).HasColumnName("Supply_Logistics_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.SellerId).HasColumnName("Seller_ID");
            entity.Property(e => e.SellerPrice)
                .HasColumnType("money")
                .HasColumnName("Seller_Price");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyLogistics)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_SupplyLogistics_Product");

            entity.HasOne(d => d.Seller).WithMany(p => p.SupplyLogistics)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SupplyLogistics_Supplier");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206D91902ED9BF8D");

            entity.ToTable(tb => tb.HasTrigger("trg_Audit_Users"));

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E46BEA5398").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053434B5FC98").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastLoginDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Login_Date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .HasColumnName("Mobile_Number");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasDefaultValueSql("(EncryptByKey(Key_Guid('EncryptionKey'),CONVERT([varbinary](256),'')))");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Registration_Date");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<VwActiveProductPromotion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ActiveProductPromotions");

            entity.Property(e => e.ActualPrice)
                .HasColumnType("money")
                .HasColumnName("Actual_Price");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("Category_Name");
            entity.Property(e => e.DiscountPercentage).HasColumnName("Discount_Percentage");
            entity.Property(e => e.DiscountedPrice).HasColumnType("money");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("Product_Name");
            entity.Property(e => e.PromotionId).HasColumnName("Promotion_ID");
            entity.Property(e => e.SellerEmail)
                .HasMaxLength(256)
                .HasColumnName("Seller_Email");
            entity.Property(e => e.SellerFullName).HasMaxLength(101);
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
        });

        modelBuilder.Entity<VwAggregatedProductReviewsExtended>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_AggregatedProductReviewsExtended");

            entity.Property(e => e.LatestReviewDate).HasColumnType("datetime");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("Product_Name");
        });

        modelBuilder.Entity<VwEnhancedCustomerOrderSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EnhancedCustomerOrderSummary");

            entity.Property(e => e.AverageOrderValue).HasColumnType("money");
            entity.Property(e => e.CustomerSegment)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(101);
            entity.Property(e => e.LastOrderDate).HasColumnType("datetime");
            entity.Property(e => e.LastOrderStatus).HasMaxLength(50);
            entity.Property(e => e.TotalSpent).HasColumnType("money");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<VwEnhancedSupportTicketOverview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EnhancedSupportTicketOverview");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.FullName).HasMaxLength(101);
            entity.Property(e => e.IssueDescription)
                .HasMaxLength(1000)
                .HasColumnName("Issue_Description");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Priority)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SupportId).HasColumnName("Support_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<VwOrderPricingSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_OrderPricingSummary");

            entity.Property(e => e.ActualPrice)
                .HasColumnType("money")
                .HasColumnName("Actual_Price");
            entity.Property(e => e.GrandTotal)
                .HasColumnType("money")
                .HasColumnName("Grand_Total");
            entity.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Order_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<VwProductInventoryStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ProductInventoryStatus");

            entity.Property(e => e.InventoryStatus)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("Product_Name");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK__Wishlist__C65247830430F344");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("Wishlist_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Wishlist_User");
        });

        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(e => e.WishlistItemId).HasName("PK__Wishlist__CE1FB2BE025CDB7F");

            entity.Property(e => e.WishlistItemId).HasColumnName("Wishlist_Item_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.WishlistId).HasColumnName("Wishlist_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.WishlistItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Wishlist_Product");

            entity.HasOne(d => d.Wishlist).WithMany(p => p.WishlistItems)
                .HasForeignKey(d => d.WishlistId)
                .HasConstraintName("FK_Wishlist_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
