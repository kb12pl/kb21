 function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.zamknij:as_button('stack','Zamknij').event=function()win:on_close()end

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr drzwi')
   ctrl.grid:add_column('Drzwi >>','drzwi')
   
   
   
end

function win:on_load()	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select drzwi_id,nazwa from uprawnienia 
join drzwi using(drzwi_id)
where osoba_id=#1
	]], inarg.osoba_id))
end





function ctrl.dodaj:event(key)
	local a,b=kb.get_list_query('Drzwi',sf([[
select nazwa, drzwi_id from drzwi
left join 
(
select  drzwi_id from uprawnienia where osoba_id=#1
)as aa using(drzwi_id)
where aa.drzwi_id isnull
order by nazwa
]], inarg.osoba_id))
	kb.sys_stop_if_empty(a)
	kb.sql(sf([[
insert into uprawnienia(osoba_id,drzwi_id)
values(#1,#2)
	]], inarg.osoba_id, b))
	win:on_show()
end

function ctrl.grid:drzwi(key)      
	local tmp=kb.get_yes_no('Usunąć')
	if not tmp then
		return
	end
	
	kb.sql(sf([[
delete from uprawnienia
where osoba_id=#1 and drzwi_id=#2
	]],inarg.osoba_id, key.drzwi_id))
	
	
	win:on_show()
	
end