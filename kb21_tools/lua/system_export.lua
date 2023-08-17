	local s=kb.sys_scripts().list
	for k,v in pairs(s) do
		kb.sql(sf([[
insert into scripts(name,code) values(#1,#2)
		]],k, win:readScript(k)    ))
	end	