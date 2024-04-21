using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleApp.Database.Tables;

public partial class Contact
{
  public Guid Id { get; set; }

  public string FullName { get; set; } = default!;
  public string Email { get; set; } = default!;

  public DateTime CreatedDate { get; set; }
  public DateTime ModifiedDate { get; set; }
}
