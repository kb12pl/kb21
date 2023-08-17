function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.date:as_date('stack','Data')
   ctrl.pokaz:as_button('stack','Pokaż').event=function()win:on_show()end
   ctrl.zamknij:as_button('stack','Zamknij')

   
   
   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Id')
   ctrl.grid:add_column('Czas')      
   ctrl.grid:add_column('Kod')         
   ctrl.grid:add_column('Drzwi Kod')         
   ctrl.grid:add_column('Drzwi Nazwa')         
   ctrl.grid:add_column('Karta Kod')   
   ctrl.grid:add_column('Karta Osoba')   
end

function win:on_load()	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select rcp_id,czas,zdarzenie,drzwi_kod, nazwa, karta_kod, concat_ws(' ',nazwisko,imie)
from rcp left join drzwi using(drzwi_kod)
left join karty using(karta_kod)
where czas::date=#1
order by czas 
	]],ctrl.date:get()) )
end


function ctrl.date:event()
	win:on_show()
end

function ctrl.zamknij:event()
	win:on_close()
end  

