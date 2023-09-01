 function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.zamknij:as_button('stack','Zamknij')

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr Osoby')
   ctrl.grid:add_column('Osoba >>','osoba')
   
   
   
end

function win:on_load()	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select osoba_id,osoba from uprawnienia 
join osoby using(osoba_id)
where drzwi_id=#1
	]], inarg.drzwi_id))
end




function ctrl.grid:drzwi_kod(key)
	local tmp=kb.get_text_empty_stop('Kod',key.drzwi_kod)	
	kb.sql(sf([[
update drzwi set drzwi_kod=#1 
where drzwi_id=#2
	]],tmp, key.drzwi_id))
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end  

function ctrl.grid:uprawnienia()
	kb.sys_dialog('rcp_drzwi_upr')
end


function ctrl.dodaj:event(key)
	local a,b=kb.get_list_query('Osoba',sf([[
select osoba, osoba_id from osoby
left join 
(
select  osoba_id from uprawnienia where drzwi_id=#1
)as aa using(osoba_id)
where aa.osoba_id isnull
order by osoba
]], inarg.drzwi_id))
	kb.sys_stop_if_empty(a)
	kb.sql(sf([[
insert into uprawnienia(osoba_id,drzwi_id)
values(#1,#2)
	]],b, inarg.drzwi_id))
	win:on_show()
end

function ctrl.grid:osoba(key)      
	local tmp=kb.get_yes_no('Usunąć')
	if not tmp then
		return
	end
	
	kb.sql(sf([[
delete from uprawnienia
where osoba_id=#1 and drzwi_id=#2
	]],key.osoba_id, inarg.drzwi_id))
	
	
	win:on_show()
	
end