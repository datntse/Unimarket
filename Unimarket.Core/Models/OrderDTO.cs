﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;

namespace Unimarket.Core.Models
{
    public class OrderDTO
    {
        [Key]
        public Guid Id { get; set; }
        public String PaymentType { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderVM
    {
        public Guid Id { get; set; }
        public String PaymentType { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public string Username { get; set; }
        public string Address {  get; set; }
        public string PhoneNumber {  get; set; }
        public String FirstName { get; set; }
        public string Note {  get; set; }
        public String LastName { get; set; }
        public List<OrderdetailVM> OrderdetailVM { get; set; }
    }

    public class OrderdetailVM
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public ItemsVM ItemsVMs { get; set; }
    }

    public class ItemsVM
    {
        public String Name { get; set; }
        public float Price { get; set; }
        public String ImageUrl { get; set; }
    }

    public class CheckOutDTO
    {
        public string UserId {  get; set; }
        public string Address { get; set; }
        public string Note {  get; set; }
        public string PaymentType { get; set; }
    }

    public class UpdateOrderUM
    {
        public Guid OrderId { get; set; }
        public int Status { get; set; }
    }
}