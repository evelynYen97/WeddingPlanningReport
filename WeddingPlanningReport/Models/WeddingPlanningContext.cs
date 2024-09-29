using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanningReport.Models;

public partial class WeddingPlanningContext : DbContext
{
    public WeddingPlanningContext(DbContextOptions<WeddingPlanningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BudgetChart> BudgetCharts { get; set; }

    public virtual DbSet<Cake> Cakes { get; set; }

    public virtual DbSet<CakeOrder> CakeOrders { get; set; }

    public virtual DbSet<CakeOrderDetail> CakeOrderDetails { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarRental> CarRentals { get; set; }

    public virtual DbSet<CarRentalDetail> CarRentalDetails { get; set; }

    public virtual DbSet<ComplaintReview> ComplaintReviews { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishesOrder> DishesOrders { get; set; }

    public virtual DbSet<DishesOrderDetail> DishesOrderDetails { get; set; }

    public virtual DbSet<EditingImgFile> EditingImgFiles { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<ImgUsing> ImgUsings { get; set; }

    public virtual DbSet<Mail> Mail { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberBudgetItem> MemberBudgetItems { get; set; }

    public virtual DbSet<MemberMaterial> MemberMaterials { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduledStaff> ScheduledStaffs { get; set; }

    public virtual DbSet<SharingWeddingPlan> SharingWeddingPlans { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ToDo> ToDos { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<WeddingPlan> WeddingPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BudgetChart>(entity =>
        {
            entity.ToTable("budgetChart");

            entity.Property(e => e.BudgetChartId).HasColumnName("budgetChartID");
            entity.Property(e => e.ChartName)
                .HasMaxLength(100)
                .HasColumnName("chartName");
            entity.Property(e => e.ChartType)
                .HasMaxLength(50)
                .HasColumnName("chartType");
            entity.Property(e => e.ChartUnit)
                .HasMaxLength(50)
                .HasColumnName("chartUnit");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
        });

        modelBuilder.Entity<Cake>(entity =>
        {
            entity.ToTable("cake");

            entity.Property(e => e.CakeId).HasColumnName("cakeID");
            entity.Property(e => e.AllergenInfo)
                .HasMaxLength(50)
                .HasColumnName("allergenInfo");
            entity.Property(e => e.CakeAnnotation)
                .HasMaxLength(100)
                .HasColumnName("cakeAnnotation");
            entity.Property(e => e.CakeContent)
                .HasMaxLength(100)
                .HasColumnName("cakeContent");
            entity.Property(e => e.CakeDescription)
                .HasMaxLength(500)
                .HasColumnName("cakeDescription");
            entity.Property(e => e.CakeImg)
                .HasMaxLength(50)
                .HasColumnName("cakeImg");
            entity.Property(e => e.CakeName)
                .HasMaxLength(50)
                .HasColumnName("cakeName");
            entity.Property(e => e.CakePrice).HasColumnName("cakePrice");
            entity.Property(e => e.CakeStatus).HasColumnName("cakeStatus");
            entity.Property(e => e.CakeStock).HasColumnName("cakeStock");
            entity.Property(e => e.CakeStyles)
                .HasMaxLength(50)
                .HasColumnName("cakeStyles");
            entity.Property(e => e.ShopId).HasColumnName("shopID");
        });

        modelBuilder.Entity<CakeOrder>(entity =>
        {
            entity.ToTable("cakeOrder");

            entity.Property(e => e.CakeOrderId).HasColumnName("cakeOrderID");
            entity.Property(e => e.CakeOrderAnnotation)
                .HasMaxLength(100)
                .HasColumnName("cakeOrderAnnotation");
            entity.Property(e => e.CakeOrderStatus)
                .HasMaxLength(50)
                .HasColumnName("cakeOrderStatus");
            entity.Property(e => e.CakeOrderTotal).HasColumnName("cakeOrderTotal");
            entity.Property(e => e.Delivery)
                .HasMaxLength(50)
                .HasColumnName("delivery");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.OrderTime)
                .HasPrecision(0)
                .HasColumnName("orderTime");
            entity.Property(e => e.Payment)
                .HasMaxLength(20)
                .HasColumnName("payment");
        });

        modelBuilder.Entity<CakeOrderDetail>(entity =>
        {
            entity.ToTable("cakeOrderDetail");

            entity.Property(e => e.CakeOrderDetailId).HasColumnName("cakeOrderDetailID");
            entity.Property(e => e.CakeAmount).HasColumnName("cakeAmount");
            entity.Property(e => e.CakeId).HasColumnName("cakeID");
            entity.Property(e => e.CakeName)
                .HasMaxLength(100)
                .HasColumnName("cakeName");
            entity.Property(e => e.CakeOrderId).HasColumnName("cakeOrderID");
            entity.Property(e => e.CakePrice).HasColumnName("cakePrice");
            entity.Property(e => e.CakeSubtotal).HasColumnName("cakeSubtotal");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK_weddingCar");

            entity.ToTable("car");

            entity.Property(e => e.CarId).HasColumnName("carID");
            entity.Property(e => e.CarDetail)
                .HasMaxLength(200)
                .HasColumnName("carDetail");
            entity.Property(e => e.CarImg)
                .HasMaxLength(100)
                .HasColumnName("carImg");
            entity.Property(e => e.CarName)
                .HasMaxLength(100)
                .HasColumnName("carName");
            entity.Property(e => e.CarStatus)
                .HasMaxLength(50)
                .HasColumnName("carStatus");
            entity.Property(e => e.PassengerCapacity).HasColumnName("passengerCapacity");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RentalPerDay).HasColumnName("rentalPerDay");
            entity.Property(e => e.ShopId).HasColumnName("shopID");
        });

        modelBuilder.Entity<CarRental>(entity =>
        {
            entity.HasKey(e => e.RentalId);

            entity.ToTable("carRental");

            entity.Property(e => e.RentalId).HasColumnName("rentalID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .HasColumnName("clientName");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(50)
                .HasColumnName("clientPhone");
            entity.Property(e => e.LeaseDate)
                .HasPrecision(0)
                .HasColumnName("leaseDate");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.OrderTime)
                .HasPrecision(0)
                .HasColumnName("orderTime");
            entity.Property(e => e.RentalStatus)
                .HasMaxLength(50)
                .HasColumnName("rentalStatus");
            entity.Property(e => e.RentalTotal).HasColumnName("rentalTotal");
            entity.Property(e => e.ReturnDate)
                .HasPrecision(0)
                .HasColumnName("returnDate");
        });

        modelBuilder.Entity<CarRentalDetail>(entity =>
        {
            entity.HasKey(e => e.RentalDetailId).HasName("PK_rentalDetail");

            entity.ToTable("carRentalDetail");

            entity.Property(e => e.RentalDetailId).HasColumnName("rentalDetailID");
            entity.Property(e => e.CarId).HasColumnName("carID");
            entity.Property(e => e.CarName)
                .HasMaxLength(100)
                .HasColumnName("carName");
            entity.Property(e => e.LeaseDays).HasColumnName("leaseDays");
            entity.Property(e => e.LeaseSubtotal).HasColumnName("leaseSubtotal");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RentalId).HasColumnName("rentalID");
        });

        modelBuilder.Entity<ComplaintReview>(entity =>
        {
            entity.HasKey(e => e.ComplaintRecordId).HasName("PK_違規/檢舉處理");

            entity.ToTable("complaintReview");

            entity.Property(e => e.ComplaintRecordId).HasColumnName("complaintRecordID");
            entity.Property(e => e.ReportDetail)
                .HasMaxLength(200)
                .HasColumnName("reportDetail");
            entity.Property(e => e.ReportTime)
                .HasPrecision(0)
                .HasColumnName("reportTime");
            entity.Property(e => e.ReporterMemberId).HasColumnName("reporterMemberID");
            entity.Property(e => e.RespondentMemberId).HasColumnName("respondentMemberID");
            entity.Property(e => e.ReviewNotes)
                .HasMaxLength(200)
                .HasColumnName("reviewNotes");
            entity.Property(e => e.ReviewResultDescription)
                .HasMaxLength(200)
                .HasColumnName("reviewResultDescription");
            entity.Property(e => e.ReviewStatus)
                .HasMaxLength(20)
                .HasColumnName("reviewStatus");
            entity.Property(e => e.ReviewTime)
                .HasPrecision(0)
                .HasColumnName("reviewTime");
            entity.Property(e => e.ReviewerId).HasColumnName("reviewerID");
            entity.Property(e => e.ReviewerName)
                .HasMaxLength(50)
                .HasColumnName("reviewerName");
            entity.Property(e => e.SharedRecordId).HasColumnName("sharedRecordID");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishesId).HasName("PK_Dishes");

            entity.ToTable("dishes");

            entity.Property(e => e.DishesId).HasColumnName("dishesID");
            entity.Property(e => e.DishesDescription)
                .HasMaxLength(200)
                .HasColumnName("dishesDescription");
            entity.Property(e => e.DishesImg)
                .HasMaxLength(200)
                .HasColumnName("dishesImg");
            entity.Property(e => e.DishesName)
                .HasMaxLength(100)
                .HasColumnName("dishesName");
            entity.Property(e => e.DishesSort)
                .HasMaxLength(50)
                .HasColumnName("dishesSort");
            entity.Property(e => e.PricePerTable).HasColumnName("pricePerTable");
            entity.Property(e => e.ShopId).HasColumnName("shopID");
        });

        modelBuilder.Entity<DishesOrder>(entity =>
        {
            entity.ToTable("dishesOrder");

            entity.Property(e => e.DishesOrderId).HasColumnName("dishesOrderID");
            entity.Property(e => e.DishesOrderName)
                .HasMaxLength(100)
                .HasColumnName("dishesOrderName");
            entity.Property(e => e.DishesSupplyDate)
                .HasPrecision(0)
                .HasColumnName("dishesSupplyDate");
            entity.Property(e => e.DishesTotalPrice).HasColumnName("dishesTotalPrice");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.OrderTime).HasColumnName("orderTime");
        });

        modelBuilder.Entity<DishesOrderDetail>(entity =>
        {
            entity.ToTable("dishesOrderDetail");

            entity.Property(e => e.DishesOrderDetailId).HasColumnName("dishesOrderDetailID");
            entity.Property(e => e.DishesAmount).HasColumnName("dishesAmount");
            entity.Property(e => e.DishesId).HasColumnName("dishesID");
            entity.Property(e => e.DishesName)
                .HasMaxLength(100)
                .HasColumnName("dishesName");
            entity.Property(e => e.DishesOrderId).HasColumnName("dishesOrderID");
            entity.Property(e => e.DishesSubtotal).HasColumnName("dishesSubtotal");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
        });

        modelBuilder.Entity<EditingImgFile>(entity =>
        {
            entity.HasKey(e => e.EditingImgFileId).HasName("PK_imgEditing");

            entity.ToTable("editingImgFile");

            entity.Property(e => e.EditingImgFileId).HasColumnName("editingImgFileID");
            entity.Property(e => e.EditTime)
                .HasPrecision(0)
                .HasColumnName("editTime");
            entity.Property(e => e.ImgEditingName)
                .HasMaxLength(200)
                .HasColumnName("imgEditingName");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.Screenshot)
                .HasMaxLength(200)
                .HasColumnName("screenshot");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_活動紀錄");

            entity.ToTable("event");

            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.CaseId).HasColumnName("caseID");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(100)
                .HasColumnName("eventLocation");
            entity.Property(e => e.EventLocationImg)
                .HasMaxLength(200)
                .HasColumnName("eventLocationImg");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .HasColumnName("eventName");
            entity.Property(e => e.EventNotes)
                .HasMaxLength(100)
                .HasColumnName("eventNotes");
            entity.Property(e => e.EventTime)
                .HasPrecision(0)
                .HasColumnName("eventTime");
            entity.Property(e => e.EventVenueImg1)
                .HasMaxLength(200)
                .HasColumnName("eventVenueImg1");
            entity.Property(e => e.EventVenueImg2)
                .HasMaxLength(200)
                .HasColumnName("eventVenueImg2");
        });

        modelBuilder.Entity<ImgUsing>(entity =>
        {
            entity.ToTable("imgUsing");

            entity.Property(e => e.ImgUsingId).HasColumnName("imgUsingID");
            entity.Property(e => e.EditingImgFileId).HasColumnName("editingImgFileID");
            entity.Property(e => e.ImgH)
                .HasMaxLength(10)
                .HasColumnName("imgH");
            entity.Property(e => e.ImgW)
                .HasMaxLength(10)
                .HasColumnName("imgW");
            entity.Property(e => e.ImgX)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("imgX");
            entity.Property(e => e.ImgY)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("imgY");
            entity.Property(e => e.MaterialId).HasColumnName("materialID");
            entity.Property(e => e.MemberMaterialId).HasColumnName("memberMaterialID");
        });

        modelBuilder.Entity<Mail>(entity =>
        {
            entity.HasKey(e => e.MailId).HasName("PK__mail__F5CD7888FA099B11");

            entity.ToTable("mail");

            entity.Property(e => e.MailId).HasColumnName("mailID");
            entity.Property(e => e.IsReplied)
                .HasMaxLength(10)
                .HasDefaultValue("未回覆")
                .HasColumnName("isReplied");
            entity.Property(e => e.MailContent)
                .HasMaxLength(500)
                .HasColumnName("mailContent");
            entity.Property(e => e.MailDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("mailDate");
            entity.Property(e => e.MailTitle)
                .HasMaxLength(200)
                .HasColumnName("mailTitle");
            entity.Property(e => e.MemberEmail)
                .HasMaxLength(100)
                .HasColumnName("memberEmail");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.MemberName)
                .HasMaxLength(50)
                .HasColumnName("memberName");
            entity.Property(e => e.ReplyContent)
                .HasMaxLength(500)
                .HasColumnName("replyContent");
            entity.Property(e => e.ReplyDate).HasColumnName("replyDate");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK_提供圖檔素材");

            entity.ToTable("material");

            entity.Property(e => e.MaterialId).HasColumnName("materialID");
            entity.Property(e => e.EstimatedL).HasColumnName("estimatedL");
            entity.Property(e => e.EstimatedW).HasColumnName("estimatedW");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .HasColumnName("imageName");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__member__7FD7CFF67197028E");

            entity.ToTable("member");

            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.BudgetCakeChart)
                .HasMaxLength(100)
                .HasColumnName("budgetCakeChart");
            entity.Property(e => e.BudgetCarChart)
                .HasMaxLength(100)
                .HasColumnName("budgetCarChart");
            entity.Property(e => e.BudgetDishesChart)
                .HasMaxLength(100)
                .HasColumnName("budgetDishesChart");
            entity.Property(e => e.BudgetOtherChart)
                .HasMaxLength(100)
                .HasColumnName("budgetOtherChart");
            entity.Property(e => e.BudgetPieChart)
                .HasMaxLength(100)
                .HasColumnName("budgetPieChart");
            entity.Property(e => e.BudgetTableImg)
                .HasMaxLength(100)
                .HasColumnName("budgetTableImg");
            entity.Property(e => e.BudgetVenueChart)
                .HasMaxLength(100)
                .HasColumnName("budgetVenueChart");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastLoginTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("lastLoginTime");
            entity.Property(e => e.MemberGrade)
                .HasMaxLength(50)
                .HasColumnName("memberGrade");
            entity.Property(e => e.MemberName)
                .HasMaxLength(50)
                .HasColumnName("memberName");
            entity.Property(e => e.MemberStatus)
                .HasMaxLength(20)
                .HasColumnName("memberStatus");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .HasColumnName("notes");
            entity.Property(e => e.PartnerName)
                .HasMaxLength(50)
                .HasColumnName("partnerName");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Preference)
                .HasMaxLength(200)
                .HasColumnName("preference");
            entity.Property(e => e.RegistrationTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("registrationTime");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .HasColumnName("sex");
            entity.Property(e => e.VerifyByEmail)
                .HasMaxLength(10)
                .HasDefaultValue("未驗證")
                .HasColumnName("verifyByEmail");
            entity.Property(e => e.WeddingStatus)
                .HasMaxLength(20)
                .HasColumnName("weddingStatus");
        });

        modelBuilder.Entity<MemberBudgetItem>(entity =>
        {
            entity.HasKey(e => e.BudgetItemId).HasName("PK_memberNewBudgetItems");

            entity.ToTable("memberBudgetItems");

            entity.Property(e => e.BudgetItemId).HasColumnName("budgetItemID");
            entity.Property(e => e.BudgetItemAmount).HasColumnName("budgetItemAmount");
            entity.Property(e => e.BudgetItemDetail)
                .HasMaxLength(200)
                .HasColumnName("budgetItemDetail");
            entity.Property(e => e.BudgetItemPrice).HasColumnName("budgetItemPrice");
            entity.Property(e => e.BudgetItemSort)
                .HasMaxLength(100)
                .HasColumnName("budgetItemSort");
            entity.Property(e => e.BudgetItemSubtotal).HasColumnName("budgetItemSubtotal");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
        });

        modelBuilder.Entity<MemberMaterial>(entity =>
        {
            entity.HasKey(e => e.MemberMaterialId).HasName("PK_會員圖檔素材");

            entity.ToTable("memberMaterial");

            entity.Property(e => e.MemberMaterialId).HasColumnName("memberMaterialID");
            entity.Property(e => e.EstimatedLength).HasColumnName("estimatedLength");
            entity.Property(e => e.EstimatedWidth).HasColumnName("estimatedWidth");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.MemberImgName)
                .HasMaxLength(200)
                .HasColumnName("memberImgName");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");
            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.ScheduleStageImg1)
                .HasMaxLength(200)
                .HasColumnName("scheduleStageImg1");
            entity.Property(e => e.ScheduleStageName)
                .HasMaxLength(50)
                .HasColumnName("scheduleStageName");
            entity.Property(e => e.ScheduleStageNotes)
                .HasMaxLength(500)
                .HasColumnName("scheduleStageNotes");
            entity.Property(e => e.ScheduleTime)
                .HasPrecision(0)
                .HasColumnName("scheduleTime");
        });

        modelBuilder.Entity<ScheduledStaff>(entity =>
        {
            entity.HasKey(e => e.PersonnelId);

            entity.ToTable("scheduledStaff");

            entity.Property(e => e.PersonnelId).HasColumnName("personnelID");
            entity.Property(e => e.AssistanceContent)
                .HasMaxLength(200)
                .HasColumnName("assistanceContent");
            entity.Property(e => e.PersonnelName)
                .HasMaxLength(100)
                .HasColumnName("personnelName");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");
        });

        modelBuilder.Entity<SharingWeddingPlan>(entity =>
        {
            entity.HasKey(e => e.SharedRecordId).HasName("PK_婚禮計畫共享發佈紀錄");

            entity.ToTable("sharingWeddingPlan");

            entity.Property(e => e.SharedRecordId).HasColumnName("sharedRecordID");
            entity.Property(e => e.CaseId).HasColumnName("caseID");
            entity.Property(e => e.SharedName)
                .HasMaxLength(100)
                .HasColumnName("sharedName");
            entity.Property(e => e.SharedStatus)
                .HasMaxLength(20)
                .HasColumnName("sharedStatus");
            entity.Property(e => e.SharedTime)
                .HasPrecision(0)
                .HasColumnName("sharedTime");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK_廠商");

            entity.ToTable("shop");

            entity.Property(e => e.ShopId).HasColumnName("shopID");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .HasColumnName("contactPerson");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .HasColumnName("contactPhone");
            entity.Property(e => e.Payment)
                .HasMaxLength(200)
                .HasColumnName("payment");
            entity.Property(e => e.ServiceArea)
                .HasMaxLength(300)
                .HasColumnName("serviceArea");
            entity.Property(e => e.ShopImg)
                .HasMaxLength(200)
                .HasColumnName("shopImg");
            entity.Property(e => e.ShopLogo)
                .HasMaxLength(200)
                .HasColumnName("shopLogo");
            entity.Property(e => e.ShopName)
                .HasMaxLength(100)
                .HasColumnName("shopName");
            entity.Property(e => e.ShopRating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("shopRating");
            entity.Property(e => e.ShopSort)
                .HasMaxLength(100)
                .HasColumnName("shopSort");
            entity.Property(e => e.ShopStatus).HasColumnName("shopStatus");
        });

        modelBuilder.Entity<ToDo>(entity =>
        {
            entity.HasKey(e => e.ToDoId).HasName("PK_ToDoList");

            entity.ToTable("ToDo");

            entity.Property(e => e.ToDoId).HasColumnName("toDoID");
            entity.Property(e => e.AdditionalData).HasColumnName("additionalData");
            entity.Property(e => e.CreateToDoTime)
                .HasPrecision(0)
                .HasColumnName("createToDoTime");
            entity.Property(e => e.ExpiringDate)
                .HasPrecision(0)
                .HasColumnName("expiringDate");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.PersonInCharge)
                .HasMaxLength(50)
                .HasColumnName("personInCharge");
            entity.Property(e => e.Precedence).HasColumnName("precedence");
            entity.Property(e => e.RemindTime)
                .HasPrecision(0)
                .HasColumnName("remindTime");
            entity.Property(e => e.RepetitiveTask).HasColumnName("repetitiveTask");
            entity.Property(e => e.TaskSort)
                .HasMaxLength(100)
                .HasColumnName("taskSort");
            entity.Property(e => e.TaskStatus)
                .HasMaxLength(20)
                .HasColumnName("taskStatus");
            entity.Property(e => e.ToDoDetail)
                .HasMaxLength(500)
                .HasColumnName("toDoDetail");
            entity.Property(e => e.ToDoName)
                .HasMaxLength(200)
                .HasColumnName("toDoName");
            entity.Property(e => e.UpdateToDoTime)
                .HasPrecision(0)
                .HasColumnName("updateToDoTime");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK_vanue");

            entity.ToTable("venue");

            entity.Property(e => e.VenueId).HasColumnName("venueID");
            entity.Property(e => e.AvailableTime)
                .HasPrecision(0)
                .HasColumnName("availableTime");
            entity.Property(e => e.GuestCapacity).HasColumnName("guestCapacity");
            entity.Property(e => e.InOurDoor)
                .HasMaxLength(10)
                .HasColumnName("inOurDoor");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.OrderTime)
                .HasPrecision(0)
                .HasColumnName("orderTime");
            entity.Property(e => e.ShopId).HasColumnName("shopID");
            entity.Property(e => e.TableCapacity).HasColumnName("tableCapacity");
            entity.Property(e => e.VenueFunction)
                .HasMaxLength(50)
                .HasColumnName("venueFunction");
            entity.Property(e => e.VenueImg).HasColumnName("venueImg");
            entity.Property(e => e.VenueImg2).HasColumnName("venueImg2");
            entity.Property(e => e.VenueInfo)
                .HasMaxLength(500)
                .HasColumnName("venueInfo");
            entity.Property(e => e.VenueName)
                .HasMaxLength(50)
                .HasColumnName("venueName");
            entity.Property(e => e.VenueRentalPrice).HasColumnName("venueRentalPrice");
            entity.Property(e => e.VenueStyle)
                .HasMaxLength(50)
                .HasColumnName("venueStyle");
        });

        modelBuilder.Entity<WeddingPlan>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK_婚禮計畫");

            entity.ToTable("weddingPlan");

            entity.Property(e => e.CaseId).HasColumnName("caseID");
            entity.Property(e => e.Introduction).HasMaxLength(500);
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.WeddingLocation)
                .HasMaxLength(50)
                .HasColumnName("weddingLocation");
            entity.Property(e => e.WeddingName)
                .HasMaxLength(100)
                .HasColumnName("weddingName");
            entity.Property(e => e.WeddingTime)
                .HasPrecision(0)
                .HasColumnName("weddingTime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
