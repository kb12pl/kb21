local this={list={}}
function this:add(name, work, func, window, page,title)
	self.list[name]={work=work, is_func=func, window=window, page=page,title=title}
end
function this:addT(name,tab)
       tab=tab or {}       
	tab.window=tab.smb or tab.window	
	self.list[name]=tab
end


function this:work_list()

	local tmp={}

	for k,v in pairs(self.list) do

		if v.work then

			table.insert(tmp,k)		

		end

	end

	table.sort(tmp)

	return tmp

end





function this:show_code(name)

	local tmp=self.list[name or '']

	if tmp then

		kb.sys_dialog('sys_code',{name=name})	

	end

end





function this:run_list()

	local tmp={}

	for k,v in pairs(self.list) do

		if v.page then

			tmp[k]=v.window..' - '..(v.title or'')

		end

	end



	local a,b=kb.get_list('',tmp,true)	
	if a then	
		self:run_uni({page=true},b)
	end
end

function this:find_script(window)
	for k,v in pairs(self.list) do
		if v.window==window then
			return k
		end
	end
end



function this:run(window,arg)
	return self:run_uni( {dialog=true} ,nil,window,arg)

end



function this:run_modeless(window,arg)      

	return self:run_uni({modeless=true},nil,window,arg)

end



function this:run_page(window,arg)

	self:run_uni( { page=true }, nil, window, arg)

end





function this:run_uni(par,script,window,arg)
	script=script or self:find_script(window)
    local a=self.list[script]       

    if not a then
		xlog('run_uni','no script',script,'window',window,' ?? ')
		return
    end        

    local script_to_run=script

    if a.link then
		script_to_run=self:find_script(a.link)
	    if not script_to_run then
			ok('run_uni brak skryptu dla linku: ',a.link)
	    return
	   end      	   
    end
	par.smb=a.window
	par.script=script_to_run
	par.title=a.title	
	par.arg=arg		
	return win:panel(par)
end

local list=this.list
this:add('indeksy_a',true,false,'Ind-A', true,'Indeksy')
this:addT('indeksy_b',{smb='Ind-B', link='Ind-A', title='Wybierz Indeks'})
this:addT('indeksy_wybierz')
this:add('indeksy_w_dodaj',true,false,'indeksy-dodaj')
list.tech_a={window='Tech-A', title='Technologie',page=true}
list.tech_e={window='Tech-E1', title='Edycja Technologii',work=true}
this:addT('web_lua',{smb='Web-Lua',page=true,work=true})
this:addT('web_google',{smb='Web-Google',page=true,work=true})
this:add('get_list')
this:add('get_text_empty_stop',true,true)
this:add('get_text',true,true)
list.sql={work=true}
list.sql_tab={work=true}
list.sql_tabn={work=true}
this:add('sql_create_database',true,true)
this:add('sys_boot',true,true)
this:add('sys_code',true,true,'sys_code',true)
list.sys_console={}
this:add('sys_ctrl',true,true)
this:add('sys_dialog',true,true)
this:add('sys_get',true,true,'sys_get')
this:add('sys_global',true,true)
list['sys_help']={work=true}
list['sys_list']={window='sys_list'}
list['sys_log']={work=true}
this:add('sys_page',true,true)
this:add('sys_query',true,true,'sys_query',true,'Query tool')
list.sys_scripts={work=true}
list.sys_scripts_list={work=true}
this:add('sys_testy',true,true,'ala',true)
this:add('sys_window',true,true)

this:add('rcp_zdarzenia',true,true,'rcp_zdarzenia',true,'Zdarzenia')
this:add('rcp_karty',true,true,'rcp_karty',true,'Karty')
this:add('rcp_drzwi',true,true,'rcp_drzwi',true,'Drzwi')
this:add('rcp_glowne',true,true,'rcp_glowne',true,'Wejście główne')
this:add('rcp_produkcja',true,true,'rcp_produkcja',true,'Wejście na produkcję')



return this