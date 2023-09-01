function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.pokaz:as_button('stack','Pokaż').event=function()win:on_show()end
   ctrl.zamknij:as_button('stack','Zamknij')
  

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr Osoby','nr')
   ctrl.grid:add_column('Nazwisko','nazwisko')
   ctrl.grid:add_column('Imie','imie')   
   ctrl.grid:add_column('Kod Karty','karta_kod')  
   ctrl.grid:add_column('Uprawnienia','uprawnienia')  
end

function win:on_load()	
	self:on_show()
end

function win:on_show()

	ctrl.grid:sql([[
select osoba_id, nazwisko, imie, karta_kod, ss, osoba_id 
from osoby 
left join
(
select osoba_id,count(*) as ss from uprawnienia 
group by osoba_id
) as aa using(osoba_id)
order by nazwisko,imie
	]])
end


function ctrl.dodaj:event()
	local tmp=kb.get_text_empty_stop('Nazwisko')	
	kb.sql(sf([[
insert into osoby(nazwisko) values(#1);
update osoby set osoba=concat_ws(' ',nazwisko,imie) where nazwisko=#1;
	]],tmp))
	win:on_show()
end


function ctrl.grid:imie(key)
	local tmp=kb.get_text_empty_stop('Imie',key.imie)	
	kb.sql(sf([[
update osoby set imie=#1 
where osoba_id=#2;
update osoby set osoba=concat_ws(' ',nazwisko,imie) where osoba_id=#2;
	]],tmp, key.osoba_id))
	win:on_show()
end


function ctrl.grid:nazwisko(key)
	local tmp=kb.get_text_empty_stop('Nazwisko',key.nazwisko)	
	kb.sql(sf([[
update osoby set nazwisko= #1
where osoba_id=#2;
update osoby set osoba=concat_ws(' ',nazwisko,imie) where osoba_id=#2;
	]],tmp, key.osoba_id))
	win:on_show()
end


function ctrl.grid:karta_kod(key)
	local a,b=kb.get_list_query('Kod Karty',[[
select * from
(
select distinct on(karta_kod) concat_ws(' -> ', czas, nazwa, karta_kod, nazwisko),
karta_kod,czas
from rcp join drzwi using(drzwi_kod) 
left join osoby using(karta_kod)
limit 20
)as aa
order by czas desc 
]])	
	kb.sys_stop_if_empty(a)
	kb.sql(sf([[
update osoby set karta_kod= #1 
where osoba_id=#2
	]],b, key.osoba_id))
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end

function ctrl.grid:uprawnienia(key)
	kb.sys_dialog('rcp_osoby_upr',{osoba_id=key.osoba_id})
end