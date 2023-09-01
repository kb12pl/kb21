local this={list={}}
function this:add(name, work, func, window, page,title)
	self.list[name]={work=work, is_func=func, window=window, page=page,title=title}
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
			tmp[k]=v.title or v.window
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
--this:add('indeksy_a',true,false,'Ind-A', true,'Indeksy')
--this:addT('indeksy_b',{smb='Ind-B', link='Ind-A', title='Wybierz Indeks'})
--this:addT('indeksy_wybierz')
--this:add('indeksy_w_dodaj',true,false,'indeksy-dodaj')
--list.tech_a={window='Tech-A', title='Technologie',page=true}
--list.tech_e={window='Tech-E1', title='Edycja Technologii',work=true}

this:add('get_list')
this:add('get_text_empty_stop',true,true)
this:add('get_text',true,true)

this:add('kb21_frame',true,true)this:add('sql',true,true)
this:add('sql_tab',true,true)
this:add('sql_tabn',true,true)

this:add('system_export',true,true)

this:add('sys_code',true,true,'sys_code',false)
this:add('sys_console',true,true,'sys_console',false)
this:add('sys_ctrl',true,true)
this:add('sys_dialog',true,true)
this:add('sys_get',true,true,'sys_get')
this:add('sys_global',true,true)
this:add('sys_help',true,true)
this:add('sys_list',true,true,'sys_list')
this:add('sys_log',true,true)
this:add('sys_page',true,true)
this:add('sys_query',true,true,'sys_query',true,'Sql query')
this:add('sys_scripts',true,true)
this:add('sys_tables',true,true,'sys_tables',true,'Sql tables')
this:add('sys_window',true,true)


this:add('web_lua',true,true,'web_lua',false,'web_lua')
this:add('web_google',true,true,'web_google',false,'web_google')

this:add('rcp_zdarzenia',true,true,'rcp_zdarzenia',true,'Zdarzenia')
this:add('rcp_osoby',true,true,'rcp_osoby',true,'Osoby')
this:add('rcp_osoby_upr',true,true,'rcp_osoby_upr',false,'Osoby uprawnienia')
this:add('rcp_drzwi',true,true,'rcp_drzwi',true,'Drzwi')
this:add('rcp_drzwi_upr',true,true,'rcp_drzwi_upr',false,'Drzwi uprawnienia')
this:add('rcp_uprawnienia',true,true,'rcp_uprawnienia',true,'Uprawnienia')
this:add('rcp_glowne',true,true,'rcp_glowne',true,'Wejście główne')
this:add('rcp_hala',true,true,'rcp_hala',true,'Wejście hala')
this:add('rcp_glowne_mies',true,true,'rcp_glowne_mies',true,'We-wy główne wg okresu')
this:add('rcp_hala_mies',true,true,'rcp_hala_mies',true,'We-wy hala wg okresu')



return this