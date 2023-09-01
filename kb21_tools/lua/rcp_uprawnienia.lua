function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')   
   ctrl.zamknij:as_button('stack','Zamknij').event=function()win:on_close()end

   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Drzwi')      
   ctrl.grid:add_column('Osoba')
end

function win:on_load()	
	self:on_show()
end

function win:on_show()

	ctrl.grid:sql([[
select nazwa, osoba
from uprawnienia 
left join osoby using(osoba_id)
left join drzwi using(drzwi_id)
order by nazwa, osoba
	]])
end


