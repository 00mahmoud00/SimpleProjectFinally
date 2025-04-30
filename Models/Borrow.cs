using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SimpleLibrary.Models;

public class Borrow
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }


    public int BookId { get; set; }
    public Book Book { get; set; }

    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
