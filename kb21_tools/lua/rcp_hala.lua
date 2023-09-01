function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.date:as_date('stack','Data').event=function()win:on_show()end
   ctrl.pokaz:as_button('stack','Pokaż').event=function()win:on_show()end
   ctrl.zamknij:as_button('stack','Zamknij').event=function()win:on_close()end  

   
   
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
case when filtr='we-hala' then '-->'||czas::time end,
case when filtr='wy-hala' then '<--'||czas::time end
from rcp join drzwi using(drzwi_kod)
left join osoby using(karta_kod) 
where czas::date=#1 and filtr in ('we-hala', 'wy-hala')
order by nazwisko,imie,czas 
	]],ctrl.date:get()) )
end


function ctrl.date:event()
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end  

