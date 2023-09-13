function win:on_create()
 	ctrl.dock:as_dock()
 	ctrl.stack:as_stack_v('dock')
	ctrl.clear:as_button('stack').event=function()ctrl.code:set() end
	ctrl.create_database:as_button('stack')
	ctrl.export_scripts:as_button('stack')
	ctrl.test:as_button('stack')
	ctrl.code:as_code('dock')
end



function ctrl.code:log(log)
	self:set(self:get()..'\n'..tostring(log) )
end

function log(log)
       log=log or '<nil>'
	ctrl.code:log(log)
end
function clear()
	ctrl.code:set()
end

function ctrl.export_scripts.event()
      clear()
	kb.sql("delete from kb_scripts")
	local s=kb.sys_scripts().list
	for k,v in pairs(s) do
		ctrl.code:log(k)
		local a,b,c=kb.sql(sf([[
insert into kb_scripts(name,code,symbol,title) values(#1,#2,#^3,#^4)
		]],k, win:readScript(k),v.window,v.title ),true)
		if c then
			if string.match(c,'^aa23505') then
				log('\t duplicate')
			else
				log(c)
			end
		end
	end	
end



function ctrl.create_database:event()
	clear()
	local tab={}
	tab[1]=[[
drop table if exist kb_scripts;	
create table kb_scripts(
	id serial primary	key,
	name text unique,
	symbol text unique, 
	title text unique,
	code text
)
	]]
	
	
	tab[2]=[[
drop table if exists tech;
create table tech
(
	tech_id serial primary key,
	index integer,
	opis text
)
	]]
	
	for k,v in pairs(tab)do
		log(v)
		log('\n')
		local a,b,c=kb.sql(v,true)
		if c then
			log(c)
		end
		log('\n')
	end	
end


function ctrl.test.event()	
	a={karol={ala=123}}
	b=string.pack("",a)	
	c=string.unpack("",b)
	log(c.karol.ala)	
end