function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   --ctrl.indeks:as_search('stack','-nazwa')
   --ctrl.indeks:as_search('stack','-indeks')   
   
   
   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('Id')
   ctrl.grid:add_column('Czas')      
   ctrl.grid:add_column('Kod')         
   ctrl.grid:add_column('Drzwi Kod')         
   ctrl.grid:add_column('Karta Kod')   
end

function win:on_load()	
	self:on_show()
end

function win:on_show()
	ctrl.grid:sql(sf([[
select rcp_id,czas,zdarzenie,drzwi_kod,karta_kod from rcp
order by czas desc 
limit 1000

	]]) )
end

function ctrl.dodaj:event()	
	win:send('Tech-E1','ok(22)')	
end

function ctrl.dodaj1:on_event()
	local tmp=kb.get_text_empty_stop()	
	kb.sql(sf([[
insert into indeksy(indeks) values(#1)	
	]],tmp))
	win:on_show()
end





function ctrl.indeks:on_event()

	win:on_show()

end





function ctrl.grid:indeks(key)

	win:exit(key.indeks)

end