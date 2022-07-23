CREATE PROCEDURE [dbo].[spTodos_GetAllAssigned]
	@AssignedTo int
as
begin
	select Id, Task, AssignedTo, IsComplete 
	from dbo.Todos
	where AssignedTo = @AssignedTo;
end