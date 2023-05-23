using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FreshMove.Constants
{
  public enum Gender
  {
    [Display(Name ="Female")]
     Female,
    [Display(Name = "Male")]
     Male,
    [Display(Name = "Other")]
     Other
  }
  public enum Race
  {
    [Display(Name = "African")]
     African,
    [Display(Name = "White")]
     White,
    [Display(Name = "Colored")]
     Colored,
    [Display(Name = "Indian")]
     Indian,
    [Display(Name = "Other")]
     Other
  }
  public enum UserRole
  {
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Stock Manager")]
        StockManager,   
        [Display(Name = "Stock Taker")]
        StockTaker,
        [Display(Name = "Sales Manager")]
        SalesManager,
        [Display(Name="Customer")]
        Customer
     
  }
  public enum PurchaseOrderStatus
  {
         New,
         Processed,
         Delivered,
         Recieved
  }
}
