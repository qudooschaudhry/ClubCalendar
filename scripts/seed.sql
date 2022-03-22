use [Clubs.Connect]
Go

if not exists (select * from Clubs where Name = 'Alpine Club of Canada')
begin
	insert into Clubs (Name, Description)
	values ('Alpine Club of Canada', 'Alpine Club of Canada')

	declare @club_id int 
	select @club_id = SCOPE_IDENTITY()

	insert into Events (Name, Description, StartDate, EndDate, Capacity, ClubId)
	select 'Ice climbing', 'Ice climbing in Calabogie', '2022-03-05', '2022-03-05', 8, @club_id union
	select 'Winter Palooza', 'We drive to the dacks :(', '2022-03-04', '2022-03-06', 40, @club_id union 
	select 'Snowshowing', 'Gatineau park', '2022-03-11', '2022-03-11', 8, @club_id 
end