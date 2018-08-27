Set Ansi_Nulls ON
Go

Set Quoted_Identifier On
Go

Create Procedure dbo.Task_Save As
Go
Alter Procedure dbo.Task_Save
    @ID Int = Null,
	@UserCode nVarChar(30),
	@Name nVarchar(255) = Null,
	@TaskDescription nVarchar(2000) = Null,
	@TaskPriority Int = Null,
	@Added DateTime = Null,
	@DateToComplete DateTime = Null,
	@TaskStatus nVarchar(10) = Null
As

/* Сохранение задач пользователя */

Set NoCount On
Set Ansi_Warnings On
Set Ansi_Padding On
Set Ansi_Null_Dflt_On On
Set ArithAbort On
Set Concat_Null_Yields_Null On
Set Numeric_Roundabort On
Set Implicit_transactions Off
Set Transaction Isolation Level Read Committed

-- Описываем внутренние переменные

Declare @ProcName sysname, @LastUpdate DateTime
Declare @ErrorMessage nVarChar(4000)

-- Присваиваем значения переменным

Select @LastUpdate=GetDate()
Select @ProcName=Object_Name(@@PROCID)

-- Проводим транзакцию

Begin Try
Begin Transaction
	if @ID Is Null
		Begin
			Insert Into Tasks (Name, TaskDescription, TaskPriority, DateToComplete, TaskStatus, UserCode)
			Values (@Name, @TaskDescription, @TaskPriority, @DateToComplete, @TaskStatus, @UserCode)
		End
	Else
		Begin
			Update Tasks Set TaskDescription=@TaskDescription, LastUpdate=@LastUpdate, UserCode=@UserCode Where TasksID=@ID And TaskDescription<>@TaskDescription
			Update Tasks Set TaskPriority=@TaskPriority, LastUpdate=@LastUpdate, UserCode=@UserCode Where TasksID=@ID And TaskPriority<>@TaskPriority
			Update Tasks Set Added=@Added, LastUpdate=@LastUpdate, UserCode=@UserCode Where TasksID=@ID And Added<>@Added
			Update Tasks Set DateToComplete=@DateToComplete, LastUpdate=@LastUpdate, UserCode=@UserCode Where TasksID=@ID And DateToComplete<>@DateToComplete
			Update Tasks Set TaskStatus=@TaskStatus, LastUpdate=@LastUpdate, UserCode=@UserCode Where TasksID=@ID And TaskStatus<>@TaskStatus
		End
Commit Transaction
End Try
Begin Catch
	RollBack
	Select @ErrorMessage = ERROR_MESSAGE();
	RaisError ('[%s] %s.', 11, 1, @ProcName, @ErrorMessage)
End Catch

Go

Grant Execute On dbo.Task_Save To WebRole
Go

