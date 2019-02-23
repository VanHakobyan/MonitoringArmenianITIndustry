create table Company (
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) not NULL,
	About [nvarchar](max) null,
	Industry [nvarchar](150) NULL,
	NumberOfEmployees int not NULL,
	DateOfFoundation date NULL,
	Website varchar(350) null,
	Address nvarchar(350) null,
	Facebook varchar(350) null,
	Linkedin varchar(350) null,
	GooglePlus varchar(350) null,
	Twitter varchar(350) null,
	Phone varchar(150) null
	)

Go


create table Job(
Id int primary key identity not null,
Title nvarchar(350) not null,
Email nvarchar(350) null,
Deadline datetime null,
EmploymentTerm nvarchar(350) not null,
TimeType nvarchar(350) null,
Category nvarchar(350) not null,
Location nvarchar(350) not null,
Description nvarchar(350) not null,
Responsibilities nvarchar(350) not null,
RequiredQualifications nvarchar(350) null,
ProfessionalSkills nvarchar(350) null,
AdditionalInformation nvarchar(350) null,
SoftSkills nvarchar(350) null,
CompanyId Int not null
)

Go

ALTER TABLE [dbo].Job  WITH CHECK ADD  CONSTRAINT [FK_dbo.Jobs_dbo.Companies_CompanyId] FOREIGN KEY(CompanyId)
REFERENCES [dbo].Company ([Id])
ON DELETE CASCADE
GO