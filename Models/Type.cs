﻿using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace TDFSv4.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}