﻿namespace ShoesAndBlouse.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<int, string> Products { get; set; }
}