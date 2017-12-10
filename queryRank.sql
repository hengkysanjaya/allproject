select * from [event] evt
join registrationevent re on re.EventId=evt.EventId 
join Registration rg on rg.RegistrationId=re.RegistrationId
join runner rn on rn.RunnerId = rg.RunnerId 
where evt.EventId='13_3FM' and racetime is not null and racetime <> 0 order by dateofbirth asc

select datediff(year,dateofbirth,  GETDATE()) from Runner

select * from
(
	select rn.email,rank() over (partition by rn.gender order by re.racetime) as ranking from [event] evt
	join registrationevent re on re.EventId=evt.EventId 
	join Registration rg on rg.RegistrationId=re.RegistrationId
	join runner rn on rn.RunnerId = rg.RunnerId 
	where evt.EventId='13_3FM' and racetime is not null
	and racetime<>0 and datediff(year,dateofbirth,  GETDATE()) >= 70
)  as ta where ta.email = 'oralia.ochoa@hotmail.com'

