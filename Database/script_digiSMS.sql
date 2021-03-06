
/****** Object:  Database [digiSMS]    Script Date: 08/26/2017 18:10:33 ******/
USE [digiSMS]
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentLedgerBalance]    Script Date: 08/26/2017 18:10:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_StudentLedgerBalance] 
	-- Add the parameters for the stored procedure here
	@ClassId int,
	@DivisionId int,
	@ReligionId int,
	@CastId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Declare @whereString nvarchar(MAX);
Declare @whereString1 nvarchar(MAX);
set @whereString1 ='';
SET @whereString = 'SELECT  StudentDetails.StudentId,   StudentDetails.SrNo, StudentDetails.RegisterId, StudentDetails.ClassDivisionId, StudentDetails.Title, StudentDetails.FirstName, StudentDetails.MiddleName, 
                      StudentDetails.LastName, StudentDetails.MotherName, StudentDetails.Gender, StudentDetails.Nationality, StudentDetails.ReligionCastId, StudentDetails.DateOfBirth, 
                      StudentDetails.PlaceOfBirth, StudentDetails.AdharcardNo, StudentDetails.DateOfAdmission, StudentDetails.LastSchoolAttended, StudentDetails.Progress, 
                      StudentDetails.Conduct, StudentDetails.DateOfLeavingSchool, StudentDetails.ClassInWhichStudingAndSinceWhen, StudentDetails.ReasonForLeavingSchool, 
                      StudentDetails.RemarkOnTC, StudentDetails.TCPrinted, ClassDivisionDetails.ClassId, ClassDivisionDetails.DivisionId, ClassDetails.Class, DivisionDetails.Division, 
                      ReligionCastDetails.ReligionId, ReligionCastDetails.CastId, ReligionDetails.Religion, CastDetails.Cast, 
                      ReligionCastDetails.ReserveCategory, 
						(select isnull(balance,0) from StudentLedger where StudentLedgerId = (select max(StudentLedgerId) from StudentLedger where StudentLedger.StudentId = StudentDetails.StudentId)) as balance,
						StudentParentDetails.MobileNumber
FROM         StudentDetails INNER JOIN
                      ClassDivisionDetails ON StudentDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId INNER JOIN
                      ReligionCastDetails ON StudentDetails.ReligionCastId = ReligionCastDetails.ReligionCastId INNER JOIN
                      ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN
                      DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId INNER JOIN
                      ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId INNER JOIN
                      CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId INNER JOIN
                      StudentParentDetails ON StudentDetails.StudentId = StudentParentDetails.StudentId
WHERE StudentDetails.Status = 1 AND ISNULL(StudentParentDetails.MobileNumber ,0) >0';


IF(@ClassId >0 AND @DivisionId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' AND ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE (ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' AND ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) +') ) ';
END 
ELSE
BEGIN
	IF(@ClassId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' ) ';
	END 
	IF(@DivisionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) + ' ) ';
	END  
END
IF(@ReligionId >0 AND @CastId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE (ReligionCastDetails.ReligionId =  '+ convert (nvarchar(10), @ReligionId) + ' AND ReligionCastDetails.CastId = '+ convert (nvarchar(10), @CastId) +') ) ';
END 
ELSE
BEGIN
	IF(@ReligionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.ReligionId = '+ convert (nvarchar(10), @ReligionId) + ' ) ';
	END 
	IF(@CastId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.CastId ='+ convert (nvarchar(10), @CastId) + ' ) ';
	END  
END
set @whereString = @whereString + @whereString1;
IF(right(@whereString,5) = 'WHERE')
BEGIN
select @whereString =  LEFT(@whereString, LEN(@whereString)-5);
END

EXECUTE(@whereString);
select @whereString;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentLedger]    Script Date: 08/26/2017 18:10:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_StudentLedger] 
	-- Add the parameters for the stored procedure here
	@SchoolId int,
	@RegisterId int,
	@ClassId int,
	@DivisionId int,
	@ReligionId int,
	@CastId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Declare @whereString nvarchar(MAX);
Declare @whereString1 nvarchar(MAX);
set @whereString1 ='';
SET @whereString = 'SELECT     StudentDetails.SrNo, StudentDetails.RegisterId, StudentDetails.ClassDivisionId, StudentDetails.Title, StudentDetails.FirstName, StudentDetails.MiddleName, 
                      StudentDetails.LastName, StudentDetails.MotherName, StudentDetails.Gender, StudentDetails.Nationality, StudentDetails.ReligionCastId, StudentDetails.DateOfBirth, 
                      StudentDetails.PlaceOfBirth, StudentDetails.AdharcardNo, StudentDetails.DateOfAdmission, StudentDetails.LastSchoolAttended, StudentDetails.Progress, 
                      StudentDetails.Conduct, StudentDetails.DateOfLeavingSchool, StudentDetails.ClassInWhichStudingAndSinceWhen, StudentDetails.ReasonForLeavingSchool, 
                      StudentDetails.RemarkOnTC, StudentDetails.TCPrinted, ClassDivisionDetails.ClassId, ClassDivisionDetails.DivisionId, ClassDetails.Class, DivisionDetails.Division, 
                      ReligionCastDetails.ReligionId, ReligionCastDetails.CastId, ReligionDetails.Religion, CastDetails.Cast, 
                      ReligionCastDetails.ReserveCategory, StudentLedger.StudentLedgerId, StudentLedger.StudentId, StudentLedger.TransactionDate, 
                      StudentLedger.FeeHeadId, FeeHeadDetails.FeeHead, StudentLedger.Cr, StudentLedger.Dr, StudentLedger.HeadBalance, StudentLedger.Balance, 
                      StudentLedger.ReceiptNo, StudentLedger.BankName, StudentLedger.ChequeNumber, StudentLedger.Status, StudentLedger.Remark, StudentLedger.CreatedBy, 
                      StudentLedger.CreatedDate, StudentLedger.ModifiedBy, StudentLedger.ModifiedDate, StudentLedger.StudentTransactionId
FROM         StudentDetails INNER JOIN
                      ClassDivisionDetails ON StudentDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId INNER JOIN
                      ReligionCastDetails ON StudentDetails.ReligionCastId = ReligionCastDetails.ReligionCastId INNER JOIN
                      ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN
                      DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId INNER JOIN
                      ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId INNER JOIN
                      CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId INNER JOIN
                      StudentLedger ON StudentDetails.StudentId = StudentLedger.StudentId INNER JOIN
                      FeeHeadDetails ON StudentLedger.FeeHeadId = FeeHeadDetails.FeeHeadId WHERE';

IF(@SchoolId >0)
BEGIN 
	SET @whereString1 = @whereString1 + ' StudentDetails.SchoolId = ' + convert (nvarchar(10), @SchoolId);
END

IF(@RegisterId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.RegisterId = ' + convert (nvarchar(10), @RegisterId);
END 
IF(@ClassId >0 AND @DivisionId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE (ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' AND ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) +') ) ';
END 
ELSE
BEGIN
	IF(@ClassId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' ) ';
	END 
	IF(@DivisionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) + ' ) ';
	END  
END
IF(@ReligionId >0 AND @CastId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE (ReligionCastDetails.ReligionId =  '+ convert (nvarchar(10), @ReligionId) + ' AND ReligionCastDetails.CastId = '+ convert (nvarchar(10), @CastId) +') ) ';
END 
ELSE
BEGIN
	IF(@ReligionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.ReligionId = '+ convert (nvarchar(10), @ReligionId) + ' ) ';
	END 
	IF(@CastId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.CastId ='+ convert (nvarchar(10), @CastId) + ' ) ';
	END  
END
set @whereString = @whereString + @whereString1;
IF(right(@whereString,5) = 'WHERE')
BEGIN
select @whereString =  LEFT(@whereString, LEN(@whereString)-5);
END

EXECUTE(@whereString);
select @whereString;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentsAttendence]    Script Date: 08/26/2017 18:10:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[sp_StudentsAttendence] 
	-- Add the parameters for the stored procedure here
	@RegisterId int,
	@ClassId int,
	@DivisionId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Declare @whereString nvarchar(MAX);
Declare @whereString1 nvarchar(MAX);
set @whereString1 ='';
--IF (LEN(@whereString) >0)
--BEGIN
--	SET @whereString = ' WHERE ' +  @whereString;
--END


SET @whereString = ' SELECT     StudentDetails.SrNo, StudentDetails.StudentId, StudentDetails.RegisterId, StudentDetails.ClassDivisionId, StudentDetails.Title, 
                      StudentDetails.FirstName, StudentDetails.MiddleName, StudentDetails.LastName, StudentDetails.MotherName, StudentDetails.Gender, StudentDetails.Nationality, 
                      StudentDetails.ReligionCastId, StudentDetails.DateOfBirth, StudentDetails.PlaceOfBirth, StudentDetails.AdharcardNo, StudentDetails.DateOfAdmission, 
                      StudentDetails.LastSchoolAttended, StudentDetails.Progress, StudentDetails.Conduct, StudentDetails.DateOfLeavingSchool, 
                      StudentDetails.ClassInWhichStudingAndSinceWhen, StudentDetails.ReasonForLeavingSchool, StudentDetails.RemarkOnTC, StudentDetails.TCPrinted, 
                      StudentDetails.Status, StudentDetails.Remark, ClassDivisionDetails.ClassId, ClassDivisionDetails.DivisionId, ClassDetails.Class, DivisionDetails.Division, 
                                          
FROM         StudentDetails INNER JOIN
                      ClassDivisionDetails ON StudentDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId INNER JOIN
                      
                      ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN
                      DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId  WHERE' ;


IF(@RegisterId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.RegisterId = ' + convert (nvarchar(10), @RegisterId);
END 
IF(@ClassId >0 AND @DivisionId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE (ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' AND ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) +') ) ';
END 
ELSE
BEGIN
	IF(@ClassId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' ) ';
	END 
	IF(@DivisionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) + ' ) ';
	END  
END

set @whereString = @whereString + @whereString1;
IF(right(@whereString,5) = 'WHERE')
BEGIN
select @whereString =  LEFT(@whereString, LEN(@whereString)-5);
END

EXECUTE(@whereString);
--EXECUTE sp_executesql N'PRINT ' + @whereString1;
select @whereString;
END
GO
/****** Object:  Table [dbo].[SchoolDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolDetails](
	[SchoolId] [int] IDENTITY(1,1) NOT NULL,
	[ManagementName] [nvarchar](100) NOT NULL,
	[SchoolName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Taluka] [nvarchar](100) NULL,
	[District] [nvarchar](100) NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[EmailId] [nvarchar](100) NULL,
	[SchoolReconginationNo] [nvarchar](100) NULL,
	[Medium] [nvarchar](50) NULL,
	[UDiscNo] [nvarchar](50) NULL,
	[Board] [nvarchar](50) NULL,
	[AffilationNo] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
	[LogoPath] [nchar](30) NULL,
 CONSTRAINT [PK_SchoolDetails] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleDetails](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_RoleDetails] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReligionDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReligionDetails](
	[ReligionId] [int] IDENTITY(1,1) NOT NULL,
	[Religion] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReligionDetails] PRIMARY KEY CLUSTERED 
(
	[ReligionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReligionCastDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReligionCastDetails](
	[ReligionCastId] [int] IDENTITY(1,1) NOT NULL,
	[ReligionId] [int] NOT NULL,
	[CastId] [int] NOT NULL,
	[ReserveCategory] [nvarchar](15) NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReligionCastDetails] PRIMARY KEY CLUSTERED 
(
	[ReligionCastId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeeHeadDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeeHeadDetails](
	[FeeHeadId] [int] IDENTITY(1,1) NOT NULL,
	[FeeHead] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_FeeHeadDetails] PRIMARY KEY CLUSTERED 
(
	[FeeHeadId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeeClassDivisionDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeeClassDivisionDetails](
	[FeeClassDivisionId] [int] IDENTITY(1,1) NOT NULL,
	[FeeHeadId] [int] NOT NULL,
	[ClassDivisionId] [int] NOT NULL,
	[PeriodInMonthly] [numeric](18, 2) NOT NULL,
	[AmountInMonthly] [numeric](18, 2) NOT NULL,
	[AmountInYearly] [numeric](18, 2) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_FeeClassDivisionDetails] PRIMARY KEY CLUSTERED 
(
	[FeeClassDivisionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeLeaveTransaction]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLeaveTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[TransactionType] [nchar](10) NOT NULL,
	[LeaveType] [int] NOT NULL,
	[LeaveFromDate] [datetime] NOT NULL,
	[LeaveToDate] [datetime] NOT NULL,
	[LeavesInDays] [decimal](10, 2) NOT NULL,
	[BalanceLeaves] [decimal](10, 2) NOT NULL,
	[Remark] [nchar](100) NULL,
	[TransactionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeLeaveTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [int] NULL,
	[EmployeeCode] [nchar](20) NULL,
	[Password] [nvarchar](15) NULL,
	[FirstName] [nchar](30) NULL,
	[MiddleName] [nchar](30) NULL,
	[LastName] [nchar](30) NULL,
	[Category] [nchar](30) NULL,
	[Address] [nchar](30) NULL,
	[ContactNo] [bigint] NULL,
	[DOB] [date] NULL,
	[JoiningDate] [date] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DivisionDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DivisionDetails](
	[DivisionId] [int] IDENTITY(1,1) NOT NULL,
	[Division] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_DivisonDetails] PRIMARY KEY CLUSTERED 
(
	[DivisionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistrictDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistrictDetails](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NOT NULL,
	[District] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_DistrictDetails] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryDetails](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_CountryDetails] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassFeeDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassFeeDetails](
	[ClassFeeId] [int] IDENTITY(1,1) NOT NULL,
	[FeeHeadId] [int] NOT NULL,
	[FeePeriodInMonth] [int] NOT NULL,
	[FeeInMonthly] [decimal](18, 0) NOT NULL,
	[FeeInYearly] [decimal](18, 0) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ClassFeeDetails] PRIMARY KEY CLUSTERED 
(
	[ClassFeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassDivisionDetails]    Script Date: 08/26/2017 18:10:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassDivisionDetails](
	[ClassDivisionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[DivisionId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ClassDivisionDetails] PRIMARY KEY CLUSTERED 
(
	[ClassDivisionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassDetails](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_ClassDetails] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedTableType [dbo].[ClassChangeDataType]    Script Date: 08/26/2017 18:10:48 ******/
CREATE TYPE [dbo].[ClassChangeDataType] AS TABLE(
	[StudentId] [int] NOT NULL,
	[Remark] [nvarchar](50) NULL
)
GO
/****** Object:  Table [dbo].[CityDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityDetails](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictId] [int] NOT NULL,
	[City] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_CityDetails] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CastDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CastDetails](
	[CastId] [int] IDENTITY(1,1) NOT NULL,
	[Cast] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_CastDetails] PRIMARY KEY CLUSTERED 
(
	[CastId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetDetails](
	[SrNo] [int] IDENTITY(1,1) NOT NULL,
	[SchoolId] [int] NOT NULL,
	[AssetName] [nvarchar](50) NOT NULL,
	[TypeofAsset] [nchar](10) NULL,
	[Quantity] [int] NULL,
	[PricePerItem] [decimal](10, 2) NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[Condition] [nchar](10) NULL,
	[AssesmentYear] [date] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nchar](50) NULL,
 CONSTRAINT [PK_AssetDetails] PRIMARY KEY CLUSTERED 
(
	[SrNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[SrNo] [int] IDENTITY(1,1) NOT NULL,
	[SchoolId] [int] NOT NULL,
	[NarrationDetails] [nvarchar](50) NOT NULL,
	[TransactionType] [nchar](10) NULL,
	[PaymentMode] [nchar](10) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Remark] [nchar](50) NULL,
	[CustomerName] [nchar](100) NULL,
	[BankName] [nchar](50) NULL,
	[ChqDDNumber] [nchar](30) NULL,
	[ContactNo] [nchar](15) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[SrNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AcademicYearDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicYearDetails](
	[AcademicYearId] [int] IDENTITY(1,1) NOT NULL,
	[AcademicYear] [nchar](10) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_AcademicYearDetails] PRIMARY KEY CLUSTERED 
(
	[AcademicYearId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_SearchStudents]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SearchStudents] 
	-- Add the parameters for the stored procedure here
	@SchoolId int,
	@RegisterId int,
	@ClassId int,
	@DivisionId int,
	@ReligionId int,
	@CastId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Declare @whereString nvarchar(MAX);
Declare @whereString1 nvarchar(MAX);
set @whereString1 ='';
--IF (LEN(@whereString) >0)
--BEGIN
--	SET @whereString = ' WHERE ' +  @whereString;
--END


SET @whereString = ' SELECT     StudentDetails.SrNo, StudentDetails.StudentId, StudentDetails.RegisterId, StudentDetails.ClassDivisionId, StudentDetails.Title, 
                      StudentDetails.FirstName, StudentDetails.MiddleName, StudentDetails.LastName, StudentDetails.MotherName, StudentDetails.Gender, StudentDetails.Nationality, 
                      StudentDetails.ReligionCastId, StudentDetails.DateOfBirth, StudentDetails.PlaceOfBirth, StudentDetails.AdharcardNo, StudentDetails.DateOfAdmission, 
                      StudentDetails.LastSchoolAttended, StudentDetails.Progress, StudentDetails.Conduct, StudentDetails.DateOfLeavingSchool, 
                      StudentDetails.ClassInWhichStudingAndSinceWhen, StudentDetails.ReasonForLeavingSchool, StudentDetails.RemarkOnTC, StudentDetails.TCPrinted, 
                      StudentDetails.Status, StudentDetails.Remark, ClassDivisionDetails.ClassId, ClassDivisionDetails.DivisionId, ClassDetails.Class, DivisionDetails.Division, 
                      ReligionCastDetails.ReligionId, ReligionCastDetails.CastId, ReligionDetails.Religion, CastDetails.Cast, 
                      ReligionCastDetails.ReserveCategory
FROM         StudentDetails INNER JOIN
                      ClassDivisionDetails ON StudentDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId INNER JOIN
                      ReligionCastDetails ON StudentDetails.ReligionCastId = ReligionCastDetails.ReligionCastId INNER JOIN
                      ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN
                      DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId INNER JOIN
                      ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId INNER JOIN
                      CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId WHERE' ;

IF(@SchoolId >0)
BEGIN 
	SET @whereString1 = @whereString1 + ' StudentDetails.SchoolId = ' + convert (nvarchar(10), @SchoolId);
END


IF(@RegisterId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.RegisterId = ' + convert (nvarchar(10), @RegisterId);
END 
IF(@ClassId >0 AND @DivisionId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE (ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' AND ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) +') ) ';
END 
ELSE
BEGIN
	IF(@ClassId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.ClassId = '+ convert (nvarchar(10), @ClassId) + ' ) ';
	END 
	IF(@DivisionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' ClassDivisionDetails.ClassDivisionId in (SELECT  ClassDivisionDetails.ClassDivisionId FROM ClassDivisionDetails WHERE ClassDivisionDetails.DivisionId ='+ convert (nvarchar(10), @DivisionId) + ' ) ';
	END  
END
IF(@ReligionId >0 AND @CastId >0)
BEGIN 
	IF (LEN(@whereString1) >0)
	BEGIN
		SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
	END
	SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE (ReligionCastDetails.ReligionId =  '+ convert (nvarchar(10), @ReligionId) + ' AND ReligionCastDetails.CastId = '+ convert (nvarchar(10), @CastId) +') ) ';
END 
ELSE
BEGIN
	IF(@ReligionId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.ReligionId = '+ convert (nvarchar(10), @ReligionId) + ' ) ';
	END 
	IF(@CastId >0)
	BEGIN 
		IF (LEN(@whereString1) >0)
		BEGIN
			SET @whereString1 = @whereString1 + convert (nvarchar(10), ' AND ');
		END
		SET @whereString1 = @whereString1 + ' StudentDetails.ReligionCastId in (SELECT ReligionCastDetails.ReligionCastId FROM ReligionCastDetails WHERE ReligionCastDetails.CastId ='+ convert (nvarchar(10), @CastId) + ' ) ';
	END  
END
set @whereString = @whereString + @whereString1;
IF(right(@whereString,5) = 'WHERE')
BEGIN
select @whereString =  LEFT(@whereString, LEN(@whereString)-5);
END

EXECUTE(@whereString);
--EXECUTE sp_executesql N'PRINT ' + @whereString1;
select @whereString;
END
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[SrNo] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [nchar](30) NOT NULL,
	[Password] [nchar](30) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[SchoolId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[SrNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTransactionSub]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTransactionSub](
	[StudentTransactionSubId] [int] IDENTITY(1,1) NOT NULL,
	[StudentTransactionId] [int] NOT NULL,
	[FeeHead] [int] NOT NULL,
	[Cr] [decimal](18, 2) NOT NULL,
	[Dr] [decimal](18, 2) NOT NULL,
	[Balance] [decimal](18, 2) NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentTransactionSub] PRIMARY KEY CLUSTERED 
(
	[StudentTransactionSubId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTransaction]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTransaction](
	[StudentTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassDivisionId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[SchoolId] [int] NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
	[ReceiptNo] [int] NULL,
	[BankName] [nvarchar](50) NULL,
	[ChequeNumber] [nvarchar](20) NULL,
	[ReceiptTotal] [decimal](18, 2) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_StudentTransaction] PRIMARY KEY CLUSTERED 
(
	[StudentTransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentParentDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentParentDetails](
	[StudentId] [int] NOT NULL,
	[Title] [nchar](10) NULL,
	[FirstName] [nchar](100) NULL,
	[MiddleName] [nchar](100) NULL,
	[LastName] [nchar](100) NULL,
	[Gender] [nchar](1) NOT NULL,
	[Address] [nchar](300) NULL,
	[CountryId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[PinCode] [int] NULL,
	[MobileNumber] [bigint] NULL,
	[ContactNo] [nvarchar](20) NULL,
	[Occupation] [nchar](10) NULL,
	[CompanyName] [nchar](20) NULL,
	[CompanyAddress] [nchar](50) NULL,
	[CompanyContactNo] [nvarchar](20) NULL,
	[BankName] [nvarchar](50) NULL,
	[AccountNo] [nvarchar](20) NULL,
	[Branch] [nvarchar](20) NULL,
	[IFSCCode] [nvarchar](20) NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentLedger]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentLedger](
	[StudentLedgerId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[FeeHeadId] [int] NOT NULL,
	[Cr] [decimal](18, 0) NOT NULL,
	[Dr] [decimal](18, 0) NOT NULL,
	[HeadBalance] [decimal](18, 0) NOT NULL,
	[Balance] [decimal](18, 0) NOT NULL,
	[ReceiptNo] [int] NOT NULL,
	[BankName] [nvarchar](50) NULL,
	[ChequeNumber] [nvarchar](20) NULL,
	[Status] [bit] NOT NULL,
	[Remark] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[StudentTransactionId] [int] NULL,
 CONSTRAINT [PK_StudentLedger] PRIMARY KEY CLUSTERED 
(
	[StudentLedgerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentDetails](
	[SrNo] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[SchoolId] [int] NOT NULL,
	[RegisterId] [int] NOT NULL,
	[ClassDivisionId] [int] NOT NULL,
	[Title] [nvarchar](10) NULL,
	[FirstName] [nchar](100) NULL,
	[MiddleName] [nchar](100) NULL,
	[LastName] [nchar](100) NULL,
	[MotherName] [nchar](100) NULL,
	[Gender] [nchar](1) NOT NULL,
	[Nationality] [nchar](10) NOT NULL,
	[ReligionCastId] [int] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[PlaceOfBirth] [nchar](20) NOT NULL,
	[AdharcardNo] [nvarchar](16) NULL,
	[DateOfAdmission] [date] NOT NULL,
	[LastSchoolAttended] [nchar](300) NULL,
	[Progress] [nchar](30) NULL,
	[Conduct] [nchar](30) NULL,
	[DateOfLeavingSchool] [date] NULL,
	[ClassInWhichStudingAndSinceWhen] [nchar](300) NULL,
	[ReasonForLeavingSchool] [nchar](300) NULL,
	[RemarkOnTC] [nchar](300) NULL,
	[TCPrinted] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nchar](30) NULL,
	[TCNo] [int] NULL,
	[MotherTounge] [nchar](30) NULL,
	[LastSchoolClass] [nchar](30) NULL,
	[LastSchoolTCNo] [nchar](30) NULL,
	[PrevSchoolDateofLiving] [date] NULL,
	[UStudentId] [nchar](20) NULL,
 CONSTRAINT [PK_StudentDetails] PRIMARY KEY CLUSTERED 
(
	[SrNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentChangeClassDivision]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentChangeClassDivision](
	[SrNo] [int] IDENTITY(1,1) NOT NULL,
	[PreviousClassDivisionId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[CurrentClassDivisionId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Remark] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_StudentChangeClassDivision] PRIMARY KEY CLUSTERED 
(
	[SrNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAttendance]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAttendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassDivisionId] [int] NOT NULL,
	[DateOfAttendance] [datetime] NOT NULL,
	[StudentId] [int] NOT NULL,
	[AttendanceInDays] [decimal](18, 2) NOT NULL,
	[AbsentRemark] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_StudentAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAddressDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAddressDetails](
	[StudentId] [int] NOT NULL,
	[CurrentAddress] [nvarchar](100) NOT NULL,
	[CurrentCountryId] [int] NOT NULL,
	[CurrentStateId] [int] NOT NULL,
	[CurrentDistrictId] [int] NOT NULL,
	[CurrentCityId] [int] NOT NULL,
	[CurrentPinCode] [int] NULL,
	[PermentAddress] [nvarchar](100) NOT NULL,
	[PermentCountryId] [int] NOT NULL,
	[PermentStateId] [int] NOT NULL,
	[PermentDistrictId] [int] NOT NULL,
	[PermentCityId] [int] NOT NULL,
	[PermentPinCode] [int] NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateDetails](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[State] [nchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_StateDetails] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateStudentTCStatus]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateStudentTCStatus] 
	-- Add the parameters for the stored procedure here
	@StudentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @maxTCNo int;	
	select @maxTCNo = isnull(max(TCNo),0) from dbo.StudentDetails;
	UPDATE dbo.StudentDetails SET TCPrinted = 1, StudentDetails.Status=0, TCNo = @maxTCNo + 1 WHERE StudentId =@StudentId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentUpdate]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_StudentUpdate] 
	-- Add the parameters for the stored procedure here
@SrNo int,
@StudentId int,
@RegisterId int,
@ClassDivisionId int,
@Title nvarchar(10),
@FirstName nvarchar(100),
@MiddleName nvarchar(100),
@LastName nvarchar(100),
@MotherName nvarchar(100),
@Gender  nchar(1),
@Nationality  nchar(10),
@ReligionCastId int,
@DateOfBirth datetime,
@PlaceOfBirth  nchar(20),
@AdharcardNo nvarchar(16),
@DateOfAdmission datetime,
@MotherTounge nvarchar(30),
@UStudentId nvarchar(30),
@LastSchoolAttended  nchar(300),
@Progress  nchar(30),
@Conduct  nchar(30),
@DateOfLeavingSchool datetime,
@LastSchoolClass nvarchar(30),
@LastSchoolTCNo nvarchar(30),
@ClassInWhichStudingAndSinceWhen   nchar(300),
@ReasonForLeavingSchool  nchar(300),
@RemarkOnTC  nchar(300),
@Status bit,
@Remark   nchar(30),
@PrevSchoolDateofLiving datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [StudentDetails]
SET
	 [RegisterId] = @RegisterId
	,[ClassDivisionId] = @ClassDivisionId
	,[Title] = @Title
	,[FirstName] = @FirstName
	,[MiddleName] = @MiddleName
	,[LastName] = @LastName
	,[MotherName] = @MotherName
	,[Gender] = @Gender
	,[Nationality] = @Nationality
	,[ReligionCastId] = @ReligionCastId
	,[DateOfBirth] = @DateOfBirth
	,[PlaceOfBirth] = @PlaceOfBirth
	,[AdharcardNo] = @AdharcardNo
	,[DateOfAdmission] = @DateOfAdmission
	,[MotherTounge]=@MotherTounge
	,[UStudentId]=@UStudentId	
	,[LastSchoolAttended] = @LastSchoolAttended
	,[Progress] = @Progress
	,[Conduct] = @Conduct
	,[DateOfLeavingSchool] = @DateOfLeavingSchool
	,[LastSchoolClass]=@LastSchoolClass
	,[LastSchoolTCNo]=@LastSchoolTCNo 
	,[ClassInWhichStudingAndSinceWhen] = @ClassInWhichStudingAndSinceWhen
	,[ReasonForLeavingSchool] = @ReasonForLeavingSchool
	,[RemarkOnTC] = @RemarkOnTC
	,[Status] = @Status
	,[Remark] = @Remark
	,[PrevSchoolDateofLiving]=@PrevSchoolDateofLiving
WHERE [StudentId] = @StudentId and [SrNo]=@SrNo;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentTransactionSubInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentTransactionSubInsert] 
	-- Add the parameters for the stored procedure here
	@StudentTransactionId int,
	@FeeHead int, 
	@Cr decimal(18,2),
	@Dr decimal(18,2),
	@Balance decimal(18,2),
	@Remark nvarchar(50),
	@StudentId int,
	@TransactionDate date,
	@ReceiptNo int,
	@BankName nvarchar(50),
	@ChequeNumber nvarchar(20),	
	@UserId int,
	@OUT_StudentTransactionSubId int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[StudentTransactionSub]
           ([StudentTransactionId]
           ,[FeeHead]
           ,[Cr]
           ,[Dr]
           ,[Balance]
           ,[Remark])
     VALUES
           (@StudentTransactionId
           ,@FeeHead
           ,@Cr
           ,@Dr
           ,@Balance
           ,@Remark);
     SET @OUT_StudentTransactionSubId=SCOPE_IDENTITY();
     
DECLARE @MaxStudentLedgerId int, @HBalance  DECIMAL(18,2) , @MBalance DECIMAL(18,2) ;
Set @HBalance = 0;
Set @MBalance = 0;
select @MaxStudentLedgerId = ISNULL(MAX(StudentLedgerId),0) from dbo.StudentLedger where StudentId = @StudentId AND FeeHeadId = @FeeHead;
IF @MaxStudentLedgerId > 0 
BEGIN
	select @HBalance = ISNULL(HeadBalance,0)
	from dbo.StudentLedger 
	where StudentLedgerId = @MaxStudentLedgerId;
	
	Set @HBalance = @HBalance + @Dr - @Cr;
END 
ELSE
BEGIN 
	Set @HBalance = @Dr - @Cr;
END

select @MaxStudentLedgerId = ISNULL(MAX(StudentLedgerId),0) from dbo.StudentLedger where StudentId = @StudentId;
IF @MaxStudentLedgerId > 0 
BEGIN
	select @MBalance = ISNULL(Balance,0) 
	from dbo.StudentLedger 
	where StudentLedgerId = @MaxStudentLedgerId;
	
	Set @MBalance = @MBalance + @Dr - @Cr;
END 
ELSE
BEGIN 
	Set @MBalance = @Dr - @Cr;
END

     INSERT INTO [dbo].[StudentLedger]
           ([StudentId]
           ,[TransactionDate]
           ,[FeeHeadId]
           ,[Cr]
           ,[Dr]
           ,[HeadBalance]
           ,[Balance]
           ,[ReceiptNo]
           ,[BankName]
           ,[ChequeNumber]
           ,[Status]
           ,[Remark]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[StudentTransactionId])
     VALUES
           (@StudentId
           ,@TransactionDate
           ,@FeeHead
           ,@Cr
           ,@Dr
           ,@HBalance
           ,@MBalance
           ,@ReceiptNo
           ,@BankName
           ,@ChequeNumber
           ,1
           ,'Student Transaction'
           ,@UserId
           ,GETDATE()
           ,null
           ,null
           ,@StudentTransactionId);

     
    RETURN  @OUT_StudentTransactionSubId;      
           
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentTransactionInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentTransactionInsert] 
	-- Add the parameters for the stored procedure here
	@ClassDivisionId int,
	@StudentId int,
	@SchoolId int,	
	@TransactionDate date,
	@Status bit,
	@Remark varchar(50),
	@ReceiptNo int,
	@BankName nvarchar(50),
	@ChequeNumber	nvarchar(20),
	@ReceiptTotal Decimal(18,2),
	@UserId int,
	@OUT_StudentTransactionId int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [StudentTransaction]
	([ClassDivisionId],[StudentId],[SchoolId],[TransactionDate],[Status],[Remark],
	ReceiptNo, BankName,ChequeNumber,ReceiptTotal,[CreatedBy],[CreatedDate]) 
	VALUES(@ClassDivisionId, @StudentId, @SchoolId, @TransactionDate, @Status, @Remark,
	@ReceiptNo, @BankName, @ChequeNumber,@ReceiptTotal,@UserId, GETDATE());
	SET @OUT_StudentTransactionId=SCOPE_IDENTITY();
    RETURN  @OUT_StudentTransactionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReligionDetailsUpdate]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReligionDetailsUpdate]
	-- Add the parameters for the stored procedure here
@Religion nchar(20),
@Status bit,
@Remark nvarchar(50),
@ReligionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 UPDATE [ReligionDetails]
        SET [Religion] = @Religion,
          [Status] = @Status,
          [Remark] = @Remark
        WHERE [ReligionId]  = @ReligionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReligionDetailsInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReligionDetailsInsert]
	-- Add the parameters for the stored procedure here
@Religion nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT INTO [ReligionDetails] ([Religion],[Status],[Remark])
	  VALUES(@Religion ,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReligionCastDetailsUpdate]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReligionCastDetailsUpdate]
	-- Add the parameters for the stored procedure here
@ReligionId int,
@CastId int,
@ReserveCategory nvarchar(10),
@Status bit,
@Remark nvarchar(50),
@ReligionCastId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 UPDATE [ReligionCastDetails]
       SET [ReligionId] = @ReligionId,
          [CastId] = @CastId,
          [Status] = @Status,
          [Remark] = @Remark,
          [ReserveCategory] = @ReserveCategory
        WHERE ReligionCastId  =  @ReligionCastId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReligionCastDetailsInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ReligionCastDetailsInsert] 
	-- Add the parameters for the stored procedure here
@ReligionId int,
@CastId int,
@ReserveCategory nvarchar(10),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [ReligionCastDetails] ([ReligionId],[CastId],[ReserveCategory],[Status],[Remark])
	 VALUES(@ReligionId,@CastId,@ReserveCategory,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertAssetDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertAssetDetails]
	-- Add the parameters for the stored procedure here
	        @SchoolId int
	       ,@AssetName nvarchar(50)
           ,@TypeofAsset nchar(10)
           ,@Quantity int
		   ,@PricePerItem decimal(10,2)
           ,@Total decimal(18,2)
           ,@PurchaseDate date	
           ,@Condition nchar(10)
           ,@AssesmentYear  date		   
           ,@Status bit
           ,@Remark nchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[AssetDetails]
           ( [SchoolId]
           ,[AssetName]
           ,[TypeofAsset]
           ,[Quantity]
           ,[PricePerItem]		   
           ,[Total]
           ,[PurchaseDate]
           ,[Condition]
           ,[AssesmentYear]		   
           ,[Status]
           ,[Remark]
			) 
     VALUES
           (@SchoolId
           ,@AssetName
           ,@TypeofAsset
           ,@Quantity
           ,@PricePerItem
           ,@Total
           ,@PurchaseDate
           ,@Condition
           ,@AssesmentYear
           ,@Status
           ,@Remark
		);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentMobileNumber]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetStudentMobileNumber] 
	-- Add the parameters for the stored procedure here
	@StudentIds varchar(Max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select StudentParentDetails.MobileNumber from StudentParentDetails where StudentId in (@StudentIds);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentMaxTCNo]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetStudentMaxTCNo] 
	-- Add the parameters for the stored procedure here
	--@StudentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select isnull(max(TCNo),0) as TCNo from dbo.StudentDetails;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentByClassDivision]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetStudentByClassDivision]
	-- Add the parameters for the stored procedure here
	@ClassDivisionId int,
	@SchoolId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT StudentDetails.StudentId, StudentDetails.RegisterId, 
	RTRIM(LTRIM(isnull(StudentDetails.Title,'')))+ ' '+ RTRIM(LTRIM(isnull(StudentDetails.FirstName,'')))+ ' '+ RTRIM(LTRIM(isnull(StudentDetails.MiddleName,'')))+' '+ RTRIM(LTRIM(StudentDetails.LastName)) AS StudentName
	FROM dbo.StudentDetails
	WHERE StudentDetails.ClassDivisionId = @ClassDivisionId
	AND StudentDetails.SchoolId = @SchoolId
	AND StudentDetails.[Status] =1;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetClassDivisionIdByClassAndDivision]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetClassDivisionIdByClassAndDivision]
	-- Add the parameters for the stored procedure here
	@Class nvarchar(20),
	@Division nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT     ClassDivisionDetails.ClassDivisionId
FROM         ClassDivisionDetails INNER JOIN
                      ClassDetails ON ClassDivisionDetails.ClassId = ClassDetails.ClassId INNER JOIN
                      DivisionDetails ON ClassDivisionDetails.DivisionId = DivisionDetails.DivisionId
WHERE LOWER(ClassDetails.Class) = LOWER(@Class)
AND LOWER(DivisionDetails.Division) = LOWER(@Division)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllStudentTransactionSub]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllStudentTransactionSub] 
	-- Add the parameters for the stored procedure here
	@StudentTransactionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     StudentTransactionSub.StudentTransactionSubId, StudentTransactionSub.FeeHead, StudentTransactionSub.StudentTransactionId, StudentTransactionSub.Cr, 
                      StudentTransactionSub.Dr, StudentTransactionSub.Balance, StudentTransactionSub.Remark, FeeHeadDetails.FeeHead AS FeeHeadName
FROM         StudentTransactionSub INNER JOIN
                      FeeHeadDetails ON StudentTransactionSub.FeeHead = FeeHeadDetails.FeeHeadId
WHERE StudentTransactionSub.StudentTransactionId = @StudentTransactionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllStudentParentDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllStudentParentDetails] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT     StudentParentDetails.StudentId, StudentParentDetails.Title, StudentParentDetails.FirstName, StudentParentDetails.MiddleName, StudentParentDetails.LastName, 
                      StudentParentDetails.Gender, StudentParentDetails.Address, StudentParentDetails.CountryId, StudentParentDetails.StateId, StudentParentDetails.DistrictId,StudentParentDetails.CityId, 
                      StudentParentDetails.PinCode, StudentParentDetails.MobileNumber, StudentParentDetails.ContactNo, StudentParentDetails.Occupation, 
                      StudentParentDetails.CompanyName, StudentParentDetails.CompanyAddress, StudentParentDetails.CompanyContactNo, StudentParentDetails.BankName, 
                      StudentParentDetails.AccountNo, StudentParentDetails.Branch, StudentParentDetails.IFSCCode, StudentParentDetails.Status, StudentParentDetails.Remark,
LTRIM(RTRIM(LTRIM(RTRIM(StudentDetails.Title)) + ' ' + LTRIM(RTRIM(StudentDetails.FirstName)) + ' ' + LTRIM(RTRIM(StudentDetails.MiddleName)) + ' ' + LTRIM(RTRIM(StudentDetails.LastName)))) as StudentFullName
FROM         StudentDetails INNER JOIN
                      StudentParentDetails ON StudentDetails.StudentId = StudentParentDetails.StudentId
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllStudentAddressDetails]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllStudentAddressDetails]
	-- Add the parameters for the stored procedure here
	@StudentId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     StudentAddressDetails.StudentId, StudentAddressDetails.CurrentAddress, StudentAddressDetails.CurrentCountryId, StudentAddressDetails.CurrentStateId, 
                      StudentAddressDetails.CurrentDistrictId, StudentAddressDetails.CurrentCityId, StudentAddressDetails.CurrentPinCode, StudentAddressDetails.PermentAddress, StudentAddressDetails.PermentCountryId, 
                      StudentAddressDetails.PermentStateId, StudentAddressDetails.PermentDistrictId, StudentAddressDetails.PermentCityId, StudentAddressDetails.PermentPinCode, StudentAddressDetails.[Status], 
                      StudentAddressDetails.Remark,
LTRIM(RTRIM(LTRIM(RTRIM(StudentDetails.Title)) + ' ' + LTRIM(RTRIM(StudentDetails.FirstName)) + ' ' + LTRIM(RTRIM(StudentDetails.MiddleName)) + ' ' + LTRIM(RTRIM(StudentDetails.LastName)))) as StudentFullName
                      
FROM         StudentAddressDetails INNER JOIN
                      StudentDetails ON StudentAddressDetails.StudentId = StudentDetails.StudentId
WHERE StudentAddressDetails.StudentId = @StudentId
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_FeesHeadDetailsupdate]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_FeesHeadDetailsupdate]
	-- Add the parameters for the stored procedure here
@FeeHead nchar(20),
@Status bit,
@Remark nvarchar(50),
@FeeHeadId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [FeeHeadDetails] 
        SET [FeeHead] = @FeeHead,
          [Status] = @Status,
          [Remark] =@Remark
          WHERE FeeHeadId  = @FeeHeadId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_FeesHeadDetailsInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_FeesHeadDetailsInsert]
	-- Add the parameters for the stored procedure here
@FeeHead nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [FeeHeadDetails]([FeeHead],[Status],[Remark]) VALUES(@FeeHead,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_FeeClassDivisionUpdate]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_FeeClassDivisionUpdate]
	-- Add the parameters for the stored procedure here
@FeeHeadId int,
@ClassDivisionId int,
@PeriodInMonthly numeric(18,2),
@AmountInMonthly numeric(18,2),
@AmountInYearly numeric(18,2),
@Status bit,
@Remark nvarchar(50),
@FeeClassDivisionId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [FeeClassDivisionDetails] 
        SET [FeeHeadId] = @FeeHeadId,
          [ClassDivisionId] = @ClassDivisionId,
          [PeriodInMonthly] =@PeriodInMonthly,
          [AmountInMonthly] = @AmountInMonthly,
          [AmountInYearly] = @AmountInYearly,
          [Status] = @Status,
          [Remark] = @Remark
        WHERE FeeClassDivisionId  = @FeeClassDivisionId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_FeeClassDivisionInsert]    Script Date: 08/26/2017 18:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_FeeClassDivisionInsert]
	-- Add the parameters for the stored procedure here
	
@FeeHeadId int,
@ClassDivisionId int,
@PeriodInMonthly numeric(18,2),
@AmountInMonthly numeric(18,2),
@AmountInYearly numeric(18,2),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [FeeClassDivisionDetails]([FeeHeadId],[ClassDivisionId],[PeriodInMonthly],[AmountInMonthly],[AmountInYearly],[Status],[Remark]) VALUES(@FeeHeadId,@ClassDivisionId,@PeriodInMonthly,@AmountInMonthly,@AmountInYearly,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DivisionDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DivisionDetailsUpdate]
	-- Add the parameters for the stored procedure here
	@Division nvarchar(50),
	@Status bit,
	@Remark nvarchar(50),
	@DivisionID int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [DivisionDetails] 
        SET [Division] = @Division,
          [Status] = @Status,
          [Remark] = @Remark
        WHERE DivisionId  = @DivisionID;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DivisionDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DivisionDetailsInsert]
	-- Add the parameters for the stored procedure here
	@Division nvarchar(50),
	@Status bit,
	@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [DivisionDetails]([Division],[Status],[Remark]) VALUES(@Division,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DistrictDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DistrictDetailsUpdate] 
	-- Add the parameters for the stored procedure here
	@StateId int,
	@DistrictName nchar(20),
	@Status bit,
	@Remark nvarchar(50),
	@DistrictId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [DistrictDetails]
       SET [StateId] = @StateId,
          [District] = @DistrictName,
          [Status] = @Status,
          [Remark] =@Remark
        WHERE DistrictId  = @DistrictId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DistrictDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DistrictDetailsInsert]
	-- Add the parameters for the stored procedure here
	@StateId int,
	@District nchar(20),
	@Status bit,
	@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [DistrictDetails]([StateId],[District],[Status],[Remark]) VALUES(@StateId, @District, @Status, @Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CountryUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CountryUpdate]
	-- Add the parameters for the stored procedure here
	@CountryName nchar(20),
	@Status bit,
	@Remark nvarchar(50),
	@CountryId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [CountryDetails] SET [Country] = @CountryName,
         [Status] = @Status,
         [Remark] = @Remark 
        WHERE CountryId  = @CountryId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CountryInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CountryInsert]
	-- Add the parameters for the stored procedure here
	@CountryName nchar(20),
	@Status bit,
	@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [CountryDetails]([Country],[Status],[Remark]) VALUES(@CountryName,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ClassDivisionDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ClassDivisionDetailsInsert]
@ClassId int,
@DivisionId int,
@Status bit,
@Remark nvarchar(50)
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT INTO [ClassDivisionDetails] ([ClassId],[DivisionId],[Status],[Remark])
	  VALUES(@ClassId, @DivisionId, @Status, @Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ClassDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ClassDetailsUpdate]
	-- Add the parameters for the stored procedure here
@Class nvarchar(50),
@Status bit,
@Remark nvarchar(50),
@ClassId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [ClassDetails]
        SET [Class] = @Class
          ,[Status] = @Status
          ,[Remark] = @Remark
         WHERE [ClassId]  = @ClassId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ClassDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ClassDetailsInsert]
	-- Add the parameters for the stored procedure here
@Class nvarchar(50),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [ClassDetails] ([Class],[Status],[Remark]) VALUES(@Class,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CityDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CityDetailsUpdate]
	-- Add the parameters for the stored procedure here
	@CityId int,
@DistrictId int,
@City nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [CityDetails] 
         SET [DistrictId] = @DistrictId
          ,[City] = @City
          ,[Status] =@Status
          ,[Remark] = @Remark
        WHERE CityId  = @CityId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CityDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CityDetailsInsert]
	-- Add the parameters for the stored procedure here
@DistrictId int,
@City nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [CityDetails]([DistrictId],[City],[Status],[Remark]) VALUES(@DistrictId, @City,@Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ChangePassword]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ChangePassword] 
	-- Add the parameters for the stored procedure here
	@UserId nchar(10),
	@Password nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.UserDetails SET dbo.UserDetails.[Password] = @Password WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CastDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CastDetailsUpdate]
	-- Add the parameters for the stored procedure here
	@CastId int,	
	@Cast nchar(20),
	@Status bit,
	@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [CastDetails] 
        SET [Cast] = @Cast
          ,[Status] = @Status
          ,[Remark] = @Remark
         WHERE CastId  = @CastId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CastDetailsInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CastDetailsInsert]
	-- Add the parameters for the stored procedure here
@Cast nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [CastDetails]([Cast],[Status],[Remark]) VALUES(@Cast,@Status, @Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AssetDetailsUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AssetDetailsUpdate]  
	-- Add the parameters for the stored procedure here
	 @SrNo int
	       ,@AssetName nvarchar(50)
           ,@TypeofAsset nchar(10)
           ,@Quantity int
           ,@PricePerItem decimal(10,2)
           ,@Total decimal(18,2)
           ,@PurchaseDate date
           ,@Condition nchar(10)
           ,@AssesmentYear date
           ,@Status bit
           ,@Remark nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update[AssetDetails]
		SET	[AssetName] = @AssetName
          ,[TypeofAsset] = @TypeofAsset
          ,[Quantity]=@Quantity
          ,[PricePerItem] = @PricePerItem
          ,[Total]=@Total
          ,[PurchaseDate]=@PurchaseDate
          ,[Condition]=@Condition
          ,[AssesmentYear]=@AssesmentYear
          ,[Status]=@Status
          ,[Remark]=@Remark 
          
          where SrNo=@SrNo;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AcademicYearUpdate]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AcademicYearUpdate] 
	-- Add the parameters for the stored procedure here
	@AcademicYearName nchar(10),
	@StartDate date,
	@EndDate date,
	@Status bit,
	@Remark nvarchar(50),
	@AcademicYearId int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [AcademicYearDetails] SET [AcademicYear] = @AcademicYearName,
         [StartDate] = @StartDate,
         [EndDate] = @EndDate,
          [Status] = @Status,
          [Remark] = @Remark 
        WHERE AcademicYearId  = @AcademicYearId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AcademicYearInsert]    Script Date: 08/26/2017 18:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AcademicYearInsert] 
	-- Add the parameters for the stored procedure here
	@AcademicYear nchar(10),
	@StartDate date,
	@EndDate date,
	@Status bit,
	@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [AcademicYearDetails] ([AcademicYear],[StartDate],[EndDate],[Status],[Remark]) VALUES(@AcademicYear, @StartDate,@EndDate, @Status,@Remark);
END
GO
/****** Object:  View [dbo].[viewGetEmployeeListForAddLeave]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewGetEmployeeListForAddLeave]
AS
SELECT     dbo.Employee.EmployeeId, dbo.Employee.FirstName, dbo.Employee.MiddleName, dbo.Employee.LastName, dbo.EmployeeLeaveTransaction.BalanceLeaves, 
                      dbo.Employee.SchoolId
FROM         dbo.Employee LEFT OUTER JOIN
                      dbo.EmployeeLeaveTransaction ON dbo.Employee.EmployeeId = dbo.EmployeeLeaveTransaction.EmployeeId AND 
                      dbo.EmployeeLeaveTransaction.TransactionId =
                          (SELECT     MAX(TransactionId) AS Expr1
                            FROM          dbo.EmployeeLeaveTransaction
                            WHERE      (dbo.Employee.EmployeeId = EmployeeId))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employee"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EmployeeLeaveTransaction"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 125
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 2700
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetEmployeeListForAddLeave'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetEmployeeListForAddLeave'
GO
/****** Object:  View [dbo].[viewGetAllStudentTransaction]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewGetAllStudentTransaction]
AS
SELECT     TOP (100) PERCENT dbo.StudentTransaction.StudentTransactionId, dbo.StudentTransaction.ClassDivisionId, 
                      dbo.ClassDivisionDetails.ClassId, dbo.ClassDivisionDetails.DivisionId, dbo.ClassDetails.Class, dbo.DivisionDetails.Division, dbo.StudentDetails.RegisterId, dbo.StudentTransaction.StudentId, 
                      LTRIM(RTRIM(LTRIM(RTRIM(ISNULL(dbo.StudentDetails.Title, ''))) + ' ' + LTRIM(RTRIM(dbo.StudentDetails.FirstName)) + ' ' + LTRIM(RTRIM(ISNULL(dbo.StudentDetails.MiddleName, ''))) 
                      + ' ' + LTRIM(RTRIM(dbo.StudentDetails.LastName)))) AS StudentFullName, dbo.StudentTransaction.TransactionDate, dbo.StudentTransaction.Status, dbo.StudentTransaction.Remark, 
                      dbo.StudentTransaction.ReceiptNo, dbo.StudentTransaction.BankName, dbo.StudentTransaction.ChequeNumber, dbo.StudentTransaction.ReceiptTotal, dbo.StudentTransaction.CreatedBy, 
                      dbo.StudentTransaction.CreatedDate, dbo.StudentTransaction.ModifiedBy, dbo.StudentTransaction.ModifiedDate, dbo.StudentDetails.SchoolId
FROM         dbo.StudentTransaction INNER JOIN
                      dbo.StudentDetails ON dbo.StudentTransaction.StudentId = dbo.StudentDetails.StudentId AND 
                      dbo.StudentTransaction.SchoolId = dbo.StudentDetails.SchoolId INNER JOIN
                      dbo.ClassDivisionDetails ON dbo.StudentTransaction.ClassDivisionId = dbo.ClassDivisionDetails.ClassDivisionId INNER JOIN
                      dbo.ClassDetails ON dbo.ClassDivisionDetails.ClassId = dbo.ClassDetails.ClassId INNER JOIN
                      dbo.DivisionDetails ON dbo.ClassDivisionDetails.DivisionId = dbo.DivisionDetails.DivisionId
ORDER BY dbo.StudentTransaction.StudentTransactionId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[25] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StudentTransaction"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 126
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StudentDetails"
            Begin Extent = 
               Top = 6
               Left = 473
               Bottom = 126
               Right = 734
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDivisionDetails"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDetails"
            Begin Extent = 
               Top = 126
               Left = 242
               Bottom = 246
               Right = 408
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DivisionDetails"
            Begin Extent = 
               Top = 126
               Left = 446
               Bottom = 246
               Right = 612
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentTransaction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'84
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentTransaction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentTransaction'
GO
/****** Object:  View [dbo].[viewGetAllStudentDetails]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewGetAllStudentDetails]
AS
SELECT     dbo.StudentDetails.SrNo, dbo.StudentDetails.StudentId, dbo.StudentDetails.SchoolId, dbo.StudentDetails.RegisterId, dbo.StudentDetails.ClassDivisionId, 
                      dbo.StudentDetails.Title, dbo.StudentDetails.FirstName, dbo.StudentDetails.MiddleName, dbo.StudentDetails.LastName, dbo.StudentDetails.MotherName, dbo.StudentDetails.Gender, 
                      dbo.StudentDetails.Nationality, dbo.StudentDetails.ReligionCastId, dbo.StudentDetails.DateOfBirth, dbo.StudentDetails.PlaceOfBirth, dbo.StudentDetails.AdharcardNo, 
                      dbo.StudentDetails.DateOfAdmission, dbo.StudentDetails.MotherTounge, dbo.StudentDetails.LastSchoolAttended, dbo.StudentDetails.Progress, dbo.StudentDetails.Conduct, 
                      dbo.StudentDetails.DateOfLeavingSchool, dbo.StudentDetails.LastSchoolClass, dbo.StudentDetails.LastSchoolTCNo, dbo.StudentDetails.ClassInWhichStudingAndSinceWhen, 
                      dbo.StudentDetails.ReasonForLeavingSchool, dbo.StudentDetails.RemarkOnTC, dbo.StudentDetails.TCPrinted, dbo.StudentDetails.Status, dbo.StudentDetails.Remark, dbo.StudentDetails.TCNo, 
                      dbo.StudentDetails.MotherTounge AS Expr1, dbo.StudentDetails.UStudentId, dbo.StudentDetails.PrevSchoolDateofLiving, 
                      dbo.ClassDivisionDetails.ClassId, dbo.ClassDivisionDetails.DivisionId, dbo.ClassDetails.Class, dbo.DivisionDetails.Division, dbo.ReligionCastDetails.ReligionId, dbo.ReligionCastDetails.CastId, 
                      dbo.ReligionCastDetails.ReserveCategory, dbo.ReligionDetails.Religion, dbo.CastDetails.Cast
FROM         dbo.StudentDetails INNER JOIN
                      dbo.ClassDivisionDetails ON dbo.StudentDetails.ClassDivisionId = dbo.ClassDivisionDetails.ClassDivisionId INNER JOIN
                      dbo.ClassDetails ON dbo.ClassDivisionDetails.ClassId = dbo.ClassDetails.ClassId INNER JOIN
                      dbo.DivisionDetails ON dbo.ClassDivisionDetails.DivisionId = dbo.DivisionDetails.DivisionId INNER JOIN
                      dbo.ReligionCastDetails ON dbo.StudentDetails.ReligionCastId = dbo.ReligionCastDetails.ReligionCastId INNER JOIN
                      dbo.ReligionDetails ON dbo.ReligionCastDetails.ReligionId = dbo.ReligionDetails.ReligionId INNER JOIN
                      dbo.CastDetails ON dbo.ReligionCastDetails.CastId = dbo.CastDetails.CastId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StudentDetails"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 126
               Right = 503
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDivisionDetails"
            Begin Extent = 
               Top = 6
               Left = 541
               Bottom = 126
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDetails"
            Begin Extent = 
               Top = 6
               Left = 745
               Bottom = 126
               Right = 911
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DivisionDetails"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReligionCastDetails"
            Begin Extent = 
               Top = 126
               Left = 242
               Bottom = 246
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReligionDetails"
            Begin Extent = 
               Top = 126
               Left = 454
               Bottom = 246
              ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' Right = 620
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CastDetails"
            Begin Extent = 
               Top = 126
               Left = 658
               Bottom = 246
               Right = 824
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllStudentDetails'
GO
/****** Object:  View [dbo].[viewGetAllAssetDetails]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewGetAllAssetDetails]
AS
SELECT     SrNo, AssetName, TypeofAsset, Quantity, PricePerItem, Total, PurchaseDate, Condition, AssesmentYear, Status, Remark, SchoolId
FROM         dbo.AssetDetails
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AssetDetails"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 201
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllAssetDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewGetAllAssetDetails'
GO
/****** Object:  View [dbo].[viewExport]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewExport]
AS
SELECT     dbo.StudentDetails.StudentId, dbo.StudentDetails.UStudentId, 
                      dbo.StudentDetails.FirstName + ' ' + dbo.StudentDetails.MiddleName + ' ' + dbo.StudentDetails.LastName AS StudentName, dbo.StudentDetails.MotherName, 
                      dbo.ClassDetails.Class, dbo.DivisionDetails.Division, dbo.StudentDetails.RegisterId AS GRNo, dbo.StudentDetails.DateOfBirth AS Birthdate, 
                      dbo.StudentDetails.PlaceOfBirth AS Birthplace, dbo.StudentDetails.DateOfAdmission, dbo.StudentDetails.ClassInWhichStudingAndSinceWhen AS AdmissionClass, 
                      dbo.ReligionDetails.Religion, dbo.CastDetails.Cast AS Subcaste, dbo.ReligionCastDetails.ReserveCategory AS Category, 
                      dbo.StudentDetails.AdharcardNo AS AadharCardNo, dbo.StudentParentDetails.MobileNumber AS ParentsContactNo, dbo.StudentParentDetails.Address, 
                      dbo.StudentParentDetails.BankName, dbo.StudentParentDetails.FirstName + ' ' + dbo.StudentParentDetails.LastName AS [Bank account holder name], 
                      dbo.StudentParentDetails.AccountNo, dbo.StudentParentDetails.Branch, dbo.StudentParentDetails.IFSCCode, 
                      dbo.StudentDetails.Status, dbo.StudentDetails.SchoolId
FROM         dbo.StudentDetails INNER JOIN
                      dbo.ClassDivisionDetails ON dbo.StudentDetails.ClassDivisionId = dbo.ClassDivisionDetails.ClassDivisionId INNER JOIN
                      dbo.DivisionDetails ON dbo.ClassDivisionDetails.DivisionId = dbo.DivisionDetails.DivisionId INNER JOIN
                      dbo.ClassDetails ON dbo.ClassDivisionDetails.ClassId = dbo.ClassDetails.ClassId INNER JOIN
                      dbo.ReligionCastDetails ON dbo.StudentDetails.ReligionCastId = dbo.ReligionCastDetails.ReligionCastId INNER JOIN
                      dbo.ReligionDetails ON dbo.ReligionCastDetails.ReligionId = dbo.ReligionDetails.ReligionId INNER JOIN
                      dbo.CastDetails ON dbo.ReligionCastDetails.CastId = dbo.CastDetails.CastId LEFT OUTER JOIN
                      dbo.StudentParentDetails ON dbo.StudentDetails.StudentId = dbo.StudentParentDetails.StudentId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[9] 2[29] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StudentDetails"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDivisionDetails"
            Begin Extent = 
               Top = 6
               Left = 337
               Bottom = 126
               Right = 503
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DivisionDetails"
            Begin Extent = 
               Top = 6
               Left = 541
               Bottom = 126
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDetails"
            Begin Extent = 
               Top = 6
               Left = 745
               Bottom = 126
               Right = 911
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReligionCastDetails"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReligionDetails"
            Begin Extent = 
               Top = 126
               Left = 250
               Bottom = 246
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CastDetails"
            Begin Extent = 
               Top = 126
               Left = 454
               Bottom = 246
               Right = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewExport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'620
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StudentParentDetails"
            Begin Extent = 
               Top = 126
               Left = 658
               Bottom = 246
               Right = 843
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 25
         Width = 284
         Width = 1500
         Width = 2910
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewExport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewExport'
GO
/****** Object:  View [dbo].[viewCasteChart]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewCasteChart]
AS
SELECT     dbo.ReligionCastDetails.ReserveCategory, COUNT(dbo.ReligionCastDetails.ReserveCategory) AS Count, dbo.StudentDetails.SchoolId
FROM         dbo.StudentDetails INNER JOIN
                      dbo.ReligionCastDetails ON dbo.StudentDetails.ReligionCastId = dbo.ReligionCastDetails.ReligionCastId
WHERE     (dbo.StudentDetails.Status = 1) AND (dbo.ReligionCastDetails.Status = 1)
GROUP BY dbo.ReligionCastDetails.ReserveCategory, dbo.StudentDetails.SchoolId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StudentDetails"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ReligionCastDetails"
            Begin Extent = 
               Top = 6
               Left = 353
               Bottom = 126
               Right = 543
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewCasteChart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'viewCasteChart'
GO
/****** Object:  View [dbo].[view_ClassWiseMaleFemale]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_ClassWiseMaleFemale]
AS
SELECT     dbo.ClassDetails.Class, COUNT(CASE WHEN StudentDetails.Gender = 'M' THEN 1 END) AS CountMale, COUNT(CASE WHEN StudentDetails.Gender = 'F' THEN 1 END) 
                      AS CountFemale, dbo.StudentDetails.SchoolId
FROM         dbo.ClassDetails INNER JOIN
                      dbo.ClassDivisionDetails ON dbo.ClassDetails.ClassId = dbo.ClassDivisionDetails.ClassId INNER JOIN
                      dbo.StudentDetails ON dbo.ClassDivisionDetails.ClassDivisionId = dbo.StudentDetails.ClassDivisionId
WHERE     (dbo.StudentDetails.Status = 1) AND (dbo.ClassDivisionDetails.Status = 1) AND (dbo.ClassDetails.Status = 1)
GROUP BY dbo.ClassDetails.Class, dbo.StudentDetails.SchoolId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[4] 2[30] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ClassDetails"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ClassDivisionDetails"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 126
               Right = 408
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StudentDetails"
            Begin Extent = 
               Top = 6
               Left = 446
               Bottom = 126
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_ClassWiseMaleFemale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_ClassWiseMaleFemale'
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentParentDetailsUpdate]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,
-- Description:	<Description,,
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentParentDetailsUpdate]
	-- Add the parameters for the stored procedure here
           @StudentId int,
           @Title nchar(10),
           @FirstName nvarchar(100),
           @MiddleName nvarchar(100),
           @LastName nvarchar(100),
           @Gender nchar(1),
           @Address nvarchar(300),
           @CountryId int,
           @StateId int,
           @DistrictId int,
           @CityId int,
           @PinCode int,
           @MobileNumber bigint,
           @ContactNo nvarchar(20),
           @Occupation nchar(10),
           @CompanyName nchar(20),
           @CompanyAddress nchar(50),
           @CompanyContactNo nvarchar(20),
           @BankName nvarchar(50),
           @AccountNo nvarchar(20),
           @Branch nvarchar(20),
           @IFSCCode nvarchar(20),
           @Status bit,
           @Remark nchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [StudentParentDetails] 
                            SET  [Title] = @Title
                            , [FirstName] =@FirstName
                            , [MiddleName] = @MiddleName
                            , [LastName] = @LastName
                            , [Gender] = @Gender
                            , [Address] = @Address
                            , [CountryId] = @CountryId
                            , [StateId] = @StateId
                            , [DistrictId] = @DistrictId
                            , [CityId] = @CityId
                            , [PinCode] = @PinCode
                            , [MobileNumber] = @MobileNumber
                            , [ContactNo] = @ContactNo
                            , [Occupation] = @Occupation
                            , [CompanyName] = @CompanyName
                            , [CompanyAddress] = @CompanyAddress
                            , [CompanyContactNo] = @CompanyContactNo
                            , [Status] = @Status
                            , [Remark] = @Remark
                            , [BankName]= @BankName
                            , [AccountNo]= @AccountNo
                            , [Branch]= @Branch
                            , [IFSCCode]= @IFSCCode
                            WHERE  [StudentId] = @StudentId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentParentDetailsInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,
-- Description:	<Description,,
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentParentDetailsInsert]
	-- Add the parameters for the stored procedure here
           @StudentId int,
           @Title nchar(10),
           @FirstName nvarchar(100),
           @MiddleName nvarchar(100),
           @LastName nvarchar(100),
           @Gender nchar(1),
           @Address nvarchar(300),
           @CountryId int,
           @StateId int,
           @DistrictId int,
           @CityId int,
           @PinCode int,
           @MobileNumber bigint,
           @ContactNo nvarchar(20),
           @Occupation nchar(10),
           @CompanyName nchar(20),
           @CompanyAddress nchar(50),
           @CompanyContactNo nvarchar(20),
           @BankName nvarchar(50),
           @AccountNo nvarchar(20),
           @Branch nvarchar(20),
           @IFSCCode nvarchar(20),
           @Status bit,
           @Remark nchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [StudentParentDetails]
							([StudentId]
                            , [Title]
                            , [FirstName]
                            , [MiddleName]
                            , [LastName]
                            , [Gender]
                            , [Address]
                            , [CountryId]
                            , [StateId]
                            , [DistrictId]
                            , [CityId]
                            , [PinCode]
                            , [MobileNumber]
                            , [ContactNo]
                            , [Occupation]
                            , [CompanyName]
                            , [CompanyAddress]
                            , [CompanyContactNo]
                            , [BankName]
                            , [AccountNo]
                            , [Branch]
                            , [IFSCCode]
                            , [Status]
                            , [Remark]
                            ) VALUES(@StudentId,
           @Title,
           @FirstName ,
           @MiddleName,
           @LastName,
           @Gender ,
           @Address,
           @CountryId ,
           @StateId ,
           @DistrictId ,
           @CityId ,
           @PinCode,
           @MobileNumber ,
           @ContactNo,
           @Occupation ,
           @CompanyName ,
           @CompanyAddress,
           @CompanyContactNo,
           @BankName ,
           @AccountNo ,
           @Branch ,
           @IFSCCode ,
           @Status ,
           @Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentLedgerUpdate]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentLedgerUpdate]
	-- Add the parameters for the stored procedure here
	@StudentLedgerId int,
@StudentId int,
@TransactionDate date,
@FeeHeadId int,
@Cr  decimal(18,0),
@Dr  decimal(18,0),
@HeadBalance decimal(18,0),
@Balance  decimal(18,0),
@ReceiptNo int,
@BankName nvarchar(50),
@ChequeNumber nvarchar(50),
@Status bit,
@Remark varchar(50),
@CreatedBy int,
@CreatedDate date,
@ModifiedBy int,
@ModifiedDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [StudentLedger] 
       SET [StudentId] =@StudentId,
        [TransactionDate] =@TransactionDate,
        [FeeHeadId] =@FeeHeadId,
        [Cr] =@Cr,
        [Dr] =@Dr,
        [HeadBalance] =@HeadBalance,
        [Balance]=@Balance,
        [Status]=@Status,[Remark]=@Remark ,
        [CreatedBy]=@CreatedBy,
        [CreatedDate]=@CreatedDate,
        [ModifiedBy]=@ModifiedBy,
        [ModifiedDate]=@ModifiedDate,
        [ReceiptNo] =@ReceiptNo,
        [BankName] =@BankName,
        [ChequeNumber] =@ChequeNumber
        WHERE StudentLedgerId  = @StudentLedgerId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentLedgerInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentLedgerInsert]
	-- Add the parameters for the stored procedure here
@StudentId int,
@TransactionDate date,
@FeeHeadId int,
@Cr  decimal(18,0),
@Dr  decimal(18,0),
@HeadBalance decimal(18,0),
@Balance  decimal(18,0),
@ReceiptNo int,
@BankName nvarchar(50),
@ChequeNumber nvarchar(50),
@Status bit,
@Remark varchar(50),
@CreatedBy int,
@CreatedDate date,
@ModifiedBy int,
@ModifiedDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT INTO [StudentLedger]([StudentId],[TransactionDate],[FeeHeadId],[Cr],[Dr],[HeadBalance],[Balance],[ReceiptNo],[BankName],[ChequeNumber],[Status],[Remark],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
	  VALUES(@StudentId,@TransactionDate,@FeeHeadId ,@Cr ,@Dr,@HeadBalance,@Balance,@ReceiptNo,@BankName,@ChequeNumber,@Status,@Remark,@CreatedBy,@CreatedDate,@ModifiedBy ,@ModifiedDate );
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentAttendanceInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_StudentAttendanceInsert] 
	-- Add the parameters for the stored procedure here
	@ClassId int,
	@DivisionId int,
	@DateOfAttendance DateTime,
	@StudentId int,
	@AttendanceInDays decimal(18,2),
	@AbsentRemark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @ClassDivisionId int;

SELECT @ClassDivisionId = ISNULL(ClassDivisionId,0) 
FROM classdivisiondetails 
WHERE ClassId = @ClassId AND DivisionId = @DivisionId;

DELETE FROM [dbo].[StudentAttendance] WHERE [ClassDivisionId]  = @ClassDivisionId
AND MONTH([DateOfAttendance]) = MONTH(@DateOfAttendance)
AND YEAR([DateOfAttendance]) = YEAR(@DateOfAttendance)
AND [StudentId] = @StudentId;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[StudentAttendance]
           ([ClassDivisionId]
           ,[DateOfAttendance]
           ,[StudentId]
           ,[AttendanceInDays]
           ,[AbsentRemark]
           ,[Status])
     VALUES
           (@ClassDivisionId
           ,@DateOfAttendance
           ,@StudentId
           ,@AttendanceInDays
           ,@AbsentRemark
           ,1);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentAttendanceGet]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentAttendanceGet] 
	-- Add the parameters for the stored procedure here
	@ClassId int,
	@DivisionId int,
	@DateOfAttendance DateTime	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  StudentDetails.ClassDivisionId, 
	StudentDetails.StudentId,  StudentDetails.RegisterId, 
	--StudentDetails.Title, StudentDetails.FirstName, StudentDetails.MiddleName, StudentDetails.LastName,
	LTRIM(RTRIM(LTRIM(RTRIM(ISNULL(StudentDetails.Title,''))) + ' ' + LTRIM(RTRIM(StudentDetails.FirstName)) + ' ' + LTRIM(RTRIM(ISNULL(StudentDetails.MiddleName,''))) + ' ' + LTRIM(RTRIM(StudentDetails.LastName)))) as StudentFullName,
	ISNULL(StudentAttendance.Id,0) as Id, ISNULL(StudentAttendance.DateOfAttendance,@DateOfAttendance) as DateOfAttendance, 
	ISNULL(StudentAttendance.AttendanceInDays,0) AS AttendanceInDays, ISNULL(StudentAttendance.AbsentRemark,'') As AbsentRemark,
	ISNULL(StudentAttendance.Status,1)  as Status

	FROM StudentDetails INNER JOIN ClassDivisionDetails ON StudentDetails.ClassDivisionId = ClassDivisionDetails.ClassDivisionId
	LEFT OUTER JOIN ( SELECT     StudentAttendance.Id, StudentAttendance.DateOfAttendance, StudentAttendance.AttendanceInDays, 
		StudentAttendance.AbsentRemark, StudentAttendance.Status, StudentAttendance.ClassDivisionId, StudentAttendance.StudentId
		FROM         StudentAttendance INNER JOIN ClassDivisionDetails ON  StudentAttendance.ClassDivisionId = ClassDivisionDetails.ClassDivisionId 				
		WHERE (month(StudentAttendance.DateOfAttendance) = Month(@DateOfAttendance))
			AND (year(StudentAttendance.DateOfAttendance) = year(@DateOfAttendance))
			AND (ClassDivisionDetails.ClassId = @ClassId)
			AND (ClassDivisionDetails.DivisionId = @DivisionId)
	) AS StudentAttendance
	ON StudentAttendance.ClassDivisionId = StudentDetails.ClassDivisionId 
		AND StudentAttendance.StudentId = StudentDetails.StudentId
	WHERE 
	(ClassDivisionDetails.ClassId = @ClassId)
	AND (ClassDivisionDetails.DivisionId = @DivisionId)
	AND (StudentDetails.Status = 1);


END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentAddressDetailsUpdate]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentAddressDetailsUpdate]
	-- Add the parameters for the stored procedure here
					@StudentId int,
                    @CurrentAddress nvarchar(100)
                    ,@CurrentCountryId int
                    ,@CurrentStateId int
                    ,@CurrentDistrictId int
                    ,@CurrentCityId int
                    ,@CurrentPinCode int
                    ,@PermentAddress nvarchar(100)
                    ,@PermentCountryId int
                    ,@PermentStateId int
                    ,@PermentDistrictId int
                    ,@PermentCityId int                    
                    ,@PermentPinCode int
                    ,@Status bit
                    ,@Remark nvarchar(50)
                   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [StudentAddressDetails]
                         SET [CurrentAddress] =@CurrentAddress,
                    [CurrentCountryId]=@CurrentCountryId,
                     [CurrentStateId]=@CurrentStateId,
                    [CurrentDistrictId]=@CurrentDistrictId,
                    [CurrentCityId]=@CurrentCityId,
                    [CurrentPinCode]=@CurrentPinCode,
                    [PermentAddress]=@PermentAddress,
                    [PermentCountryId]=@PermentCountryId,
                    [PermentStateId]=@PermentStateId,
                    [PermentDistrictId]=@PermentDistrictId,
                    [PermentCityId]=@PermentCityId,
                    [PermentPinCode]=@PermentPinCode,
                     [Status] = @Status,
                      [Remark] = @Remark
                   WHERE StudentId  = @StudentId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentAddressDetailsInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentAddressDetailsInsert]
	-- Add the parameters for the stored procedure here
					@StudentId int,
                    @CurrentAddress nvarchar(100)
                    ,@CurrentCountryId int
                    ,@CurrentStateId int
                    ,@CurrentDistrictId int
                    ,@CurrentCityId int
                    ,@CurrentPinCode int
                    ,@PermentAddress nvarchar(100)
                    ,@PermentCountryId int
                    ,@PermentStateId int
                    ,@PermentDistrictId int
                    ,@PermentCityId int                    
                    ,@PermentPinCode int
                    ,@Status bit
                    ,@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT INTO [StudentAddressDetails]
					([StudentId],
                    [CurrentAddress],
                    [CurrentCountryId],
                    [CurrentStateId],
                    [CurrentDistrictId],
                    [CurrentCityId],
                    [CurrentPinCode],
                    [PermentAddress],
                    [PermentCountryId],
                    [PermentStateId],
                    [PermentDistrictId],
                    [PermentCityId] ,                 
                    [PermentPinCode],
                    [Status],
                    [Remark]
                    ) VALUES(@StudentId,
                    @CurrentAddress
                    ,@CurrentCountryId 
                    ,@CurrentStateId
                    ,@CurrentDistrictId
                    ,@CurrentCityId
                    ,@CurrentPinCode 
                    ,@PermentAddress
                    ,@PermentCountryId 
                    ,@PermentStateId 
                    ,@PermentDistrictId 
                    ,@PermentCityId                
                    ,@PermentPinCode
                    ,@Status 
                    ,@Remark );
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StateDetailsUpdate]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StateDetailsUpdate]
	-- Add the parameters for the stored procedure here
@CountryId int,
@State nchar(20),
@Status bit,
@Remark nvarchar(50),
@StateId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [StateDetails] 
       SET [CountryId] =@CountryId,
          [State] = @State,
          [Status] = @Status,
          [Remark] = @Remark
         WHERE StateId  = @StateId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StateDetailsInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StateDetailsInsert]
	-- Add the parameters for the stored procedure here
@CountryId int,
@State nchar(20),
@Status bit,
@Remark nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [StateDetails]([CountryId],[State],[Status],[Remark]) 
	VALUES(@CountryId,@State, @Status,@Remark);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentInsert]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentInsert] 
	-- Add the parameters for the stored procedure here
	@StudentId int = NULL,
	@SchoolId int,
	@RegisterId int,
	@ClassDivisionId int,
	@Title nvarchar(10),
	@FirstName nvarchar(100),
	@MiddleName nvarchar(100),
	@LastName nvarchar(100),
	@MotherName nvarchar(100),
	@Gender  nchar(1),
	@Nationality  nchar(10),
	@ReligionCastId int,
	@DateOfBirth date,
	@PlaceOfBirth  nchar(20),
	@AdharcardNo nvarchar(16),
	@DateOfAdmission date,
	@MotherTounge nchar(30),
	@UStudentId nchar(30),
	@LastSchoolAttended  nchar(300),
	@Progress  nchar(30),
	@Conduct  nchar(30),
	@DateOfLeavingSchool date,
	@LastSchoolClass nchar(30),
	@LastSchoolTCNo nchar(30),
	@ClassInWhichStudingAndSinceWhen   nchar(300),
	@ReasonForLeavingSchool  nchar(300),
	@RemarkOnTC  nchar(300),
	@Status bit,
	@Remark   nchar(30),
	@PrevSchoolDateofLiving date,
	@OUT_StudentId int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--If student is null or zero then get max student id and add one value.
	IF @StudentId IS NULL
    BEGIN
        select @StudentId = ISNULL(MAX(StudentId),0) from dbo.StudentDetails;
        set @StudentId= @StudentId + 1;
    END
    ELSE IF @StudentId = 0
    BEGIN
        select @StudentId = ISNULL(MAX(StudentId),0) from dbo.StudentDetails;
        set @StudentId= @StudentId + 1;
    END
    
	--Insert student record in student details table
	INSERT INTO [StudentDetails](
			[StudentId]
			,[SchoolId]
			,[RegisterId]
			,[ClassDivisionId]
			,[Title]
			,[FirstName]
			,[MiddleName]
			,[LastName]
			,[MotherName]
			,[Gender]
			,[Nationality]
			,[ReligionCastId]
			,[DateOfBirth]
			,[PlaceOfBirth]
			,[AdharcardNo]
			,[DateOfAdmission]
			,[MotherTounge]
			,[UStudentId]
			,[LastSchoolAttended]
			,[Progress]
			,[Conduct]
			,[DateOfLeavingSchool]
			,[ClassInWhichStudingAndSinceWhen]
			,[ReasonForLeavingSchool]
			,[LastSchoolClass]
			,[LastSchoolTCNo]
			,[RemarkOnTC]
			,[Status]
			,[Remark]
			,[PrevSchoolDateofLiving]
			) VALUES(
			@StudentId
			,@SchoolId
			,@RegisterId
			,@ClassDivisionId
			,@Title
			,@FirstName
			,@MiddleName
			,@LastName
			,@MotherName
			,@Gender
			,@Nationality
			,@ReligionCastId
			,@DateOfBirth
			,@PlaceOfBirth
			,@AdharcardNo
			,@DateOfAdmission
			,@MotherTounge
			,@UStudentId
			,@LastSchoolAttended
			,@Progress
			,@Conduct
			,@DateOfLeavingSchool
			,@ClassInWhichStudingAndSinceWhen
			,@ReasonForLeavingSchool
			,@LastSchoolClass
			,@LastSchoolTCNo
			,@RemarkOnTC
			,@Status
			,@Remark
			,@PrevSchoolDateofLiving);
                             

	--select @OUT_StudentId = Scope_Identity();
	select @OUT_StudentId = @StudentId;

	DECLARE @FeeHeadId INT, @AmountInYearly DECIMAL(18,2);
	DECLARE cursorName CURSOR -- Declare cursor
	LOCAL SCROLL STATIC 
	FOR	select FeeHeadId, AmountInYearly from dbo.FeeClassDivisionDetails where ClassDivisionId =@ClassDivisionId;
	OPEN cursorName -- open the cursor
	FETCH NEXT FROM cursorName 
	   INTO @FeeHeadId, @AmountInYearly
	WHILE @@FETCH_STATUS = 0 
	BEGIN

		--Get Max student ledger id according student id, fee head id
		DECLARE @MaxStudentLedgerId int, @HBalance  DECIMAL(18,2) , @MBalance DECIMAL(18,2) ;
		Set @HBalance = 0;
		Set @MBalance = 0;
		select @MaxStudentLedgerId = ISNULL(MAX(StudentLedgerId),0) from dbo.StudentLedger where StudentId = @OUT_StudentId AND FeeHeadId = @FeeHeadId;
		IF @MaxStudentLedgerId > 0 
		BEGIN
			select @HBalance = ISNULL(HeadBalance,0)
			from dbo.StudentLedger 
			where StudentLedgerId = @MaxStudentLedgerId;
			
			Set @HBalance = @HBalance + @AmountInYearly;
		END 
		ELSE
		BEGIN 
			Set @HBalance = @AmountInYearly;
		END

		--Get Max student ledger id according student id 
		select @MaxStudentLedgerId = ISNULL(MAX(StudentLedgerId),0) from dbo.StudentLedger where StudentId = @OUT_StudentId;
		IF @MaxStudentLedgerId > 0 
		BEGIN
			select @MBalance = ISNULL(Balance,0) 
			from dbo.StudentLedger 
			where StudentLedgerId = @MaxStudentLedgerId;
			
			Set @MBalance = @MBalance + @AmountInYearly;
		END 
		ELSE
		BEGIN 
			Set @MBalance = @AmountInYearly;
		END

		-- Insert record in student ledger
		INSERT INTO [dbo].[StudentLedger]
			   ([StudentId]
			   ,[TransactionDate]
			   ,[FeeHeadId]
			   ,[Cr]
			   ,[Dr]
			   ,[HeadBalance]
			   ,[Balance]
			   ,[ReceiptNo]
			   ,[BankName]
			   ,[ChequeNumber]
			   ,[Status]
			   ,[Remark]
			   ,[CreatedBy]
			   ,[CreatedDate]
			   ,[ModifiedBy]
			   ,[ModifiedDate])
		 VALUES
			   (@OUT_StudentId
			   ,GETDATE()
			   ,@FeeHeadId
			   ,0
			   ,@AmountInYearly
			   ,@HBalance--<HeadBalance, decimal(18,0),>
			   ,@MBalance--<Balance, decimal(18,0),>
			   ,0--<ReceiptNo, int,>
			   ,null
			   ,null
			   ,1
			   ,'Fee Transfer'
			   ,null
			   ,null
			   ,null
			   ,null);
 
   FETCH NEXT FROM cursorName 
   INTO @FeeHeadId, @AmountInYearly
END 
CLOSE cursorName -- close the cursor
DEALLOCATE cursorName -- Deallocate the cursor
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_StudentChangeClassDivision]    Script Date: 08/26/2017 18:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_StudentChangeClassDivision]
	-- Add the parameters for the stored procedure here
	--@SrNo int
	@SchoolId int,
	@PreviousClassDivisionId int,
	@CurrentClassDivisionId	int,
	@Status bit = 1,
	@CreatedBy	int = null,
	@CreatedDate	date,
	@ClassChangeDataType ClassChangeDataType  readonly  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
           
	DECLARE @StudentId int, @RemarkTYPE nvarchar(50)
	DECLARE db_cursor CURSOR FOR  
	SELECT StudentId, Remark from @ClassChangeDataType

	OPEN db_cursor   
	FETCH NEXT FROM db_cursor INTO @StudentId, @RemarkTYPE

	WHILE @@FETCH_STATUS = 0   
	BEGIN  

		INSERT INTO [dbo].[StudentChangeClassDivision]
           ([PreviousClassDivisionId]
           ,[StudentId]
           ,[CurrentClassDivisionId]
           ,[Status]
           ,[Remark]
           ,[CreatedBy]
           ,[CreatedDate])
     VALUES
           (@PreviousClassDivisionId
           ,@StudentId
           ,@CurrentClassDivisionId
           ,@Status
           ,@RemarkTYPE
           ,@CreatedBy
           ,GETDATE());
           
		DECLARE @RegisterId1 int,
			@ClassDivisionId1 int,
			@Title1 nchar(5),
			@FirstName1 nvarchar(15),
			@MiddleName1 nvarchar(15),
			@LastName1 nvarchar(15),
			@MotherName1 nvarchar(50),
			@Gender1  nchar(1),
			@Nationality1  nchar(10),
			@ReligionCastId1 int,
			@DateOfBirth1 date,
			@PlaceOfBirth1  nchar(20),
			@AdharcardNo1 nvarchar(16),
			@DateOfAdmission1 date,
			@MotherTounge1 nchar(30),
			@LastSchoolAttended1  nchar(30),
			@Progress1  nchar(30),
			@Conduct1  nchar(30),
			@DateOfLeavingSchool1	 date,
			@LastSchoolClass1 nchar(30),
           		@LastSchoolTCNo1 nchar(30),
			@ClassInWhichStudingAndSinceWhen1   nchar(30),
			@ReasonForLeavingSchool1  nchar(30),
			@PrevSchoolDateofLiving1 date,
			@UStudentId1  nchar(30)		
			
		SELECT @RegisterId1= StudentDetails.RegisterId,
			@ClassDivisionId1 = StudentDetails.ClassDivisionId,
			@Title1 = StudentDetails.Title,
			@FirstName1 = StudentDetails.FirstName,
			@MiddleName1 = StudentDetails.MiddleName,
			@LastName1 = StudentDetails.LastName,
			@MotherName1  = StudentDetails.MotherName,
			@Gender1  = StudentDetails.Gender,
			@Nationality1 = StudentDetails.Nationality,
			@ReligionCastId1 = StudentDetails.ReligionCastId,
			@DateOfBirth1 = StudentDetails.DateOfBirth,
			@PlaceOfBirth1 = StudentDetails.PlaceOfBirth,
			@AdharcardNo1 = StudentDetails.AdharcardNo,
			@DateOfAdmission1 = StudentDetails.DateOfAdmission,
			@MotherTounge1=StudentDetails.MotherTounge,
			@LastSchoolAttended1  = StudentDetails.LastSchoolAttended,
			@Progress1 = StudentDetails.Progress ,
			@Conduct1 = StudentDetails.Conduct,
			@DateOfLeavingSchool1 = StudentDetails.DateOfLeavingSchool,
			@LastSchoolClass1=StudentDetails.LastSchoolClass,
			@LastSchoolTCNo1=StudentDetails.LastSchoolTCNo,
			@ClassInWhichStudingAndSinceWhen1  = StudentDetails.ClassInWhichStudingAndSinceWhen,
			@ReasonForLeavingSchool1 = StudentDetails.ReasonForLeavingSchool,
			@PrevSchoolDateofLiving1=StudentDetails.PrevSchoolDateofLiving,
			@UStudentId1 = StudentDetails.UStudentId
		FROM StudentDetails 
		WHERE StudentDetails.StudentId = @StudentId 
			AND StudentDetails.ClassDivisionId = @PreviousClassDivisionId;
		
		 -- Get ReligionCastId according to current year
		DECLARE @preligionId int, @pcastId int, @preligion nvarchar(20), @pcast nvarchar(20);
		DECLARE @creligioncastId int
	       
	   SELECT     @preligionId = isnull(ReligionCastDetails.ReligionId,0), @pcastId = isnull(ReligionCastDetails.CastId,0), 
				@preligion = ReligionDetails.Religion, @pcast = CastDetails.[Cast]
		FROM         ReligionCastDetails INNER JOIN
					  ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId INNER JOIN
					  CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId
		WHERE ReligionCastDetails.ReligionCastId = @ReligionCastId1;
		
		SELECT  @creligioncastId = ReligionCastDetails.ReligionCastId				 
		FROM         ReligionCastDetails INNER JOIN
					  ReligionDetails ON ReligionCastDetails.ReligionId = ReligionDetails.ReligionId INNER JOIN
					  CastDetails ON ReligionCastDetails.CastId = CastDetails.CastId
		WHERE LTRIM(RTRIM(ReligionDetails.Religion)) = LTRIM(RTRIM(@preligion)) AND LTRIM(RTRIM(CastDetails.[Cast])) = LTRIM(RTRIM(@pcast));	

	   UPDATE StudentDetails SET Status=0 WHERE StudentDetails.StudentId = @StudentId
	   AND StudentDetails.ClassDivisionId = @PreviousClassDivisionId;
	       
		DECLARE	@return_value int, @OUT_StudentId int

		EXEC	@return_value = [dbo].[sp_StudentInsert]
		@StudentId = @StudentId,
		@SchoolId = @SchoolId,
		@RegisterId = @RegisterId1,
		@ClassDivisionId = @CurrentClassDivisionId,
		@Title = @Title1,
		@FirstName = @FirstName1,
		@MiddleName = @MiddleName1,
		@LastName = @LastName1,
		@MotherName = @MotherName1,
		@Gender = @Gender1,
		@Nationality = @Nationality1,
		@ReligionCastId = @creligioncastId,
		@DateOfBirth = @DateOfBirth1,
		@PlaceOfBirth = @PlaceOfBirth1,
		@AdharcardNo = @AdharcardNo1,		
		@DateOfAdmission = @DateOfAdmission1,
		@MotherTounge=@MotherTounge1,
		@UStudentId= @UStudentId1,
		@LastSchoolAttended = @LastSchoolAttended1,
		@Progress = @Progress1,
		@Conduct = @Conduct1,
		@DateOfLeavingSchool = @DateOfLeavingSchool1,
		@LastSchoolClass=@LastSchoolClass1,
		@LastSchoolTCNo=@LastSchoolTCNo1,
		@ClassInWhichStudingAndSinceWhen = @ClassInWhichStudingAndSinceWhen1,
		@ReasonForLeavingSchool = @ReasonForLeavingSchool1,
		@RemarkOnTC=0,
		@Status = 1,
		@Remark = @RemarkTYPE,
		@PrevSchoolDateofLiving=@PrevSchoolDateofLiving1,
		
		@OUT_StudentId = @OUT_StudentId OUTPUT;

       FETCH NEXT FROM db_cursor INTO @StudentId, @RemarkTYPE   
END   

CLOSE db_cursor   
DEALLOCATE db_cursor
           

           
END
GO
/****** Object:  Default [DF_ReligionDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[ReligionDetails] ADD  CONSTRAINT [DF_ReligionDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_ReligionCastDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[ReligionCastDetails] ADD  CONSTRAINT [DF_ReligionCastDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_FeeHeadDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[FeeHeadDetails] ADD  CONSTRAINT [DF_FeeHeadDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_FeeClassDivisionDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[FeeClassDivisionDetails] ADD  CONSTRAINT [DF_FeeClassDivisionDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_DivisonDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[DivisionDetails] ADD  CONSTRAINT [DF_DivisonDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_DistrictDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[DistrictDetails] ADD  CONSTRAINT [DF_DistrictDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_CountryDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[CountryDetails] ADD  CONSTRAINT [DF_CountryDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_ClassFeeDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[ClassFeeDetails] ADD  CONSTRAINT [DF_ClassFeeDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_ClassDivisionDetails_Status]    Script Date: 08/26/2017 18:10:46 ******/
ALTER TABLE [dbo].[ClassDivisionDetails] ADD  CONSTRAINT [DF_ClassDivisionDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_ClassDetails_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[ClassDetails] ADD  CONSTRAINT [DF_ClassDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_CastDetails_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[CastDetails] ADD  CONSTRAINT [DF_CastDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_AssetDetails_SchoolId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[AssetDetails] ADD  CONSTRAINT [DF_AssetDetails_SchoolId]  DEFAULT ((1)) FOR [SchoolId]
GO
/****** Object:  Default [DF_Account_SchoolId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_SchoolId]  DEFAULT ((1)) FOR [SchoolId]
GO
/****** Object:  Default [DF_AcademicYearDetails_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[AcademicYearDetails] ADD  CONSTRAINT [DF_AcademicYearDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_UserDetails_SchoolId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetails_SchoolId]  DEFAULT ((1)) FOR [SchoolId]
GO
/****** Object:  Default [DF_StudentTransaction_SchoolId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentTransaction] ADD  CONSTRAINT [DF_StudentTransaction_SchoolId]  DEFAULT ((1)) FOR [SchoolId]
GO
/****** Object:  Default [DF_StudentLedger_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentLedger] ADD  CONSTRAINT [DF_StudentLedger_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_StudentDetails_StudentId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentDetails] ADD  CONSTRAINT [DF_StudentDetails_StudentId]  DEFAULT ((1)) FOR [StudentId]
GO
/****** Object:  Default [DF_StudentDetails_SchoolId]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentDetails] ADD  CONSTRAINT [DF_StudentDetails_SchoolId]  DEFAULT ((1)) FOR [SchoolId]
GO
/****** Object:  Default [DF_StudentDetails_TCPrinted]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentDetails] ADD  CONSTRAINT [DF_StudentDetails_TCPrinted]  DEFAULT ((0)) FOR [TCPrinted]
GO
/****** Object:  Default [DF_StudentChangeClassDivision_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentChangeClassDivision] ADD  CONSTRAINT [DF_StudentChangeClassDivision_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_StudentAttendance_AttendanceInDays]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentAttendance] ADD  CONSTRAINT [DF_StudentAttendance_AttendanceInDays]  DEFAULT ((0)) FOR [AttendanceInDays]
GO
/****** Object:  Default [DF_StudentAttendance_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentAttendance] ADD  CONSTRAINT [DF_StudentAttendance_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_StudentAddressDetails_CurrentPinCode]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentAddressDetails] ADD  CONSTRAINT [DF_StudentAddressDetails_CurrentPinCode]  DEFAULT ((0)) FOR [CurrentPinCode]
GO
/****** Object:  Default [DF_StudentAddressDetails_PermentPinCode]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StudentAddressDetails] ADD  CONSTRAINT [DF_StudentAddressDetails_PermentPinCode]  DEFAULT ((0)) FOR [PermentPinCode]
GO
/****** Object:  Default [DF_StateDetails_Status]    Script Date: 08/26/2017 18:10:48 ******/
ALTER TABLE [dbo].[StateDetails] ADD  CONSTRAINT [DF_StateDetails_Status]  DEFAULT ((1)) FOR [Status]
GO
SET IDENTITY_INSERT [dbo].[RoleDetails] ON
INSERT [dbo].[RoleDetails] ([RoleId], [Role], [Status], [Remark]) VALUES (1, N'Admin', 1, NULL)
INSERT [dbo].[RoleDetails] ([RoleId], [Role], [Status], [Remark]) VALUES (2, N'Clerk', 1, NULL)
INSERT [dbo].[RoleDetails] ([RoleId], [Role], [Status], [Remark]) VALUES (3, N'Employee', 1, NULL)
SET IDENTITY_INSERT [dbo].[RoleDetails] OFF
SET IDENTITY_INSERT [dbo].[SchoolDetails] ON
INSERT [dbo].[SchoolDetails] ([SchoolId], [ManagementName], [SchoolName], [Address], [Taluka], [District], [ContactNumber], [EmailId], [SchoolReconginationNo], [Medium], [UDiscNo], [Board], [AffilationNo], [Status], [Remark], [LogoPath]) VALUES (1, N'Management Name', N'School Name', N'Address', N'Taluka', N'District', N'Contact Number', N'email_id', N'Saction Number', N'Medium', N'UDiscNo', N'Board', N'Affilation No', 1, NULL, N'/digiSMS/Images/SchoolIcon.jpg')
SET IDENTITY_INSERT [dbo].[SchoolDetails] OFF
SET IDENTITY_INSERT [dbo].[UserDetails] ON
INSERT [dbo].[UserDetails] ([SrNo], [RoleId], [UserId], [Password], [UserName], [SchoolId], [Status], [Remark]) VALUES (1, 1, N'Admin                         ', N'Admin                         ', N'Super Admin', 1, 1, NULL)
INSERT [dbo].[UserDetails] ([SrNo], [RoleId], [UserId], [Password], [UserName], [SchoolId], [Status], [Remark]) VALUES (2, 2, N'Clerk                         ', N'Clerk                         ', N'Clerk', 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[UserDetails] OFF