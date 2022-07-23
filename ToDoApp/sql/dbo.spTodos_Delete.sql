CREATE PROCEDURE [dbo].[spTodos_Delete]
	@AssignedTo int,
	@TodoId int
as
begin
	delete from dbo.Todos
	where Id = @TodoId 
		and AssignedTo = @AssignedTo;
end