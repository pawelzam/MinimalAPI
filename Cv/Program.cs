using Cv.DB.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.EnableSensitiveDataLogging();
    opt.UseSqlServer(connectionString: "Server=localhost,8002;Database=CV;User Id=sa;Password=myStong_Password123#;TrustServerCertificate=true");
});

var app = builder.Build();

app.MapPost("/candidates", async (Candidate candidate, AppDbContext db) =>
{
    db.Candidates.Add(candidate);
    await db.SaveChangesAsync();

    return Results.Created($"/candidates/{candidate.Id}", candidate);
});

app.MapGet("/candidates", async (AppDbContext db, CancellationToken cancellationToken) => await db.Candidates.Include(p=>p.Experiences).ToListAsync(cancellationToken));

app.Run();


public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Candidate>().HasData(ReadAsset<Candidate[]>("Cv.DB.Seed.Candidates.json"));
        modelBuilder.Entity<Experience>().HasData(ReadAsset<Experience[]>("Cv.DB.Seed.Experiences.json"));
    }

    private static T ReadAsset<T>(string path)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)!;
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(json)!;
    }
}
