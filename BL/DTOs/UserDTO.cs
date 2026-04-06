using BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AppResources;

namespace BL.DTOs
{
    public class UserDTO:BaseDTOs
    {

    [Required(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "EmailRequired")]
    [EmailAddress(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "InvalidEmail")]
    public string Email { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "PasswordRequired")]
    [MinLength(6,
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "PasswordMinLength")]
    public string Password { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "FirstNameRequired")]
    public string? FirstName { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "LastNameRequired")]
    public string? LastName { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "PhoneRequired")]
    [Phone(
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "InvalidPhone")]
    public string? Phone { get; set; }

    public string? Role { get; set; }

    [Compare("Password",
        ErrorMessageResourceType = typeof(Shipping),
        ErrorMessageResourceName = "PasswordMismatch")]
    public string? ConfirmPassword { get; set; }

    public string? ReturnUrl { get; set; }
}


    }

