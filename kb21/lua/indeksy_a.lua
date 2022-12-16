function win:on_create()
   ctrl.dock:as_dock()   
   ctrl.stack:as_stack_v('dock')
   ctrl.indeks:as_search('stack','-nazwa')
   ctrl.indeks:as_search('stack','-indeks')
   ctrl.dodaj:as_button('stack')
  
   ctrl.grid:as_grid('dock')
   ctrl.grid:add_column('indeks')
   ctrl.grid:add_column('3','b')   
   ctrl.grid:add_column('3','b')   
   
    win:short('ctrl-enter',function() ctrl.grid:focus() end)     
end

function win:on_load()
	--self:on_show()
end


function win:on_show()
	ctrl.grid:sql(sf([[
select indeks from indeksy
where indeks ilike '%@1%'
	]],ctrl.indeks:get()) )
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