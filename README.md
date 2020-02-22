### Architecture (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

#### Required Nuget Packages
1. Microsoft.EntityFrameworkCore 3.1.0 --> install to Persistence folder
2. Microsoft.EntityFrameworkCore.SQLite 3.1.0 --> install to Persistence folder
3. Microsoft.EntityFrameworkCore.Design 3.1.0 --> install to API folder
4. MediatR.Extensions.Microsoft.DependencyInjection 7.x.x --> install to Application folder
5. FluentValidation.AspNetCore --> install latest version to Application folder
6. Microsoft.AspNetCore.Identity.EntityFrameworkCore --> install to Domain folder
7. Microsoft.AspNetCore.Identity.UI --> install to API folder
8. System.IdentityModel.Tokens.Jwt --> install latest version to Infrastructure folder
9. Microsoft.AspNetCore.Authentication.JwtBearer --> install same version of project to API folder
10. AutoMapper.Extensions.Microsoft.DependencyInjection --> install latest version (v6.1.1) to Application folder
11. Microsoft.EntityFrameworkCore.Proxies --> (use for lazy loading) install same project version to Persistence folder
12. CloudinaryDotNet --> install latest verion to Infrastructure folder