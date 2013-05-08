USE [PhoneCases]
GO

/****** Object:  StoredProcedure [dbo].[InsertBaseData]    Script Date: 05/08/2013 15:12:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Tommy
-- Create date: 2013-05-07
-- Description:	GrundData
-- =============================================
CREATE PROCEDURE [dbo].[InsertBaseData] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SET IDENTITY_INSERT pc.Users ON
	insert into PhoneCases.pc.Users(Id,Name,PhoneNumber) values (1,'Tommy','358967047716681');
	insert into PhoneCases.pc.Users(Id,Name,PhoneNumber) values (2,'Pelle','0734186414');
	insert into PhoneCases.pc.Users(Id,Name,PhoneNumber) values (3,'Anders','0734186409');
	SET IDENTITY_INSERT pc.Users OFF
	
	SET IDENTITY_INSERT pc.Locations ON
	insert into PhoneCases.pc.Locations(Id,Name) values (1,'Värmland');
	SET IDENTITY_INSERT pc.Locations OFF

	SET IDENTITY_INSERT pc.CompanyTypes ON
	insert into PhoneCases.pc.CompanyTypes(Id,Name) values (1,'Amb');
	insert into PhoneCases.pc.CompanyTypes(Id,Name) values (2,'Rtj');
	insert into PhoneCases.pc.CompanyTypes(Id,Name) values (3,'Polis');
	SET IDENTITY_INSERT pc.CompanyTypes OFF

	SET IDENTITY_INSERT pc.Companies ON
	insert into PhoneCases.pc.Companies(Id,Name,CompanyTypesId,LocationId) values (1,'Landstinget i Värmland',1,1);
	SET IDENTITY_INSERT pc.Companies OFF

	SET IDENTITY_INSERT pc.Customers ON
	insert into PhoneCases.pc.Customers(Id,Name,CompanyId,PhoneNumber) values (1,'Emil Palm',1,'0');
	insert into PhoneCases.pc.Customers(Id,Name,CompanyId,PhoneNumber) values (2,'Tommy Welleby',1,'0707269686');
	insert into PhoneCases.pc.Customers(Id,Name,CompanyId,PhoneNumber) values (3,'Emulator',1,'15555215554');
	SET IDENTITY_INSERT pc.Customers OFF
	
END

GO


