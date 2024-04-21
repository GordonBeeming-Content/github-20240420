
Param(
  [switch]$skipDeploy = $false,
  [switch]$sln1 = $false
)

Write-Host "🚢 Starting Docker Compose"
docker compose up -d

function DeployFromPath {
  param (
    [string]$path
  )
  Write-Host "👀 Deploying from $($path)"
  Set-Location $path

  $windows = $env:OS -eq "Windows_NT"
  if (-not $windows) {
    Write-Host "⚙️ Building SampleApp Database AppDbContext Bundle"
    dotnet restore --runtime 'linux-x64'
    Set-Location ./src/
    dotnet ef migrations bundle --project 'SampleApp.Database' --startup-project 'SampleApp.Web' --force --context AppDbContext --output AppDbContextEfBundle

    Write-Host "🚀 Deploying SampleApp Database AppDbContext Bundle"
    . ./AppDbContextEfBundle --connection "Server=.,1900;Database=SampleApp;User Id=sa;Password=Password!@2;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  }
  else {
    Write-Host "⚙️ Building SampleApp Database AppDbContext Bundle"
    dotnet restore --runtime 'win-x64'
    Set-Location .\src\
    dotnet ef migrations bundle --project 'SampleApp.Database' --startup-project 'SampleApp.Web' --force --context AppDbContext --output AppDbContextEfBundle.exe

    Write-Host "🚀 Deploying SampleApp Database AppDbContext Bundle"
    . .\AppDbContextEfBundle.exe --connection "Server=.,1900;Database=SampleApp;User Id=sa;Password=Password!@2;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  }
}

if (-not $skipDeploy)
{
  Write-Host "⚙️ Restore dotnet tools"
  dotnet tool restore

  Set-Location $($Script:MyInvocation.MyCommand.Path | Split-Path)

  if ($sln1)
  {
    DeployFromPath -Path "Solution1"
  }
  else
  {
  Write-Host "⚠️  No SLN selected to deploy" -ForegroundColor DarkYellow
  }

  Set-Location $($Script:MyInvocation.MyCommand.Path | Split-Path)
}
