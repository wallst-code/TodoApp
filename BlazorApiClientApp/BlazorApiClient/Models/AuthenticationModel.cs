using System.ComponentModel.DataAnnotations;

namespace BlazorApiClient.Models;

public class AuthenticationModel
{
    //[Required]
    public string UserName { get; set; }

    //[Required]
    public string Password { get; set; }
}
