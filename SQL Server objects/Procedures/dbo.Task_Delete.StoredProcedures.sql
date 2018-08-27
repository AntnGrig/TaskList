Set Ansi_Nulls ON
Go

Set Quoted_Identifier On
Go

Create Procedure dbo.Task_Delete As
GO
Alter Procedure dbo.Task_Delete
    @ID Int = Null,
	@UserCode nVarChar(30) = Null
As

/* Удаление задач пользователя */

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

Declare @ProcName sysname
Declare @ErrorMessage nVarChar(4000)

-- Присваиваем значения переменным

Select @ProcName=Object_Name(@@PROCID)

-- Удаляем задачу

Begin Try
	Begin Transaction
		Delete From Tasks Where TasksID=@ID
	Commit Transaction
End Try
Begin Catch
	RollBack
	Select @ErrorMessage = ERROR_MESSAGE();
	RaisError ('[%s] %s.', 11, 1, @ProcName, @ErrorMessage)
End Catch

Go

Grant Execute On dbo.Task_Delete To WebRole
Go
