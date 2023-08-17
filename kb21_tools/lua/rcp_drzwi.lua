function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.zamknij:as_button('stack','Zamknij')

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr Drzwi','nr')
   ctrl.grid:add_column('Nazwa','nazwa')      
   ctrl.grid:add_column('Kod','drzwi_kod')  
   ctrl.grid:add_column('Filtr','filtr')  
end

function win:on_load()	
	self:on_show()
end

function win:on_show()

	ctrl.grid:sql([[
select drzwi_id, nazwa, drzwi_kod, filtr
from drzwi
order by nazwa
	]])
end


function ctrl.dodaj:event()
	local tmp=kb.get_text_empty_stop('Nazwisko')	
	kb.sql(sf([[
insert into drzwi(nazwa) values(#1)	
	]],tmp))
	win:on_show()
end


function ctrl.grid:nazwa(key)
	local tmp=kb.get_text_empty_stop('Nazwa',key.nazwa)	
	kb.sql(sf([[
update drzwi set nazwa=#1 
where drzwi_id=#2
	]],tmp, key.drzwi_id))
	win:on_show()
end


function ctrl.grid:filtr(key)
	local tmp=kb.get_text_empty_stop('Filtr',key.filtr)	
	kb.sql(sf([[
update drzwi set filtr=#1 
where drzwi_id=#2
	]],tmp, key.drzwi_id))
	win:on_show()
end


function ctrl.grid:drzwi_kod(key)
	local tmp=kb.get_text_empty_stop('Nazwa',key.drzwi_kod)	
	kb.sql(sf([[
update drzwi set drzwi_kod=#1 
where drzwi_id=#2
	]],tmp, key.drzwi_id))
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end  

