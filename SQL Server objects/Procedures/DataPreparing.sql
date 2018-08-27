Set Ansi_Nulls On
Set Quoted_Identifier On
Set NoCount On
Set Ansi_Warnings On
Set Ansi_Padding On
Set Ansi_Null_Dflt_On On
Set ArithAbort On
Set Concat_Null_Yields_Null On
Set Numeric_Roundabort On
Set Implicit_transactions Off
Set Transaction Isolation Level Read Committed

Declare @cnt Int = 0
Declare @cntStr VarChar(6)
Declare @curDate DateTime = GetDate();
Declare @dateToComplete DateTime

Truncate Table Tasks

While @cnt < 50000 
	Begin 
		Set @cntStr = Convert(VarChar, @cnt)
		Set @dateToComplete = DateAdd(day, @cnt, @curDate);

		Insert Into Tasks (Name, TaskDescription, TaskPriority, DateToComplete, TaskStatus, UserCode)
		Select 'Task #' + @cntStr, 'Description #' + @cntStr, 1, @dateToComplete, 'Active', 'ANTON'

		Set @cnt = @cnt + 1
	End

While @cnt < 100000 
	Begin 
		Set @cntStr = Convert(VarChar, @cnt)
		Set @dateToComplete = DateAdd(day, @cnt, @curDate);

		Insert Into Tasks (Name, TaskDescription, TaskPriority, DateToComplete, TaskStatus, UserCode)
		Select 'Task #' + @cntStr, 'Description #' + @cntStr, 1, @dateToComplete, 'Completed', 'ANTON'

		Set @cnt = @cnt + 1
	End

Select * From Tasks
Order by TasksID