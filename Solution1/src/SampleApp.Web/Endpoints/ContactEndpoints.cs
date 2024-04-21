using SampleApp.Database.Tables;

namespace SampleApp.Web.Endpoints;

public static class ContactEndpoints
{
  public static void MapContactEndpoints(this IEndpointRouteBuilder app)
  {
    var contacts = app.MapGroup("/Contacts");

    contacts.MapGet("", GetContacts)
      .WithName(nameof(GetContacts))
      .WithOpenApi();
    contacts.MapPost("sample-data", AddSampleContacts)
      .WithName(nameof(AddSampleContacts))
      .WithOpenApi();
  }

  private static IResult GetContacts(AppDbContext dbContext)
  {
    var result = dbContext.Contacts.Select(c => new ContactInfo { FullName = c.FullName, Email = c.Email }).ToArray();
    return Results.Ok(result);
  }

  private static IResult AddSampleContacts(AppDbContext dbContext)
  {
    try
    {
      if (!dbContext.Contacts.Any())
      {
        dbContext.Contacts.Add(new Contact { FullName = "Gordon Beeming", Email = "gordon@beeming.net" });
        dbContext.Contacts.Add(new Contact { FullName = "John Doe", Email = "john.doe@example.com" });
        dbContext.Contacts.Add(new Contact { FullName = "Jane Doe", Email = "jane.doe@example.com" });
        dbContext.SaveChanges();
      }
      return Results.Ok("OK");
    }
    catch
    {
      return Results.Problem("An error occurred while adding sample data to the database.");
    }
  }
}

record ContactInfo()
{
  public string FullName { get; init; } = default!;
  public string Email { get; init; } = default!;
}
