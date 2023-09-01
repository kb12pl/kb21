function win:on_create()

   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.dodaj:as_button('stack','Dodaj')
   ctrl.zamknij:as_button('stack','Zamknij')
  

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Nr Osoby','nr')
   ctrl.grid:add_column('Imie','imie')   
   ctrl.grid:add_column('Nazwisko','nazwisko')
   ctrl.grid:add_column('Kod Karty','karta_kod')  
end

function win:on_load()	
	self:on_show()
end

function win:on_show()

	ctrl.grid:sql(sf([[
SELECT  
	c.relname as tabela, f.attname AS kolumna,
    pg_catalog.format_type(f.atttypid,f.atttypmod) AS typ,  
    pgd.description ,     nullif(f.attnotnull,false) ,p.contype
FROM pg_attribute f  
    JOIN pg_class c ON c.oid = f.attrelid  
    JOIN pg_type t ON t.oid = f.atttypid  
    LEFT JOIN pg_attrdef d ON d.adrelid = c.oid AND d.adnum = f.attnum  
    LEFT JOIN pg_namespace n ON n.oid = c.relnamespace  
    LEFT JOIN pg_constraint p ON p.conrelid = c.oid AND f.attnum = ANY (p.conkey)  
    LEFT JOIN pg_class AS g ON p.confrelid = g.oid  
    left join pg_description pgd on pgd.objoid=f.attrelid and pgd.objsubid=f.attnum
WHERE c.relkind = 'r'::char  
    AND n.nspname = 'public'  
    AND c.relname ilike  '%@2%'
    AND f.attname ilike  '%@3%'
    AND f.attnum > 0 
    ORDER BY c.relname,f.attname
]]))
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