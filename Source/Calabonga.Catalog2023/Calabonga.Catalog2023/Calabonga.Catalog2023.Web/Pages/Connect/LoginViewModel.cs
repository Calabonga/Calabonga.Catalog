﻿using System.ComponentModel.DataAnnotations;

namespace Calabonga.Catalog2023.Web.Pages.Connect;

public class LoginViewModel
{

    [Required]
    [EmailAddress]
    [Display(Name = "Имя для входа")]
    public string UserName { get; set; } = null!;

    [Required]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Required]
    public string ReturnUrl { get; set; } = null!;
}