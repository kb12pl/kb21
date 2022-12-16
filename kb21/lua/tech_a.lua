function win:on_create()
	self:set_layout({grid=true})	
	ctrl.grid:add_column('nr_tech')
	ctr.grid:add_column('indeks')
	ctrl.grid:add_column('opis')
	ctrl.nazwa:as_search('nav','-')
	ctrl.indeks:as_search('nav','-')
	ctrl.nr_tech:as_search('nav','nazwa')
	ctrl.dodaj:as_button('nav')
end

function win:on_load()
	self:on_show()	
end

function win:on_show()
	ctrl.grid:sql(sf([[
select  tech_id, indeks, opis,1,2,3 from tech
	]]))
	ctrl.grid:focus()	
end

function ctr.dodaj:event()
	local indeks=kb.indeksy_wybierz()
	local opis=kb.get_text_empty_stop('opis')
	kb.sql(sf([[
insert into tech(indeks,opis) values(#1,#2)
	]],indeks,opis))		
	ctrl.indeks:set(indeks)
	win:on_show()
end

function ctrl.grid:nr_tech(key)	
	kb.sys_dialog('Tech-E1',{tech_id=key.tech_id})
end
