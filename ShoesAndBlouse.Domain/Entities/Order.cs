﻿using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Entities;

public sealed class Order
{
    [Key]
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ModifiedAt { get; init; }
    public OrderStatus Status { get; set; } = 0;
    public User User { get; init; }
    public Address? ShippingAddress { get; init; }
    public Address BillingAddress { get; init; }
    public List<OrderDetail> OrderDetails { get; init; }
    public Money Total { get; init; }
}