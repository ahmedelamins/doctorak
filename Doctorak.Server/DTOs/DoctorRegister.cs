﻿namespace Doctorak.Server.DTOs;

public class DoctorRegister : UserRegister
{
    [Required]
    public string Gender { get; set; } = string.Empty;
    [Required]
    public string Specialties { get; set; } = string.Empty;
    [Required]
    public string PracticeName { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public List<string> Qualifications { get; set; } = new List<string>();
    [Required]
    public string About { get; set; } = string.Empty;
}
