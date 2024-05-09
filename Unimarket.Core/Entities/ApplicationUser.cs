using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? DOB { get; set; }
        public String FirstName { get; set; }
        public String LastName {  get; set; }
        public String PhoneNumber {  get; set; }
        public String CCCDNumber {  get; set; }
        public bool Gender{  get; set; }
        public int Status {  get; set; }
        public String? RefreshToken { get; set; }
        public DateTime? DateExpireRefreshToken { get; set; }
        public virtual UserAddress? UserAddress { get; set; }
        public virtual CartItem? CartItem { get; set; }
        public virtual List<UserNotification>? UserNotifications { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Post>? Posts { get; set; }
        public virtual List<UserPackage>? UserPackages { get; set; }
        public virtual List<ItemReview>? ItemReviews { get; set; }
    }
}
