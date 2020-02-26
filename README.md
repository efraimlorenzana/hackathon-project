### Application Architecture (https://docs.google.com/presentation/d/1bGiCuwr5Yo2ak9Lbr0hDyxXelZBwrlI2ci_Q0bS0-D4/edit?usp=sharing)

#### React App : Required NPM
npx create-react-app app-name --use-npm --typescript
npm install semantic-ui-react
npm install mobx mobx-react-lite
npm install react-router-dom or npm install @types/react-router-dom
npm install react-toastify
npm install react-final-form final-form
npm install react-widgets react-widgets-date-fns
npm install revalidate

#### Required Nuget Packages
Microsoft.EntityFrameworkCore 3.1.0 --> install to Persistence folder
Microsoft.EntityFrameworkCore.SQLite 3.1.0 --> install to Persistence folder
Microsoft.EntityFrameworkCore.Design 3.1.0 --> install to API folder
MediatR.Extensions.Microsoft.DependencyInjection 7.x.x --> install to Application folder
FluentValidation.AspNetCore --> install latest version to Application folder
Microsoft.AspNetCore.Identity.EntityFrameworkCore --> install to Domain folder
Microsoft.AspNetCore.Identity.UI --> install to API folder
System.IdentityModel.Tokens.Jwt --> install latest version to Infrastructure folder
Microsoft.AspNetCore.Authentication.JwtBearer --> install same version of project to API folder
AutoMapper.Extensions.Microsoft.DependencyInjection --> install latest version (v6.1.1) to Application folder
Microsoft.EntityFrameworkCore.Proxies --> (use for lazy loading) install same project version to Persistence folder
CloudinaryDotNet --> install latest verion to Infrastructure folder
Microsoft.EntityFrameworkCore.SqlServer --> install same version