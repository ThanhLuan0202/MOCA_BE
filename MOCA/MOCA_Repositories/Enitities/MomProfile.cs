using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class MomProfile
{
    public int MomId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? MaritalStatus { get; set; }

    public string? BloodType { get; set; }

    public string? MedicalHistory { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<UserPregnancy> UserPregnancies { get; set; } = new List<UserPregnancy>();
}
