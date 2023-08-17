function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.date:as_date('stack','Data')
   ctrl.zamknij:as_button('stack','Zamknij')

   
   
   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Data')
   ctrl.grid:add_column('Osoba')      
   ctrl.grid:add_column('Wejście')         
   ctrl.grid:add_column('Wyjście')         
end

function win:on_load()	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select czas::date as dd,concat_ws(' ',nazwisko,imie),
case when filtr='we-produkcja' then '-->'||czas::time end,
case when filtr='wy-produkcja' then '<--'||czas::time end
from rcp join karty using(karta_kod)
join drzwi using(drzwi_kod)
where czas::date=#1 and filtr in ('we-produkcja', 'wy-produkcja')
order by nazwisko,imie,czas 
	]],ctrl.date:get()) )
end


function ctrl.date:event()
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end  

