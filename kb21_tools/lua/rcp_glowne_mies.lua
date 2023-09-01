function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.osoba:as_search('stack','Osoba')
   ctrl.data_od:as_date('stack','Data Od').event=function()win:on_show()end
   ctrl.data_do:as_date('stack','Data Do').event=function()win:on_show()end
   ctrl.pokaz:as_button('stack','Pokaż').event=function()win:on_show()end
   ctrl.zamknij:as_button('stack','Zamknij').event=function()win:on_close()end  


   
   
   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Data')
   ctrl.grid:add_column('Osoba')      
   ctrl.grid:add_column('Wejście')         
   ctrl.grid:add_column('Wyjście')         
end

function win:on_load()	
 	local a,b=kb.sql_values("select date_trunc('month',current_date)::date,date_trunc('month',current_date+'1month'::interval)::date-1")
 	ctrl.data_od:set(a)
 	ctrl.data_do:set(b) 	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select osoba,czas::date as dd, min(czas)::time,max(czas)::time
from rcp join  drzwi using(drzwi_kod)
join osoby using(karta_kod)
where czas::date>=#2 and czas::date<=#3
and filtr in ('we-glowne', 'wy-glowne')
and osoba ilike '%@1%'
group by osoba,czas::date
order by osoba
	]],ctrl.osoba:get(), ctrl.data_od:get(), ctrl.data_do:get() ))
end
