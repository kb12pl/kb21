function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.zamknij:as_button('stack','Zamknij')
  

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr Karty','nr')
   ctrl.grid:add_column('Imie','imie')   
   ctrl.grid:add_column('Nazwisko','nazwisko')
   ctrl.grid:add_column('Kod Karty','karta_kod')  
end

function win:on_load()	
	self:on_show()
end

function win:on_show()

	ctrl.grid:sql([[
select karta_id, imie, nazwisko, karta_kod 
from karty 
order by nazwisko,imie
	]])
end


function ctrl.dodaj:event()
	local tmp=kb.get_text_empty_stop('Nazwisko')	
	kb.sql(sf([[
insert into karty(nazwisko) values(#1)	
	]],tmp))
	win:on_show()
end


function ctrl.grid:imie(key)
	local tmp=kb.get_text_empty_stop('Imie',key.imie)	
	kb.sql(sf([[
update karty set imie=#1 
where karta_id=#2
	]],tmp, key.karta_id))
	win:on_show()
end


function ctrl.grid:nazwisko(key)
	local tmp=kb.get_text_empty_stop('Nazwisko',key.nazwisko)	
	kb.sql(sf([[
update karty set nazwisko= #1 
where karta_id=#2
	]],tmp, key.karta_id))
	win:on_show()
end


function ctrl.grid:karta_kod(key)
	local a,b=kb.get_list_query('Kod Karty',[[
select distinct on(karta_kod) concat_ws(' -> ', czas, nazwa, karta_kod, nazwisko),
karta_kod,czas
from rcp join drzwi using(drzwi_kod) 
left join karty using(karta_kod)
order by 2 desc
limit 20
	]])	
	kb.sys_stop_if_empty(a)
	kb.sql(sf([[
update karty set karta_kod= #1 
where karta_id=#2
	]],b, key.karta_id))
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end