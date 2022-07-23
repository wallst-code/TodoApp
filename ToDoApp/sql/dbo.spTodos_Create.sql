alter procedure [dbo].[spTodos_Create]
	@Task nvarchar(50),
	@AssignedTo int
as
begin
	insert into dbo.Todos (task, AssignedTo)
	values (@Task, @AssignedTo);

	select Id, Task, AssignedTo, IsComplete
	from dbo.Todos
	where Id = SCOPE_IDENTITY(); 
end